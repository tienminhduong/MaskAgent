using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using SOEventSystem;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D.Animation;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(FSM))]
public class PlayerController : MonoBehaviour, IInteractable
{
    Collider2D _collider;
    Rigidbody2D _rigidbody;
    Animator _animator;
    [SerializeField] FSM _fsm;


    // ================= Props ================-
    [Header("Props")]
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float scaleSpeed = 2.5f;

    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] HumanInfo playerInfo;

    [Range(0, GameLimit.MAX_SUSPECT_LEVEL)][SerializeField] int suspectLevel = 0;

    [Header("Events")]
    [SerializeField] VoidPublisher gameOverEvent;

    PlayerInteractLogic playerInteractLogic;

    [Header("Scan zone Controller")]
    [SerializeField] private Transform scanZone;
    [SerializeField] private Transform scanEffect;
    [SerializeField] private float scanZoneExpandDuration = 0.5f;
    [SerializeField] private float scanCheckDuration = 1f;
    [SerializeField] private IdentityCopyController identityCopyController;
    [SerializeField] private SpriteLibrary spriteLibrary;

    bool isInteract = false;
    bool isRunning = false;
    bool isChecking = false;
    bool isCopyPressed = false;

    private HumanInfo copiedInfo;
    public HumanInfo CopiedInfo => copiedInfo;

    // ================= value INPUT =================

    Vector2 input = new Vector2(0, 0);
    bool jumpPressed = false;

    #region Getter-Setter
    public Animator Animator
    {
        get { return _animator; }
        set { _animator = value; }
    }
    public FSM Fsm
    {
        get { return _fsm; }
        set { _fsm = value; }
    }
    public HumanInfo PlayerInfo
    {
        get { return playerInfo; }
        set { playerInfo = value; }
    }

    public PlayerInteractLogic PlayerInteractLogic
    {
        get { return playerInteractLogic; }
        set { playerInteractLogic = value; }
    }
    public bool IsCopyPressed
    {
        get { return isCopyPressed; }
        set { isCopyPressed = value; }
    }
    #endregion

    void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _fsm = GetComponent<FSM>();
        playerInteractLogic = GetComponentInChildren<PlayerInteractLogic>();

        if (scanZone != null)
            scanZone.localScale = Vector3.zero;
    }

    void Start()
    {
        _fsm.ChangeState(new IdleState());
    }

    private void Update()
    {
        //Debug.Log(_fsm.currentState.ToString());
        //Debug.Log(IsGrounded());

        HandleCopy();
    }

    // ================= MOVEMENT =================
    public bool HandleMoving()
    {
        Vector2 moveInput = input.normalized;
        Rotation();

        if (moveInput.magnitude >= 0.1f)
        {
            _rigidbody.linearVelocity = moveInput * moveSpeed * ((isRunning) ? scaleSpeed : 1);

            float footStepInterval = (isRunning) ? 0.1f : 0.25f;
            AudioManager.Instance.PlayFootStep(footStepInterval);

            _fsm.ChangeState(new RunState());
            return true;
        }

        _rigidbody.linearVelocity = Vector2.zero;

        AudioManager.Instance.StopFootStep();

        return false;
    }

    void Rotation()
    {
        if (input.magnitude < 0.1f) return;

        float targetAngle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
        float newAngle = Mathf.LerpAngle(
            _rigidbody.rotation,
            targetAngle,
            rotationSpeed * Time.fixedDeltaTime
        );

        _rigidbody.rotation = newAngle;
    }

    // ========= INPUT EVENTS =========
    public void OnMove(InputValue movementvalue)
    {
        input = movementvalue.Get<Vector2>();
    }
    public void OnRun(InputValue shift)
    {
        this.isRunning = shift.isPressed;
    }
    public void OnInteract(InputValue isInteract)
    {
        this.isInteract = isInteract.isPressed;
        Debug.Log("PlayerController OnInteract: " + this.isInteract);
        if (this.isInteract)
            playerInteractLogic.Interact();
    }

    public void HandleLure()
    {
        if (playerInteractLogic.Lure(playerInfo.Role))
            Debug.Log("Lure successful or no interactable.");
        else
        {
            Debug.Log("Lure failed.");
            RaiseSuspectLevel();
            if (suspectLevel >= GameLimit.MAX_SUSPECT_LEVEL)
            {
                Debug.Log("Game Over! Suspect level reached maximum.");
                gameOverEvent.RaiseEvent();
            }
        }
    }

    private void HandleCopy()
    {
        IsCopyPressed = Input.GetKey(KeyCode.C);

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isChecking || identityCopyController.IsCopying) return;
            _fsm.ChangeState(new ScanState());
        }
    }

    public void OnCopy(InputValue isCopy)
    {
        IsCopyPressed = isCopy.isPressed;

        Debug.Log("PlayerController OnCopy: " + IsCopyPressed);

        if (isCopy.isPressed)
        {
           if (isChecking || identityCopyController.IsCopying) return;

           _fsm.ChangeState(new ScanState());
        } 
    }

    public void OnScanState()
    {
        StartCoroutine(StartCheckRoutine());
    }
    public void OffScanState()
    {
        StopCoroutine(StartCheckRoutine());
    }

    public void RaiseSuspectLevel()
    {
        suspectLevel++;
    }

    private IEnumerator StartCheckRoutine()
    {
        isChecking = true;
        scanZone.localScale = Vector3.one;
        scanEffect.gameObject.SetActive(false);

        var overlappedInteractable = playerInteractLogic.OverlappedInteractable;
        Debug.Log($"Overlapped Interactable: {overlappedInteractable}");

        if (overlappedInteractable == null)
        {
            // Show Failed Copy Effect
            _fsm.ChangeState(new IdleState());
            scanZone.localScale = Vector3.zero;
        }
        else
        {
            // Start Copying Process
            identityCopyController.StartCopy(this);
        }

        isChecking = false;
        yield return null;
    }

    public void Interacted(IInteractable interacted)
    {
    }

    public void Overlapped(IInteractable overlapped)
    {
    }

    public void OverlapExited(IInteractable overlapExited)
    {
    }

    public void TeleportToCheckpoint()
    {
        transform.position = playerInteractLogic.CheckpointPosition;
    }
    public void ForceStopChecking()
    {
        isChecking = false;
        scanZone.localScale = Vector3.zero;
        _fsm.ChangeState(new IdleState());
    }

    public void ChangeIdentity()
    {
        IInteractable overlapped = playerInteractLogic.OverlappedInteractable;
        BaseCharacter targetCharacter = overlapped as BaseCharacter;
        if (targetCharacter != null)
        {
            Debug.Log($"Changing identity to {targetCharacter.HumanInfo.Name}");
            this.copiedInfo = targetCharacter.HumanInfo;

            spriteLibrary.spriteLibraryAsset = copiedInfo.SpriteLibrary;
        }
    }
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(FSM))]
public class PlayerController : MonoBehaviour
{
    Collider2D _collider;
    Rigidbody2D _rigidbody;
    Animator _animator;
    [SerializeField] FSM _fsm;


    // ================= Props ================-
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float scaleSpeed = 2.5f;

    [SerializeField] float rotationSpeed = 10f;

    [Header("Scan zone Controller")]
    [SerializeField] private Transform scanZone;
    [SerializeField] private Transform scanEffect;
    [SerializeField] private float scanZoneExpandDuration = 0.5f;
    [SerializeField] private float scanCheckDuration = 1f;
    [SerializeField] private IdentityCopyController identityCopyController;

    bool isInteract = false;
    bool isRunning = false;
    bool isChecking = false;

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
    #endregion

    void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _fsm = GetComponent<FSM>();

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
    }

    // ================= MOVEMENT =================
    public bool HandleMoving()
    {
        Vector2 moveInput = input.normalized;
        Rotation();

        if (moveInput.magnitude >= 0.1f)
        {
            _rigidbody.linearVelocity = moveInput * moveSpeed * ((isRunning) ? scaleSpeed : 1);
            _fsm.ChangeState(new RunState());
            return true;
        }
        _rigidbody.linearVelocity = Vector2.zero;
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
    }

    public void OnCopy(InputValue isCopy)
    {
        if (isCopy.isPressed)
        {
            if (!isChecking)
            {
                StartCoroutine(StartCheckRoutine());
            }
        }
    }

    // =========== Collision ================

    //Vector2 boxSize = new Vector2(0.6f, 0.1f);
    //Vector3 boxCenterOffset = Vector3.down * 0.515f;

    //public bool IsGrounded()
    //{
    //    Vector2 boxCenter = (Vector2)transform.position + (Vector2)boxCenterOffset;

    //    Collider2D hit = Physics2D.OverlapBox(
    //        boxCenter,
    //        boxSize,
    //        0f,
    //        LayerMask.GetMask(LayerMaskName.Ground)
    //    );
    //    return hit != null;
    //}
    //void OnDrawGizmos()
    //{
    //    Vector2 boxCenter = (Vector2)transform.position + (Vector2)boxCenterOffset;

    //    // Change color based on grounded state (Editor only)
    //    Gizmos.color = IsGrounded() ? Color.green : Color.red;

    //    Gizmos.DrawWireCube(boxCenter, boxSize);

    //}



    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnCollisionExit2D(Collision2D collision)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }

    private IEnumerator StartCheckRoutine()
    {
        isChecking = true;

        scanZone.DOScale(1, scanZoneExpandDuration).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(scanZoneExpandDuration);

        float rotateSpeed = 360f / scanCheckDuration;
        float elapsed = 0f;

        while (elapsed < scanCheckDuration)
        {
            float deltaTime = Time.deltaTime;
            elapsed += deltaTime;

            scanEffect.Rotate(0, 0, -rotateSpeed * deltaTime);

            yield return null;
        }

        scanZone.DOScale(0, scanZoneExpandDuration).SetEase(Ease.InBack);
        yield return new WaitForSeconds(scanZoneExpandDuration);

        // TO DO: Check for copyable objects in the scan zone
        if (false)
        {
            // Show Failed Copy Effect
        }
        else
        {
            // Start Copying Process
            identityCopyController.StartCopy(this);
        }

        isChecking = false;
    }

}

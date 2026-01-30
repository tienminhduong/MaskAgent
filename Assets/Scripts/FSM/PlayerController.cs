using System.Collections.Generic;
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
    [SerializeField] float jumpForce = 5.0f;
    [SerializeField] float climbSpeed = 10.0f;
    bool isFacingRight = true;
    bool isInteract = false;

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
        Flip();

        if (Mathf.Abs(input.x) >= 0.1f)
        {
            _rigidbody.linearVelocity = new Vector2(input.x * moveSpeed, _rigidbody.linearVelocity.y);
            if (IsGrounded() == true)
            {
                _fsm.ChangeState(new RunState());
            }
            return true;
        }
        else
        {
            _rigidbody.linearVelocity = new Vector2(0, _rigidbody.linearVelocity.y);
        }
        return false;
    }

    public bool HandleJump()
    {
        if (jumpPressed && IsGrounded())
        {
            jumpPressed = false;
            _fsm.ChangeState(new JumpState());
            return true;
        }
        return false;
    }
    public void Jump()
    {
        Vector2 force = new Vector2(0.0f, jumpForce);
        _rigidbody.AddForce(force, ForceMode2D.Impulse);
    }
    private void Flip()
    {
        if (isFacingRight && input.x < 0 || !isFacingRight && input.x > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 Oscale = transform.localScale;
            Oscale.x = Oscale.x * -1;
            transform.localScale = Oscale;
        }
    }

    // ========= INPUT EVENTS =========
    public void OnMove(InputValue movementvalue)
    {
        input = movementvalue.Get<Vector2>();
    }


    public void OnJump(InputValue isJump)
    {
        if (IsGrounded())
        {
            jumpPressed = isJump.isPressed;
        }
    }

    public void OnInteract(InputValue isInteract)
    {
        this.isInteract = isInteract.isPressed;
    }

    // =========== Collision ================

    Vector2 boxSize = new Vector2(0.6f, 0.1f);
    Vector3 boxCenterOffset = Vector3.down * 0.515f;

    public bool IsGrounded()
    {
        Vector2 boxCenter = (Vector2)transform.position + (Vector2)boxCenterOffset;

        Collider2D hit = Physics2D.OverlapBox(
            boxCenter,
            boxSize,
            0f,
            LayerMask.GetMask(LayerMaskName.Ground)
        );
        return hit != null;
    }
    void OnDrawGizmos()
    {
        Vector2 boxCenter = (Vector2)transform.position + (Vector2)boxCenterOffset;

        // Change color based on grounded state (Editor only)
        Gizmos.color = IsGrounded() ? Color.green : Color.red;

        Gizmos.DrawWireCube(boxCenter, boxSize);

    }



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

}

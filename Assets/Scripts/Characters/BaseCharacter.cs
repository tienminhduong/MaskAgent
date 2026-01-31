using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(FSM))]


public class BaseCharacter : MonoBehaviour, IInteractable, ILureable
{
    [SerializeField] protected FSM fsm;
    protected Collider2D col;
    protected Rigidbody2D rb;
    protected Animator animator;

    // ===== Movement =====
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected float rotationSpeed = 10f;
    [SerializeField] protected float reachDistance = 0.1f;
    protected int direction = 1; // 1 = forward, -1 = backward

    [SerializeField] protected List<Transform> patroPath;
    [SerializeField] protected List<Transform> luredPath;
    [SerializeField] protected List<Transform> path;
    protected int currentIndex;
    [SerializeField] protected bool isLoop = true;
    [SerializeField] protected bool isLured = false;
    [SerializeField] protected HumanInfo humanInfo;

    public HumanInfo HumanInfo => humanInfo;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        fsm = GetComponent<FSM>();
    }

    protected virtual void Start()
    {
        fsm.ChangeState(new CharIdle());
        path = patroPath;
    }

    public void SwitchToPatroPath()
    {
        if (currentIndex == 0)
            path = patroPath;
    }
    public void SwitchToLuredPath()
    {
        if (currentIndex == 0)
            path = luredPath;
    }
    public virtual bool HandleMoving()
    {
        if (path == null || path.Count == 0)
        {
            StopMove();
            return false;
        }
        if (isLured)
            SwitchToLuredPath();
        else
            SwitchToPatroPath();

        Vector2 dir = GetDirectionToTarget();
        if (dir == Vector2.zero) return false;

        RotateTo(dir);
        rb.linearVelocity = dir * moveSpeed;

        fsm.ChangeState(new RunState());
        return true;
    }


    protected virtual Vector2 GetDirectionToTarget()
    {
        Vector2 pos = rb.position;

        if (path[currentIndex] == null)
            return Vector2.zero;

        Vector2 target = path[currentIndex].position;
        Vector2 dir = target - pos;

        if (dir.magnitude <= reachDistance)
        {
            currentIndex += direction;

            if (isLoop)
            {
                if (currentIndex >= path.Count)
                    currentIndex = 0;
                else if (currentIndex < 0)
                    currentIndex = path.Count - 1;
            }
            else
            {
                if (currentIndex >= path.Count)
                {
                    direction = -1;
                    currentIndex = path.Count - 2;
                }
                else if (currentIndex < 0)
                {
                    direction = 1;
                    currentIndex = 1;
                }
            }

            if (path[currentIndex] == null)
                return Vector2.zero;

            target = path[currentIndex].position;
            dir = target - pos;
        }

        return dir.normalized;
    }


    protected virtual void RotateTo(Vector2 dir)
    {
        if (dir.magnitude < 0.01f) return;

        float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float angle = Mathf.LerpAngle(
            rb.rotation,
            targetAngle,
            rotationSpeed * Time.fixedDeltaTime
        );
        rb.rotation = angle;
    }

    protected virtual void StopMove()
    {
        rb.linearVelocity = Vector2.zero;
    }

    private void OnDrawGizmos()
    {
        if (path == null) return;

        Gizmos.color = Color.green;
        for (int i = 0; i < path.Count; i++)
        {
            if (path[i] == null) continue;

            Gizmos.DrawSphere(path[i].position, 0.1f);

            if (i + 1 < path.Count && path[i + 1] != null)
                Gizmos.DrawLine(path[i].position, path[i + 1].position);
        }
    }

    public void OnLured(Role lurerRole)
    {
        if (!((ILureable)this).IsLureable(lurerRole)) return;
        isLured = true;
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
}

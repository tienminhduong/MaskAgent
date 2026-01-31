using UnityEngine;

public class ScanState : FSMState
{
    PlayerController player;
    Rigidbody2D rb;
    public override void Enter()
    {
        player = obj.GetComponent<PlayerController>();
        rb = obj.GetComponent<Rigidbody2D>();
        player.Animator.SetBool("isMoving", false);
        player.OnScanState();
        rb.linearVelocity = Vector2.zero;
    }

    public override void UpdateState(float delta)
    {

    }
}

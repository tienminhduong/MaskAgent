using UnityEngine;

public class RunState : FSMState
{
    PlayerController player;
    Rigidbody2D rb;

    public override void Enter()
    {
        player = obj.GetComponent<PlayerController>();
        rb = obj.GetComponent<Rigidbody2D>();
        //player.Animator.Play("Run");
        player.Animator.SetBool("isMoving", true);
    }

    public override void UpdateState(float delta)
    {
        player.HandleMoving();
        if (player.HandleMoving() == false)
        {
            ChangeState(new IdleState());
        }
    }
}

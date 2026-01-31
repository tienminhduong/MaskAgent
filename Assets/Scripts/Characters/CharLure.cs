using UnityEngine;

public class CharLure : FSMState
{
    BaseCharacter character;
    Rigidbody2D rb;

    public override void Enter()
    {
        character = obj.GetComponent<BaseCharacter>();
        rb = obj.GetComponent<Rigidbody2D>();
        //player.Animator.Play("Run");
        timer = Random.Range(2f, 3f);
    }

    public override void UpdateState(float delta)
    {
        character.HandleMoving();
        if (UpdateTimer(timer))
            ChangeState(new CharIdle());
    }
}

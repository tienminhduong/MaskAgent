using UnityEngine;

public class CharIdle : FSMState
{
    BaseCharacter character;
    Rigidbody2D rb;

    public override void Enter()
    {
        character = obj.GetComponent<BaseCharacter>();
        rb = obj.GetComponent<Rigidbody2D>();
        //player.Animator.Play("Run");
        timer = Random.Range(3f, 5f);
        character.SetIdleBaseAngle(rb.rotation);
        character.StopMove();
    }

    public override void UpdateState(float delta)
    {
        character.HandleIdle(timer);
        if (UpdateTimer(delta))
        {
            ChangeState(new CharPatro());
        }    
    }
}

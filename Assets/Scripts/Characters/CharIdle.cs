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
    }

    public override void UpdateState(float delta)
    {
        if (UpdateTimer(timer))
            ChangeState(new CharPatro());
    }
}

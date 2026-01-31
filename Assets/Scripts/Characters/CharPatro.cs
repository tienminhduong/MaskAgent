using UnityEngine;

public class CharPatro : FSMState
{
    BaseCharacter character;
    Rigidbody2D rb;

    public override void Enter()
    {
        character = obj.GetComponent<BaseCharacter>();
        rb = obj.GetComponent<Rigidbody2D>();
        timer = Random.Range(2f, 3f);
    }

    public override void UpdateState(float delta)
    {
        character.HandleMoving();
        if (UpdateTimer(delta))
            ChangeState(new CharIdle());
    }
}

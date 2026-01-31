using UnityEngine;

public class CharFreeze : FSMState
{
    BaseCharacter character;
    Rigidbody2D rb;

    public override void Enter()
    {
        character = obj.GetComponent<BaseCharacter>();
        rb = obj.GetComponent<Rigidbody2D>();
        //player.Animator.Play("Freeze");
        character.StopMove();
    }

    public override void UpdateState(float delta)
    {
        
    }
}

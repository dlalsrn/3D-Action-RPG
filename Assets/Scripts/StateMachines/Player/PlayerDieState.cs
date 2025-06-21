using UnityEngine;

public class PlayerDieState : PlayerBaseState
{
    public PlayerDieState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    private readonly int Die = Animator.StringToHash("OnDie"); // Die 애니메이션의 해시

    public override void Enter()
    {
        playerStateMachine.WeaponDamage.gameObject.SetActive(false); // 무기 비활성화

        playerStateMachine.Animator.SetTrigger(Die);
    }

    public override void Tick(float deltaTime)
    {
    }

    public override void Exit()
    {
    }
}

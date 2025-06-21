using UnityEngine;

public class PlayerBaseState : State
{
    public PlayerBaseState(PlayerStateMachine playerStateMachine)
    {
        this.playerStateMachine = playerStateMachine;
    }

    protected PlayerStateMachine playerStateMachine;

    protected readonly int Change_FreeLook_State = Animator.StringToHash("ChangeFreeLookState"); // FreeLook 블렌드 트리로 전환하는 트리거
    protected readonly int Change_Targeting_State = Animator.StringToHash("ChangeTargetingState"); // Targeting 블렌드 트리로 전환하는 트리거

    // 상태 진입 시 호출되는 메서드
    public override void Enter() { }

    // 상태가 활성화되어 있는 동안 매 프레임 호출되는 메서드
    public override void Tick(float deltaTime) { }

    // 상태가 종료될 때 호출되는 메서드
    public override void Exit() { }

    protected void RetunrToPreviousState()
    {
        if (playerStateMachine.playerPrevState is PlayerTargetingState)
        {
            // Targeting 상태로 돌아가기
            playerStateMachine.Animator.CrossFade("TargetingBlendTree", playerStateMachine.AnimationDampTime);
            playerStateMachine.ChangeState(new PlayerTargetingState(playerStateMachine));
        }
        else
        {
            // FreeLook 상태로 돌아가기
            playerStateMachine.Animator.CrossFade("FreeLookBlendTree", playerStateMachine.AnimationDampTime);
            playerStateMachine.ChangeState(new PlayerFreeLookState(playerStateMachine));
        }
    }
}

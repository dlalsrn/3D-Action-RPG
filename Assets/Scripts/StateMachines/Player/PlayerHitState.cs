using UnityEngine;

public class PlayerHitState : PlayerBaseState
{
    public PlayerHitState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    private readonly int Hit = Animator.StringToHash("OnHit"); // Hit 애니메이션의 해시
    private float hitDuration = 0.7f; // Hit 상태 지속 시간
    private float timer;

    public override void Enter()
    {
        playerStateMachine.Animator.SetTrigger(Hit);
        timer = hitDuration;
    }

    public override void Tick(float deltaTime)
    {
        timer -= deltaTime;
        if (timer <= 0f)
        {
            // Hit 상태가 끝나면 이전 상태로 돌아가기
            RetunrToPreviousState();
        }
    }

    public override void Exit()
    {
    }
}

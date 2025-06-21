using UnityEngine;

public class EnemyHitState : EnemyBaseState
{
    public EnemyHitState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }

    private readonly int Hit = Animator.StringToHash("OnHit"); // Hit 애니메이션의 해시
    private float hitDuration = 1f; // Hit 상태 지속 시간
    private float timer;

    public override void Enter()
    {
        // 적이 Hit 상태에 진입할 때 애니메이션을 재생
        enemyStateMachine.Animator.SetTrigger(Hit);
        timer = hitDuration;
    }

    public override void Tick(float deltaTime)
    {
        timer -= deltaTime;
        if (timer <= 0f)
        {
            // Hit 상태가 끝나면 이전 상태로 돌아가기
            RetunrToIdleState();
        }
    }

    public override void Exit()
    {
    }
}

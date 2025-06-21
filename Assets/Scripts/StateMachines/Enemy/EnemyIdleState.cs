// Enemy가 아무것도 안 하고 있는 상태
public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }

    public override void Enter()
    {
    }

    public override void Tick(float deltaTime)
    {
        // 플레이어가 가까이 오면 추적 상태로 전환
        if (IsPlayerInChaseRange(enemyStateMachine.ChaseRange))
        {
            enemyStateMachine.ChangeState(new EnemyChasingState(enemyStateMachine));
            return;
        }

        enemyStateMachine.Animator.SetFloat(Enemy_Speed, 0f, enemyStateMachine.AnimationDampTime, deltaTime); // 애니메이션 속도 설정
    }

    public override void Exit()
    {
    }
}

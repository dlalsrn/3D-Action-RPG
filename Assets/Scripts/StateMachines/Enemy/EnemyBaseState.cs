using UnityEngine;

public class EnemyBaseState : State
{
    public EnemyBaseState(EnemyStateMachine enemyStateMachine)
    {
        this.enemyStateMachine = enemyStateMachine;
    }

    protected EnemyStateMachine enemyStateMachine;

    protected readonly int Enemy_Speed = Animator.StringToHash("EnemySpeed"); // EnemyBlendTree의 MoveSpeed 파라미터

    // 상태 진입 시 호출되는 메서드
    public override void Enter() { }

    // 상태가 활성화되어 있는 동안 매 프레임 호출되는 메서드
    public override void Tick(float deltaTime) { }

    // 상태가 종료될 때 호출되는 메서드
    public override void Exit() { }

    // 플레이어가 추적 범위 내에 있는지 확인하는 로직
    protected bool IsPlayerInChaseRange(float chaseRange)
    {
        if (enemyStateMachine.Player == null)
        {
            return false;
        }
        else if (enemyStateMachine.Player.Health.IsDead)
        {
            return false;
        }

        Vector3 directionToPlayer = enemyStateMachine.Player.transform.position - enemyStateMachine.transform.position; // 플레이어 방향
        directionToPlayer.y = 0; // y축 방향은 무시 (수평 거리만 고려)

        float distanceToPlayer = directionToPlayer.magnitude; // 플레이어와의 거리

        return distanceToPlayer <= chaseRange; // 플레이어가 추적 범위 내에 있는지 확인
    }

    protected void RetunrToIdleState()
    {
        // Idle 상태로 돌아가기
        enemyStateMachine.ChangeState(new EnemyIdleState(enemyStateMachine));
        enemyStateMachine.Animator.CrossFade("EnemyBlendTree", enemyStateMachine.AnimationDampTime);
    }
}

using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    public EnemyChasingState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }

    public override void Enter()
    {
        enemyStateMachine.NavMeshAgent.enabled = true; // NavMeshAgent 활성화
        enemyStateMachine.NavMeshAgent.updatePosition = false; // NavMeshAgent의 위치 업데이트를 비활성화
    }

    public override void Tick(float deltaTime)
    {
        // 플레이어 캐릭터가 추적 범위를 벗어나면 Idle 상태로 전환
        if (!IsPlayerInChaseRange(enemyStateMachine.ChaseRange) || enemyStateMachine.Player == null || enemyStateMachine.Player.Health.IsDead)
        {
            enemyStateMachine.ChangeState(new EnemyIdleState(enemyStateMachine));
            return;
        }
        // 플레이어가 공격 범위 내에 있으면 공격 상태로 전환
        else if (IsPlayerInChaseRange(enemyStateMachine.AttackRange))
        {
            enemyStateMachine.ChangeState(new EnemyAttackState(enemyStateMachine));
            return;
        }
        // 범위 내에 있으면 플레이어를 추적
        else
        {
            // 회전 처리
            FocusPlayer(deltaTime);

            // NavMeshAgent를 사용하여 플레이어를 추적
            MoveToPlayer(deltaTime);

            // 애니메이션 처리
            enemyStateMachine.Animator.SetFloat(Enemy_Speed, 1f, enemyStateMachine.AnimationDampTime, deltaTime); // 애니메이션 속도 설정
        }
    }

    public override void Exit()
    {
        enemyStateMachine.NavMeshAgent.enabled = false; // NavMeshAgent 활성화
        enemyStateMachine.NavMeshAgent.velocity = Vector3.zero; // NavMeshAgent의 속도를 0으로 설정
    }

    private void MoveToPlayer(float deltaTime)
    {
        enemyStateMachine.NavMeshAgent.SetDestination(enemyStateMachine.Player.transform.position); // NavMeshAgent를 사용하여 플레이어 위치로 이동

        enemyStateMachine.Controller.Move(enemyStateMachine.NavMeshAgent.desiredVelocity.normalized * enemyStateMachine.ChasingMoveSpeed * deltaTime); // NavMeshAgent의 원하는 속도로 이동

        enemyStateMachine.NavMeshAgent.nextPosition = enemyStateMachine.transform.position; // NavMeshAgent와 Transform의 위치를 동기화
    }

    // Chaseing State에서는 NavMeshAgent의 방향을 바라보도록 회전 처리 (플레이어를 바라볼 경우 몸의 방향과 이동 방향이 불일치할 수 있음)
    private void FocusPlayer(float deltaTime)
    {
        if (enemyStateMachine.Player == null)
        {
            return;
        }

        Vector3 focusDirection = enemyStateMachine.NavMeshAgent.desiredVelocity.normalized; // NavMeshAgent의 방향 Vector
        focusDirection.y = 0; // y축 방향은 무시 (수평 방향만 고려)

        if (focusDirection == Vector3.zero)
        {
            return; // 이동 방향이 없으면 회전하지 않음
        }

        enemyStateMachine.transform.rotation = Quaternion.Slerp(enemyStateMachine.transform.rotation,
                                                                Quaternion.LookRotation(focusDirection),
                                                                enemyStateMachine.RotationDampSpeed * deltaTime); // 부드럽게 회전
    }
}

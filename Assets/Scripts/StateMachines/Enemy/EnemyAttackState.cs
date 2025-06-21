using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public EnemyAttackState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }

    private int attackDamage = 20; // 공격 데미지
    private float knockbackForce = 7f; // 넉백 힘

    public override void Enter()
    {
        enemyStateMachine.Animator.SetTrigger("Attack"); // 공격 애니메이션 트리거 설정
        enemyStateMachine.WeaponDamage.SetAttack(attackDamage, knockbackForce); // 공격 데미지 설정
    }

    public override void Tick(float deltaTime)
    {
        FocusPlayer(deltaTime);
    }

    public override void Exit()
    {
        // 애니메이션이 정상적으로 끝나지 않은 경우를 대비하여 무기를 비활성화
        enemyStateMachine.WeaponHandler.DisableWeapon();
    }

    public void EndAttack()
    {
        // 공격이 끝났을 때 공격 범위 내에 없으면 Chasing State로 전환
        if (IsPlayerInChaseRange(enemyStateMachine.AttackRange))
        {
            enemyStateMachine.ChangeState(new EnemyAttackState(enemyStateMachine));
        }
        else
        {
            enemyStateMachine.ChangeState(new EnemyChasingState(enemyStateMachine));
            enemyStateMachine.Animator.CrossFade("EnemyBlendTree", enemyStateMachine.AnimationDampTime);
        }
    }

    // Attack State에서 플레이어를 계속 바라보도록 회전 처리
    private void FocusPlayer(float deltaTime)
    {
        if (enemyStateMachine.Player == null)
        {
            return;
        }

        Vector3 focusDirection = enemyStateMachine.Player.transform.position - enemyStateMachine.transform.position; // 플레이어 방향
        focusDirection.y = 0; // y축 방향은 무시 (수평 거리만 고려)

        enemyStateMachine.transform.rotation = Quaternion.Slerp(enemyStateMachine.transform.rotation,
                                                                Quaternion.LookRotation(focusDirection),
                                                                enemyStateMachine.RotationDampSpeed * deltaTime); // 부드럽게 회전
    }
}

using UnityEngine;

public class EnemyDieState : EnemyBaseState
{
    public EnemyDieState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }

    private readonly int Die = Animator.StringToHash("OnDie"); // Die 애니메이션의 해시

    public override void Enter()
    {
        enemyStateMachine.Animator.SetTrigger(Die);
        enemyStateMachine.WeaponDamage.gameObject.SetActive(false); // 무기 비활성화

        if (enemyStateMachine.Target != null)
        {
            GameObject.Destroy(enemyStateMachine.Target); // 타겟이 되지 않도록 컴포넌트 제거
        }

        enemyStateMachine.SetDisableComponents(); // Ragdoll 활성화를 위해 나머지 컴포넌트 비활성화
        enemyStateMachine.SetEnableRagdoll(); // Ragdoll 활성화

        GameObject.Destroy(enemyStateMachine.gameObject, 3f); // 3초 후에 제거
    }

    public override void Tick(float deltaTime)
    {
    }

    public override void Exit()
    {
    }
}

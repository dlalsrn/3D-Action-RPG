using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : BaseStateMachine
{
    public PlayerStateMachine Player; // 플레이어의 Transform
    public NavMeshAgent NavMeshAgent; // NavMeshAgent 컴포넌트
    public Target Target; // Target 컴포넌트

    public float ChaseRange = 10f; // 플레이어를 추적할 거리
    public float AttackRange = 1f;   // 플레이어를 공격할 거리

    public float ChasingMoveSpeed { get; private set; } = 4f; // Player를 추적할 때 Enemy의 이동 속도

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStateMachine>();
        ChangeState(new EnemyIdleState(this));
    }

    private void OnEnable()
    {
        Health.OnTakeDamage += HandleTakeDamage;
        Health.OnDie += HandleDie;
    }

    private void OnDisable()
    {
        Health.OnTakeDamage -= HandleTakeDamage;
        Health.OnDie -= HandleDie;
    }

    // Scene 뷰에서 선택되었을 때만 Gizmos를 그림.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ChaseRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }

    public void AnimationEndAttack()
    {
        if (currentState is EnemyAttackState attackState)
        {
            attackState.EndAttack();
        }
    }

    public void HandleTakeDamage()
    {
        ChangeState(new EnemyHitState(this));
    }

    public void HandleDie()
    {
        GameManager.Instance.DecreaseEnemyCount(); // 적의 수 감소
        ChangeState(new EnemyDieState(this));
    }

    public void SetDisableComponents()
    {
        NavMeshAgent.enabled = false; // NavMeshAgent 비활성화
        Controller.enabled = false; // CharacterController 비활성화
        WeaponHandler.enabled = false; // WeaponHandler 비활성화
        ForceReceiver.enabled = false; // ForceReceiver 비활성화
        Animator.enabled = false; // Animator 비활성화
    }

    public void SetEnableRagdoll()
    {
        foreach (var rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = false; // Ragdoll을 활성화하기 위해 Rigidbody를 비활성화
        }
    }
}

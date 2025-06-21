using UnityEngine;

public class PlayerStateMachine : BaseStateMachine
{
    // Player 전용 Components
    public PlayerInputReader InputReader;
    public Transform MainCameraTransform;
    public Scanner Scanner;

    public float FreeLookMoveSpeed { get; private set; } = 6f; // 자유롭게 돌아다닐 때의 이동 속도
    public float TargetingMoveSpeed { get; private set; } = 3f; // Enemy를 Targeting 했을 때의 이동 속도

    private void Start()
    {
        MainCameraTransform = Camera.main.transform;

        // 초기 상태를 PlayerFreeLookState로 설정
        ChangeState(new PlayerFreeLookState(this));
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, Scanner.SphereCollider.radius);
    }

    public void AnimationSetReady()
    {
        if (currentState is PlayerAttackState attackState)
        {
            attackState.SetReady();
        }
    }

    public void AnimationTryNextCombo()
    {
        if (currentState is PlayerAttackState attackState)
        {
            attackState.TryNextCombo();
        }
    }

    public void AnimationEndAttack()
    {
        if (currentState is PlayerAttackState attackState)
        {
            attackState.EndAttack();
        }
    }

    public void HandleTakeDamage()
    {
        ChangeState(new PlayerHitState(this));
    }

    public void HandleDie()
    {
        ChangeState(new PlayerDieState(this));
    }
}

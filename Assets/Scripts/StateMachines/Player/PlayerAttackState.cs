public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    private int maxCombo = 3; // 공격 단계 1, 2, 3 존재
    private int currentCombo = 0;
    private bool isReady = true;
    private bool isComboQueue = false;
    public int[] Damage = { 10, 15, 25 }; // 각 콤보 단계의 데미지
    public float[] Knockback = { 5f, 5f, 10f }; // 각 콤보 단계의 넉백

    public override void Enter()
    {
        playerStateMachine.InputReader.AttackPressed += OnAttackPressed;
        playerStateMachine.WeaponDamage.SetAttack(1, 2f);
        StartAttack();
    }

    public override void Tick(float deltaTime)
    {
        //ApplyForce(deltaTime); // 중력, 기타 물리력
    }

    public override void Exit()
    {
        // 애니메이션이 정상적으로 끝나지 않은 경우를 대비하여 무기를 비활성화
        playerStateMachine.WeaponHandler.DisableWeapon();
        playerStateMachine.InputReader.AttackPressed -= OnAttackPressed;
    }

    private void StartAttack()
    {
        currentCombo = 1;
        playerStateMachine.WeaponDamage.SetAttack(Damage[currentCombo - 1], Knockback[currentCombo - 1]);
        isReady = false;
        isComboQueue = false;
        playerStateMachine.Animator.SetTrigger($"Attack{currentCombo}");
    }

    private void OnAttackPressed()
    {
        // 공격 중일 때, 입력 예약 처리
        if (isReady && currentCombo < maxCombo)
        {
            isComboQueue = true;
        }
    }

    // 콤보 예약
    public void SetReady()
    {
        isReady = true;
    }

    // 콤보가 끝나고 다음 콤보로 진행하거나 공격을 종료
    public void TryNextCombo()
    {
        if (isComboQueue)
        {
            // 다음 콤보로 진행
            isComboQueue = false;
            isReady = false;
            currentCombo++;
            playerStateMachine.WeaponDamage.SetAttack(Damage[currentCombo - 1], Knockback[currentCombo - 1]);
            playerStateMachine.Animator.SetTrigger($"Attack{currentCombo}");
        }
    }

    public void EndAttack()
    {
        playerStateMachine.Animator.ResetTrigger($"Attack{currentCombo}");
        isReady = true;
        isComboQueue = false;
        currentCombo = 0;

        RetunrToPreviousState();
    }

    // private void RetunrToPreviousState()
    // {
    //     if (playerStateMachine.prevState is PlayerTargetingState)
    //     {
    //         // Targeting 상태로 돌아가기
    //         playerStateMachine.ChangeState(new PlayerTargetingState(playerStateMachine));
    //         playerStateMachine.Animator.CrossFade("TargetingBlendTree", 0.1f);
    //     }
    //     else
    //     {
    //         // FreeLook 상태로 돌아가기
    //         playerStateMachine.ChangeState(new PlayerFreeLookState(playerStateMachine));
    //         playerStateMachine.Animator.CrossFade("FreeLookBlendTree", 0.1f);
    //     }
    // }
}

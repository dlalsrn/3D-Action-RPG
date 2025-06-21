using UnityEngine;

// 플레이어가 Enemy를 항상 바라보고 있는 상태에서 카메라 시점을 바꿀 수 있는 상태
public class PlayerTargetingState : PlayerBaseState
{
    public PlayerTargetingState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    private readonly int Targeting_Forward = Animator.StringToHash("TargetingForward"); // Targeting 블렌드 트리의 앞, 뒤 움직임 Parameter
    private readonly int Targeting_Right = Animator.StringToHash("TargetingRight"); // Targeting 블렌드 트리의 좌, 우 움직임 Parameter

    // 상태 진입 시 호출되는 메서드
    public override void Enter()
    {
        playerStateMachine.InputReader.TargetPressed += OnTargetPressed;
        playerStateMachine.InputReader.AttackPressed += OnAttackPressed;
        playerStateMachine.Animator.SetTrigger(Change_Targeting_State);
    }

    // 상태가 활성화되어 있는 동안 매 프레임 호출되는 메서드
    public override void Tick(float deltaTime)
    {
        // 현재 Target이 없으면 FreeLook 상태로 전환 (일정 거리 이상 멀어졌거나, Target이 죽었을 경우)
        if (playerStateMachine.Scanner.currentTarget == null)
        {
            playerStateMachine.ChangeState(new PlayerFreeLookState(playerStateMachine));
            return;
        }

        //ApplyForce(deltaTime); // 중력, 기타 물리력

        // 플레이어의 이동을 처리
        Vector3 movement = CalculateMovement();
        playerStateMachine.Controller.Move(movement * playerStateMachine.FreeLookMoveSpeed * deltaTime);

        // 플레이어의 회전을 처리
        FocusTarget();

        // 플레이어의 애니메이션을 처리
        ChangeAnimator(movement, deltaTime);
    }

    // 상태가 종료될 때 호출되는 메서드
    public override void Exit()
    {
        playerStateMachine.Animator.ResetTrigger(Change_Targeting_State);
        playerStateMachine.InputReader.TargetPressed -= OnTargetPressed;
        playerStateMachine.InputReader.AttackPressed -= OnAttackPressed;
    }

    // Mouse 공격 Button이 눌렸을 때 호출되는 메서드
    private void OnAttackPressed()
    {
        playerStateMachine.ChangeState(new PlayerAttackState(playerStateMachine));
    }

    // Target Button(키보드 Tab)이 눌렸을 때 호출되는 메서드
    private void OnTargetPressed()
    {
        CancelTarget();
    }

    private void CancelTarget()
    {
        playerStateMachine.Scanner.CancelTarget();
        playerStateMachine.ChangeState(new PlayerFreeLookState(playerStateMachine));
    }

    private void FocusTarget()
    {
        if (playerStateMachine.Scanner.currentTarget == null)
        {
            return;
        }

        Vector3 targetPos = playerStateMachine.Scanner.currentTarget.transform.position;
        Vector3 playerPos = playerStateMachine.transform.position;

        Vector3 lookVec = targetPos - playerPos; // 플레이어 -> Target 방향
        lookVec.y = 0f; // Y축 회전은 없으므로 0으로 설정

        playerStateMachine.transform.rotation = Quaternion.LookRotation(lookVec);
    }

    private void ChangeAnimator(Vector3 movement, float deltaTime)
    {
        if (playerStateMachine.InputReader.MovementValue == Vector2.zero)
        {
            playerStateMachine.Animator.SetFloat(Targeting_Forward, 0f, playerStateMachine.AnimationDampTime, deltaTime);
            playerStateMachine.Animator.SetFloat(Targeting_Right, 0f, playerStateMachine.AnimationDampTime, deltaTime);
        }
        else
        {
            playerStateMachine.Animator.SetFloat(Targeting_Forward, movement.z, playerStateMachine.AnimationDampTime, deltaTime);
            playerStateMachine.Animator.SetFloat(Targeting_Right, movement.x, playerStateMachine.AnimationDampTime, deltaTime);
        }
    }
    
    private Vector3 CalculateMovement()
    {
        // 적을 바라보는 방향을 기준으로 플레이어의 이동 방향을 계산
        Vector3 forward = playerStateMachine.transform.forward;
        Vector3 right = playerStateMachine.transform.right;
        forward.y = right.y = 0f; // Y축 이동은 없으므로 0으로 설정

        Vector3 moveVec = new Vector3();
        moveVec += forward * playerStateMachine.InputReader.MovementValue.y;
        moveVec += right * playerStateMachine.InputReader.MovementValue.x;

        return moveVec;
    }
}

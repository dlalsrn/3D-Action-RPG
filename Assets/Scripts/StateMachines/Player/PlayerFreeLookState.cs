using UnityEngine;

// 플레이어가 자유롭게 돌아다니면서 카메라 시점을 바꿀 수 있는 상태
public class PlayerFreeLookState : PlayerBaseState
{
    public PlayerFreeLookState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }
    
    private readonly int FreeLook_Move = Animator.StringToHash("FreeLookMove"); // FreeLook 블렌드 트리의 Move Parameter

    // 상태 진입 시 호출되는 메서드
    public override void Enter()
    {
        playerStateMachine.InputReader.TargetPressed += OnTargetPressed;
        playerStateMachine.InputReader.AttackPressed += OnAttackPressed;
        playerStateMachine.Animator.SetTrigger(Change_FreeLook_State);
    }

    // 상태가 활성화되어 있는 동안 매 프레임 호출되는 메서드
    public override void Tick(float deltaTime)
    {
        //ApplyForce(deltaTime); // 중력, 기타 물리력
        
        // 플레이어의 이동을 처리
        Vector3 movement = CalculateMovement();
        playerStateMachine.Controller.Move(movement * playerStateMachine.FreeLookMoveSpeed * deltaTime);

        // 플레이어의 회전을 처리
        if (movement != Vector3.zero)
        {
            playerStateMachine.transform.rotation = CalculateRotation(movement, deltaTime);
        }

        // 플레이어의 애니메이션을 처리
        if (playerStateMachine.InputReader.MovementValue == Vector2.zero)
        {
            playerStateMachine.Animator.SetFloat(FreeLook_Move, 0f, playerStateMachine.AnimationDampTime, deltaTime);
        }
        else
        {
            playerStateMachine.Animator.SetFloat(FreeLook_Move, 1f, playerStateMachine.AnimationDampTime, deltaTime);
        }
    }

    // 상태가 종료될 때 호출되는 메서드
    public override void Exit()
    {
        playerStateMachine.Animator.ResetTrigger(Change_FreeLook_State);
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
        if (!playerStateMachine.Scanner.SelectTarget())
        {
            return;
        }

        // FreeLook 상태에서 Targeting 상태로 전환
        playerStateMachine.ChangeState(new PlayerTargetingState(playerStateMachine));
    }


    private Vector3 CalculateMovement()
    {
        // 카메라의 방향을 기준으로 플레이어의 이동 방향을 계산
        Vector3 forward = playerStateMachine.MainCameraTransform.forward;
        Vector3 right = playerStateMachine.MainCameraTransform.right;
        forward.y = right.y = 0f; // Y축 이동은 없으므로 0으로 설정

        Vector3 moveVec = new Vector3();
        moveVec += forward * playerStateMachine.InputReader.MovementValue.y;
        moveVec += right * playerStateMachine.InputReader.MovementValue.x;

        return moveVec;
    }

    private Quaternion CalculateRotation(Vector3 movement, float deltaTime)
    {
        return Quaternion.Slerp(playerStateMachine.transform.rotation,
                                Quaternion.LookRotation(movement),
                                playerStateMachine.RotationDampSpeed * deltaTime);
    }
}

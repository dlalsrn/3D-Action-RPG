using UnityEngine;

// Player와 Enemy의 상태 정보를 관리하는 Class
public class BaseStateMachine : MonoBehaviour
{
    // StateMachine에서 사용할 공통 Components
    public CharacterController Controller;
    public WeaponDamage WeaponDamage;
    public WeaponHandler WeaponHandler;
    public ForceReceiver ForceReceiver;
    public Animator Animator;
    public Health Health;

    // 현재 상태
    public State currentState;
    public State playerPrevState = null;

    public float AnimationDampTime { get; private set; } = 0.1f; // 애니메이션 블렌드 트리의 Damp 시간
    public float RotationDampSpeed { get; private set; } = 15f; // 회전 Damp 속도

    // 상태를 변경하는 메서드
    public void ChangeState(State newState)
    {
        // 현재 상태가 null이 아니면 Exit 호출
        currentState?.Exit();
        if (transform.CompareTag("Player"))
        {
            if (currentState is PlayerFreeLookState || currentState is PlayerTargetingState)
            {
                playerPrevState = currentState; // Player의 이전 상태를 저장
            }
        }

        // 새로운 상태로 변경하고 Enter 호출
        currentState = newState;
        currentState.Enter();
    }

    // 매 프레임 호출되는 메서드
    private void Update()
    {
        currentState?.Tick(Time.deltaTime);
    }
}

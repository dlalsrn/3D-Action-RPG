using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    public float verticalVelocity;
    public Vector3 Movement => impact + (verticalVelocity * Vector3.up);
    private CharacterController controller;

    private Vector3 impact; // 캐릭터에 적용되는 추가 물리력
    private Vector3 dampingVelocity;
    public float impactDamping = 0.1f; // 물리력 감소 속도

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // isGrounded는 Move 함수 호출 후에 상태가 갱신되기 때문에 isGrounded 위에서 호출해줘야함.
        // isGrounded 아래에서 호출할 경우 isGrounded가 false로 나올 수 있음.
        controller.Move(Movement * Time.deltaTime); // 캐릭터 이동
        
        if (verticalVelocity < 0f && controller.isGrounded)
        {
            verticalVelocity = 0;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime; // 중력 가속도
        }

        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, impactDamping); // 물리력 감소
    }

    public void AddImpact(Vector3 impactForce)
    {
        impact += impactForce; // 캐릭터에 적용되는 물리력 추가
    }

    public void ClearImpact()
    {
        impact = Vector3.zero; // 캐릭터에 적용되는 물리력 초기화
    }
}

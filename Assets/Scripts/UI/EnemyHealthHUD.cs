using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthHUD : MonoBehaviour
{
    public EnemyStateMachine stateMachine;
    public Slider healthSlider;

    public float hideTime = 3f;
    private Coroutine hideCoroutine;
    private bool prevTargeted = false;

    private void OnEnable()
    {
        stateMachine.Health.OnTakeDamage += HandleTakeDamage; // 데미지를 받았을 때 이벤트 등록
    }

    private void OnDisable()
    {
        stateMachine.Health.OnTakeDamage -= HandleTakeDamage;
    }

    private void HandleTakeDamage()
    {
        // 체력바 활성화
        healthSlider.gameObject.SetActive(true);
        
        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
            hideCoroutine = null; // 코루틴이 중지되었음을 표시
        }

        // 현재 Targeting 중이 아니라면, 2초 후에 사라지도록 코루틴 실행
        if (stateMachine.Player.Scanner.currentTarget != stateMachine.Target)
        {
            hideCoroutine = StartCoroutine(HideHUDRoutine(hideTime));
        }
    }

    IEnumerator HideHUDRoutine(float time)
    {
        yield return new WaitForSeconds(time);

        if (stateMachine.Player.Scanner.currentTarget != stateMachine.Target)
        {
            healthSlider.gameObject.SetActive(false); // HUD를 비활성화
        }
        hideCoroutine = null; // 코루틴이 끝났음을 표시
    }

    private void Update()
    {
        bool isTargeted = (stateMachine.Player.Scanner.currentTarget == stateMachine.Target);

        if (isTargeted)
        {
            if (!healthSlider.gameObject.activeSelf)
            {
                healthSlider.gameObject.SetActive(true);
            }

            if (hideCoroutine != null)
            {
                StopCoroutine(hideCoroutine);
                hideCoroutine = null;
            }
        }
        // 타겟팅이 해제되는 순간
        else if (prevTargeted && !isTargeted)
        {
            // Target이 해제되었을 때, 2초 후에 HUD를 비활성화
            if (hideCoroutine != null)
            {
                StopCoroutine(hideCoroutine);
            }
            hideCoroutine = StartCoroutine(HideHUDRoutine(hideTime));
        }

        prevTargeted = isTargeted;
    }

    private void LateUpdate()
    {
        if (stateMachine.Health.IsDead)
        {
            healthSlider.gameObject.SetActive(true);
        }

        healthSlider.value = stateMachine.Health.CurrentHealthRatio;
    }
}

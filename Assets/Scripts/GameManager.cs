using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public PlayerStateMachine player;
    public GameOverEvent gameOverEvent;

    public int EnemyCount { get; set; } // 현재 남아있는 적의 수

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        player.Health.OnDie += HandleDie; // 플레이어가 죽었을 때 이벤트 구독
        EnemyCount = FindObjectsByType<Target>(FindObjectsSortMode.None).Length; // 초기 적의 수 설정
    }

    public void DecreaseEnemyCount()
    {
        EnemyCount--; // 적의 수 감소
        if (EnemyCount <= 0)
        {
            StartCoroutine(GameOverRoutine(true)); // 모든 적이 죽었을 때 게임 오버
        }
    }

    private void HandleDie()
    {
        StartCoroutine(GameOverRoutine(false)); // 플레이어가 죽었을 때 게임 오버
    }

    IEnumerator GameOverRoutine(bool result)
    {
        yield return new WaitForSeconds(2f); // 2초 대기
        gameOverEvent.Play(result); // 게임 오버 이벤트 실행
        MouseUnlock(); // 마우스 잠금 해제
    }

    private void MouseUnlock()
    {
        Cursor.lockState = CursorLockMode.None; // 커서 잠금 해제
        Cursor.visible = true; // 커서 보이기
    }

    public void Quit()
    {
        Application.Quit();
    }
}

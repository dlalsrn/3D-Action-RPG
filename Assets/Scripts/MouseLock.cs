using UnityEngine;

public class MouseLock : MonoBehaviour
{
    public GameOverEvent gameOverEvent;

    void Update()
    {
        if (gameOverEvent.isGameOver)
        {
            // 게임 오버 패널이 활성화된 경우 커서 잠금 해제
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            return; // 게임 오버 상태에서는 더 이상 처리하지 않음
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        // 게임 플레이 중에는 다시 숨기고 고정 (원하는 상황에 따라 조건 설정)
        else if (Input.GetMouseButtonDown(0)) // 예시: 마우스 클릭 시 다시 잠그기
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}

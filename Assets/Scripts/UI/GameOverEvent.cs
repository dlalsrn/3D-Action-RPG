using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameOverEvent : MonoBehaviour
{
    public float FadeOutSpeed = 0.25f;

    public CanvasGroup canvasGroup;
    public Button button;
    public Image backgroundImage; // 배경 이미지
    public TextMeshProUGUI resultText; // 결과 텍스트

    public bool isGameOver = false;
    public bool isFinished = false;

    private void Start()
    {
        button.onClick.AddListener(RestartGame);
    }

    private void Update()
    {
        if (isFinished)
        {
            return;
        }
        
        if (isGameOver)
        {
            canvasGroup.alpha += FadeOutSpeed * Time.deltaTime;

            if (canvasGroup.alpha >= 1f)
            {
                canvasGroup.alpha = 1f; // 최대값으로 제한
                isFinished = true; // 패널이 완전히 표시되었음을 표시
            }
        }
    }

    public void Play(bool result)
    {
        backgroundImage.color = result ? Color.white : Color.black; // 승리 시 흰색, 패배 시 검은색
        resultText.text = result ? "VICTORY" : "YOU DIED"; // 승리 또는 패배 메시지 설정
        resultText.color = result ? Color.black : Color.red; // 승리 시 검은색, 패배 시 빨간색

        isGameOver = true;
        canvasGroup.alpha = 0f; // 초기화
        gameObject.SetActive(true); // 패널 활성화
    }

    public void RestartGame()
    {
        if (isFinished)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

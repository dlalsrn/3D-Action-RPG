using TMPro;
using UnityEngine;

public class EnemyCountHUD : MonoBehaviour
{
    public TextMeshProUGUI enemyCountText;

    private void Update()
    {
        enemyCountText.SetText($"{GameManager.Instance.EnemyCount}");
    }
}

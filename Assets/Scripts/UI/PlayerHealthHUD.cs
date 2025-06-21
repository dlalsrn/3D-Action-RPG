using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthHUD : MonoBehaviour
{
    public PlayerStateMachine stateMachine;
    private Slider healthSlider;

    private void Awake()
    {
        healthSlider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        healthSlider.value = stateMachine.Health.CurrentHealthRatio;
    }
}

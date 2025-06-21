using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour, PlayerControl.IPlayerActions
{
    PlayerControl control;

    public event Action TargetPressed;
    public event Action AttackPressed;

    public Vector2 MovementValue { get; private set; }

    private void Start()
    {
        control = new PlayerControl();
        control.Player.SetCallbacks(this);
        control.Player.Enable();
    }

    private void OnDestroy()
    {
        control.Player.Disable();
    }

    public void OnTarget(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            TargetPressed?.Invoke();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            AttackPressed?.Invoke();
        }
    }
}

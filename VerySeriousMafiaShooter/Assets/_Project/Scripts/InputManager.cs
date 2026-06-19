using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;


    public event EventHandler<OnPlayerMovementEventArgs> OnPlayerMovement;
    public class OnPlayerMovementEventArgs : EventArgs
    {
        public Vector2 InputDirection;
    }

    private InputSystem_Actions _inputActions;


    private void Awake()
    {
        Instance = this;
        _inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        _inputActions.Enable();

        _inputActions.Player.Move.performed += PlayerMovement;
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }



    private void PlayerMovement(InputAction.CallbackContext value)
    {
        OnPlayerMovement?.Invoke(this, new OnPlayerMovementEventArgs {
            InputDirection = value.ReadValue<Vector2>()
        });
    }
}

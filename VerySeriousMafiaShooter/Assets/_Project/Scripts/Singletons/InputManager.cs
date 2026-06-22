using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;


    public event EventHandler OnPlayerShoot;
    public event EventHandler OnPlayerReload;
    public event EventHandler<OnPlayerMovementEventArgs> OnPlayerMovement;
    public class OnPlayerMovementEventArgs : EventArgs
    {
        public Vector2 InputDirection;
    }

    private InputSystem_Actions _inputActions;


    private Vector2 _mousePosition;
    public Vector2 MousePosition { get { return _mousePosition; } set { } }


    private void Awake()
    {
        Instance = this;
        _inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        _inputActions.Enable();

        _inputActions.Player.Move.performed += MovementInput;
        _inputActions.Player.Shoot.performed += ShootInput;
        _inputActions.Player.Reload.performed += ReloadInput;
        _inputActions.Player.MousePosition.performed += MousePositionInput;
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }



    private void MovementInput(InputAction.CallbackContext value)
    {
        OnPlayerMovement?.Invoke(this, new OnPlayerMovementEventArgs {
            InputDirection = value.ReadValue<Vector2>()
        });
    }

    private void ShootInput(InputAction.CallbackContext value)
    {
        OnPlayerShoot?.Invoke(this, EventArgs.Empty);
    }

    private void ReloadInput(InputAction.CallbackContext value)
    {
        OnPlayerReload?.Invoke(this, EventArgs.Empty);
    }

    private void MousePositionInput(InputAction.CallbackContext value)
    {
        _mousePosition = value.ReadValue<Vector2>();
    }
}

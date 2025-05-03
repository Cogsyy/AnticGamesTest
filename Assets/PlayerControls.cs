using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private InputActionReference move;
    [SerializeField] private float speed = 5f;

    private InputSystem_Actions _playerInputActions;

    private Vector2 _moveInput;

    private void Start()
    {
        //Starting flow -> Main menu, then enable player controls on game start
        _playerInputActions = new InputSystem_Actions();
        _playerInputActions.Player.Disable();
        _playerInputActions.UI.Enable();
    }

    private void Update()
    {
        //_moveInput = _playerInputActions.Player.Move.ReadValue<Vector2>();

        //transform.Translate(_moveInput * speed * Time.deltaTime);
    }

    #region control map switching / UI

    public void OnPausePress(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SwitchActionMap(_playerInputActions.UI, _playerInputActions.Player);
            MainMenu.Instance.OnPause();
        }
    }

    public void SwitchToPlayerActionMap()
    {
        SwitchActionMap(_playerInputActions.Player, _playerInputActions.UI);
    }

    private void SwitchActionMap(InputActionMap newActionMap, InputActionMap currentActionMap)
    {
        // Disable the current action map
        if (currentActionMap != null)
        {
            currentActionMap.Disable();
        }

        // Enable the new action map
        newActionMap.Enable();
    }

    #endregion
}

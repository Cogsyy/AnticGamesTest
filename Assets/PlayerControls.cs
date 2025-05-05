using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    private const string TAG_ENEMY = "Enemy";

    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private InputActionReference move;
    [SerializeField] private GameController _gameController;

    private InputSystem_Actions _playerInputActions;
    private Camera _mainCamera;

    private void Start()
    {
        //Starting flow -> Main menu, then enable player controls on game start
        _playerInputActions = new InputSystem_Actions();
        _playerInputActions.Player.Disable();
        _playerInputActions.UI.Enable();

        _mainCamera = Camera.main;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        //Perform a raycast from the mouse position
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = _mainCamera.ScreenPointToRay(mousePosition);

        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if (hit.collider != null && hit.transform.CompareTag(TAG_ENEMY))
        {
            IUnit closestAntAlly = _gameController.grid.FindClosestUnitTo(hit.transform.GetComponent<IUnit>(), friendly:true);
            if (closestAntAlly != null)
            {
                ((AntUnit)closestAntAlly).SetMoveTarget(hit.transform);
            }
        }
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

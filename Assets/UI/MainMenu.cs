using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainMenu : MonoSingleton<MainMenu>
{
    [SerializeField] private Canvas _mainMenuCanvas;
    [Header("Buttons")]
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;
    [Header("Controls")]
    [SerializeField] private PlayerControls _playerControls;
    [SerializeField] private InputActionReference _submitAction;

    private void Start()
    {
        _startButton.onClick.AddListener(OnStartButtonClicked);
        _exitButton.onClick.AddListener(() => Application.Quit());

        _submitAction.action.performed += OnSubmitPerformed;

        SetMainMenuActive(false);
    }

    private void OnSubmitPerformed(InputAction.CallbackContext context)
    {
        GameObject selectedObject = EventSystem.current.currentSelectedGameObject;

        if (selectedObject != null && selectedObject.TryGetComponent<Button>(out Button button))
        {
            button.onClick.Invoke();
        }
    }

    public void OnStartButtonClicked()
    {
        _playerControls.SwitchToPlayerActionMap();
        SetMainMenuActive(false);
    }

    public void OnPause()
    {
        SetMainMenuActive(true);
    }

    public void SetMainMenuActive(bool active)
    {
        _mainMenuCanvas.enabled = active;
    }
}

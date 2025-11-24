using UnityEngine;
using UnityEngine.InputSystem;

public class PauseController_Client : MonoBehaviour {
    [SerializeField] private PauseView_Client _view;

    public void Initialize() {
        _view.ResumeButton.onClick.AddListener( OnResumeButton );
        _view.OpenSettingsButton.onClick.AddListener( OnOpenSettingsButton );
        _view.StopGameButton.onClick.AddListener( OnStopGameButton );
        _view.QuitButton.onClick.AddListener( OnQuitButton );
    }

    public void OnShowPauseMenuBtn( InputAction.CallbackContext context ) {
        if ( context.performed ) {
            _view.Show();
        }
    }

    private void OnResumeButton() {
        _view.Hide();
    }

    private void OnOpenSettingsButton() {
        SettingsEvents.OnSettingsOpened?.Invoke();
        _view.Hide();
    }

    private void OnStopGameButton() {
        GameEvents.OnStopGameBtn?.Invoke();
        _view.Hide();
    }

    private void OnQuitButton() {
        GameEvents.OnQuitToMenuBtn?.Invoke();
    }
}
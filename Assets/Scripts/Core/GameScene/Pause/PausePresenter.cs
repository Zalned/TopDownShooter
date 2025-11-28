using UnityEngine;
using UnityEngine.InputSystem;

public class PausePresenter : MonoBehaviour {
    [SerializeField] private PauseView _view;
    private bool _isSettingsOpened = false;

    private void Awake() {
        _view.ResumeButton.onClick.AddListener( OnResumeButton );
        _view.OpenSettingsButton.onClick.AddListener( OnOpenSettingsButton );
        _view.StopGameButton.onClick.AddListener( OnStopGameButton );
        _view.QuitButton.onClick.AddListener( OnQuitButton );
        HandleClientState();
    }

    private void OnDestroy() {
        _view.ResumeButton.onClick.RemoveListener( OnResumeButton );
        _view.OpenSettingsButton.onClick.RemoveListener( OnOpenSettingsButton );
        _view.StopGameButton.onClick.RemoveListener( OnStopGameButton );
        _view.QuitButton.onClick.RemoveListener( OnQuitButton );
    }

    public void OnShowPauseMenuBtn( InputAction.CallbackContext context ) {
        if ( context.performed ) {
            if ( _isSettingsOpened ) {
                _view.Hide();
                _isSettingsOpened = false;
            } else {
                _view.Show();
                _isSettingsOpened = true;
            }
        }
    }

    private void OnResumeButton() {
        _isSettingsOpened = false;
        _view.Hide();
    }

    private void OnOpenSettingsButton() {
        SettingsEvents.OnSettingsOpened?.Invoke();
        _isSettingsOpened = false;
        _view.Hide();
    }

    private void OnStopGameButton() {
        GameEvents.OnStopGameBtn.Invoke();
        _isSettingsOpened = false;
        _view.Hide();
    }

    private void OnQuitButton() {
        GameEvents.OnQuitToMenuBtn.Invoke();
    }

    private void HandleClientState() {
        if ( NetcodeHelper.IsClient ) {
            _view.StopGameButton.gameObject.SetActive( false );
        }
    }
}
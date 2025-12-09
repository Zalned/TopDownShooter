using UnityEngine;
using UnityEngine.InputSystem;

public class PausePresenter : MonoBehaviour {
    [SerializeField] private PauseView _view;
    private bool _isPauseOpened = false;

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
            if ( _isPauseOpened ) {
                _view.Hide();
                _isPauseOpened = false;
            } else {
                _view.Show();
                _isPauseOpened = true;
            }
        }
    }

    private void OnResumeButton() {
        _isPauseOpened = false;
        _view.Hide();
    }

    private void OnOpenSettingsButton() {
        EventBus.Publish( new SettingsOpenEvent() );
        _isPauseOpened = false;
        _view.Hide();
    }

    private void OnStopGameButton() { 
        EventBus.Publish( new StopGameButtonEvent() );
        _isPauseOpened = false;
        _view.Hide();
    }

    private void OnQuitButton() { 
        EventBus.Publish( new ExitGameButtonEvent() );
    }

    private void HandleClientState() {
        if ( NetcodeHelper.IsClient ) {
            _view.StopGameButton.gameObject.SetActive( false );
        }
    }
}
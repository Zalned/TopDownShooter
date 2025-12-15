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
        EventBus.Subscribe<PlayerStatsChanged>( InitializeStatsText );
        HandleClientState();
    }

    private void OnDestroy() {
        _view.ResumeButton.onClick.RemoveListener( OnResumeButton );
        _view.OpenSettingsButton.onClick.RemoveListener( OnOpenSettingsButton );
        _view.StopGameButton.onClick.RemoveListener( OnStopGameButton );
        _view.QuitButton.onClick.RemoveListener( OnQuitButton );
        EventBus.Unsubscribe<PlayerStatsChanged>( InitializeStatsText );
    }

    public void OnShowPauseMenuBtn( InputAction.CallbackContext context ) {
        if ( context.performed ) {
            if ( _isPauseOpened ) {
                HandlePauseClose();
            } else {
                HandlePauseOpen();
            }
        }
    }

    private void OnResumeButton() {
        HandlePauseClose();
    }

    private void OnOpenSettingsButton() {
        EventBus.Publish( new SettingsOpenEvent() );
        HandlePauseClose();
    }

    private void OnStopGameButton() {
        EventBus.Publish( new StopGameButtonEvent() );
        HandlePauseClose();
    }

    private void OnQuitButton() {
        EventBus.Publish( new ExitGameButtonEvent() );
    }

    private void HandleClientState() {
        if ( NetcodeHelper.IsHost ) {
            _view.StopGameButton.gameObject.SetActive( true );
        } else {
            _view.StopGameButton.gameObject.SetActive( false );
        }
    }

    private void HandlePauseClose() {
        _isPauseOpened = false;
        _view.Hide();
    }

    private void HandlePauseOpen() {
        _view.Show();
        _isPauseOpened = true;
    }

    private void InitializeStatsText( PlayerStatsChanged evt ) {
        string buffer = $"{evt.playerStats.RuntimeConfig.Player.GetAsText()}" +
                        $"\n{evt.playerStats.RuntimeConfig.Bullet.GetAsText()}";
        _view.SetStatsText( buffer );
    }
}
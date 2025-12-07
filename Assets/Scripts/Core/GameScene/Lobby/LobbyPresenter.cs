using System.Collections;
using UnityEngine;

public class LobbyPresenter : MonoBehaviour {
    [SerializeField] private LobbyView _view;
    private NetworkPlayerManager _playerManager;
    public void Initialize( NetworkPlayerManager playerManager ) {
        _playerManager = playerManager;
        _view.StartButton.onClick.AddListener( OnStartBtnClicked );
        _playerManager.OnRegistredPlayersUpdated += OnPlayersListUpdated;

        OnPlayersListUpdated();
        HandleClientState();
    }

    private void Awake() {
        if ( !NetcodeHelper.IsServer ) { this.enabled = false; return; }
    }

    private void OnDestroy() {
        _playerManager.OnRegistredPlayersUpdated -= OnPlayersListUpdated;
    }

    public void OnStartBtnClicked() {
        EventBus.Publish( new StartButtonClicked() );
    }

    private void OnPlayersListUpdated() {
        HandlePlayerListUpdated();
        StartCoroutine( DelayedUiRefresh() );
    }

    // MyNote: Не знаю как еще обновить список игроков у последнего подключившегося клиента не используя Update,
    // проблема в том, что UI обновляется до того как игрок прогрузился.
    private IEnumerator DelayedUiRefresh() {
        yield return new WaitForSeconds( 1f );
        OnPlayersListUpdated();
    }

    private void HandlePlayerListUpdated() {
        string buffer = string.Empty;
        foreach ( var player in _playerManager.RegistredPlayers.Values ) {
            buffer = buffer + player.Name + "\n";
        }
        _view.UpdatePlayerListClientRpc( buffer );
    }

    private void HandleClientState() {
        if ( NetcodeHelper.IsServer ) {
            _view.WaitingHostText.gameObject.SetActive( false );
            _view.StartButton.gameObject.SetActive( true );
        }
    }

    public void Show() {
        _view.ShowClientRpc();
    }

    public void Hide() {
        _view.HideClientRpc();
    }
}
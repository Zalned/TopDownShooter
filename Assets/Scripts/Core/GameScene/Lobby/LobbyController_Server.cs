using System.Collections;
using UnityEngine;

public class LobbyController_Server : MonoBehaviour {
    [SerializeField] private LobbyView_Global _view;
    private PlayerManager_Server _playerManager;

    public void Initialize( PlayerManager_Server playerManager ) {
        _playerManager = playerManager;
        _view.StartButton.onClick.AddListener( OnStartBtnClicked );
        _playerManager.OnRegistredPlayersUpdated += OnPlayersListUpdated;

        OnPlayersListUpdated();
    }

    private void Awake() {
        if ( !NetcodeHelper.IsServer ) { this.enabled = false; return; }
    }

    private void OnDestroy() {
        _playerManager.OnRegistredPlayersUpdated -= OnPlayersListUpdated;
    }

    public void OnStartBtnClicked() {
        GameEvents.OnStartBtn.Invoke();
    }

    // MyNote: Не знаю как еще обновить список игроков у последнего подключившегося клиента не используя Update,
    // проблема в том, что UI обновляется до того как игрок прогрузился.
    private IEnumerator DelayedUiRefresh() {
        yield return new WaitForSeconds( 1f );
        OnPlayersListUpdated();
    }

    private void OnPlayersListUpdated() {
        HandlePlayerListUpdated();
        StartCoroutine( DelayedUiRefresh() );
    }

    private void HandlePlayerListUpdated() {
        if ( _playerManager == null ) { Debug.LogWarning( "PlayerManager is null" ); return; }
        if ( _playerManager.RegistredPlayers == null ) { Debug.LogWarning( "RegistredPlayers is null" ); return; }

        string buffer = string.Empty;
        foreach ( var player in _playerManager.RegistredPlayers.Values ) {
            buffer = buffer + player.Name + "\n";
        }
        _view.UpdatePlayerListClientRpc( buffer );
    }

    public void Show() {
        _view.ShowClientRpc();
    }

    public void Hide() {
        _view.HideClientRpc();
    }
}
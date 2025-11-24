using UnityEngine;

public class LobbyController_Server : MonoBehaviour {
    [SerializeField] private LobbyView_Global _view;
    private PlayerManager_Server _playerManager;

    public void Initialize( PlayerManager_Server playerManager ) {
        _playerManager = playerManager;
        _view.StartButton.onClick.AddListener( OnStartBtnClicked );
        _playerManager.OnRegistredPlayersUpdated += OnPlayersListUpdated;
    }

    private void Start() {
        if ( NetcodeHelper.IsServer ) {
            OnPlayersListUpdated();
        } else {
            this.enabled = false;
        }
    }

    private void OnDestroy() {
        _playerManager.OnRegistredPlayersUpdated -= OnPlayersListUpdated;
    }

    public void OnStartBtnClicked() {
        GameEvents.OnStartBtn.Invoke();
    }

    private void OnPlayersListUpdated() {
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
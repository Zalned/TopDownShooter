using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class LobbyView : NetworkBehaviour {
    [SerializeField] private GameObject _lobbyObject;
    [SerializeField] private Button _startButton;
    [SerializeField] private TextMeshProUGUI _playerListText;
    [SerializeField] private TextMeshProUGUI _waitingHostText;

    public Button StartButton => _startButton;
    public TextMeshProUGUI WaitingHostText => _waitingHostText;

    [ClientRpc]
    public void ShowClientRpc() {
        _lobbyObject.SetActive( true );
    }

    [ClientRpc]
    public void HideClientRpc() {
        _lobbyObject.SetActive( false );
    }

    [ClientRpc]
    public void UpdatePlayerListClientRpc( string text ) {
        if ( !NetcodeHelper.IsClient ) { return; }
        _playerListText.text = text;
    }
}
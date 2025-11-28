using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class LobbyView : NetworkBehaviour {
    [SerializeField] private GameObject _lobbyObject;
    [SerializeField] private Button _startButton;
    [SerializeField] private TextMeshProUGUI _playerList;

    public Button StartButton => _startButton;

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
        _playerList.text = text;
    }
}
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class PlayerWinGameView : MonoBehaviour {
    [SerializeField] private GameObject _playerWinGameUI;
    [SerializeField] private TextMeshProUGUI _playerWinGameText;

    private string _gameWinnerPlayerName;

    [ServerRpc]
    public void ShowServerRpc() { ShowClientRpc(); }
    [ClientRpc]
    private void ShowClientRpc() {
        _playerWinGameUI.SetActive( true );
        UpdateWinText();
    }

    [ServerRpc]
    public void HideServerRpc() { HideClientRpc(); }
    [ClientRpc]
    private void HideClientRpc() {
        _playerWinGameUI.SetActive( false );
    }

    [ServerRpc]
    public void SetPlayerWinNameServer( string playerName ) { SetPlayerWinNameClientRpc( playerName ); }
    [ClientRpc]
    public void SetPlayerWinNameClientRpc( string playerName ) {
        _gameWinnerPlayerName = playerName;
    }

    private void UpdateWinText() {
        _playerWinGameText.text = $"{_gameWinnerPlayerName} Won the game!";
    }
}
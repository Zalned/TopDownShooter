using TMPro;
using Unity.Netcode;
using UnityEngine;

public class PlayerWinGameView : MonoBehaviour {
    [SerializeField] private GameObject _playerWinGameUI;
    [SerializeField] private TextMeshProUGUI _playerWinGameText;

    private string _gameWinnerPlayerName;

    [ClientRpc]
    public void ShowClientRpc() {
        _playerWinGameUI.SetActive( true );
        UpdateWinText();
    }

    [ClientRpc]
    public void HideClientRpc() {
        _playerWinGameUI.SetActive( false );
    }

    [ClientRpc]
    public void SetPlayerWinNameClientRpc( string playerName ) {
        _gameWinnerPlayerName = playerName;
    }

    private void UpdateWinText() {
        _playerWinGameText.text = $"{_gameWinnerPlayerName} Won the game!";
    }
}
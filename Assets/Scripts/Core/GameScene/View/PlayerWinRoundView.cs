using TMPro;
using Unity.Netcode;
using UnityEngine;

public class PlayerWinRoundView : NetworkBehaviour {
    [SerializeField] private GameObject _playerWinRoundUI;
    [SerializeField] private TextMeshProUGUI _playerWinRoundText;

    private string _roundWinnerName;

    [ClientRpc]
    public void ShowClientRpc() {
        _playerWinRoundUI.SetActive( true );
        UpdateWinText();
    }

    [ClientRpc]
    public void HideClientRpc() {
        _playerWinRoundUI.SetActive( false );
    }

    [ClientRpc]
    public void SetPlayerWinNameClientRpc( string playerName ) {
        _roundWinnerName = playerName;
    }

    private void UpdateWinText() {
        _playerWinRoundText.text = $"{_roundWinnerName} Won the round!";
    }
}
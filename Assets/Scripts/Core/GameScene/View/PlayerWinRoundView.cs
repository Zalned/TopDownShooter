using TMPro;
using Unity.Netcode;
using UnityEngine;

public class PlayerWinRoundView : MonoBehaviour {
    [SerializeField] private GameObject _playerWinRoundUI;
    [SerializeField] private TextMeshProUGUI _playerWinRoundText;

    private string _roundWinnerName;

    [ServerRpc]
    public void ShowServerRpc() { ShowClientRpc(); }
    [ClientRpc]
    private void ShowClientRpc() {
        _playerWinRoundUI.SetActive( true );
        UpdateWinText();
    }

    [ServerRpc]
    public void HideServerRpc() { HideClientRpc(); }
    [ClientRpc]
    private void HideClientRpc() {
        _playerWinRoundUI.SetActive( false );
    }

    [ServerRpc]
    public void SetPlayerWinNameServerRpc( string playerName ) { SetPlayerWinNameClientRpc( playerName ); }
    [ClientRpc]
    private void SetPlayerWinNameClientRpc( string playerName ) {
        _roundWinnerName = playerName;
    }

    private void UpdateWinText() {
        _playerWinRoundText.text = $"{_roundWinnerName} Won the round!";
    }
}
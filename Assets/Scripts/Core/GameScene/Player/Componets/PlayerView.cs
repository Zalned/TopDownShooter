using TMPro;
using Unity.Netcode;
using UnityEngine;

public class PlayerView : NetworkBehaviour {
    [SerializeField] private Renderer _renderer;
    [SerializeField] private TextMeshProUGUI _playerName;

    [ClientRpc]
    public void InitializeClientRpc( NetworkPlayerData data ) {
        SetColor( data.TeamColor );
        SetPlayerName( data.Name );
    }

    private void SetColor( Color color ) {
        _renderer.material.color = color;
    }

    private void SetPlayerName( string name ) {
        _playerName.text = name;
    }
}
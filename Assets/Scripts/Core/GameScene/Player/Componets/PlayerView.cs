using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : NetworkBehaviour {
    [SerializeField] private Renderer _renderer;
    [SerializeField] private TextMeshProUGUI _playerName;
    [SerializeField] private Slider _playerHealth;
    [SerializeField] private Image _playerHealthSliderFill;

    [Rpc( SendTo.Server, InvokePermission = RpcInvokePermission.Everyone )]
    public void InitializeServerRpc( NetworkPlayerData data, float maxHealth ) {
        InitializeClientRpc( data, maxHealth );
    }
    [ClientRpc]
    private void InitializeClientRpc( NetworkPlayerData data, float maxHealth ) {
        SetColor( data.TeamColor );
        SetPlayerName( data.Name );
        InitializePlayerHeatlhSlider( maxHealth );
        SetPlayerHeatlhServerRpc( maxHealth );
    }

    private void SetColor( Color color ) {
        _renderer.material.color = color;
    }

    private void SetPlayerName( string name ) {
        _playerName.text = name;
    }

    private void InitializePlayerHeatlhSlider( float maxValue ) {
        _playerHealth.maxValue = maxValue;
    }

    [Rpc( SendTo.Server, InvokePermission = RpcInvokePermission.Everyone )]
    public void SetPlayerHeatlhServerRpc( float value ) {
        SetPlayerHeatlhClientRpc( value );
    }
    [ClientRpc]
    private void SetPlayerHeatlhClientRpc( float value ) {
        _playerHealth.value = value;
    }
}
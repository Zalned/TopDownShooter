using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : NetworkBehaviour {
    [SerializeField] private Canvas _playerHudCanvas;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private TextMeshProUGUI _playerName;

    [SerializeField] private TextMeshProUGUI _healthHudText;
    [SerializeField] private Slider _healthWorldSlider;
    [SerializeField] private Image _healthWorldSliderFill;

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
        //_playerName.text = name; // world canvas с данными игрока не используется
    }

    private void InitializePlayerHeatlhSlider( float maxValue ) {
        //_playerHealth.maxValue = maxValue;
    }

    public void UpdateHealthHud( float value ) {
        _healthHudText.text = value.ToString();
    }

    [Rpc( SendTo.Server, InvokePermission = RpcInvokePermission.Everyone )]
    public void SetPlayerHeatlhServerRpc( float value ) {
        //SetPlayerHeatlhClientRpc( value );
    }
    [ClientRpc]
    private void SetPlayerHeatlhSliderClientRpc( float value ) {
        //_playerHealth.value = value;
    }
    private void SetPlayerHeatlhHud( float value ) {
        //_playerHealth.value = value;
    }

    public void HideHudCanvas() {
        _playerHudCanvas.enabled = false;
    }
}
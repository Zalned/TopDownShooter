using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class PlayerView : NetworkBehaviour {
    [SerializeField] private Canvas _playerHudCanvas;
    [SerializeField] private Renderer _cubeRenderer;
    [SerializeField] private Renderer _faceRenderer;
    [Space]
    [SerializeField] private TextMeshProUGUI _healthHudText;
    [Space]
    [SerializeField] private Canvas _worldCanvas;
    [SerializeField] private TextMeshProUGUI _worldName;
    [SerializeField] private Slider _worldHealthSlider;

    [Rpc( SendTo.Server, InvokePermission = RpcInvokePermission.Everyone )]
    public void InitializeServerRpc( NetworkPlayerData data, float maxHealth ) {
        InitializeClientRpc( data, maxHealth );
    }
    [ClientRpc]
    private void InitializeClientRpc( NetworkPlayerData data, float maxHealth ) {
        SetColor( data.TeamColor );
        InitializeWorldName( data.Name );
        InitializeWorldHealthSlider( maxHealth );
    }

    private void SetColor( Color color ) {
        Color emission = color * 2.5f; 

        _cubeRenderer.material.EnableKeyword( "_EMISSION" );
        _cubeRenderer.material.SetColor( "_EmissionColor", emission );
        _cubeRenderer.material.SetColor( "_BaseColor", emission );

        _faceRenderer.material.EnableKeyword( "_EMISSION" );
        _faceRenderer.material.SetColor( "_EmissionColor", emission );
        _faceRenderer.material.SetColor( "_BaseColor", emission );
    }

    private void InitializeWorldName( string name ) {
        _worldName.text = name;
    }

    private void InitializeWorldHealthSlider( float maxHealth ) {
        _worldHealthSlider.maxValue = maxHealth;
        _worldHealthSlider.value = maxHealth;
    }

    [Rpc( SendTo.Server, InvokePermission = RpcInvokePermission.Everyone )]
    public void UpdateHealthServerRpc( float value ) {
        UpdateWorldHealthClientRpc( value );
        UpdateHudHealth( value );
    }

    [ClientRpc]
    private void UpdateWorldHealthClientRpc( float value ) {
        _worldHealthSlider.value = value;
    }

    private void UpdateHudHealth( float value ) {
        _healthHudText.text = value.ToString();
    }

    public void HideHudCanvas() {
        _playerHudCanvas.enabled = false;
    }

    public void HideWorldCanvas() {
        _worldCanvas.enabled = false;
    }
}
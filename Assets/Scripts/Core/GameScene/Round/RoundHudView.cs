using TMPro;
using Unity.Netcode;
using UnityEngine;

public class RoundHudView : NetworkBehaviour {
    [SerializeField] private GameObject RoundHudObject;
    [SerializeField] private TextMeshProUGUI RoundHudText;

    [ClientRpc]
    public void ShowClientRpc() { RoundHudObject.SetActive( true ); }

    [ClientRpc]
    public void HideClientRpc() { RoundHudObject.SetActive( false ); }

    [ClientRpc]
    public void UpdateHudDataClientRpc( string text ) { RoundHudText.text = text; }
}
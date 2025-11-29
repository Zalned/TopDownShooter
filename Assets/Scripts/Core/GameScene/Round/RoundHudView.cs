using TMPro;
using Unity.Netcode;
using UnityEngine;
using System.Collections.Generic;

public class RoundHudView : NetworkBehaviour {
    [SerializeField] private GameObject RoundHudObject;
    [SerializeField] private List<TextMeshProUGUI> RoundHudTexts;

    [ClientRpc]
    public void ShowClientRpc() { RoundHudObject.SetActive( true ); }

    [ClientRpc]
    public void HideClientRpc() { RoundHudObject.SetActive( false ); }

    [ClientRpc]
    public void UpdateHudDataClientRpc( HudPlayerDataArray hudPlayerDataArray) {
        HudPlayerData[] hudPlayerDatas = hudPlayerDataArray.Data;
        for ( int i = 0; i < hudPlayerDataArray.Data.Length; i++ ) {
            RoundHudTexts[ i ].text = hudPlayerDatas[ i ].PlayerText.ToString();
            RoundHudTexts[ i ].color = hudPlayerDatas[ i ].PlayerColor;
        }
    }
}
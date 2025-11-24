using TMPro;
using UnityEngine;

public class RoundViewUI : MonoBehaviour {
    [SerializeField] private GameObject RoundHudObject;
    [SerializeField] private TextMeshProUGUI RoundHudText;

    public void ShowHUD() { RoundHudObject.SetActive( true ); }
    public void HideHUD() { RoundHudObject.SetActive( false ); }
    public void UpdateHudData( string text ) { RoundHudText.text = text; }
}
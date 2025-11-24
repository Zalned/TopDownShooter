using UnityEngine;

public class SettingsView_Client : MonoBehaviour {
    [SerializeField] private GameObject _settingsMenuObj;

    public void Show() {
        _settingsMenuObj.SetActive( true );
    }

    public void Hide() {
        _settingsMenuObj.SetActive( false );
    }
}
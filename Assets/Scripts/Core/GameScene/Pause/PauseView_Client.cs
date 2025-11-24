using UnityEngine;
using UnityEngine.UI;

public class PauseView_Client : MonoBehaviour {
    [SerializeField] private GameObject _pauseMenuObj;
    [SerializeField] private Button _resume;
    [SerializeField] private Button _openSettingsBtn;
    [SerializeField] private Button _stopGameBtn;
    [SerializeField] private Button _quitBtn;

    public Button ResumeButton => _resume;
    public Button OpenSettingsButton => _openSettingsBtn;
    public Button StopGameButton => _stopGameBtn;
    public Button QuitButton => _quitBtn;


    public void Show() {
        _pauseMenuObj.SetActive( true );
    }
    public void Hide() {
        _pauseMenuObj.SetActive( false );
    }
}
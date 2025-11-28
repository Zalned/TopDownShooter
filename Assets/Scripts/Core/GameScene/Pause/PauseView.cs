using UnityEngine;
using UnityEngine.UI;

public class PauseView : MonoBehaviour {
    [SerializeField] private GameObject _pauseMenuObj;
    [SerializeField] private Button _resumeBtn;
    [SerializeField] private Button _openSettingsBtn;
    [SerializeField] private Button _stopGameBtn;   
    [SerializeField] private Button _quitBtn;

    public Button ResumeButton => _resumeBtn;
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
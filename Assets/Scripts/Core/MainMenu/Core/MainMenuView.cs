using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour {
    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private Button _hostBtn;
    [SerializeField] private Button _joinBtn;
    [SerializeField] private Button _quitBtn;

    public Button HostButton => _hostBtn;
    public Button JoinButton => _joinBtn;
    public Button QuitButton => _quitBtn;

    public void Show() {
        _mainMenuPanel.SetActive( true );
    }
    public void Hide() {
        _mainMenuPanel.SetActive( false );
    }
}
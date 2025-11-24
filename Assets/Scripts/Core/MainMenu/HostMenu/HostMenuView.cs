using UnityEngine;
using UnityEngine.UI;

public class HostMenuView : MonoBehaviour {
    [SerializeField] private GameObject _menuObj;
    [SerializeField] private InputField _inputIpField;
    [SerializeField] private Button _startBtn;
    [SerializeField] private Button _returnButton;

    public InputField InputIpField => _inputIpField;
    public Button StartBtn => _startBtn;
    public Button ReturnBtn => _returnButton;

    public void Show() {
        _menuObj.SetActive( true );
    }

    public void Hide() {
        _menuObj.SetActive( false );
    }
}
using UnityEngine;
using UnityEngine.UI;

public class JoinMenuView : MonoBehaviour {
    [SerializeField] private GameObject _menuObj;
    [SerializeField] private InputField _inputIpField;
    [SerializeField] private Button _startBtn;
    [SerializeField] private Button _returnBtn;

    public InputField InputIpField => _inputIpField;
    public Button StartBtn => _startBtn;
    public Button ReturnBtn => _returnBtn;

    public void Show() {
        _menuObj.SetActive( true );
    }

    public void Hide() {
        _menuObj.SetActive( false );
    }
}
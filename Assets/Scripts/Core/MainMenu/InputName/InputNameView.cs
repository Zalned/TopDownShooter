using UnityEngine;
using UnityEngine.UI;

public class InputNameView : MonoBehaviour {
    [SerializeField] private GameObject _menuObj;
    [SerializeField] private InputField _inputNameField;
    [SerializeField] private GameObject _incorrectNameTextObj;
    [SerializeField] private Button _confirmBtn;
    [SerializeField] private Button _returnBtn;

    public InputField InputNameField => _inputNameField;
    public Button ConfirmBtn => _confirmBtn;
    public Button ReturnBtn => _returnBtn;

    public void Show() {
        _menuObj.SetActive( true );
    }

    public void Hide() {
        _menuObj.SetActive( false ); 
    }

    public void ShowIncorrectNameText() {
        _incorrectNameTextObj.SetActive( true );
    }

    public void HideIncorrectNameText() {
        _incorrectNameTextObj.SetActive( false );
    }
}
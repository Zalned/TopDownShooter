using UnityEngine;
using UnityEngine.UI;

public class SettingsView : MonoBehaviour {
    [SerializeField] private GameObject _menuObj;
    [SerializeField] public Button _closeBtn;
    [SerializeField] private Slider _effectsVolumeSlider;

    public GameObject MenuObj => _menuObj;
    public Button CloseBtn => _closeBtn;
    public Slider EffectVolumeSlider => _effectsVolumeSlider;

    public void Show() {
        _menuObj.SetActive( true );
    }

    public void Hide() {
        _menuObj.SetActive( false );
    }
}
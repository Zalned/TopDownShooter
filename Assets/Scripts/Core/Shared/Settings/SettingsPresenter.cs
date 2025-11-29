public class SettingsPresenter {
    private readonly SettingsView _view;
    private readonly SettingsConfigSO _config;

    public SettingsPresenter( SettingsView view, SettingsConfigSO config ) {
        _view = view;
        _config = config;

        _view.EffectVolumeSlider.value = _config.EffectsVolume;
        _view.CloseBtn.onClick.AddListener( OnCloseBtnClicked );
        _view.EffectVolumeSlider.onValueChanged.AddListener( OnEffectsVolumeChanged );

        SettingsEvents.OnSettingsOpened += _view.Show;
    }

    private void OnCloseBtnClicked() {
        _view.Hide();
    }

    private void OnEffectsVolumeChanged( float value ) {
        _config.EffectsVolume = value;
    }
}
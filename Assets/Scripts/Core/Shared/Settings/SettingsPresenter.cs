using System;

public class SettingsPresenter : IDisposable {
    private readonly SettingsView _view;
    private readonly SettingsConfigSO _config;

    private IDisposable _onSettingsOpenedSub;

    public SettingsPresenter( SettingsView view, SettingsConfigSO config ) {
        _view = view;
        _config = config;

        _view.EffectVolumeSlider.value = _config.EffectsVolume;
        _view.CloseBtn.onClick.AddListener( OnCloseBtnClicked );
        _view.EffectVolumeSlider.onValueChanged.AddListener( OnEffectsVolumeChanged );

        _onSettingsOpenedSub = EventBus.Subscribe<SettingsOpenEvent>( e => _view.Show() );
    }

    public void Dispose() {
        _view.CloseBtn.onClick.RemoveListener( OnCloseBtnClicked );
        _view.EffectVolumeSlider.onValueChanged.RemoveListener( OnEffectsVolumeChanged );
        _onSettingsOpenedSub.Dispose();
    }

    private void OnCloseBtnClicked() {
        _view.Hide();
    }

    private void OnEffectsVolumeChanged( float value ) {
        _config.EffectsVolume = value;
    }
}
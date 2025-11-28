public class SettingsService {
    private readonly SettingsView _view;

    public SettingsService(SettingsView view ) {
        _view = view;

        SettingsEvents.OnSettingsOpened += Show;
        SettingsEvents.OnSettingsClosed += Hide;
    }

    public void Show() {
        _view.Show();
    }

    public void Hide() {
        _view.Hide();
    }
}
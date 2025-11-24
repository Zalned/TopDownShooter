public class SettingsService_Client {
    private readonly SettingsView_Client _view;

    public SettingsService_Client(SettingsView_Client view ) {
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
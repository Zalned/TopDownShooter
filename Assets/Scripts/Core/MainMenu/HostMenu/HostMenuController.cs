using System;
using Zenject;

public class HostMenuController : IDisposable {
    private HostMenuView _view;

    public event Action<string> OnStartBtn;
    public event Action OnReturnBtn;

    [Inject]
    public HostMenuController( HostMenuView view ) {
        _view = view;

        _view.StartBtn.onClick.AddListener( OnStartBtnClicked );
        _view.ReturnBtn.onClick.AddListener( OnReturnBtnClicked );

    }
    public void Dispose() {
        _view.StartBtn.onClick.RemoveListener( OnStartBtnClicked );
        _view.ReturnBtn.onClick.RemoveListener( OnReturnBtnClicked );
    }

    public void OnReturnBtnClicked() {
        OnReturnBtn.Invoke();
    }

    public void OnStartBtnClicked() {
        OnStartBtn.Invoke( _view.InputIpField.text );
    }

    public void Show() => _view.Show();
    public void Hide() => _view.Hide();
}
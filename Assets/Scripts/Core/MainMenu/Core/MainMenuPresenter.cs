using System;
using UnityEngine;
using Zenject;

public class MainMenuPresenter : IDisposable {
    private MainMenuView _mainMenuView;
    private HostMenuPresenter _hostMenuPresenter;
    private JoinMenuPresenter _joinMenuPresenter;
    private InputNamePresenter _inputNamePresenter;

    private MenuConnectionStarter _connectionStarter;

    [Inject]
    public MainMenuPresenter(
        MainMenuView view,
        HostMenuPresenter hostMenuController,
        JoinMenuPresenter joinMenuController,
        InputNamePresenter inputNameController,
        MenuConnectionStarter connectionStarter ) {
        _mainMenuView = view;
        _hostMenuPresenter = hostMenuController;
        _joinMenuPresenter = joinMenuController;
        _inputNamePresenter = inputNameController;
        _connectionStarter = connectionStarter;

        _mainMenuView.HostButton.onClick.AddListener( OnHostButton );
        _mainMenuView.JoinButton.onClick.AddListener( OnJoinButton );
        _mainMenuView.QuitButton.onClick.AddListener( OnQuitBtn );
        _hostMenuPresenter.OnStartBtn += HostMenuStartBtn;
        _hostMenuPresenter.OnReturnBtn += HostMenuReturnBtn;
        _joinMenuPresenter.OnStartBtn += JoinMenuStartBtn;
        _joinMenuPresenter.OnReturnBtn += JoinMenuReturnBtn;
        _inputNamePresenter.OnNameConfirmed += InputNameMenuStartBtn;
        _inputNamePresenter.OnReturnBtn += InputNameMenuReturnBtn;
    }

    public void Dispose() {
        _mainMenuView.HostButton.onClick.RemoveListener( OnHostButton );
        _mainMenuView.JoinButton.onClick.RemoveListener( OnJoinButton );
        _mainMenuView.QuitButton.onClick.RemoveListener( OnQuitBtn );
        _hostMenuPresenter.OnStartBtn -= HostMenuStartBtn;
        _hostMenuPresenter.OnReturnBtn -= HostMenuReturnBtn;
        _joinMenuPresenter.OnStartBtn -= JoinMenuStartBtn;
        _joinMenuPresenter.OnReturnBtn -= JoinMenuReturnBtn;
        _inputNamePresenter.OnNameConfirmed -= InputNameMenuStartBtn;
        _inputNamePresenter.OnReturnBtn -= InputNameMenuReturnBtn;
    }

    private void OnHostButton() {
        _hostMenuPresenter.Show();
        if ( _inputNamePresenter.IsNameInitialized ) {
            _mainMenuView.Hide();
        } else {
            _inputNamePresenter.Show();
        }
    }

    private void OnJoinButton() {
        _joinMenuPresenter.Show();
        if ( _inputNamePresenter.IsNameInitialized ) {
            _mainMenuView.Hide();
        } else {
            _inputNamePresenter.Show();
        }
    }

    public void HostMenuStartBtn( string ip ) {
        _connectionStarter.StartServer( ip, _inputNamePresenter.Name );
    }

    public void HostMenuReturnBtn() {
        _hostMenuPresenter.Hide();
        _mainMenuView.Show();
    }

    public void JoinMenuStartBtn( string ip ) {
        _connectionStarter.StartClient( ip, _inputNamePresenter.Name );
    }

    public void JoinMenuReturnBtn() {
        _joinMenuPresenter.Hide();
        _mainMenuView.Show();
    }

    public void InputNameMenuStartBtn( string name ) {
        _inputNamePresenter.Hide();
        _mainMenuView.Hide();
    }

    public void InputNameMenuReturnBtn() {
        _inputNamePresenter.Hide();
        _hostMenuPresenter.Hide();
        _joinMenuPresenter.Hide();
    }

    public void OnQuitBtn() {
        Application.Quit();
    }
}
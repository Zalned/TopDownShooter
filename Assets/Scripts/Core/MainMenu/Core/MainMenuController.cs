using System;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class MainMenuController : IDisposable {
    private MainMenuView _mainMenuView;
    private HostMenuController _hostMenuController;
    private JoinMenuController _joinMenuController;
    private InputNameController _inputNameController;

    private MenuConnectionStarter _connectionStarter;

    [Inject]
    public MainMenuController(
        MainMenuView view,
        HostMenuController hostMenuController,
        JoinMenuController joinMenuController,
        InputNameController inputNameController,
        MenuConnectionStarter connectionStarter ) {
        _mainMenuView = view;
        _hostMenuController = hostMenuController;
        _joinMenuController = joinMenuController;
        _inputNameController = inputNameController;
        _connectionStarter = connectionStarter;

        _mainMenuView.HostButton.onClick.AddListener( OnHostButton );
        _mainMenuView.JoinButton.onClick.AddListener( OnJoinButton );
        _mainMenuView.QuitButton.onClick.AddListener( OnQuitBtn );

        _hostMenuController.OnStartBtn += HostMenuStartBtn;
        _hostMenuController.OnReturnBtn += HostMenuReturnBtn;

        _joinMenuController.OnStartBtn += JoinMenuStartBtn;
        _joinMenuController.OnReturnBtn += JoinMenuReturnBtn;

        _inputNameController.OnNameConfirmed += InputNameMenuStartBtn;
        _inputNameController.OnReturnBtn += InputNameMenuReturnBtn;
    }

    public void Dispose() {
        _mainMenuView.HostButton.onClick.RemoveListener( OnHostButton );
        _mainMenuView.JoinButton.onClick.RemoveListener( OnJoinButton );
        _mainMenuView.QuitButton.onClick.RemoveListener( OnQuitBtn );
        _hostMenuController.OnStartBtn -= HostMenuStartBtn;
        _hostMenuController.OnReturnBtn -= HostMenuReturnBtn;
        _joinMenuController.OnStartBtn -= JoinMenuStartBtn;
        _joinMenuController.OnReturnBtn -= JoinMenuReturnBtn;
        _inputNameController.OnNameConfirmed -= InputNameMenuStartBtn;
        _inputNameController.OnReturnBtn -= InputNameMenuReturnBtn;
    }

    private void OnHostButton() {
        _hostMenuController.Show();
        if ( _inputNameController.IsNameInitialized ) {
            _mainMenuView.Hide();
        } else {
            _inputNameController.Show();
        }
    }

    private void OnJoinButton() {
        _joinMenuController.Show();
        if ( _inputNameController.IsNameInitialized ) {
            _mainMenuView.Hide();
        } else {
            _inputNameController.Show();
        }
    }

    public void HostMenuStartBtn( string ip ) {
        _connectionStarter.StartServer( ip, _inputNameController.Name );
    }

    public void HostMenuReturnBtn() {
        _hostMenuController.Hide();
        _mainMenuView.Show();
    }

    public void JoinMenuStartBtn( string ip ) {
        _connectionStarter.StartClient( ip, _inputNameController.Name );
    }

    public void JoinMenuReturnBtn() {
        _joinMenuController.Hide();
        _mainMenuView.Show();
    }

    public void InputNameMenuStartBtn( string name ) {
        _inputNameController.Hide();
        _mainMenuView.Hide();
    }

    public void InputNameMenuReturnBtn() {
        _inputNameController.Hide();
        _hostMenuController.Hide();
        _joinMenuController.Hide();
    }

    public void OnQuitBtn() {
        Application.Quit();
    }
}
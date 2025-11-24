using System;
using UnityEngine;
using Zenject;

public class InputNameController : IDisposable {
    [SerializeField] private InputNameView _view;

    private const int MIN_NAME_CHARACTERS = 3;
    private const int MAX_NAME_CHARACTERS = 10;

    public event Action<string> OnNameConfirmed;
    public event Action OnReturnBtn;

    public bool IsNameInitialized { get; private set; } = false;
    public string Name { get; private set; } = "Unknown";

    [Inject]
    public InputNameController( InputNameView view ) {
        _view = view;

        _view.ConfirmBtn.onClick.AddListener( HandleNameCorrect );
        _view.ReturnBtn.onClick.AddListener( OnReturnBtnClicked );
    }
    public void Dispose() {
        _view.ConfirmBtn.onClick.RemoveListener( HandleNameCorrect );
        _view.ReturnBtn.onClick.RemoveListener( OnReturnBtnClicked );
    }

    public void OnReturnBtnClicked() {
        OnReturnBtn.Invoke();
    }

    private void HandleNameCorrect() {
        string name = _view.InputNameField.text;

        if ( name.Length < MIN_NAME_CHARACTERS || name.Length > MAX_NAME_CHARACTERS ) {
            _view.ShowIncorrectNameText();
        } else {
            Name = name;
            IsNameInitialized = true;
            OnNameConfirmed.Invoke( name );
        }
    }

    public void Show() => _view.Show();
    public void Hide() => _view.Hide();
}
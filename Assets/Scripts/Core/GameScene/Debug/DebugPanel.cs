using System;
using UnityEngine;
using UnityEngine.UI;

public static class DebugEvents {
    public static Action OnShowCardPickViewBtn;
    public static Action<ulong, string> OnEndRoundBtn;
}

public class DebugPanel : MonoBehaviour {
    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _showPickCardMenuBtn;
    [SerializeField] private Button _endRoundBtn;
    private bool _isDebugPanelActive = false;

    private void Awake() {
        _showPickCardMenuBtn.onClick.AddListener( OnOnShowCardPickViewBtnClicked );
        _endRoundBtn.onClick.AddListener( OnEngRoundBtnClicked );
    }
    private void OnDestroy() {
        _showPickCardMenuBtn.onClick.RemoveListener( OnOnShowCardPickViewBtnClicked );
        _endRoundBtn.onClick.RemoveListener( OnEngRoundBtnClicked );
    }

    public void OnOpenDebugPanelInput() {
        if ( _isDebugPanelActive ) {
            _panel.SetActive( false );
            _isDebugPanelActive = false;
        } else {
            _panel.SetActive( true );
            _isDebugPanelActive = true;
        }
    }

    private void OnOnShowCardPickViewBtnClicked() {
        DebugEvents.OnShowCardPickViewBtn?.Invoke();
    }
    private void OnEngRoundBtnClicked() {
        DebugEvents.OnEndRoundBtn?.Invoke( 0, "DebugPlayer" );
    }
}
using System;
using UnityEngine;
using UnityEngine.UI;

public static class DebugEvents {
    public static Action OnShowCardPickViewBtn;
    public static Action<ulong, string> OnEndRoundBtn;
}

public class DebugPanel : MonoBehaviour {
    [SerializeField] private Button _showPickCardMenuBtn;
    [SerializeField] private Button _endRoundBtn;

    private void Awake() {
        _showPickCardMenuBtn.onClick.AddListener( OnOnShowCardPickViewBtn );
        _endRoundBtn.onClick.AddListener( () => DebugEvents.OnEndRoundBtn?.Invoke( 0, "DebugPlayer" ) );
    }

    private void OnOnShowCardPickViewBtn() {
        Debug.Log( "OnShowCardPickViewBtn" );
        DebugEvents.OnShowCardPickViewBtn?.Invoke();
    }
}
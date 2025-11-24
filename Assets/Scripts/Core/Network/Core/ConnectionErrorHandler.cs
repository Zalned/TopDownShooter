using System;
using UnityEngine;
using Zenject;

public class ConnectionErrorHandler : IDisposable {
    private SceneLoader _sceneLoader;

    [Inject]
    public ConnectionErrorHandler( SceneLoader sceneLoader ) {
        _sceneLoader = sceneLoader;
        NetworkEvents.OnNetworkError += HandleNetworkError;
    }

    private void HandleNetworkError( string context, Exception exception ) {
        Debug.LogError( $"[{nameof( ConnectionErrorHandler )}] Network error. {context}: {exception.Message}" );

        _sceneLoader.LoadMenuScene();
    }

    public void Dispose() {
        NetworkEvents.OnNetworkError -= HandleNetworkError;
    }
}
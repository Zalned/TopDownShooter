using System;
using UnityEngine;
using Zenject;

public class ConnectionErrorHandler : IDisposable {
    private SceneLoader _sceneLoader;

    private IDisposable _networkErrorEventSubscription;

    [Inject]
    public ConnectionErrorHandler( SceneLoader sceneLoader ) {
        _sceneLoader = sceneLoader;

        _networkErrorEventSubscription =
            EventBus.Subscribe<NetworkErrorEvent>( e => HandleNetworkError( e.Context, e.Error ) );
    }

    public void Dispose() {
        _networkErrorEventSubscription.Dispose();
    }

    private void HandleNetworkError( string context, Exception exception ) {
        Debug.LogError( $"[{nameof( ConnectionErrorHandler )}] Network error. {context}: {exception.Message}" );

        _sceneLoader.LoadMenuScene();
    }
}
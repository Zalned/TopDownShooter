using System;
using Zenject;

public class ConnectionHandler : IDisposable {
    private readonly SceneLoader _sceneLoader;


    [Inject]
    public ConnectionHandler( SceneLoader sceneLoader ) {
        _sceneLoader = sceneLoader;
        EventBus.Subscribe<HostStartedEvent>( HandleHostStarted );
        EventBus.Subscribe<ClientConnectedEvent>( HandleClientConnected );
    }

    public void Dispose() {
        EventBus.Unsubscribe<HostStartedEvent>( HandleHostStarted );
        EventBus.Unsubscribe<ClientConnectedEvent>( HandleClientConnected );
    }

    private void HandleHostStarted( HostStartedEvent e ) {
        _sceneLoader.LoadGameScene();
    }

    private void HandleClientConnected( ClientConnectedEvent e ) {
        _sceneLoader.LoadGameScene();
    }
}
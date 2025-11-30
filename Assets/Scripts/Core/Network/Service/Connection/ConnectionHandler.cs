using System;
using Zenject;

public class ConnectionHandler : IDisposable {
    private readonly SceneLoader _sceneLoader;

    private IDisposable _onClientConnectedSub;
    private IDisposable _onHostStartedSub;

    [Inject]
    public ConnectionHandler( SceneLoader sceneLoader ) {
        _sceneLoader = sceneLoader;
        _onHostStartedSub = EventBus.Subscribe<HostStartedEvent>( e => HandleHostStarted() );
        _onClientConnectedSub = EventBus.Subscribe<ClientConnectedEvent>( e => HandleClientConnected( e.ClientId ) );
    }

    public void Dispose() {
        _onHostStartedSub.Dispose();
        _onClientConnectedSub.Dispose();
    }

    private void HandleHostStarted() {
        _sceneLoader.LoadGameScene();
    }

    private void HandleClientConnected( ulong _ ) {
        _sceneLoader.LoadGameScene();
    }
}
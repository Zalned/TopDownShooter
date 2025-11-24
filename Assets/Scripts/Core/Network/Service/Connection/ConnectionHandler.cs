using System;
using Zenject;

public class ConnectionHandler : IDisposable {
    private readonly SceneLoader _sceneLoader;

    [Inject]
    public ConnectionHandler( SceneLoader sceneLoader ) {
        _sceneLoader = sceneLoader;
        NetworkEvents.OnClientConnected += HandleClientConnected;
        NetworkEvents.OnHostStarted += HandleHostStarted;
    }

    private void HandleHostStarted() {
        _sceneLoader.LoadGameScene();
    }

    private void HandleClientConnected( ulong _ ) {
        _sceneLoader.LoadGameScene();
    }

    public void Dispose() {
        NetworkEvents.OnClientConnected -= HandleClientConnected;
    }
}
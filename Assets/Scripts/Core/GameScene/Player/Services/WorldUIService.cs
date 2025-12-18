using System;
using UnityEngine;

public class WorldUIService : MonoBehaviour {
    private Camera _localCamera;

    private IDisposable _playerSpawnedEventDispose;

    public void Initialize( Camera camera ) {
        _localCamera = camera;
        InitializeBoards();

        _playerSpawnedEventDispose = 
            EventBus.Subscribe<PlayerSpawnedClientEvent>( e => InitializeBoards() );
    }

    private void OnDestroy() {
        _playerSpawnedEventDispose.Dispose();
    }

    private void InitializeBoards() {
        foreach ( var board in FindObjectsByType<PlayerWorldUIBoard>( FindObjectsSortMode.None ) ) {
            board.Initialize( _localCamera );
        }
    }
}

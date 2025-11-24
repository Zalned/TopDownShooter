using UnityEngine;
using Zenject;

public class PlayerDieHandler_Server {
    private SessionPlayerManager_Server _sessionPlayerManager;
    private PlayerSpawnController_Server _playerSpawnController;

    [Inject]
    public PlayerDieHandler_Server(
        SessionPlayerManager_Server sessionPlayerManager,
        PlayerSpawnController_Server playerSpawn ) {
        _sessionPlayerManager = sessionPlayerManager;
        _playerSpawnController = playerSpawn;

        GameEvents.OnPlayerDie += HandlePlayerDie;
    }

    private void HandlePlayerDie( ulong id, GameObject playerObj ) {
        _sessionPlayerManager.RemoveLivePlayer( id);
        _sessionPlayerManager.AddDeadPlayer( id, playerObj );
        _playerSpawnController.DespawnPlayerOnMap( playerObj );
    }
}
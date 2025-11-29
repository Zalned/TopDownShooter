using UnityEngine;
using Zenject;

public class PlayerDieHandler {
    private SessionPlayerManager _sessionPlayerManager;
    private PlayerSpawnService _playerSpawnService;

    [Inject]
    public PlayerDieHandler(
        SessionPlayerManager sessionPlayerManager,
        PlayerSpawnService playerSpawn ) {
        _sessionPlayerManager = sessionPlayerManager;
        _playerSpawnService = playerSpawn;

        GameEvents.OnPlayerDie += HandlePlayerDie;
    }

    private void HandlePlayerDie( ulong id, GameObject playerObj ) {
        _sessionPlayerManager.RemoveLivePlayer( id);
        _sessionPlayerManager.AddDeadPlayer( id, playerObj );
        _playerSpawnService.DespawnPlayerOnMap( playerObj );
    }
}
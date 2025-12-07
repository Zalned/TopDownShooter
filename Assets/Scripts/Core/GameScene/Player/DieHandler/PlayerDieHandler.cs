using System;
using Zenject;

public class PlayerDieHandler : IDisposable {
    private SessionPlayerManager _sessionPlayerManager;
    private PlayerSpawnService _playerSpawnService;

    [Inject]
    public PlayerDieHandler(
        SessionPlayerManager sessionPlayerManager,
        PlayerSpawnService playerSpawn ) {
        _sessionPlayerManager = sessionPlayerManager;
        _playerSpawnService = playerSpawn;

        EventBus.Subscribe<PlayerDiedEvent>( HandlePlayerDie );
    }
    public void Dispose() {
        EventBus.Subscribe<PlayerDiedEvent>( HandlePlayerDie );
    }

    private void HandlePlayerDie( PlayerDiedEvent evt ) {
        _sessionPlayerManager.RemoveLivePlayer( evt.id );
        _sessionPlayerManager.AddDeadPlayer( evt.id, evt.playerObj );
        _playerSpawnService.DespawnPlayerOnMap( evt.playerObj );
    }
}
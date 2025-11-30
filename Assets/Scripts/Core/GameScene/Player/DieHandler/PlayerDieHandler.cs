using System;
using UnityEngine;
using Zenject;

public class PlayerDieHandler : IDisposable {
    private SessionPlayerManager _sessionPlayerManager;
    private PlayerSpawnService _playerSpawnService;

    private IDisposable _subscription;

    [Inject]
    public PlayerDieHandler(
        SessionPlayerManager sessionPlayerManager,
        PlayerSpawnService playerSpawn ) {
        _sessionPlayerManager = sessionPlayerManager;
        _playerSpawnService = playerSpawn;

        _subscription = EventBus.Subscribe<PlayerDiedEvent>( e => HandlePlayerDie( e.id, e.playerObj ) );
    }
    public void Dispose() {
        _subscription.Dispose();
    }

    private void HandlePlayerDie( ulong id, GameObject playerObj ) {
        _sessionPlayerManager.RemoveLivePlayer( id );
        _sessionPlayerManager.AddDeadPlayer( id, playerObj );
        _playerSpawnService.DespawnPlayerOnMap( playerObj );
    }
}
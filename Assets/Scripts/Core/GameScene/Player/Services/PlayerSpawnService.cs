using System.Linq;
using UnityEngine;
using Zenject;

public class PlayerSpawnService {
    private readonly NetworkPlayerManager _playerManager;
    private readonly MapService _mapService;
    private readonly SessionPlayerManager _sessionPlayerManager;
    private readonly PlayerInitializer _playerInitializer;

    [Inject]
    public PlayerSpawnService(
        NetworkPlayerManager playerManager,
        MapService mapService,
        SessionPlayerManager sessionPlayerManager,
        PlayerInitializer playerInitializer ) {
        _playerManager = playerManager;
        _mapService = mapService;
        _sessionPlayerManager = sessionPlayerManager;
        _playerInitializer = playerInitializer;
    }

    public void SpawnPlayersOnMap() {
        var registered = _playerManager.RegistredPlayers;

        foreach ( var player in registered.Values ) {
            var spawnedPlayer = SpawnPlayerOnMap( player.NetID );
            _sessionPlayerManager.AddLivePlayer( spawnedPlayer.Item1, spawnedPlayer.Item2 );
        }
    }

    public (ulong, GameObject) SpawnPlayerOnMap( ulong id ) {
        Vector3 spawnPosition = _mapService.GetRandomSpawnPosition();

        GameObject playerPrefab = Resources.Load<GameObject>( Defines.ObjectPaths.PLAYER_PREFAB );
        GameObject playerObj = Object.Instantiate( playerPrefab, spawnPosition, Quaternion.identity );
        NetcodeHelper.SpawnAsPlayerObject( playerObj, id, true );

        _playerInitializer.InitializePlayer( playerObj, id );
        return (id, playerObj);
    }

    public void DespawnPlayersOnMap() {
        var toRemove = _sessionPlayerManager.LivePlayers.Keys.ToList();

        foreach ( var playerId in toRemove ) {
            var playerObj = _sessionPlayerManager.LivePlayers[ playerId ];
            DespawnPlayerOnMap( playerObj );
            _sessionPlayerManager.RemoveLivePlayer( playerId );
        }
    }

    public void DespawnPlayerOnMap( GameObject player ) {
        NetcodeHelper.Despawn( player, true );
    }
}
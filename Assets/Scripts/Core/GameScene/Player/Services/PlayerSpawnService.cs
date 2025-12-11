using System.Linq;
using UnityEngine;
using Zenject;

public class PlayerSpawnService {
    private readonly MapService _mapService;
    private readonly SessionPlayerManager _sessionPlayerManager;

    [Inject]
    public PlayerSpawnService( MapService mapService, SessionPlayerManager sessionPlayerManager ) {
        _mapService = mapService;
        _sessionPlayerManager = sessionPlayerManager;
    }

    public void SpawnPlayersOnMap() {
        var activePlayers = _sessionPlayerManager.ActivePlayers;

        foreach ( var player in activePlayers.Values ) {
            var spawnedPlayer = SpawnPlayerOnMap( player.NetworkPlayerData.NetID );
            _sessionPlayerManager.AddLivePlayer( spawnedPlayer.Item1, spawnedPlayer.Item2 );
        }
    }

    public (ulong, GameObject) SpawnPlayerOnMap( ulong id ) {
        Vector3 spawnPosition = _mapService.GetRandomSpawnPosition();

        GameObject playerPrefab = Resources.Load<GameObject>( Defines.PrefabPaths.PLAYER );
        GameObject playerObj = Object.Instantiate( playerPrefab, spawnPosition, Quaternion.identity );
        NetcodeHelper.SpawnAsPlayerObject( playerObj, id, true );

        EventBus.Publish( new PlayerSpawnedEvent( id, playerObj ) );
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
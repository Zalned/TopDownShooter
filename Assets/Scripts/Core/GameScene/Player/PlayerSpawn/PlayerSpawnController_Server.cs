using System.Linq;
using UnityEngine;

public class PlayerSpawnController_Server {
    private readonly PlayerManager_Server _playerManager;
    private readonly MapService_Server _mapService;
    private readonly SessionPlayerManager_Server _sessionPlayerManager;

    public PlayerSpawnController_Server(
        PlayerManager_Server playerManager,
        MapService_Server mapService,
         SessionPlayerManager_Server sessionPlayerManager ) {
        _playerManager = playerManager;
        _mapService = mapService;
        _sessionPlayerManager = sessionPlayerManager;
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

        var data = _playerManager.GetPlayerById( NetcodeHelper.GetOwnerClientID( playerObj ) );
        playerObj.GetComponent<PlayerController>().InitalizeClientRpc( data );

        Debug.Log( $"Spawned player {id} at {spawnPosition} on Map" );
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
//using System.Collections.Generic;
//using UnityEngine;
//using Zenject;

//public class PlayerReviveService {
//    private PlayerSpawnService _playerSpawn;
//    private SessionPlayerManager _sessionPlayerManager;
//    private Dictionary<ReviveTimer, ulong> _activeRevivesPlayers = new();

//    [Inject]
//    public PlayerReviveService(
//        PlayerSpawnService playerSpawn,
//        SessionPlayerManager sessionPlayerManager ) {
//        _playerSpawn = playerSpawn;
//        _sessionPlayerManager = sessionPlayerManager;
//    }

//    public void AddToReviveList( ulong id ) {
//        _activeRevivesPlayers.Add( new ReviveTimer(), id );
//    }

//    public void RevivePlayer( ulong id ) {
//        _sessionPlayerManager.RemoveDeadPlayer( id );
//        _playerSpawn.SpawnPlayerOnMap( id );

//        GameEvents.OnPlayerRevived?.Invoke( id );
//    }

//    public void ReviveCycle() {
//        foreach ( var deadPlayers in _activeRevivesPlayers ) {
//            deadPlayers.Key.UpdateTime( Time.deltaTime );

//            if ( deadPlayers.Key.IsReviveComplete ) {
//                RevivePlayer( deadPlayers.Value );
//                _activeRevivesPlayers.Remove( deadPlayers.Key );
//                break;
//            }
//        }
//    }
//}
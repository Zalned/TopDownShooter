//using System.Collections.Generic;
//using UnityEngine;
//using Zenject;

//public class PlayerReviveService_Server {
//    private PlayerSpawnController_Server _playerSpawnController;
//    private SessionPlayerManager_Server _sessionPlayerManager;
//    private Dictionary<ReviveTimer, ulong> _activeRevivesPlayers = new();

//    [Inject]
//    public PlayerReviveService_Server(
//        PlayerSpawnController_Server playerSpawn,
//        SessionPlayerManager_Server sessionPlayerManager ) {
//        _playerSpawnController = playerSpawn;
//        _sessionPlayerManager = sessionPlayerManager;
//    }

//    public void AddToReviveList( ulong id ) {
//        _activeRevivesPlayers.Add( new ReviveTimer(), id );
//    }

//    public void RevivePlayer( ulong id ) {
//        _sessionPlayerManager.RemoveDeadPlayer( id );
//        _playerSpawnController.SpawnPlayerOnMap( id );

//        //GameEvents.OnPlayerRevived?.Invoke( id );
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
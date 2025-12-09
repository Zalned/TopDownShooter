using UnityEngine;

public class PlayerInitializer {
    private readonly NetworkPlayerManager _playerManager;
    private readonly SessionPlayerManager _sessionPlayerManager;

    public PlayerInitializer( NetworkPlayerManager playerManager, SessionPlayerManager sessionPlayerManager ) {
        _playerManager = playerManager;
        _sessionPlayerManager = sessionPlayerManager;
    }

    public void InitializePlayer( GameObject obj, ulong id ) {
        var data = _playerManager.GetPlayerById( NetcodeHelper.GetOwnerClientID( obj ) );
        var activePlayer = _sessionPlayerManager.GetActivePlayerByID( id );

        var cardsIds = activePlayer.CardDeckIDs.ToArray();

        if ( cardsIds.Length > 0 ) { Debug.Log( $"InitializePlayer CardDeckIds: {cardsIds[ 0 ]}" ); }

        obj.GetComponent<PlayerController>().InitalizeClientRpc( data, cardsIds );
    }
}
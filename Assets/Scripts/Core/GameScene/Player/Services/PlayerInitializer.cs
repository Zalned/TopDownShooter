using System;

public class PlayerInitializer : IDisposable {
    private readonly NetworkPlayerManager _playerManager;
    private readonly SessionPlayerManager _sessionPlayerManager;

    public PlayerInitializer( NetworkPlayerManager playerManager, SessionPlayerManager sessionPlayerManager ) {
        _playerManager = playerManager;
        _sessionPlayerManager = sessionPlayerManager;
        EventBus.Subscribe<PlayerSpawnedEvent>( InitializePlayer );
    }
    public void Dispose() {
        EventBus.Unsubscribe<PlayerSpawnedEvent>( InitializePlayer );
    }

    public void InitializePlayer( PlayerSpawnedEvent evt ) {
        var data = _playerManager.GetPlayerById( NetcodeHelper.GetOwnerClientID( evt.obj ) );
        var activePlayer = _sessionPlayerManager.GetActivePlayerByID( evt.id );

        var cardsIds = activePlayer.CardDeckIDs.ToArray();
        evt.obj.GetComponent<PlayerController>().InitalizeClientRpc( data, cardsIds );
    }
}
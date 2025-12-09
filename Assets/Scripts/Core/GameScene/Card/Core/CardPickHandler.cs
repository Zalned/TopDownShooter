using Unity.Netcode;

public class CardPickHandler : NetworkBehaviour {
    private SessionPlayerManager _sessionPlayerManager;

    public void Initialize( SessionPlayerManager sessionPlayerManager ) {
        _sessionPlayerManager = sessionPlayerManager;
    }

    [Rpc( SendTo.Server )]
    public void HandleCardPickedToServerRpc( ulong playerId, int cardId ) {
        var activePlayer = _sessionPlayerManager.GetActivePlayerByID( playerId );
        activePlayer.CardDeckIDs.Add( cardId );
    }
}
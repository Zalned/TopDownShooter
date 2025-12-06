using Unity.Netcode;

public class CardPickController : NetworkBehaviour {
    private ulong _playerID;

    public void OnCardPicked( ulong playerId, ushort CardId ) {
        _playerID = NetcodeHelper.LocalClientId;
        SendCardPickedToServerRpc( playerId, CardId );
    }

    [Rpc( SendTo.Server )]
    private void SendCardPickedToServerRpc( ulong playerId, ushort CardId ) {
        EventBus.Publish( new PlayerCardPickEvent( _playerID, CardId ) );
    }
}

public class PlayerExitHandler {
    private SessionPlayerManager _sessionPlayerManager;

    public PlayerExitHandler(
        SessionPlayerManager sessionPlayerManager ) {
        _sessionPlayerManager = sessionPlayerManager;

        EventBus.Subscribe<PlayerExitEvent>( HandlePlayerExit );
    }

    private void HandlePlayerExit( PlayerExitEvent evt ) {
        _sessionPlayerManager.RemoveSessionPlayer( evt.playerId );
        _sessionPlayerManager.RemoveActivePlayer( evt.playerId );
        _sessionPlayerManager.RemoveLivePlayer( evt.playerId );
        _sessionPlayerManager.RemoveDeadPlayer( evt.playerId );
    }
}
public record PlayerWinGameEvent( ulong playerId );
public record GameStartedEvent();
public record GameStoppedEvent();
public record PlayerExitEvent( ulong playerId );
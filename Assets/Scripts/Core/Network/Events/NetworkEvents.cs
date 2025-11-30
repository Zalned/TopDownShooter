using System;

public record NetworkStartedEvent();

public record PlayerJoinedEvent( NetworkPlayerData Data );
public record PlayerLeftEvent( NetworkPlayerData Data );

public record ClientConnectedEvent( ulong ClientId );
public record ClientDisconnectedEvent( ulong ClientId );

public record StartHostRequestEvent( string IpAdress );
public record StopHostRequestEvent();

public record StartClientRequestEvent( string IpAdress );

public record HostStartedEvent();

public record NetworkErrorEvent( string Context, Exception Error );
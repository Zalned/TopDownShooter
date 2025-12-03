using System;

public record NetworkStartedEvent();

public record PlayerJoinedEvent( NetworkPlayerData Data );
public record PlayerLeftEvent( NetworkPlayerData Data );

public record ClientConnectedEvent( ulong ClientID );
public record ClientDisconnectedEvent( ulong ClientID );

public record StartHostRequestEvent( string IpAdress );
public record StopHostRequestEvent();

public record StartClientRequestEvent( string IpAdress );

public record HostStartedEvent();

public record NetworkErrorEvent( string Context, Exception Error );
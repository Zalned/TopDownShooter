using UnityEngine;
public record PlayerSpawnedEvent( ulong id, GameObject obj );
public record PlayerDiedEvent( ulong id, GameObject playerObj );
public record PlayerStatsChanged( ulong id, PlayerStats playerStats );
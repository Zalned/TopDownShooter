using UnityEngine;
public record PlayerDiedEvent( ulong id, GameObject playerObj );
public record PlayerStatsChanged( ulong id, PlayerStats playerStats );
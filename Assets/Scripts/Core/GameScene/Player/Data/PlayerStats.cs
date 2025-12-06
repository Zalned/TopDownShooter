using UnityEngine;

public enum PlayerStatType {
    MaxHealth, 
    Speed,
    DashTime, 
    DashLength, 
    DashCount,
    ReloadTime
}

public enum BulletStatType {
    Damage, 
    Speed, 
    Lifetime, 
    Radius,
    RicochetCount,
    PenetrationCount,
    HasSplash,
    Scale
}

public class PlayerStats {
    public PlayerRuntimeConfig RuntimeConfig { get; private set; }

    public PlayerStats() {
        var basePlayerConfig = Resources.Load<BasePlayerConfigSO>( Defines.ConfigPaths.PLAYER_CONFIG );
        var baseBulletConfig = Resources.Load<BaseBulletConfigSO>( Defines.ConfigPaths.BULLET_CONFIG );
        RuntimeConfig = new PlayerRuntimeConfig( basePlayerConfig, baseBulletConfig );
    }

    public void ApplyPlayerModifier( PlayerStatType type, float value ) {
        PlayerRuntimeStats runtimeStats = new();
        switch ( type ) {
            case PlayerStatType.MaxHealth: runtimeStats.MaxHealth += value; break;
            case PlayerStatType.Speed: runtimeStats.Speed += value; break;
            case PlayerStatType.DashLength: runtimeStats.DashLength += value; break;
            case PlayerStatType.ReloadTime: runtimeStats.ReloadTime += value; break;
        }
        RuntimeConfig.SetPlayerRuntimeStats( runtimeStats );
    }

    public void ApplyPlayerModifier( PlayerStatType type, int count ) {
        PlayerRuntimeStats runtimeStats = new();
        switch ( type ) {
            case PlayerStatType.DashCount: runtimeStats.DashCount += count; break;
        }
        RuntimeConfig.SetPlayerRuntimeStats( runtimeStats );
    }

    public void ApplyBulletModifier( BulletStatType type, float value ) {
        BulletRuntimeStats runtimeStats = new();
        switch ( type ) {
            case BulletStatType.Damage: runtimeStats.Damage += value; break;
            case BulletStatType.Speed: runtimeStats.Speed += value; break;
            case BulletStatType.Lifetime: runtimeStats.Lifetime += value; break;
            case BulletStatType.Radius: runtimeStats.Radius += value; break;
            case BulletStatType.Scale: runtimeStats.Scale += value; break;

        }
        RuntimeConfig.SetBulletRuntimeStats( runtimeStats );
    }

    public void ApplyBulletModifier( BulletStatType type, int count ) {
        BulletRuntimeStats runtimeStats = new();
        switch ( type ) {
            case BulletStatType.RicochetCount: runtimeStats.RicochetCount += count; break;
            case BulletStatType.PenetrationCount: runtimeStats.PenetrationCount += count; break;
        }
        RuntimeConfig.SetBulletRuntimeStats( runtimeStats );
    }

    public void ApplyBulletModifier( BulletStatType type, bool active ) {
        BulletRuntimeStats runtimeStats = new();
        switch ( type ) {
            case BulletStatType.RicochetCount: runtimeStats.HasSplash = active; break;
        }
        RuntimeConfig.SetBulletRuntimeStats( runtimeStats );
    }
}
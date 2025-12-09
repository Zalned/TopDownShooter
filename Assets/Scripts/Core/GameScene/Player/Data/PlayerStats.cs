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
        var stats = RuntimeConfig.Player;
        switch ( type ) {
            case PlayerStatType.MaxHealth: stats.MaxHealth += value; break;
            case PlayerStatType.Speed: stats.Speed += value; break;
            case PlayerStatType.DashLength: stats.DashLength += value; break;
            case PlayerStatType.ReloadTime: stats.ReloadTime += value; break;
        }
    }

    public void ApplyPlayerModifier( PlayerStatType type, int count ) {
        var stats = RuntimeConfig.Player;
        switch ( type ) {
            case PlayerStatType.DashCount: stats.DashCount += count; break;
        }
    }

    public void ApplyBulletModifier( BulletStatType type, float value ) {
        var stats = RuntimeConfig.Bullet;
        switch ( type ) {
            case BulletStatType.Damage: stats.Damage += value; break;
            case BulletStatType.Speed: stats.Speed += value; break;
            case BulletStatType.Lifetime: stats.Lifetime += value; break;
            case BulletStatType.Radius: stats.Radius += value; break;
            case BulletStatType.Scale: stats.Scale += value; break;
        }
    }

    public void ApplyBulletModifier( BulletStatType type, int count ) {
        var stats = RuntimeConfig.Bullet;
        switch ( type ) {
            case BulletStatType.RicochetCount: stats.RicochetCount += count; break;
            case BulletStatType.PenetrationCount: stats.PenetrationCount += count; break;
        }
    }

    public void ApplyBulletModifier( BulletStatType type, bool active ) {
        var stats = RuntimeConfig.Bullet;
        switch ( type ) {
            case BulletStatType.RicochetCount: stats.HasSplash = active; break;
        }
    }
}
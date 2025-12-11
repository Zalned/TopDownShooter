using UnityEngine;

public enum PlayerStatType {
    MaxHealth,
    Speed,
    DashTime,
    DashLength,
    DashCount,
    ReloadTime,
    ShotCooldown
}

public enum BulletStatType {
    Damage,
    Speed,
    Lifetime,
    Radius,
    BounceCount,
    PenetrationCount,
    SplashRadius,
    HasSplash,
    Scale
}

public class PlayerStats {
    public PlayerRuntimeConfig RuntimeConfig { get; private set; }

    public PlayerStats() {
        var basePlayerConfig = Resources.Load<BasePlayerConfigSO>( Defines.ConfigPaths.PLAYER );
        var baseBulletConfig = Resources.Load<BaseBulletConfigSO>( Defines.ConfigPaths.BULLET );
        RuntimeConfig = new PlayerRuntimeConfig( basePlayerConfig, baseBulletConfig );
    }

    public void ApplyPlayerModifier( PlayerStatType type, float value ) {
        var stats = RuntimeConfig.Player;
        switch ( type ) {
            case PlayerStatType.MaxHealth: stats.MaxHealth += value; break;
            case PlayerStatType.Speed: stats.Speed += value; break;
            case PlayerStatType.DashLength: stats.DashLength += value; break;
            case PlayerStatType.ReloadTime: stats.ReloadTime += value; break;
            case PlayerStatType.ShotCooldown: stats.ShotCooldown += value; break;
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
            case BulletStatType.Radius: stats.Radius += value; break;
            case BulletStatType.Scale: stats.Scale += value; break;
            case BulletStatType.SplashRadius: stats.SplashRadius += value; break;
        }
    }

    public void ApplyBulletModifier( BulletStatType type, int count ) {
        var stats = RuntimeConfig.Bullet;
        switch ( type ) {
            case BulletStatType.BounceCount: stats.BounceCount += count; break;
            case BulletStatType.PenetrationCount: stats.PenetrationCount += count; break;
        }
    }

    public void ApplyBulletModifier( BulletStatType type, bool active ) {
        var stats = RuntimeConfig.Bullet;
        switch ( type ) {
            case BulletStatType.HasSplash: stats.HasSplash = active; break;
        }
    }
}
using UnityEngine;

public enum PlayerStatType {
    MaxHealth, Speed, DashTime, DashLength,
}

public enum BulletStatType {
    Damage, Speed, Lifetime, Radius
}

public class PlayerStats {
    public PlayerRuntimeConfig RuntimeConfig { get; private set; }

    public PlayerStats() {
        var basePlayerConfig = Resources.Load<BasePlayerConfigSO>( "Configs/PlayerConfig" );
        var baseBulletConfig = Resources.Load<BaseBulletConfigSO>( "Configs/BulletConfig" );
        RuntimeConfig = new PlayerRuntimeConfig( basePlayerConfig, baseBulletConfig );
    }

    public void ApplyPlayerModifier( PlayerStatType type, float value ) {
        switch ( type ) {
            case PlayerStatType.MaxHealth: RuntimeConfig.Player.MaxHealth += value; break;
            case PlayerStatType.Speed: RuntimeConfig.Player.Speed += value; break;
            case PlayerStatType.DashTime: RuntimeConfig.Player.DashTime += value; break;
            case PlayerStatType.DashLength: RuntimeConfig.Player.DashLength += value; break;
        }
    }
    public void ApplyBulletModifier( BulletStatType type, float value ) {
        switch ( type ) {
            case BulletStatType.Damage: RuntimeConfig.Bullet.Damage += value; break;
            case BulletStatType.Speed: RuntimeConfig.Bullet.Speed += value; break;
            case BulletStatType.Lifetime: RuntimeConfig.Bullet.Lifetime += value; break;
            case BulletStatType.Radius: RuntimeConfig.Bullet.Radius += value; break;
        }
    }
}
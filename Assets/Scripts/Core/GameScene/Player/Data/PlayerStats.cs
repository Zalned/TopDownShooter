using UnityEngine;

public enum StatType {
    PlayerHealth, PlayerSpeed, 
    Damage, BulletSpeed,
}

public class PlayerStats {
    public PlayerRuntimeConfig RuntimeConfig { get; private set; }

    public PlayerStats() {
        var basePlayerConfig = Resources.Load<BasePlayerConfigSO>( "Configs/PlayerConfig" );
        var baseBulletConfig = Resources.Load<BaseBulletConfigSO>( "Configs/BulletConfig" );
        RuntimeConfig = new PlayerRuntimeConfig( basePlayerConfig, baseBulletConfig );
    }

    public void ApplyModifier( StatType type, float value ) {
        switch ( type ) {
            case StatType.PlayerHealth: RuntimeConfig.MaxHealth += value; break;
            case StatType.PlayerSpeed: RuntimeConfig.PlayerSpeed += value; break;

            case StatType.Damage: RuntimeConfig.Damage += value; break;
            case StatType.BulletSpeed: RuntimeConfig.BulletSpeed += value; break;
        }
    }
}
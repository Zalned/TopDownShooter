[System.Serializable]
public class PlayerRuntimeConfig {
    public PlayerRuntimeConfig( BasePlayerConfigSO basePlayerCfg, BaseBulletConfigSO baseBulletCfg ) {
        MaxHealth = basePlayerCfg.baseHealth;
        PlayerSpeed = basePlayerCfg.baseSpeed;

        PlayerDashLenght = basePlayerCfg.baseDashLenght;
        PlayerDashTime = basePlayerCfg.baseDashTime;
        PlayerDashCooldown = basePlayerCfg.baseDashCooldown;

        Damage = baseBulletCfg.baseDamage;
        BulletSpeed = baseBulletCfg.baseSpeed;
        Lifetime = baseBulletCfg.baseLifetime;
        BulletRadius = baseBulletCfg.baseRadius;
    }

    // Player
    public float MaxHealth;
    public float PlayerSpeed;
    public float PlayerDashLenght;
    public float PlayerDashTime;
    public float PlayerDashCooldown;

    // Bullet
    public float Damage;
    public float BulletSpeed;
    public float Lifetime;
    public float BulletRadius;
}

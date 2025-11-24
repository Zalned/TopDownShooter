[System.Serializable]
public class PlayerRuntimeConfig {
    public PlayerRuntimeConfig( BasePlayerConfigSO basePlayerCfg, BaseBulletConfigSO baseBulletCfg ) {
        MaxHealth = basePlayerCfg.baseHealth;
        PlayerSpeed = basePlayerCfg.baseSpeed;

        Damage = baseBulletCfg.baseDamage;
        BulletSpeed = baseBulletCfg.baseSpeed;
        Lifetime = baseBulletCfg.baseLifetime;
        BulletRadius = baseBulletCfg.baseRadius;
    }

    // Player
    public float MaxHealth;
    public float PlayerSpeed;

    // Bullet
    public float Damage;
    public float BulletSpeed;
    public float Lifetime;
    public float BulletRadius;
}

[System.Serializable]
public class PlayerRuntimeConfig {
    public PlayerRuntimeStats Player { get; private set; } = new();
    public BulletRuntimeStats Bullet { get; private set; } = new();

    public PlayerRuntimeConfig( BasePlayerConfigSO basePlayerCfg, BaseBulletConfigSO baseBulletCfg ) {
        Player.MaxHealth = basePlayerCfg.baseHealth;
        Player.Speed = basePlayerCfg.baseSpeed;

        Player.MaxAmmoCount = basePlayerCfg.maxAmmoCount;
        Player.ShotCooldown = basePlayerCfg.shotCooldown;
        Player.ReloadTime = basePlayerCfg.reloadTime;

        Player.DashLength = basePlayerCfg.baseDashLenght;
        Player.DashTime = basePlayerCfg.baseDashTime;
        Player.DashCooldown = basePlayerCfg.baseDashCooldown;

        Bullet.Damage = baseBulletCfg.baseDamage;
        Bullet.Speed = baseBulletCfg.baseSpeed;
        Bullet.Lifetime = baseBulletCfg.baseLifetime;
        Bullet.Radius = baseBulletCfg.baseRadius;
    }
}
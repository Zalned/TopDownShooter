using System.Collections.Generic;

public class PlayerRuntimeStats {
    public List<IPlayerMod> Mods = new();
    public float MaxHealth;
    public float Speed;
    public int MaxAmmoCount;
    public float ShotCooldown;
    public float ReloadTime;
    public int DashCount;
    public float DashLength;
    public float DashCooldown;

    public float DashTime;

    public PlayerRuntimeStats( BasePlayerConfigSO basePlayerCfg ) {
        MaxHealth = basePlayerCfg.baseHealth;
        Speed = basePlayerCfg.baseSpeed;

        MaxAmmoCount = basePlayerCfg.maxAmmoCount;
        ShotCooldown = basePlayerCfg.shotCooldown;
        ReloadTime = basePlayerCfg.reloadTime;

        DashCount = basePlayerCfg.baseDashCount;
        DashLength = basePlayerCfg.baseDashLenght;
        DashCooldown = basePlayerCfg.baseDashCooldown;

        DashTime = basePlayerCfg.baseDashTime;
    }
}
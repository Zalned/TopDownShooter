using System.Collections.Generic;
using System.Text;

public class PlayerRuntimeStats {
    public List<IPlayerMod> Mods = new();
    public Stat MaxHealth;
    public Stat Speed;
    public Stat MaxAmmoCount;
    public Stat AttackSpeed;
    public Stat ReloadTime;
    public Stat DashCount;
    public Stat DashLength;
    public Stat DashReloadTime;
    public Stat DashTime;

    private BasePlayerConfigSO _basePlayerCfg;

    public PlayerRuntimeStats( BasePlayerConfigSO basePlayerCfg ) {
        _basePlayerCfg = basePlayerCfg;
        Reset();
    }

    public void Reset() {
        Mods = new();

        MaxHealth.Base = _basePlayerCfg.baseHealth;
        Speed.Base = _basePlayerCfg.baseSpeed;

        MaxAmmoCount.Base = _basePlayerCfg.maxAmmoCount;
        AttackSpeed.Base = _basePlayerCfg.shotCooldown;
        ReloadTime.Base = _basePlayerCfg.reloadTime;

        DashCount.Base = _basePlayerCfg.baseDashCount;
        DashLength.Base = _basePlayerCfg.baseDashLenght;
        DashReloadTime.Base = _basePlayerCfg.DashReloadTime;

        DashTime.Base = _basePlayerCfg.baseDashTime;
    }

    public string GetAsText() {
        var sb = new StringBuilder();

        sb.AppendLine( $"PLAYER" );
        sb.AppendLine( $"MaxHealth: {MaxHealth.Value}" );
        sb.AppendLine( $"Speed: {Speed.Value}" );
        sb.AppendLine( $"MaxAmmoCount: {MaxAmmoCount.Value}" );
        sb.AppendLine( $"ShotCooldown: {AttackSpeed.Value}" );
        sb.AppendLine( $"ReloadTime: {ReloadTime.Value}" );
        sb.AppendLine( $"DashCount: {DashCount.Value}" );
        sb.AppendLine( $"DashLength: {DashLength.Value}" );
        sb.AppendLine( $"DashCooldown: {DashReloadTime.Value}" );
        sb.AppendLine( $"DashTime: {DashTime.Value}" );

        return sb.ToString();
    }
}
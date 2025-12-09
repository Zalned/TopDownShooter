using System.Collections.Generic;
using System.Text;

public class PlayerRuntimeStats {
    public List<IPlayerMod> Mods = new();
    public float MaxHealth;
    public float Speed;
    public int MaxAmmoCount;
    public float ShotCooldown;
    public float ReloadTime;
    public int DashCount;
    public float DashLength;
    public float DashReloadTime;
    public float DashTime;

    private BasePlayerConfigSO _basePlayerCfg;

    public PlayerRuntimeStats( BasePlayerConfigSO basePlayerCfg ) {
        _basePlayerCfg = basePlayerCfg;
        Reset();
    }

    public void Reset() {
        Mods = new();

        MaxHealth = _basePlayerCfg.baseHealth;
        Speed = _basePlayerCfg.baseSpeed;

        MaxAmmoCount = _basePlayerCfg.maxAmmoCount;
        ShotCooldown = _basePlayerCfg.shotCooldown;
        ReloadTime = _basePlayerCfg.reloadTime;

        DashCount = _basePlayerCfg.baseDashCount;
        DashLength = _basePlayerCfg.baseDashLenght;
        DashReloadTime = _basePlayerCfg.DashReloadTime;

        DashTime = _basePlayerCfg.baseDashTime;
    }

    public string GetAsText() {
        var sb = new StringBuilder();

        sb.AppendLine( $"MaxHealth: {MaxHealth}" );
        sb.AppendLine( $"Speed: {Speed}" );
        sb.AppendLine( $"MaxAmmoCount: {MaxAmmoCount}" );
        sb.AppendLine( $"ShotCooldown: {ShotCooldown}" );
        sb.AppendLine( $"ReloadTime: {ReloadTime}" );
        sb.AppendLine( $"DashCount: {DashCount}" );
        sb.AppendLine( $"DashLength: {DashLength}" );
        sb.AppendLine( $"DashCooldown: {DashReloadTime}" );
        sb.AppendLine( $"DashTime: {DashTime}" );

        return sb.ToString();
    }
}
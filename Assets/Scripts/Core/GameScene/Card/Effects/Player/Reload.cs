using System;

public class ReloadMod : BaseMod, IMod {
    public ReloadMod( StatType type, float value ) : base( type, value ) { }

    public void Install( PlayerStats stats, CardContext _ ) {
        var reload = stats.RuntimeConfig.Player.ReloadTime;

        switch ( type ) {
            case StatType.Additive: reload.Additive += value; break;
            case StatType.Percent: reload.Percent += value; break;
            default: throw new ArgumentOutOfRangeException();
        }
        stats.RuntimeConfig.Player.ReloadTime = reload;
    }
}
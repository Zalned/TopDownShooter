using System;

public class DashLengthMod : BaseMod, IMod {
    public DashLengthMod( StatType type, float value ) : base( type, value ) { }

    public void Install( PlayerStats stats, CardContext _ ) {
        var dashLength = stats.RuntimeConfig.Player.DashLength;

        switch ( type ) {
            case StatType.Additive: dashLength.Additive += value; break;
            case StatType.Percent: dashLength.Percent += value; break;
            default: throw new ArgumentOutOfRangeException();
        }
        stats.RuntimeConfig.Player.DashLength = dashLength;
    }
}
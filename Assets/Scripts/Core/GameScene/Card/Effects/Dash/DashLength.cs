using System;

public class DashLengthMod : BaseMod, IMod {
    public DashLengthMod( StatType type, float value ) : base( type, value ) { }

    public void Install( PlayerContext playerCtx, CardContext _ ) {
        var dashLength = playerCtx.Stats.RuntimeConfig.Player.DashLength;

        switch ( type ) {
            case StatType.Additive: dashLength.Additive += value; break;
            case StatType.Percent: dashLength.Percent += value; break;
            default: throw new ArgumentOutOfRangeException();
        }
        playerCtx.Stats.RuntimeConfig.Player.DashLength = dashLength;
    }
}
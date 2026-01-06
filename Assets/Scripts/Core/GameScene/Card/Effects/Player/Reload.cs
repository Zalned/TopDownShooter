using System;

public class ReloadMod : BaseMod, IMod {
    public ReloadMod( StatType type, float value ) : base( type, value ) { }

    public void Install( PlayerContext playerCtx, CardContext _ ) {
        var reload = playerCtx.Stats.RuntimeConfig.Player.ReloadTime;

        switch ( type ) {
            case StatType.Additive: reload.Additive += value; break;
            case StatType.Percent: reload.Percent += value; break;
            default: throw new ArgumentOutOfRangeException();
        }
        playerCtx.Stats.RuntimeConfig.Player.ReloadTime = reload;
    }
}
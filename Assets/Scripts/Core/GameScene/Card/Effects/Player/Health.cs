using System;

public class HealthMod : BaseMod, IMod {
    public HealthMod( StatType type, float value ) : base( type, value ) { }

    public void Install( PlayerContext playerCtx, CardContext _ ) {
        var health = playerCtx.Stats.RuntimeConfig.Player.MaxHealth;

        switch ( type ) {
            case StatType.Additive: health.Additive += value; break;
            case StatType.Percent: health.Percent += value; break;
            default: throw new ArgumentOutOfRangeException();
        }
        playerCtx.Stats.RuntimeConfig.Player.MaxHealth = health;
    }
}

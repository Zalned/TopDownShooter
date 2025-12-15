using System;

public class HealthMod : BaseMod, IMod {
    public HealthMod( StatType type, float value ) : base( type, value ) { }

    public void Install( PlayerStats stats, CardContext _ ) {
        var health = stats.RuntimeConfig.Player.MaxHealth;

        switch ( type ) {
            case StatType.Additive: health.Additive += value; break;
            case StatType.Percent: health.Percent += value; break;
            default: throw new ArgumentOutOfRangeException();
        }
        stats.RuntimeConfig.Player.MaxHealth = health;
    }
}

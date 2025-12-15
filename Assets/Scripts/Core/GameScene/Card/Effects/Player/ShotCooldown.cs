using System;

public class AttackSpeedMod : BaseMod, IMod {
    public AttackSpeedMod( StatType type, float value ) : base( type, value ) { }

    public void Install( PlayerStats stats, CardContext _ ) {
        var attackSpeed = stats.RuntimeConfig.Player.AttackSpeed;

        switch ( type ) {
            case StatType.Additive: attackSpeed.Additive += value; break;
            case StatType.Percent: attackSpeed.Percent += value; break;
            default: throw new ArgumentOutOfRangeException();
        }
        stats.RuntimeConfig.Player.AttackSpeed = attackSpeed;
    }
}
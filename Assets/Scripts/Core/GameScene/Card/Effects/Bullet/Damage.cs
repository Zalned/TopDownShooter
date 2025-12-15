using System;

public class DamageMod : BaseMod, IMod {
    public DamageMod( StatType type, float value ) : base( type, value ) { }

    public void Install( PlayerStats stats, CardContext _ ) {
        var damage = stats.RuntimeConfig.Bullet.Damage;

        switch ( type ) {
            case StatType.Additive: damage.Additive += value; break;
            case StatType.Percent: damage.Percent += value; break;
            default: throw new ArgumentOutOfRangeException();
        }
        stats.RuntimeConfig.Bullet.Damage = damage;
    }
}


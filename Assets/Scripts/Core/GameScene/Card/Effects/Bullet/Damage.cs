using System;

public class DamageMod : BaseMod, IMod {
    public DamageMod( StatType type, float value ) : base( type, value ) { }

    public void Install( PlayerContext playerCtx, CardContext _ ) {
        var damage = playerCtx.Stats.RuntimeConfig.Bullet.Damage;

        switch ( type ) {
            case StatType.Additive: damage.Additive += value; break;
            case StatType.Percent: damage.Percent += value; break;
            default: throw new ArgumentOutOfRangeException();
        }
        playerCtx.Stats.RuntimeConfig.Bullet.Damage = damage;
    }
}


using System;

public class ScaleMod : BaseMod, IMod {
    public ScaleMod( StatType type, float value ) : base( type, value ) { }

    public void Install( PlayerStats stats, CardContext _ ) {
        var scale = stats.RuntimeConfig.Bullet.Scale;

        switch ( type ) {
            case StatType.Additive: scale.Additive += value; break;
            case StatType.Percent: scale.Percent += value; break;
            default: throw new ArgumentOutOfRangeException();
        }
        stats.RuntimeConfig.Bullet.Scale = scale;
    }
}


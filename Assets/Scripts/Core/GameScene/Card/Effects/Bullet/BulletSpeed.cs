using System;

public class BulletSpeedMod : BaseMod, IMod {
    public BulletSpeedMod( StatType type, float value ) : base( type, value ) { }

    public void Install( PlayerStats stats, CardContext _ ) {
        var speed = stats.RuntimeConfig.Bullet.Speed;

        switch ( type ) {
            case StatType.Additive: speed.Additive += value; break;
            case StatType.Percent: speed.Percent += value; break;
            default: throw new ArgumentOutOfRangeException();
        }
        stats.RuntimeConfig.Bullet.Speed = speed;
    }
}

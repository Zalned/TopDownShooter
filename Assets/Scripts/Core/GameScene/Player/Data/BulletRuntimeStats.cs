using System.Collections.Generic;
using System.Text;

public class BulletRuntimeStats {
    public List<IBulletMod> Mods = new();
    public Stat Damage;
    public Stat Speed;
    public Stat Radius;
    public Stat PenetrationCount;
    public Stat BounceCount;
    public Stat Scale;
    public Stat SplashRadius;
    public Stat LifeSteal;

    private BaseBulletConfigSO _baseBulletCfg;

    public BulletRuntimeStats( BaseBulletConfigSO baseBulletCfg ) {
        _baseBulletCfg = baseBulletCfg;
        Reset();
    }

    public void Reset() {
        Mods = new();

        Damage.Base = _baseBulletCfg.damage;
        Speed.Base = _baseBulletCfg.speed;
        Radius.Base = _baseBulletCfg.radius;
        Scale.Base = _baseBulletCfg.scale;
        PenetrationCount.Base = _baseBulletCfg.penetrationCount;
        BounceCount.Base = _baseBulletCfg.bounceCount;
        SplashRadius.Base = _baseBulletCfg.splashRadius;
    }

    public string GetAsText() {
        var sb = new StringBuilder();

        sb.AppendLine( $"BULLET" );
        sb.AppendLine( $"Damage: {Damage.Value}" );
        sb.AppendLine( $"Speed: {Speed.Value}" );
        sb.AppendLine( $"Radius: {Radius.Value}" );
        sb.AppendLine( $"Scale: {Scale.Value}" );
        sb.AppendLine( $"PenetrationCount: {PenetrationCount.Value}" );
        sb.AppendLine( $"RicochetCount: {BounceCount.Value}" );
        sb.AppendLine( $"SplashRadius: {SplashRadius.Value}" );

        return sb.ToString();
    }
}
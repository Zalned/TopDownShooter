using System.Collections.Generic;
using System.Text;

public class BulletRuntimeStats {
    public List<IBulletMod> Mods = new();
    public float Damage;
    public float Speed;
    public float Radius;
    public int PenetrationCount;
    public int BounceCount;
    public float Scale;
    public float SplashRadius;
    public bool HasSplash;
    public float LifeSteal;

    private BaseBulletConfigSO _baseBulletCfg;

    public BulletRuntimeStats( BaseBulletConfigSO baseBulletCfg ) {
        _baseBulletCfg = baseBulletCfg;
        Reset();
    }

    public void Reset() {
        Mods = new();

        Damage = _baseBulletCfg.damage;
        Speed = _baseBulletCfg.speed;
        Radius = _baseBulletCfg.radius;
        Scale = _baseBulletCfg.scale;
        PenetrationCount = _baseBulletCfg.penetrationCount;
        BounceCount = _baseBulletCfg.bounceCount;
        HasSplash = _baseBulletCfg.hasSplash;
        SplashRadius = _baseBulletCfg.splashRadius;
        LifeSteal = _baseBulletCfg.leech;
    }

    public string GetAsText() {
        var sb = new StringBuilder();

        sb.AppendLine( $"Damage: {Damage}" );
        sb.AppendLine( $"Speed: {Speed}" );
        sb.AppendLine( $"Radius: {Radius}" );
        sb.AppendLine( $"Scale: {Scale}" );
        sb.AppendLine( $"PenetrationCount: {PenetrationCount}" );
        sb.AppendLine( $"RicochetCount: {BounceCount}" );
        sb.AppendLine( $"SplashRadius: {SplashRadius}" );
        sb.AppendLine( $"HasSplash: {HasSplash}" );

        return sb.ToString();
    }
}
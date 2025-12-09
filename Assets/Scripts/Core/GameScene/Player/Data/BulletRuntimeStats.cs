using System.Collections.Generic;
using System.Text;

public class BulletRuntimeStats {
    public List<IBulletMod> Mods = new();
    public float Damage;
    public float Speed;
    public float Lifetime;
    public float Radius;
    public int PenetrationCount;
    public int RicochetCount;
    public float Scale;
    public bool HasSplash;

    private BaseBulletConfigSO _baseBulletCfg;

    public BulletRuntimeStats( BaseBulletConfigSO baseBulletCfg ) {
        _baseBulletCfg = baseBulletCfg;
        Reset();
    }

    public void Reset() {
        Mods = new();

        Damage = _baseBulletCfg.damage;
        Speed = _baseBulletCfg.speed;
        Lifetime = _baseBulletCfg.lifetime;
        Radius = _baseBulletCfg.radius;
        Scale = _baseBulletCfg.scale;

        PenetrationCount = _baseBulletCfg.penetrationCount;
        RicochetCount = _baseBulletCfg.bounceCount;
    }

    public string GetAsText() {
        var sb = new StringBuilder();

        sb.AppendLine( $"Damage: {Damage}" );
        sb.AppendLine( $"Speed: {Speed}" );
        sb.AppendLine( $"Lifetime: {Lifetime}" );
        sb.AppendLine( $"Radius: {Radius}" );
        sb.AppendLine( $"Scale: {Scale}" );
        sb.AppendLine( $"PenetrationCount: {PenetrationCount}" );
        sb.AppendLine( $"RicochetCount: {RicochetCount}" );
        sb.AppendLine( $"HasSplash: {HasSplash}" );

        return sb.ToString();
    }
}
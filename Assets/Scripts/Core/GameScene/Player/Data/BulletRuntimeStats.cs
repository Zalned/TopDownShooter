using System.Collections.Generic;

public struct BulletRuntimeStats {
    public List<IBulletMod> Mods;
    public float Damage;
    public float Speed;
    public float Lifetime;
    public float Radius;
    public int PenetrationCount;
    public int RicochetCount;
    public float Scale;
    public bool HasSplash;

    public void SetStats( BaseBulletConfigSO baseBulletCfg ) {
        Damage = baseBulletCfg.damage;
        Speed = baseBulletCfg.speed;
        Lifetime = baseBulletCfg.lifetime;
        Radius = baseBulletCfg.radius;
        Scale = baseBulletCfg.scale; 

        PenetrationCount = baseBulletCfg.penetrationCount;
        RicochetCount = baseBulletCfg.bounceCount;
    }
}
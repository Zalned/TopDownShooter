using System;

public class ExplosionMod : BaseMod, IBulletMod {
    private CardContext _context;
    private BulletRuntimeStats _stats;

    public ExplosionMod( StatType type, float value ) : base( type, value ) { }

    public void Install( PlayerStats stats, CardContext ctx ) {
        _context = ctx;
        _stats = stats.RuntimeConfig.Bullet;

        var splash = _stats.SplashRadius;

        switch ( type ) {
            case StatType.Additive: splash.Additive += value; break;
            case StatType.Percent: splash.Percent += value; break;
            default: throw new ArgumentOutOfRangeException();
        }
        stats.RuntimeConfig.Bullet.SplashRadius = splash;
    }

    public void OnHit( BulletHitContext hitContext ) {
        var point = hitContext.Hit.point;
        _context.ExplosionService.Explode( point, _stats.SplashRadius.Value, _stats.Damage.Value );
    }
}

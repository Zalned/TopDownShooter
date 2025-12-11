public class ExplosionMod : IBulletMod {
    private CardContext _context;
    private BulletRuntimeStats _stats;
    private readonly float _mult;

    public ExplosionMod( float splashRaidus ) {
        _mult = splashRaidus;
    }

    public void Install( PlayerStats stats, CardContext ctx ) {
        _context = ctx;
        _stats = stats.RuntimeConfig.Bullet;
        stats.ApplyBulletModifier( BulletStatType.HasSplash, true );
        stats.ApplyBulletModifier( BulletStatType.SplashRadius, _mult );
    }

    public void OnHit( BulletHitContext hitContext ) {
        var point = hitContext.Hit.point;
        _context.ExplosionService.Explode( point, _stats.SplashRadius, _stats.Damage );
    }
}

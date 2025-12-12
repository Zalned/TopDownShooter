public class LifeStealMod : IBulletMod {
    private readonly float _mult;
    private CardContext _context;

    public LifeStealMod( float mult ) {
        _mult = mult;
    }

    public void Install( PlayerStats stats, CardContext _ ) {
        float damage = stats.RuntimeConfig.Bullet.Damage;
        stats.ApplyBulletModifier( BulletStatType.LifeSteal, damage * _mult );
    }

    public void OnHit( BulletHitContext hitContext ) {
        _context.LifeStealService.LifeSteal( hitContext.Damage );
    }
}
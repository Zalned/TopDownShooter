using System;

public class LifeStealMod : BaseMod, IBulletMod {
    private BulletRuntimeStats _stats;
    private CardContext _context;

    public LifeStealMod( StatType type, float value ) : base( type, value ) { }

    public void Install( PlayerStats stats, CardContext _ ) {
        var lifeStyle = stats.RuntimeConfig.Bullet.LifeSteal;

        switch ( type ) {
            case StatType.Additive: lifeStyle.Additive += value; break;
            case StatType.Percent: lifeStyle.Percent += value; break;
            default: throw new ArgumentOutOfRangeException();
        }
        stats.RuntimeConfig.Bullet.LifeSteal = lifeStyle;
    }

    public void OnHit( BulletHitContext hitContext ) {
        _context.LifeStealService.LifeSteal( hitContext.Damage * _stats.LifeSteal.Value );
    }
}
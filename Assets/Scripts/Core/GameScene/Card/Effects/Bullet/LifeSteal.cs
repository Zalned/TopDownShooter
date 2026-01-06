using System;

public class LifeStealMod : BaseMod, IBulletMod {
    private BulletRuntimeStats _stats;
    private CardContext _context;
    private PlayerContext _playerCtx;

    public LifeStealMod( StatType type, float value ) : base( type, value ) { }

    public void Install( PlayerContext playerCtx, CardContext _ ) {
        _playerCtx = playerCtx;
        var lifeStyle = _playerCtx.Stats.RuntimeConfig.Bullet.LifeSteal;

        switch ( type ) {
            case StatType.Additive: lifeStyle.Additive += value; break;
            case StatType.Percent: lifeStyle.Percent += value; break;
            default: throw new ArgumentOutOfRangeException();
        }
        _playerCtx.Stats.RuntimeConfig.Bullet.LifeSteal = lifeStyle;
    }

    public void OnHit( BulletHitContext hitContext ) {
        _context.LifeStealService.LifeSteal( 
            _playerCtx.AddHealth, hitContext.Damage * _stats.LifeSteal.Value );
    }
}
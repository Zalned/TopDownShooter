public class DamageMod : IMod {
    private readonly float _mult;

    public DamageMod( float mult ) {
        _mult = mult;
    }

    public void Install( PlayerStats stats, CardContext _ ) {
        float damage = stats.RuntimeConfig.Bullet.Damage;
        stats.ApplyBulletModifier( BulletStatType.Damage, damage * _mult );
    }
}


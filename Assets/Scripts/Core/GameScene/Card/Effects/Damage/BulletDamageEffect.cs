public class BulletDamageEffect : IEffect {
    private readonly float _mult;
    private PlayerStats _stats;

    public string Id => "bullet_damage";

    public BulletDamageEffect( float mult ) {
        _mult = mult;
    }

    public void Install( PlayerStats stats, CardContext ctx ) {
        _stats = stats;
        float damage = _stats.RuntimeConfig.Bullet.Damage;
        _stats.ApplyBulletModifier( BulletStatType.Damage, +(damage * _mult) );
    }

    public void Uninstall() { }
}

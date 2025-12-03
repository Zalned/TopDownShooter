public class RicochetEffect : IEffect {
    private readonly float _count;
    private PlayerStats _stats;

    public RicochetEffect( int count ) {
        _count = count;
    }

    public void Install( PlayerStats stats, CardContext ctx ) {
        _stats.ApplyBulletModifier( BulletStatType.RicochetCount, _count );
    }

    public void Uninstall() { }
}
public class PenetrationEffect : IEffect {
    private readonly float _count;
    private PlayerStats _stats;

    public PenetrationEffect( int count ) {
        _count = count;
    }

    public void Install( PlayerStats stats, CardContext ctx ) {
        _stats.ApplyBulletModifier( BulletStatType.PenetrationCount, _count );
    }

    public void Uninstall() { }
}
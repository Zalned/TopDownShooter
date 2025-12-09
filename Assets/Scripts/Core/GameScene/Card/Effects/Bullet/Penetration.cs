public class PenetrationMod : IMod {
    private readonly float _count;

    public PenetrationMod( int count ) {
        _count = count;
    }

    public void Install( PlayerStats stats, CardContext ctx ) {
        stats.ApplyBulletModifier( BulletStatType.PenetrationCount, _count );
    }

    public void Uninstall() { }
}
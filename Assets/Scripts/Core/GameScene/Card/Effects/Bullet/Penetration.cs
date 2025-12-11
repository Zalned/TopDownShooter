public class PenetrationMod : IMod {
    private readonly int _count;

    public PenetrationMod( int count ) {
        _count = count;
    }

    public void Install( PlayerStats stats, CardContext _ ) {
        stats.ApplyBulletModifier( BulletStatType.PenetrationCount, _count );
    }
}
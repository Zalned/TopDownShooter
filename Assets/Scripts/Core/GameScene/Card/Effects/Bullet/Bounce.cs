public class BounceMod : IMod {
    private readonly int _count;

    public BounceMod( int count ) {
        _count = count;
    }

    public void Install( PlayerStats stats, CardContext _ ) {
        stats.ApplyBulletModifier( BulletStatType.BounceCount, _count );
    }
}
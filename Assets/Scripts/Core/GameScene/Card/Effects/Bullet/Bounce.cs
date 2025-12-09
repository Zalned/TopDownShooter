public class BounceMod : IMod {
    private readonly float _count;
    private PlayerStats _stats;

    public BounceMod( int count ) {
        _count = count;
    }

    public void Install( PlayerStats stats, CardContext ctx ) {
        _stats.ApplyBulletModifier( BulletStatType.RicochetCount, _count );
    }
}
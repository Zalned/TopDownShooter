public class DashCountMod : IMod {
    private readonly int _count;

    public DashCountMod( int count) {
        _count = count;
    }

    public void Install( PlayerStats stats, CardContext ctx ) {
        stats.ApplyPlayerModifier( PlayerStatType.DashCount, _count );
    }
}
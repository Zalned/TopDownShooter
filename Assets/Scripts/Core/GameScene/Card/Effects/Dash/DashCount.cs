public class DashCountMod : IMod {
    private readonly int _value;

    public DashCountMod( int value ) { _value = value; }

    public void Install( PlayerStats stats, CardContext _ ) {
        stats.RuntimeConfig.Player.DashCount.Additive += _value;
    }
}
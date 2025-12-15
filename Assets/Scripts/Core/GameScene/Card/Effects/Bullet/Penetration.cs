public class PenetrationMod : IMod {
    private int _value;

    public PenetrationMod( int value ) { _value = value; }

    public void Install( PlayerStats stats, CardContext _ ) {
        stats.RuntimeConfig.Bullet.PenetrationCount.Additive += _value;
    }
}
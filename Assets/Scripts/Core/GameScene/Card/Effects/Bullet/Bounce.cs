public class BounceMod : IMod {
    private readonly int _value;

    public BounceMod( int value ) { _value = value; }

    public void Install( PlayerStats stats, CardContext _ ) {
        stats.RuntimeConfig.Bullet.BounceCount.Additive += _value;
    }
}
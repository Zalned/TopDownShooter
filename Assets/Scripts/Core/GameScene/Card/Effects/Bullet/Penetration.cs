public class PenetrationMod : IMod {
    private int _value;

    public PenetrationMod( int value ) { _value = value; }

    public void Install( PlayerContext playerCtx, CardContext _ ) {
        playerCtx.Stats.RuntimeConfig.Bullet.PenetrationCount.Additive += _value;
    }
}
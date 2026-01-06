public class BounceMod : IMod {
    private readonly int _value;

    public BounceMod( int value ) { _value = value; }

    public void Install( PlayerContext playerCtx, CardContext _ ) {
        playerCtx.Stats.RuntimeConfig.Bullet.BounceCount.Additive += _value;
    }
}
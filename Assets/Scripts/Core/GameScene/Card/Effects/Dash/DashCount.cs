public class DashCountMod : IMod {
    private readonly int _value;

    public DashCountMod( int value ) { _value = value; }

    public void Install( PlayerContext playerCtx, CardContext _ ) {
        playerCtx.Stats.RuntimeConfig.Player.DashCount.Additive += _value;
    }
}
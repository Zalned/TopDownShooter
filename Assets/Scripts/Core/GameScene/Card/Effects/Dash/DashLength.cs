public class DashLengthMod : IMod {
    private readonly float _mult;

    public DashLengthMod( float multiplier ) {
        _mult = multiplier;
    }

    public void Install( PlayerStats stats, CardContext _ ) {
        var dashLength = stats.RuntimeConfig.Player.DashLength;
        stats.ApplyPlayerModifier( PlayerStatType.DashLength, dashLength * _mult );
    }
}
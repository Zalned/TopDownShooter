public class ReloadMod : IMod {
    private readonly float _time;

    public ReloadMod( float time ) {
        _time = time;
    }

    public void Install( PlayerStats stats, CardContext _ ) {
        stats.ApplyPlayerModifier( PlayerStatType.ReloadTime, _time );
    }
}
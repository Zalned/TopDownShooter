public class ReloadMod : IMod {
    private readonly float _mult;

    public ReloadMod( float mult ) {
        _mult = mult;
    }

    public void Install( PlayerStats stats, CardContext _ ) {
        float reloadSpeed = stats.RuntimeConfig.Player.ReloadTime;
        stats.ApplyPlayerModifier( PlayerStatType.ReloadTime, -(reloadSpeed * _mult) );
    }
}
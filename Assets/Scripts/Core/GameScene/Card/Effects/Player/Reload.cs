public class ReloadMod : IMod {
    private readonly float _mult;
    private PlayerStats _stats;

    public ReloadMod( float mult ) {
        _mult = mult;
    }

    public void Install( PlayerStats stats, CardContext ctx ) {
        _stats = stats;
        float reloadSpeed = _stats.RuntimeConfig.Player.ReloadTime;
        _stats.ApplyPlayerModifier( PlayerStatType.ReloadTime, -(reloadSpeed * _mult) );
    }
}
public class ReloadEffect : IEffect {
    private readonly float _mult;
    private PlayerStats _stats;

    public ReloadEffect( float mult ) {
        _mult = mult;
    }

    public void Install( PlayerStats stats, CardContext ctx ) {
        _stats = stats;
        float reloadSpeed = _stats.RuntimeConfig.Player.ReloadTime;
        _stats.ApplyPlayerModifier( PlayerStatType.ReloadTime, -(reloadSpeed * _mult) );
    }

    public void Uninstall() { }
}

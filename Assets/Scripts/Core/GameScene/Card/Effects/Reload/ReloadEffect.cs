public class ReloadEffect : IEffect {
    private readonly float _mult;
    private PlayerStats _stats;

    public string Id => "faster_reload";

    public ReloadEffect( float mult ) {
        _mult = mult;
    }

    public void Install( PlayerStats player, CardContext ctx ) {
        _stats = player;
        float reloadSpeed = _stats.RuntimeConfig.Player.ReloadTime;
        _stats.ApplyPlayerModifier( PlayerStatType.ReloadTime, -(reloadSpeed * _mult) );
    }

    public void Uninstall() { }
}

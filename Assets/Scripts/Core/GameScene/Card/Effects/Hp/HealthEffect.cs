public class HealthEffect : IEffect {
    private readonly float _mult;
    private PlayerStats _stats;

    public HealthEffect( float mult ) {
        _mult = mult;
    }

    public void Install( PlayerStats stats, CardContext ctx ) {
        _stats = stats;
        float maxHealth = _stats.RuntimeConfig.Player.MaxHealth;
        _stats.ApplyPlayerModifier( PlayerStatType.ReloadTime, maxHealth * _mult );
    }

    public void Uninstall() { }
}

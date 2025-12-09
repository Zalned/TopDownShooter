public class HealthMod : IMod {
    private readonly float _mult;

    public HealthMod( float mult ) {
        _mult = mult;
    }

    public void Install( PlayerStats stats, CardContext ctx ) {
        float maxHealth = stats.RuntimeConfig.Player.MaxHealth;
        stats.ApplyPlayerModifier( PlayerStatType.MaxHealth, maxHealth * _mult );
    }
}

public class AttackSpeedMod : IMod {
    private readonly float _mult;

    public AttackSpeedMod( float mult ) {
        _mult = mult;
    }

    public void Install( PlayerStats stats, CardContext _ ) {
        float shotCd = stats.RuntimeConfig.Player.ShotCooldown;
        stats.ApplyPlayerModifier( PlayerStatType.ShotCooldown, -(shotCd * _mult) );
    }
}
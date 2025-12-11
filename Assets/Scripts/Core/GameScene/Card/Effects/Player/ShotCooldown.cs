public class ShotCdMod : IMod {
    private readonly float _mult;

    public ShotCdMod( float mult ) {
        _mult = mult;
    }

    public void Install( PlayerStats stats, CardContext _ ) {
        float shotCd = stats.RuntimeConfig.Player.ShotCooldown;
        stats.ApplyPlayerModifier( PlayerStatType.ShotCooldown, -(shotCd * _mult) );
    }
}
using UnityEngine;

[CreateAssetMenu( fileName = "HealthMod", menuName = "PlayerMods/Health" )]
public class HealthSO : ModSO {
    [Range( 0f, 1f )]
    public float Multiplier = 0f;

    public override IMod CreateRuntime() {
        return new HealthMod( Multiplier );
    }
}

public class HealthMod : IMod {
    private readonly float _mult;
    private PlayerStats _stats;

    public HealthMod( float mult ) {
        _mult = mult;
    }

    public void Install( PlayerStats stats, CardContext ctx ) {
        _stats = stats;
        float maxHealth = _stats.RuntimeConfig.Player.MaxHealth;
        _stats.ApplyPlayerModifier( PlayerStatType.ReloadTime, maxHealth * _mult );
    }
}

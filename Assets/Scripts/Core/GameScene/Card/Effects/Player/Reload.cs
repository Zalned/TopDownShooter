using UnityEngine;

[CreateAssetMenu( fileName = "ReloadMod", menuName = "PlayerMods/Reload" )]
public class ReloadSO : ModSO {
    [Range( 0f, 1f )]
    public float Multiplier = 1f;

    public override ISimpleMod CreateRuntime() {
        return new ReloadMod( Multiplier );
    }
}

public class ReloadMod : ISimpleMod {
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
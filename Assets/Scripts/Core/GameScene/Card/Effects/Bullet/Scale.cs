using UnityEngine;

[CreateAssetMenu( fileName = "DamageMod", menuName = "BulletMods/Damage" )]
public class ScaleSO : ModSO {
    public float Multiplier = 0f;

    public override ISimpleMod CreateRuntime() {
        return new ScaleMod( Multiplier );
    }
}

public class ScaleMod : ISimpleMod {
    private readonly float _mult;
    private PlayerStats _stats;

    public ScaleMod( float mult ) {
        _mult = mult;
    }

    public void Install( PlayerStats stats, CardContext _ ) {
        _stats = stats;
        float scale = _stats.RuntimeConfig.Bullet.Scale;
        _stats.ApplyBulletModifier( BulletStatType.Scale, scale * _mult );
    }
}


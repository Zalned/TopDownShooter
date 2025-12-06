using UnityEngine;

[CreateAssetMenu( fileName = "DamageMod", menuName = "BulletMods/Damage" )]
public class DamageSO : ModSO {
    public float Multiplier = 0f;

    public override ISimpleMod CreateRuntime() {
        return new DamageMod( Multiplier );
    }
}

public class DamageMod : ISimpleMod {
    private readonly float _mult;
    private PlayerStats _stats;

    public DamageMod( float mult ) {
        _mult = mult;
    }

    public void Install( PlayerStats stats, CardContext _ ) {
        _stats = stats;
        float damage = _stats.RuntimeConfig.Bullet.Damage;
        _stats.ApplyBulletModifier( BulletStatType.Damage, damage * _mult );
    }
}


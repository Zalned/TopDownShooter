using UnityEngine;

[CreateAssetMenu( menuName = "Effects/BulletDamage" )]
public class BulletDamageSO : EffectSO {
    [Range( 0f, 1f )]
    public float Multiplier = 0f;

    public override IEffect CreateRuntime() {
        return new BulletDamageEffect( Multiplier );
    }
}

using UnityEngine;

[CreateAssetMenu( menuName = "Effects/Explosion" )]
public class ExplosionOnHitSO : EffectSO {
    public override IEffect CreateRuntime() {
        return new ExplosionOnHitEffect();
    }
}

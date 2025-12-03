using UnityEngine;

[CreateAssetMenu( menuName = "Effects/Health" )]
public class HealthSO : EffectSO {
    [Range( 0f, 1f )]
    public float Multiplier = 0f;

    public override IEffect CreateRuntime() {
        return new HealthEffect( Multiplier );
    }
}
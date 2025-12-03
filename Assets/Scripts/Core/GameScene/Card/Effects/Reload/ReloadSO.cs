using UnityEngine;

[CreateAssetMenu( menuName = "Effects/Reload" )]
public class ReloadSO : EffectSO {
    [Range( 0f, 1f )]
    public float Multiplier = 1f;

    public override IEffect CreateRuntime() {
        return new ReloadEffect( Multiplier );
    }
}

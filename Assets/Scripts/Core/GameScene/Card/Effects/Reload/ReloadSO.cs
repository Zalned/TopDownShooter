using UnityEngine;

[CreateAssetMenu( menuName = "Effects/Reload" )]
public class ReloadSO : EffectSO {
    public float ReloadMultiplier = 1f;

    public override IEffect CreateRuntime() {
        return new ReloadEffect( ReloadMultiplier );
    }
}

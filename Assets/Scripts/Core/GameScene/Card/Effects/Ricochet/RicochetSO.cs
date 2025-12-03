using UnityEngine;

[CreateAssetMenu( menuName = "Effects/Ricochet" )]
public class RicochetSO : EffectSO {
    public int Count = 0;

    public override IEffect CreateRuntime() {
        return new RicochetEffect( Count );
    }
}

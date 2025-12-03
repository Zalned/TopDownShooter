using UnityEngine;

[CreateAssetMenu( menuName = "Effects/Penetration" )]
public class PenetrationSO : EffectSO {
    public int Count = 0;

    public override IEffect CreateRuntime() {
        return new RicochetEffect( Count );
    }
}

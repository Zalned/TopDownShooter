using UnityEngine;

[CreateAssetMenu( fileName = "EffectSO", menuName = "ScriptableObjects/EffectSO" )]
public abstract class EffectSO : ScriptableObject {
    public abstract IEffect CreateRuntime();
}

using UnityEngine;

[CreateAssetMenu( fileName = "ModSO", menuName = "ScriptableObjects/ModSO" )]
public abstract class ModSO : ScriptableObject {
    public abstract IMod CreateRuntime();
}

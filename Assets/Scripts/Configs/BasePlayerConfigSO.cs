using UnityEngine;

[CreateAssetMenu( fileName = "PlayerConfig", menuName = "Game/Player Config" )]
public class BasePlayerConfigSO : ScriptableObject {
    public float baseHealth = 100f;
    public float baseSpeed = 0f;
}
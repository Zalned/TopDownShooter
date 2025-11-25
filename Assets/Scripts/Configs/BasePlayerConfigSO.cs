using UnityEngine;

[CreateAssetMenu( fileName = "PlayerConfig", menuName = "Game/Player Config" )]
public class BasePlayerConfigSO : ScriptableObject {
    public float baseHealth = 100f;
    public float baseSpeed = 0f;

    public float baseDashLenght = 4f;
    public float baseDashTime = 0.5f;
    public float baseDashCooldown = 2f;
}
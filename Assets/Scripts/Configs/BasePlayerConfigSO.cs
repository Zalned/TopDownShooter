using UnityEngine;

[CreateAssetMenu( fileName = "PlayerConfig", menuName = "Game/Player Config" )]
public class BasePlayerConfigSO : ScriptableObject {
    public float baseHealth = 100f;
    public float baseSpeed = 0f;

    public int maxAmmoCount = 5;
    public float shotCooldown = 0.5f;
    public float reloadTime = 3f;

    public int baseDashCount = 1;
    public float baseDashLenght = 4f;
    public float DashReloadTime = 2f;

    // Не модифицирующиеся
    public float baseDashTime = 0.5f;
}
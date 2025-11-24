using UnityEngine;

[CreateAssetMenu( fileName = "BulletConfig", menuName = "Game/Bullet Config" )]
public class BaseBulletConfigSO : ScriptableObject {
    public float baseDamage = 25f;
    public float baseSpeed = 10f;
    public float baseLifetime = 5f;
    public float baseRadius = 0.1f;
}
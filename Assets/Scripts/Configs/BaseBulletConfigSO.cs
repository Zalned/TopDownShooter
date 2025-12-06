using UnityEngine;

[CreateAssetMenu( fileName = "BulletConfig", menuName = "Game/Bullet Config" )]
public class BaseBulletConfigSO : ScriptableObject {
    public float damage = 25f;
    public float speed = 10f;
    public float lifetime = 5f;
    public float radius = 0.1f;
    public int penetrationCount = 0;
    public int bounceCount = 0;
    public float scale = 0;
    public bool hasSplash = false;
}
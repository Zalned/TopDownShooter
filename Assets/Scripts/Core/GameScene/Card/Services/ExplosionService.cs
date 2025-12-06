using UnityEngine;

public class ExplosionService {
    private const float MinExplosionRadius = 1.5f;
    private const float ExplosionMultiple = 0.12f;

    public void Explode( Vector3 position, float radius, float damage ) {
        Debug.Log( $"Explosion at {position} with radius {radius} dealing {damage} damage." );
    }
}
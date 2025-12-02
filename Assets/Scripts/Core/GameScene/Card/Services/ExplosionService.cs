using UnityEngine;

public class ExplosionService {
    public void Explode( Vector3 position, float radius, float damage ) {
        Debug.Log( $"Explosion at {position} with radius {radius} dealing {damage} damage." );
    }
}
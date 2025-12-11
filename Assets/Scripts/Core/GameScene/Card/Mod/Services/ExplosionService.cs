using UnityEngine;

public class ExplosionService {
    private const float EXPLOSION_EFFECT_RADIUS_SCALER = 2;
    private LayerMask _hitMask = LayerMask.GetMask( Defines.Layers.PLAYER );

    private GameObject _explosionEffect = Resources.Load<GameObject>( Defines.EffectPaths.EXPLOSION );
    private float _radius;

    public void Explode( Vector3 point, float radius, float damage ) {
        _radius = radius;

        var hits = Physics.OverlapSphere( point, radius, _hitMask, QueryTriggerInteraction.Collide );

        foreach ( var hit in hits ) {
            Debug.Log( $"Explosion hit: {hit.transform.name}" );

            if ( hit.transform.CompareTag( Defines.Tags.PLAYER ) ) {
                Debug.Log( $"—фера задела игрока" );

                Vector3 direction = (hit.transform.position - point).normalized;
                Physics.Raycast( point, direction, out RaycastHit rayHit );

                if ( rayHit.transform.CompareTag( Defines.Tags.PLAYER ) ) {
                    Debug.Log( $"¬зрыв дошел до игрока" );
                    hit.transform.GetComponent<PlayerController>().TakeDamageClientRpc( damage );
                }
            }

        }

        CreateEffect( point );
    }

    private void CreateEffect( Vector3 point ) {
        var effect = Object.Instantiate( _explosionEffect );
        effect.transform.position = point;
        var particleSystem = effect.GetComponent<ParticleSystem>();
        ConfigureExplosionEffect( particleSystem );
    }

    private void ConfigureExplosionEffect( ParticleSystem ps ) {
        var main = ps.main;
        main.startSpeed = _radius * EXPLOSION_EFFECT_RADIUS_SCALER;
    }
}
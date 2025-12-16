using UnityEngine;

public class ExplosionService {
    private const float EXPLOSION_EFFECT_RADIUS_SCALER = 4.5f; // Scaler подобран вручную, нормального рассчета не сделал
    private LayerMask _playerMask = LayerMask.GetMask( Defines.Layers.PLAYER );

    private GameObject _explosionEffectPrefab = Resources.Load<GameObject>( Defines.EffectPaths.EXPLOSION );
    private GameObject _spawnedEffect;
    private float _radius;

    public void Explode( Vector3 point, float radius, float damage ) {
        _radius = radius;

        var hits = Physics.OverlapSphere( point, radius, _playerMask, QueryTriggerInteraction.Collide );

        foreach ( var hit in hits ) {
            var target = hit.transform.position;
            Vector3 direction = target - point;

            float distance = direction.magnitude;
            direction.Normalize();

            Physics.Raycast( point, direction, out RaycastHit hitInfo, distance );

            if ( hitInfo.transform.CompareTag( Defines.Tags.PLAYER ) ) {
                hit.transform.GetComponent<PlayerController>().TakeDamageClientRpc( damage );
            }
        }
        CreateEffect( point );
    }

    private void CreateEffect( Vector3 point ) {
        _spawnedEffect = NetworkSpawner.Instance.NetworkSpawnObject( _explosionEffectPrefab );
        _spawnedEffect.transform.position = point;

        var particleSystem = _spawnedEffect.GetComponent<ParticleSystem>();
        ConfigureExplosionEffect( particleSystem );

        var stopCallback = _spawnedEffect.GetComponent<ParticleStopCallback>();
        stopCallback.Initialize( OnDestroy );
    }

    private void ConfigureExplosionEffect( ParticleSystem ps ) {
        var main = ps.main;
        main.startSpeed = _radius * EXPLOSION_EFFECT_RADIUS_SCALER;
    }

    private void OnDestroy() {
        NetworkSpawner.Instance.NetworkDespawnObject( _spawnedEffect );
    }
}
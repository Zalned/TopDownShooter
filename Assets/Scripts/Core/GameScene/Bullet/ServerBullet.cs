using System;
using UnityEngine;

public struct ProjectileState {
    public Vector3 Position;
    public Vector3 PreviousPosition;
    public Vector3 Velocity;
    public bool IsActive;
}

public class ServerBullet : MonoBehaviour {
    public ulong OwnerID { get; private set; }

    private PlayerRuntimeConfig _config;
    private ProjectileState _state;
    private LayerMask _hitMask;

    private const float MAX_STEP_DISTANCE = 0.5f;
    private const float SPAWN_CHECK_RADIUS = 0.1f;

    private float _age = 0;

    public event Action<GameObject> DestroyRequest;

    public void Construct( PlayerRuntimeConfig config, ulong ownerID, Quaternion rotation ) {
        _config = config;
        OwnerID = ownerID;
        transform.rotation = rotation;

        _hitMask = LayerMask.GetMask(
        Defines.Layers.Environment,
        Defines.Layers.PlayerObject,
        Defines.Layers.Bullet );

        InitializeProjectileState();
        CheckOnSpawnInObject();
    }

    private void InitializeProjectileState() {
        _state = new();
        _state.Position = transform.position;
        _state.PreviousPosition = transform.position;
        _state.Velocity = transform.forward * _config.BulletSpeed;
        _state.IsActive = true;
    }

    public void Tick() {
        if ( !_state.IsActive ) return;
        TickProjectile( TickService.TickDeltaTime );
        IncreaseAge();
        HandleLifetime();
        SyncPosition();
    }

    private void CheckOnSpawnInObject() {
        Collider[] initialHits = Physics.OverlapSphere( _state.Position, SPAWN_CHECK_RADIUS,
            _hitMask, QueryTriggerInteraction.Collide );

        HandleHitOnSpawn( initialHits );
    }

    private void HandleHitOnSpawn( Collider[] initialHits ) {
        foreach ( var hit in initialHits ) {
            if ( hit.transform.CompareTag( Defines.Tags.Player ) ) {
                if ( hit.GetComponent<PlayerController>().OwnerClientId == OwnerID ) {
                    Debug.Log( "Hitting a self player on spawn. Continue." );
                    continue;
                } else {
                    HandleProjectileColliderHit( hit );
                }

            } else if ( hit.transform.CompareTag( Defines.Tags.Bullet ) ) {
                if ( hit.GetComponent<ServerBullet>().OwnerID == OwnerID ) {
                    Debug.Log( "Hitting a self bullet on spawn. Continue." );
                    continue;
                } else {
                    HandleProjectileColliderHit( hit );
                }
            }

            HandleProjectileColliderHit( hit );
            _state.IsActive = false;
            return;
        }
    }

    private void TickProjectile( float dt ) {
        _state.PreviousPosition = _state.Position;

        Vector3 targetPosition = _state.Position + _state.Velocity * dt;
        Vector3 movement = targetPosition - _state.Position;
        float remainingDistance = movement.magnitude;

        Vector3 direction = movement.normalized;

        // –азбиваем движение на шаги не больше MAX_STEP_DISTANCE
        while ( remainingDistance > 0f ) {
            float stepDistance = Mathf.Min( remainingDistance, MAX_STEP_DISTANCE );

            if ( Physics.SphereCast( _state.Position, _config.BulletRadius, direction,
                out RaycastHit hit, stepDistance, _hitMask, QueryTriggerInteraction.Collide ) ) {

                _state.Position = hit.point;
                HandleProjectileRaycastHit( hit );
                _state.IsActive = false;
                return;
            }

            _state.Position += direction * stepDistance;
            remainingDistance -= stepDistance;
        }
    }

    private void SyncPosition() {
        transform.position = _state.Position;
    }

    private void HandleProjectileRaycastHit( RaycastHit hit ) {
        if ( hit.transform == null ) { Debug.LogWarning( $"[{nameof( ServerBullet )}] hit is null." ); return; }
        HandleProjectileHit( hit.transform );
    }

    private void HandleProjectileColliderHit( Collider hit ) {
        if ( hit.transform == null ) { Debug.LogWarning( $"[{nameof( ServerBullet )}] hit is null." ); return; }
        HandleProjectileHit( hit.transform );
    }

    private void HandleProjectileHit( Transform hitTransform ) {
        if ( hitTransform.CompareTag( Defines.Tags.Player ) ) {
            HandlePlayerHit( hitTransform );
            Debug.Log( "Hitting a player." );
        } else if ( hitTransform.CompareTag( Defines.Tags.Bullet ) ) {
            HandleBulletHit( hitTransform );
            Debug.Log( "Hitting a bullet" );
        } else {
            HandleAnotherHit();
        }
    }

    private void HandlePlayerHit( Transform hitTransform ) {
        hitTransform.GetComponent<PlayerController>().TakeDamage( _config.Damage );
        HandleDestroy();
    }

    private void HandleBulletHit( Transform hitTransform ) {
        if ( hitTransform.TryGetComponent( out ServerBullet component ) ) {
            if ( component.OwnerID == OwnerID ) { return; }

            float damage = component.GetDamage();
            HandleHitFromAnotherBullet( damage );
            PlayHitAudio();
        }
    }

    private void HandleHitFromAnotherBullet( float damageFromAnotherBullet ) {
        if ( _config.Damage <= damageFromAnotherBullet ) {
            HandleDestroy();
        }
    }

    private void HandleAnotherHit() {
        HandleDestroy();
        PlayHitAudio();
    }

    private void PlayHitAudio() {
        NetworkAudioManager.Instance.PlayClipAtPointServerRpc( Sounds.Hit, transform.position );
    }

    public float GetDamage() {
        return _config.Damage;
    }

    private void HandleLifetime() {
        if ( _age >= _config.Lifetime ) {
            HandleDestroy();
        }
    }

    private void IncreaseAge() {
        _age += TickService.TickDeltaTime;
    }

    private void HandleDestroy() {
        _state.IsActive = false;
        DestroyRequest.Invoke( gameObject );
    }
}
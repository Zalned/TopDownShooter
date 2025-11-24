using Unity.Netcode;
using UnityEngine;
using System.Collections.Generic;
using Zenject;

public class BulletManager_Server : ITickable {
    private readonly Dictionary<int, ServerBullet> _spawnedBullets = new();
    private readonly List<GameObject> _bulletsToRemove = new();

    [Inject]
    public BulletManager_Server( ITickService tickService ) {
        tickService.Register( this );
    }

    private void SafeAddSpawnedBullet( GameObject bulletObj, ServerBullet bullet ) {
        int bulletId = bulletObj.GetInstanceID();

        if ( _spawnedBullets.ContainsKey( bulletId ) ) {
            Debug.LogWarning( $"Bullet {bulletId} already exists." ); return;
        }
        _spawnedBullets.Add( bulletId, bullet );
    }

    public ServerBullet CreateServerBullet( PlayerRuntimeConfig playerRuntimeConfig, Transform transform, ulong ownerID ) {
        var bulletPrefab = Resources.Load<GameObject>( Defines.ObjectPaths.BULLET_PREFAB );
        var bulletObject = Object.Instantiate( bulletPrefab, transform.position, Quaternion.identity );
        var bulletComponent = bulletObject.GetComponent<ServerBullet>();

        bulletObject.GetComponent<NetworkObject>().SpawnWithOwnership( ownerID, true );
        bulletComponent.DestroyRequest += HandleDestroyBullet;
        bulletComponent.Construct( playerRuntimeConfig, ownerID, transform.rotation );

        SafeAddSpawnedBullet( bulletObject, bulletComponent );
        return bulletComponent;
    }

    private void HandleDestroyBullet( GameObject bulletObj ) {
        _bulletsToRemove.Add( bulletObj );
        NetcodeHelper.Despawn( bulletObj, true );
    }

    public void Tick() {
        foreach ( var bullet in _spawnedBullets ) {
            bullet.Value.Tick();
        }

        if ( _bulletsToRemove.Count > 0 ) {
            foreach ( var bulletObj in _bulletsToRemove ) {
                _spawnedBullets.Remove( bulletObj.GetInstanceID() );
            }
            _bulletsToRemove.Clear();
        }
    }
}
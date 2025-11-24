using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : NetworkBehaviour {
    [SerializeField] private Transform _shootPoint;

    private BulletManager_Server _bulletManager;
    private PlayerStats _stats;
    private ulong _playerId;

    public void Initialize( BulletManager_Server bulletManager, PlayerStats stats, ulong playerId ) {
        _bulletManager = bulletManager;
        _stats = stats;
        _playerId = playerId;
    }

    public void OnShootInput( InputAction.CallbackContext context ) {
        if ( !IsOwner ) { return; }

        if ( context.performed ) {
            ShootServerRpc();
        }
    }

    [ServerRpc]
    private void ShootServerRpc() {
        _bulletManager.CreateServerBullet( _stats.RuntimeConfig, _shootPoint, _playerId );
        SpawnVisualBulletClientRpc();
    }

    [ClientRpc]
    private void SpawnVisualBulletClientRpc() {
        var clientBulletPrefab = Resources.Load<GameObject>( Defines.ObjectPaths.CLIENT_BULLET_PREFAB );
        var clientBulletObj = Instantiate( clientBulletPrefab );
        clientBulletObj.transform.position = transform.position;
        clientBulletObj.transform.rotation = transform.rotation;
        clientBulletObj.GetComponent<ClientBullet>().Construct( _stats.RuntimeConfig );
    }
}
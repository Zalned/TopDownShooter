using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : NetworkBehaviour {
    [SerializeField] private Transform _shootPoint;

    private BulletManager _bulletManager;
    private PlayerRuntimeConfig _config;
    private ulong _playerId;

    public event Action OnShoot;

    public void Initialize( BulletManager bulletManager, PlayerRuntimeConfig config, ulong playerId ) {
        _bulletManager = bulletManager;
        _config = config;
        _playerId = playerId;
    }

    public void OnShootInput( InputAction.CallbackContext context ) {
        if ( !IsOwner ) { return; }

        if ( context.performed ) {
            ShootServerRpc();
            OnShoot.Invoke();
        }
    }

    [ServerRpc]
    private void ShootServerRpc() {
        _bulletManager.CreateServerBullet( _config, _shootPoint, _playerId );
    }

    //[ClientRpc] MyTodo
    //private void SpawnVisualBulletClientRpc() {
    //    var clientBulletPrefab = Resources.Load<GameObject>( Defines.ObjectPaths.CLIENT_BULLET_PREFAB );
    //    var clientBulletObj = Instantiate( clientBulletPrefab );
    //    clientBulletObj.transform.position = _shootPoint.position;
    //    clientBulletObj.transform.rotation = transform.rotation;
    //    clientBulletObj.GetComponent<ClientBullet>().Construct( _stats.RuntimeConfig );
    //}
}
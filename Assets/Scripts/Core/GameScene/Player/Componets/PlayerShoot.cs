using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : NetworkBehaviour {
    [SerializeField] private Transform _shootPoint;

    private BulletManager _bulletManager;
    private PlayerRuntimeConfig _config;
    private ulong _playerId;

    private float _currentCooldown = 0;
    public event Action OnShoot;

    public void Initialize( BulletManager bulletManager, PlayerRuntimeConfig config, ulong playerId ) {
        _bulletManager = bulletManager;
        _config = config;
        _playerId = playerId;
    }

    public void OnShootInput( InputAction.CallbackContext context ) {
        if ( !IsOwner ) { return; }
        if ( !context.performed ) { return; }
        if ( _currentCooldown > 0 ) { return; }

        ShootServerRpc();
        OnShoot.Invoke();
    }

    public void Tick() {
        DecreaseCooldown();
    }

    private void DecreaseCooldown() {
        if(_currentCooldown <= 0) { return; }
        _currentCooldown -= TickService.TickDeltaTime;
    }

    [ServerRpc]
    private void ShootServerRpc() {
        _currentCooldown += _config.Player.ShotCooldown;
        _bulletManager.CreateServerBullet( _config, _shootPoint, _playerId );
    }
}
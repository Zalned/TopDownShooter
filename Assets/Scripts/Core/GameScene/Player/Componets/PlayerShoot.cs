using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : NetworkBehaviour {
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private TextMeshProUGUI _ammoCountText;

    private BulletManager _bulletManager;
    private PlayerRuntimeConfig _config;
    private ulong _playerId;

    private int _currentAmmo = 0;
    private float _currentCooldown = 0;
    private float _reloadCycleAccumulatedTime = 0;

    public event Action OnShot;

    public void Initialize( BulletManager bulletManager, PlayerRuntimeConfig config, ulong playerId ) {
        _bulletManager = bulletManager;
        _config = config;
        _playerId = playerId;

        _currentAmmo = _config.Player.MaxAmmoCount;
    }

    public void OnShootInput( InputAction.CallbackContext context ) {
        if ( !IsOwner ) { return; }
        if ( !context.performed ) { return; }
        if ( _currentCooldown > 0 ) { return; }
        if ( _currentAmmo <= 0 ) { return; }
        Shot();
    }

    private void Shot() {
        _currentCooldown += _config.Player.ShotCooldown;
        _currentAmmo--;
        _reloadCycleAccumulatedTime = 0;

        OnShot.Invoke();
        CreateBulletServerRpc();
        RefreshAmmoCountText();
    }

    [ServerRpc]
    private void CreateBulletServerRpc() {
        _bulletManager.CreateServerBullet( _config, _shootPoint, _playerId );
    }

    public void Tick() {
        DecreaseCooldown();
        ReloadCycle();
    }

    private void ReloadCycle() {
        if ( _currentAmmo >= _config.Player.MaxAmmoCount ) { return; }
        _reloadCycleAccumulatedTime += TickService.TickDeltaTime;

        if ( _reloadCycleAccumulatedTime >= _config.Player.ReloadTime ) {
            _currentAmmo = _config.Player.MaxAmmoCount;
            _reloadCycleAccumulatedTime = 0;
            RefreshAmmoCountText();
        }
    }

    private void DecreaseCooldown() {
        if ( _currentCooldown <= 0 ) { return; }
        _currentCooldown -= TickService.TickDeltaTime;
    }

    private void RefreshAmmoCountText() {
        _ammoCountText.text = _currentAmmo.ToString();
    }
}
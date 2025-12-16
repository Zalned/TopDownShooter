using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerShoot : NetworkBehaviour {
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private TextMeshProUGUI _ammoCountText;
    [SerializeField] private Slider _reloadSlider;

    private BulletManager _bulletManager;
    private PlayerRuntimeStats _playerCfg;
    private BulletRuntimeStats _bulletCfg;
    private ulong _playerId;
    private CardContext _cardContext;

    private int _currentAmmo = 0;
    private float _currentCooldown = 0;
    private float _reloadCycleAccumulatedTime = 0;

    private float _refreshReloadSliderTime = 0;
    private float _timeForRefreshReloadSliderTime = 0.1f;

    public event Action OnShot;

    public void Initialize(
        BulletManager bulletManager,
        PlayerRuntimeConfig config,
        ulong playerId,
        CardContext cardCtx ) {

        _bulletManager = bulletManager;
        _playerCfg = config.Player;
        _bulletCfg = config.Bullet;
        _playerId = playerId;
        _cardContext = cardCtx;

        _currentAmmo = (int)_playerCfg.MaxAmmoCount.Value;
        _reloadSlider.maxValue = _playerCfg.ReloadTime.Value;

        RefreshAmmoCountText();
        ResetReloadSlider();
    }

    public void OnShootInput( InputAction.CallbackContext context ) {
        if ( !IsOwner ) { return; }
        if ( !context.performed ) { return; }
        if ( _currentCooldown > 0 ) { return; }
        if ( _currentAmmo <= 0 ) { return; }
        Shot();
    }

    private void Shot() {
        _currentCooldown += _playerCfg.AttackSpeed.Value;
        _currentAmmo--;
        _reloadCycleAccumulatedTime = 0;

        OnShot.Invoke();
        CreateBulletServerRpc();
        RefreshAmmoCountText();
    }

    [ServerRpc]
    private void CreateBulletServerRpc() {
        _bulletManager.CreateServerBullet(
            _bulletCfg, _shootPoint, _playerId, _cardContext );
    }

    public void Tick() {
        DecreaseCooldown();
        ReloadCycle();
    }

    private void ReloadCycle() {
        if ( _currentAmmo >= _playerCfg.MaxAmmoCount.Value ) { return; }

        _reloadCycleAccumulatedTime += TickService.TickDeltaTime;
        _refreshReloadSliderTime += TickService.TickDeltaTime;

        if ( _reloadCycleAccumulatedTime >= _playerCfg.ReloadTime.Value ) {
            _currentAmmo = (int)_playerCfg.MaxAmmoCount.Value;
            _reloadCycleAccumulatedTime = 0;

            RefreshAmmoCountText();
            ResetReloadSlider();
        }

        if ( _refreshReloadSliderTime >= _timeForRefreshReloadSliderTime ) {
            RefreshReloadSlider();
            _refreshReloadSliderTime = 0;
        }
    }

    private void DecreaseCooldown() {
        if ( _currentCooldown <= 0 ) { return; }
        _currentCooldown -= TickService.TickDeltaTime;
    }

    private void RefreshAmmoCountText() {
        _ammoCountText.text = _currentAmmo.ToString();
    }

    private void RefreshReloadSlider() {
        _reloadSlider.value = _reloadCycleAccumulatedTime;
    }

    private void ResetReloadSlider() {
        _reloadSlider.value = _reloadSlider.maxValue;
    }
}
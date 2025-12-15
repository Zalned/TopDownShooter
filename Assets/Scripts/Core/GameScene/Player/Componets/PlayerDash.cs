using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : NetworkBehaviour {
    [SerializeField] private PlayerInput _playerInput;
    private PlayerRuntimeStats _config;
    private LayerMask _wallLayerMask;
    private float _checkCollisionRadius = 0.25f;

    int _currentDashCount = 0;
    float _dashTimeAccumulated = 0;

    private float _reloadCycleAccumulatedTime = 0;

    private bool _inDash = false;
    public event Action OnDash;

    public void Initialize( PlayerRuntimeConfig config ) {
        _config = config.Player;
        _currentDashCount = (int)_config.DashCount.Value;
        _wallLayerMask = LayerMask.GetMask( Defines.Layers.ENVIROMENT );
    }

    public void OnDashInput( InputAction.CallbackContext context ) {
        if ( !IsOwner ) return;
        if ( !context.performed ) return;
        if ( _currentDashCount <= 0 ) return;

        HandleDash();
    }

    private void HandleDash() {
        _inDash = true;
        _currentDashCount--;
        _reloadCycleAccumulatedTime = 0;
        OnDash.Invoke();
    }

    public void Tick() {
        if ( _inDash ) {
            MoveToTargetPosition();
        }
        ReloadCycle();
    }

    private void MoveToTargetPosition() {
        if ( _dashTimeAccumulated <= _config.DashTime.Value ) {
            Vector3 velocity = transform.forward * (_config.DashLength.Value / _config.DashTime.Value);
            var newPosition = transform.position + velocity * TickService.TickDeltaTime;

            if ( !Physics.CheckSphere( newPosition, _checkCollisionRadius, _wallLayerMask ) ) {
                transform.position = newPosition;
            } else {
                _inDash = false;
                _dashTimeAccumulated = 0;
                return;
            }

            _dashTimeAccumulated += TickService.TickDeltaTime;
        } else {
            _inDash = false;
            _dashTimeAccumulated = 0;
        }
    }

    private void ReloadCycle() {
        if ( _currentDashCount >= _config.DashCount.Value ) { return; }
        _reloadCycleAccumulatedTime += TickService.TickDeltaTime;

        if ( _reloadCycleAccumulatedTime >= _config.DashReloadTime.Value ) {
            _currentDashCount = (int)_config.DashCount.Value;
            _reloadCycleAccumulatedTime = 0;
        }
    }
}
using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : NetworkBehaviour {
    [SerializeField] private PlayerInput _playerInput;
    private PlayerRuntimeConfig _config;
    private LayerMask _wallLayerMask;
    private float _checkCollisionRadius = 0.25f;

    private Vector3 _dashDirection = Vector3.zero;
    float _dashTimeAccumulated = 0;
    float _cooldown = 0;

    private bool _inDash = false;
    public event Action OnDash;

    public void Initialize( PlayerRuntimeConfig config ) {
        _config = config;
        _wallLayerMask = LayerMask.GetMask( Defines.Layers.Environment );
        TickService.OnTick += Tick;
    }
    public override void OnDestroy() {
        TickService.OnTick -= Tick;
    }

    public void OnDashInput( InputAction.CallbackContext context ) {
        if ( !IsOwner ) return;
        if ( !context.performed ) return;
        if ( _cooldown > 0 ) return;

        Vector2 moveInput = _playerInput.actions[ "Move" ].ReadValue<Vector2>();

        _dashDirection = GetDashDirection( moveInput );
        _inDash = true;
        _cooldown = _config.PlayerDashCooldown;
        OnDash.Invoke();
    }


    private void Tick() {
        if ( _inDash ) {
            MoveToTargetPosition();
        }
        DecreaseCooldown();
    }

    private void MoveToTargetPosition() {
        if ( _dashTimeAccumulated <= _config.PlayerDashTime ) {
            Vector3 velocity = _dashDirection * (_config.PlayerDashLenght / _config.PlayerDashTime);
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

    private Vector3 GetDashDirection( Vector2 move ) {
        if ( move == Vector2.zero ) {
            return transform.forward;
        }

        Vector3 dir = new( move.x, transform.position.y, move.y );
        dir.Normalize();
        return dir;
    }

    private void DecreaseCooldown() {
        _cooldown -= TickService.TickDeltaTime;
        _cooldown = Mathf.Clamp( _cooldown, 0, 999 );
    }
}
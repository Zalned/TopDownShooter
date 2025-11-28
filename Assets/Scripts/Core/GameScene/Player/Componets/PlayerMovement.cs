using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : NetworkBehaviour {
    private Camera _playerCamera;
    private PlayerRuntimeConfig _config;

    private Rigidbody _rb;
    private Vector2 _inputVector = Vector2.zero;

    public void Initialize( Camera camera, PlayerRuntimeConfig config ) {
        _playerCamera = camera;
        _config = config;
    }

    public void Start() {
        _rb = GetComponent<Rigidbody>();
    }

    public void FixedUpdate() {
        if ( IsOwner ) {
            Move();
            Rotate();
        }
    }

    private void Move() {
        Vector3 velocity = new Vector3( _inputVector.x, 0, _inputVector.y ) * _config.Player.Speed;
        _rb.linearVelocity = velocity;
    }

    private void Rotate() {
        var target = GetLookPosition();
        target.y = transform.position.y;
        transform.LookAt( target );
    }

    public void OnMoveInput( InputAction.CallbackContext context ) {
        if ( !IsOwner ) { return; }

        if ( context.performed ) {
            _inputVector = context.ReadValue<Vector2>();
        } else {
            _inputVector = Vector2.zero;
        }
    }
    
    public Vector3 GetLookPosition() {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = _playerCamera.ScreenPointToRay( mousePosition );
        if ( Physics.Raycast( ray, out RaycastHit hit ) ) {
            Vector3 targetPosition = hit.point;
            return targetPosition;
        }
        return Vector3.zero;
    }
}
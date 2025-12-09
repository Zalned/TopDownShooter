using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour {
    [SerializeField] private Transform _target;
    private float _currentZoom = 15;
    private const int MIN_ZOOM = 10;
    private const int MAX_ZOOM = 16;
    private const float ZOOM_SPEED = 2;

    private Quaternion _cameraRotation = Quaternion.Euler( 90, 0, 0 );

    private void LateUpdate() {
        UpdatePosition();
    }

    public void UpdatePosition() {
        Vector3 offset = new( 0, _currentZoom, 0 );
        transform.position = _target.position + offset;
        transform.rotation = _cameraRotation;
    }

    public void OnScroll( InputAction.CallbackContext context ) {
        if ( context.performed ) {
            float scrollValue = context.ReadValue<Vector2>().y;
            _currentZoom = Mathf.Clamp( _currentZoom - scrollValue * ZOOM_SPEED, MIN_ZOOM, MAX_ZOOM );
        }
    }
}
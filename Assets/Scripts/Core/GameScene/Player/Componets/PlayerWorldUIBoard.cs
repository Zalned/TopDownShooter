using UnityEngine;

public class PlayerWorldUIBoard : MonoBehaviour {
    private Camera _camera;

    public void Initialize( Camera camera ) {
        _camera = camera;
    }

    private void LateUpdate() {
        if ( _camera == null ) { return; }
        transform.forward = _camera.transform.forward;
    }
}
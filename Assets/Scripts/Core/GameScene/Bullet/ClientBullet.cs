using UnityEngine;

public class ClientBullet : MonoBehaviour {
    private Rigidbody _rigidbody;
    private PlayerRuntimeConfig _config;

    private float _age = 0;

    public void Construct( PlayerRuntimeConfig config ) {
        _rigidbody = GetComponent<Rigidbody>();
        _config = config;
    }

    private void FixedUpdate() {
        Move();
        IncreaseAge();
        HandleLifetime();
    }

    private void Move() {
        var movement = (transform.forward * _config.BulletSpeed) * Time.fixedDeltaTime;
        _rigidbody.MovePosition( transform.position + movement );
    }

    private void IncreaseAge() {
        _age += Time.fixedDeltaTime;
    }

    private void HandleLifetime() {
        if ( _age >= _config.Lifetime ) {
            Destroy( gameObject );
            return;
        }
    }

    private void OnTriggerEnter( Collider other ) {
        Destroy( gameObject );
    }
}
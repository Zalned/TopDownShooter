using Unity.Netcode;
using UnityEngine;
using Zenject;

public class PlayerController : NetworkBehaviour {
    [SerializeField] private PlayerView _view;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerShooting _playerShoot;
    [SerializeField] private PlayerDash _playerDash;
    [SerializeField] private Camera _playerCamera;

    public ulong NetID { get; private set; }
    private PlayerStats _playerStats;
    private float _currentHealth = 0;

    [ClientRpc]
    public void InitalizeClientRpc( NetworkPlayerData data ) {
        var bulletManager =
            FindFirstObjectByType<SceneContext>().Container.Resolve<BulletManager_Server>();

        NetID = OwnerClientId;

        _playerStats = new PlayerStats();
        _currentHealth = _playerStats.RuntimeConfig.MaxHealth;

        _view.InitializeClientRpc( data );
        _playerMovement.Initialize( _playerCamera, _playerStats );
        _playerShoot.Initialize( bulletManager, _playerStats, NetID );
        _playerDash.Initialize( _playerStats.RuntimeConfig );

        _playerShoot.OnShoot += OnShoot;
        _playerDash.OnDash += OnDash;

        if ( !IsOwner ) {
            _playerCamera.gameObject.SetActive( false );
        }

        Debug.Log( $"Player {NetID} initialized." );
    }

    public void TakeDamage( float damage ) {
        HandleHit( damage );
    }

    private void HandleHit( float damage ) {
        _currentHealth -= damage;

        if ( _currentHealth <= 0 ) {
            HandleDie();
        }
    }

    public void HandleDie() {
        GameEvents.OnPlayerDie.Invoke( NetID, gameObject );
    }

    private void OnShoot() {
        NetworkAudioManager.Instance.PlayClipAtPointServerRpc( Sounds.PlayerShoot, transform.position );
    }

    private void OnDash() {
        NetworkAudioManager.Instance.PlayClipAtPointServerRpc( Sounds.Dash, transform.position );
    }
}
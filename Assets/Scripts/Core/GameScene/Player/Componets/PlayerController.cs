using Unity.Netcode;
using UnityEngine;
using Zenject;
using UniRx;

public class PlayerController : NetworkBehaviour {
    [SerializeField] private PlayerView _view;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerShooting _playerShoot;
    [SerializeField] private PlayerDash _playerDash;
    [SerializeField] private Camera _playerCamera;

    public ulong NetID { get; private set; }
    private PlayerStats _playerStats;
    private ReactiveProperty<float> _currentHealth;

    [ClientRpc]
    public void InitalizeClientRpc( NetworkPlayerData data ) {
        var bulletManager =
            FindFirstObjectByType<SceneContext>().Container.Resolve<BulletManager_Server>();

        NetID = OwnerClientId;
        _playerStats = new PlayerStats();

        _currentHealth = new( _playerStats.RuntimeConfig.Player.MaxHealth );
        _currentHealth.Where( h => h <= 0 ).Take(1).Subscribe( _ => HandleDie() ).AddTo( this );
        _currentHealth.Subscribe( v => _view.SetPlayerHeatlhServerRpc(v) ).AddTo( this );

        _view.InitializeServerRpc( data, _playerStats.RuntimeConfig.Player.MaxHealth );
        _playerMovement.Initialize( _playerCamera, _playerStats.RuntimeConfig );
        _playerShoot.Initialize( bulletManager, _playerStats.RuntimeConfig, NetID );
        _playerDash.Initialize( _playerStats.RuntimeConfig );

        _playerShoot.OnShoot += OnShoot;
        _playerDash.OnDash += OnDash;

        if ( !IsOwner ) {
            _playerCamera.gameObject.SetActive( false );
        }

        Debug.Log( $"Player {NetID} initialized." );
    }

    public void TakeDamage( float damage ) {
        _currentHealth.Value -= damage;
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
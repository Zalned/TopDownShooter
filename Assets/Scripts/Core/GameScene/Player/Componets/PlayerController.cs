using Unity.Netcode;
using UnityEngine;
using Zenject;
using UniRx;

public class PlayerController : NetworkBehaviour {
    [SerializeField] private PlayerView _view;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerShoot _playerShoot;
    [SerializeField] private PlayerDash _playerDash;
    [SerializeField] private Camera _playerCamera;

    public PlayerModel Model;
    private PlayerModHandler _modHandler;

    [ClientRpc] // MyTodo: Нужно ли выполнять не визуальную логику для всех клиентов?
    public void InitalizeClientRpc( NetworkPlayerData data, int[] cardsIDs ) {
        var container = FindFirstObjectByType<SceneContext>().Container;
        var bulletManager = container.Resolve<BulletManager>();
        var cardManager = container.Resolve<CardManager>();

        Model = new();
        Model.SetCards( cardManager.GetRegistredCardsByIDs( cardsIDs ) );

        PlayerContext playerContext = new( gameObject, Model.PlayerStats.RuntimeConfig.Player );
        _modHandler = new( playerContext );

        Model.CurrentHealth = new( Model.PlayerStats.RuntimeConfig.Player.MaxHealth );
        Model.CurrentHealth.Where( h => h <= 0 ).Take( 1 ).Subscribe( _ => HandleDie() ).AddTo( this );
        Model.CurrentHealth.Subscribe( v => _view.SetPlayerHeatlhServerRpc( v ) ).AddTo( this );

        _view.InitializeServerRpc( data, Model.PlayerStats.RuntimeConfig.Player.MaxHealth );
        _playerMovement.Initialize( _playerCamera, Model.PlayerStats.RuntimeConfig );
        _playerShoot.Initialize( bulletManager, Model.PlayerStats.RuntimeConfig, OwnerClientId );
        _playerDash.Initialize( Model.PlayerStats.RuntimeConfig );

        TickService.OnTick += Tick;
        _playerShoot.OnShot += OnShoot;
        _playerDash.OnDash += OnDash;

        _modHandler.OnSpawn();

        if ( !IsOwner ) {
            _playerCamera.gameObject.SetActive( false );
            _view.HideHudCanvas();
        }
    }
    public override void OnDestroy() {
        TickService.OnTick -= Tick;
        _playerShoot.OnShot -= OnShoot;
        _playerDash.OnDash -= OnDash;
    }

    private void Tick() {
        _playerShoot.Tick();
        _playerDash.Tick();
        _modHandler.OnTick();
    }

    public void TakeDamage( float damage ) {
        Model.CurrentHealth.Value -= damage;
    }

    public void HandleDie() {
        EventBus.Publish( new PlayerDiedEvent( OwnerClientId, gameObject ) );
        NetworkAudioManager.Instance.PlayClipAtPointServerRpc( Sounds.PlayerDie, transform.position );
    }

    private void OnShoot() {
        NetworkAudioManager.Instance.PlayClipAtPointServerRpc( Sounds.PlayerShoot, transform.position );
    }

    private void OnDash() {
        NetworkAudioManager.Instance.PlayClipAtPointServerRpc( Sounds.Dash, transform.position );
        _modHandler.OnDash();
    }
}
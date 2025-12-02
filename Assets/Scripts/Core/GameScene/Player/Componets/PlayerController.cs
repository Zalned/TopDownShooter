using System.Collections.Generic;
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

    //[ClientRpc] // DEBUG: Временно убрано для тестов 
    public void Initalize( NetworkPlayerData data, List<CardSO> cards ) {
        var container = FindFirstObjectByType<SceneContext>().Container; // MyNote: Не лучший способ получения
        var bulletManager = container.Resolve<BulletManager>();
        var cardContext = container.Resolve<CardContext>();

        Model = new();
        Model.SetCards( cards );

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

        if ( !IsOwner ) {
            _playerCamera.gameObject.SetActive( false );
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
    }
}
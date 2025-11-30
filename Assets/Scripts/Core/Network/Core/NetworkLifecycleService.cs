using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Zenject;

public class NetworkLifecycleService : IDisposable {
    private ConnectionService _connectionService;
    private bool _networkManagerIsAlreadyInitialized = false; // MyNote: Какой-то баг с дублирующимся NetworkManager

    private readonly List<IDisposable> _subscriptions = new();

    [Inject]
    public NetworkLifecycleService( ConnectionService connectionService ) {
        _connectionService = connectionService;

        _subscriptions.Add( EventBus.Subscribe<StartHostRequestEvent>( e => StartHost( e.IpAdress ) ) );
        _subscriptions.Add( EventBus.Subscribe<StartClientRequestEvent>( e => StartClient( e.IpAdress ) ) );
        _subscriptions.Add( EventBus.Subscribe<StopHostRequestEvent>( e => Shutdown() ) ); ;
        NetworkManager.OnInstantiated += OnNetManagerCreated;
    }

    public void Dispose() {
        foreach ( var sub in _subscriptions ) sub.Dispose();
        NetworkManager.OnInstantiated -= OnNetManagerCreated;
    }

    private void OnNetManagerCreated( NetworkManager networkManager ) {
        if ( _networkManagerIsAlreadyInitialized ) {
            networkManager.OnClientConnectedCallback += OnClientConnected;
            _networkManagerIsAlreadyInitialized = true;
        }
    }

    private void OnClientConnected( ulong clientId ) {
        EventBus.Publish( new ClientDisconnectedEvent( clientId ) );
    }

    private void StartHost( string ipAddress ) {
        _connectionService.TryStartHostWithIP( ipAddress );
    }

    public void StartClient( string ipAddress ) {
        _connectionService.TryStartClientWithIP( ipAddress );
    }

    public void Shutdown() {
        Debug.Log( $"[{nameof( NetworkLifecycleService )}] Shutdown server." );
        NetworkManager.Singleton.Shutdown();
    }
}
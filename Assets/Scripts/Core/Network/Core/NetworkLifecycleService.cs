using System;
using Unity.Netcode;
using UnityEngine;
using Zenject;

public class NetworkLifecycleService : IDisposable {
    private ConnectionService _connectionService;
    private bool _networkManagerIsAlreadyInitialized = false; // MyNote: Какой-то баг с дублирующимся NetworkManager

    [Inject]
    public NetworkLifecycleService( ConnectionService connectionService ) {
        _connectionService = connectionService;

        NetworkEvents.StartHostRequest += StartHost;
        NetworkEvents.StartClientRequest += StartClient;
        NetworkEvents.StopHostRequest += Shutdown;
        NetworkManager.OnInstantiated += OnNetManagerCreated;
    }

    public void Dispose() {
        NetworkEvents.StartHostRequest -= StartHost;
        NetworkEvents.StartClientRequest -= StartClient;
        NetworkEvents.StopHostRequest -= Shutdown;
        NetworkManager.OnInstantiated -= OnNetManagerCreated;
    }

    private void OnNetManagerCreated( NetworkManager networkManager ) {
        if ( _networkManagerIsAlreadyInitialized ) {
            networkManager.OnClientConnectedCallback += OnClientConnected;
            _networkManagerIsAlreadyInitialized = true;
        }
    }

    private void OnClientConnected( ulong clientId ) {
        NetworkEvents.OnClientConnected?.Invoke( clientId );
    }

    private void StartHost(string ipAddress ) {
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
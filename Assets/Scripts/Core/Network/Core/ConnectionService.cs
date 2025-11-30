using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using Zenject;

public class ConnectionService {
    public void TryStartHostWithIP( string ipAddress ) {
        try {
            StartHostWithIP( ipAddress );
        } catch ( System.Exception e ) {
            EventBus.Publish( new NetworkErrorEvent( $"Failed to start host with IP {ipAddress}.", e ) );
        }
    }

    public void TryStartClientWithIP( string ipAddress ) {
        try {
            StartClientWithIP( ipAddress );
        } catch ( System.Exception e ) {
            EventBus.Publish( new NetworkErrorEvent( $"Failed to start client with IP {ipAddress}.", e ) );
        }
    }

    private void StartHostWithIP( string ipAdress ) {
        Debug.Log( $"Starting host on custom IP: {ipAdress}" );

        var unityTransport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        unityTransport.SetConnectionData( ipAdress, 7777 );

        NetworkManager.Singleton.StartHost();
        EventBus.Publish( new HostStartedEvent() );
    }

    private void StartClientWithIP( string ipAddress ) {
        Debug.Log( $"Starting client at IP: {ipAddress}" );

        var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        transport.SetConnectionData( ipAddress, 7777 );

        NetworkManager.Singleton.StartClient();
    }
}
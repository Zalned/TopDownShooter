using System;
using Unity.Netcode;
using UnityEngine;

public class MenuConnectionStarter {
    public void StartServer( string ip, string playerName ) {
        InitializePlayerData( playerName );
        EventBus.Publish( new StartHostRequestEvent( ip ) );
    }

    public void StartClient( string ip, string playerName ) {
        InitializePlayerData( playerName );
        EventBus.Publish( new StartClientRequestEvent( ip ) );
    }

    public void InitializePlayerData( string playerName ) {
        var payload = new PlayerConnectionPayload {
            PlayerName = playerName
        };

        string json = JsonUtility.ToJson( payload );
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes( json );

        HostDataTransfer.SetHostData( payload );
        NetworkManager.Singleton.NetworkConfig.ConnectionData = bytes;
    }
}
using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Zenject;

public class NetworkPlayerManager : IDisposable {
    private PlayerFactory _playerFactory;

    public Dictionary<ulong, NetworkPlayerData> RegistredPlayers { get; private set; } = new();

    public event Action OnRegistredPlayersUpdated;

    [Inject]
    public NetworkPlayerManager( PlayerFactory playerFactory ) {
        _playerFactory = playerFactory;

        if ( NetworkManager.Singleton != null ) {
            NetworkManager.Singleton.ConnectionApprovalCallback += OnClientConnectionRequest;
            NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;

        } else { Debug.LogError( $"[{nameof( NetworkPlayerManager )}] NetworkManager is null!" ); }

        RegisterHost();
    }

    public void Dispose() {
        NetworkManager.Singleton.ConnectionApprovalCallback -= OnClientConnectionRequest;
        NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnected;
    }

    private void RegisterHost() {
        var payloadData = HostDataTransfer.HostData;
        NetworkPlayerData player = _playerFactory.CreatePlayer( 0, payloadData );
        RegisterPlayer( player );
    }

    private void OnClientConnectionRequest( NetworkManager.ConnectionApprovalRequest request,
                                    NetworkManager.ConnectionApprovalResponse response ) {

        string json = System.Text.Encoding.UTF8.GetString( request.Payload );
        var payload = JsonUtility.FromJson<PlayerConnectionPayload>( json );

        ulong clientId = request.ClientNetworkId;

        Debug.Log( $"Player '{payload.PlayerName}' is trying to connect with id {clientId}" );
        NetworkPlayerData player = _playerFactory.CreatePlayer( clientId, payload );

        response.Approved = true;
        response.CreatePlayerObject = true;
        response.PlayerPrefabHash = null;

        RegisterPlayer( player );
    }

    private void OnClientDisconnected( ulong clientId ) {
        Debug.Log( $"[{nameof( NetworkPlayerManager )}] {nameof( OnClientDisconnected )} Player ID: {clientId}" );

        var playerData = GetPlayerById( clientId );
        UnRegisterPlayer( playerData );
    }

    private void RegisterPlayer( NetworkPlayerData playerData ) {
        if ( !RegistredPlayers.ContainsKey( playerData.NetID ) ) {
            Debug.Log( $"[{nameof( NetworkPlayerManager )}] Player: {playerData.NetID} registred" ); ;
            RegistredPlayers.Add( playerData.NetID, playerData );
            OnRegistredPlayersUpdated?.Invoke();

        } else {
            Debug.LogWarning( $"[{nameof( NetworkPlayerManager )}] Player: {playerData.NetID} is already registred!" );
        }
    }

    private void UnRegisterPlayer( NetworkPlayerData playerData ) {
        if ( RegistredPlayers.ContainsKey( playerData.NetID ) ) {
            RegistredPlayers.Remove( playerData.NetID );
            OnRegistredPlayersUpdated?.Invoke();
            Debug.Log( $"[{nameof( NetworkPlayerManager )}] Player: {playerData.NetID} unregistred" ); ;

        } else {
            Debug.LogWarning( $"[{nameof( NetworkPlayerManager )}] Player: {playerData.NetID} is not registred!" );
        }
    }

    public NetworkPlayerData GetPlayerById( ulong netID ) {
        return RegistredPlayers[ netID ];
    }
}
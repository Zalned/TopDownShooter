using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class SessionPlayerManager {
    public Dictionary<ulong, NetworkPlayerData> SessionPlayers { get; private set; } = new();
    public Dictionary<ulong, ActivePlayerData> ActivePlayers { get; private set; } = new();
    public Dictionary<ulong, GameObject> LivePlayers { get; private set; } = new();
    public Dictionary<ulong, GameObject> DeadPlayers { get; private set; } = new();

    public event Action OnLivePlayerAdded;
    public event Action OnLivePlayerRemoved;

    public SessionPlayerManager() {
        NetworkEvents.OnClientDisconnected += RemoveSessionPlayer;
    }

    // Session players
    public void SetSessionPlayers( Dictionary<ulong, NetworkPlayerData> players ) {
        foreach ( var player in players ) {
            AddSessionPlayer( player.Key, player.Value );
        }
    }
    public void ResetSessionPlayers() {
        SessionPlayers.Clear();
    }
    public void AddSessionPlayer( ulong playerID, NetworkPlayerData data ) {
        SessionPlayers.Add( playerID, data );
    }
    public void RemoveSessionPlayer( ulong playerID ) {
        SessionPlayers.Remove( playerID );
    }

    // Active players
    public void SetActivePlayers( Dictionary<ulong, NetworkPlayerData> players ) {
        foreach ( var player in players ) {
            AddActivePlayer( player.Key, player.Value );
        }
    }
    public void ResetActivePlayers() {
        ActivePlayers.Clear();
    }
    public void AddActivePlayer( ulong playerID, NetworkPlayerData data ) {
        ActivePlayers.Add( playerID, new( data ) );
    }
    public void RemoveActivePlayer( ulong playerID ) {
        ActivePlayers.Remove( playerID );
    }

    public void AddWinScore( ulong winner ) {
        if ( ActivePlayers.ContainsKey( winner ) ) {
            ActivePlayers[ winner ].AddScore();
        } else { Debug.LogWarning( $"[{nameof( SessionPlayerManager )}] Winner ID not fount in dictionary." ); }
    }

    // Live/Dead players
    public void AddLivePlayer( ulong id, GameObject player ) {
        if ( !LivePlayers.Keys.Contains( id ) ) {
            LivePlayers.Add( id, player );
            OnLivePlayerAdded?.Invoke();
        } else {
            Debug.LogWarning( $"[{nameof( SessionPlayerManager )}] Player already in LivePlayers list." );
        }
    }

    public void RemoveLivePlayer( ulong id ) {
        if ( LivePlayers.Remove( id ) ) {
            OnLivePlayerRemoved?.Invoke();
        } else {
            Debug.LogWarning( $"[{nameof( SessionPlayerManager )}] Player not found in LivePlayers list." );
        }
    }

    public void AddDeadPlayer( ulong id, GameObject playerObj ) {
        if ( !DeadPlayers.Keys.Contains( id ) ) {
            DeadPlayers.Add( id, playerObj );
        } else {
            Debug.LogWarning( $"[{nameof( SessionPlayerManager )}] Player already in DeadPlayers list." );
        }
    }

    public void RemoveDeadPlayer( ulong id ) {
        if ( DeadPlayers.Remove( id ) ) {

        } else {
            Debug.LogWarning( $"[{nameof( SessionPlayerManager )}] Player not found in DeadPlayers list." );
        }
    }

    public ulong GetLastLifePlayerID() {
        foreach ( var kvp in LivePlayers ) { return kvp.Key; }
        throw new Exception( "No live players." );
    }
}
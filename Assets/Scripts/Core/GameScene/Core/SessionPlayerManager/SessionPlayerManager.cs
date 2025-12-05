using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SessionPlayerManager {
    public Dictionary<ulong, NetworkPlayerData> SessionPlayers { get; private set; } = new();
    public Dictionary<ulong, ActivePlayerData> ActivePlayers { get; private set; } = new();
    public Dictionary<ulong, GameObject> LivePlayers { get; private set; } = new();
    public Dictionary<ulong, GameObject> DeadPlayers { get; private set; } = new();

    public Dictionary<ulong, ActivePlayerData> LosePlayers { get; private set; } = new();
    public List<ulong> PlayersWhoChoseCard { get; private set; } = new();
    public bool IsAllLosePlayersChoseCard => LosePlayers.Count == PlayersWhoChoseCard.Count;

    public event Action OnLivePlayerAdded;
    public event Action OnLivePlayerRemoved;
    public event Action OnAllPlayersPickCard;

    public void AddPlayerWhoChoseCard( ulong playerID ) {
        if( PlayersWhoChoseCard.Contains( playerID ) ) {
            Debug.LogWarning( $"[{nameof( SessionPlayerManager )}] Player already chose card." );
            return;
        }
        PlayersWhoChoseCard.Add( playerID );

        if ( IsAllLosePlayersChoseCard ) {
            OnAllPlayersPickCard?.Invoke();
        }
    }

    public void ClearPlayersWhoChoseCard() {
        PlayersWhoChoseCard.Clear();
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
    public void AddSessionPlayer( ulong playerId, NetworkPlayerData data ) {
        SessionPlayers.Add( playerId, data );
    }
    public void RemoveSessionPlayer( ulong playerId ) {
        SessionPlayers.Remove( playerId );
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

    public ActivePlayerData GetActivePlayerByID( ulong id ) {
        if ( ActivePlayers.ContainsKey( id ) ) {
            return ActivePlayers[ id ];
        } else { throw new Exception( "ID not fount in dictionary." ); }
    }

    public void AddWinScore( ulong id ) {
        if ( ActivePlayers.ContainsKey( id ) ) {
            ActivePlayers[ id ].AddScore();
        } else { throw new Exception( "ID not fount in dictionary." ); }
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
        throw new Exception( "No live players." ); // MyTodo: последний игрок может умереть после победы
    }

    // Lose players
    public void InitializeLosePlayers( ulong winnerID ) {
        LosePlayers.Clear();
        foreach ( var player in ActivePlayers ) {
            LosePlayers.Add( player.Key, player.Value );
        }
        LosePlayers.Remove( winnerID );
    }
}
using System;
using System.Linq;
using UnityEngine;
using Zenject;

public class RoundManager : IDisposable {
    private RoundHudView _roundHUD;
    private SessionPlayerManager _sessionPlayerManager;
    private SessionConfigSO _sessionConfig;
    private PlayerHudDataBuilder _playerHudDataBuilder = new();

    public event Action<ulong, string> OnRoundEnd;
    public event Action<ulong, string> OnPlayerWin;

    [Inject]
    public RoundManager( RoundHudView view, SessionPlayerManager sessionPlayerManager ) {
        _roundHUD = view;
        _sessionPlayerManager = sessionPlayerManager;

        _sessionConfig = Resources.Load<SessionConfigSO>( Defines.ConfigPaths.SESSION_CONFIG );
        _sessionPlayerManager.OnLivePlayerRemoved += HandlePlayerDie;
    }

    public void Dispose() {
        _sessionPlayerManager.OnLivePlayerRemoved -= HandlePlayerDie;
    }

    private void HandlePlayerDie() {
        if ( _sessionPlayerManager.LivePlayers.Count == 1 ) {
            StopRound();
        }
    }

    public void StartRound() {
        _roundHUD.UpdateHudDataClientRpc(
            _playerHudDataBuilder.GetHudData( _sessionPlayerManager.ActivePlayers.Values.ToList() ) );
        _roundHUD.ShowClientRpc();
    }

    public void StopRound() {
        _roundHUD.HideClientRpc();

        var lastLivePlayer = _sessionPlayerManager.GetLastLifePlayerID();
        AddWinScoreForLastPlayer( lastLivePlayer );

        if ( !HasWinner() ) {
            var winnerName = _sessionPlayerManager.GetActivePlayerByID( lastLivePlayer ).NetworkPlayerData.Name;
            OnRoundEnd.Invoke( lastLivePlayer, winnerName );
        }
    }

    private void AddWinScoreForLastPlayer( ulong lastLivePlayer ) {
        _sessionPlayerManager.AddWinScore( lastLivePlayer );
    }

    private bool HasWinner() {
        foreach ( var (playerId, ActivePlayerData) in _sessionPlayerManager.ActivePlayers ) {
            if ( ActivePlayerData.Score >= _sessionConfig.RoundForWin ) {
                OnPlayerWin?.Invoke( playerId, ActivePlayerData.NetworkPlayerData.Name );
                return true;
            }
        }
        return false;
    }
}
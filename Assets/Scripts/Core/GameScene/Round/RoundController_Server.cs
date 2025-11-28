using System;
using UnityEngine;

public class RoundController_Server {
    private RoundHudView _roundHUD;
    private SessionPlayerManager_Server _sessionPlayerManager;
    private SessionConfigSO _sessionConfig;

    public event Action<ulong> OnRoundEnd;
    public event Action<ulong> OnPlayerWin;

    public RoundController_Server( RoundHudView view, SessionPlayerManager_Server sessionPlayerManager ) {
        _roundHUD = view;
        _sessionPlayerManager = sessionPlayerManager;

        _sessionConfig = Resources.Load<SessionConfigSO>( "Configs/SessionConfig" );
        _sessionPlayerManager.OnLivePlayerRemoved += HandlePlayerDie;
    }

    private void HandlePlayerDie() {
        if ( _sessionPlayerManager.LivePlayers.Count == 1 ) {
            StopRound();
        }
    }

    public void StartRound() {
        _roundHUD.UpdateHudDataClientRpc( GetHudData() );
        _roundHUD.ShowClientRpc();
    }

    public void StopRound() {
        _roundHUD.HideClientRpc();

        var lastLivePlayer = _sessionPlayerManager.GetLastLifePlayerID();
        AddWinScoreForLastPlayer( lastLivePlayer );

        if ( !HasWinner() ) {
            OnRoundEnd.Invoke( lastLivePlayer );
        }
    }

    private void AddWinScoreForLastPlayer( ulong lastLivePlayer ) {
        _sessionPlayerManager.AddWinScore( lastLivePlayer );
    }

    private bool HasWinner() {
        foreach ( var (playerId, ActivePlayerData) in _sessionPlayerManager.ActivePlayers ) {
            if ( ActivePlayerData.Score >= _sessionConfig.RoundForWin ) {
                OnPlayerWin?.Invoke( playerId );
                return true;
            }
        }
        return false;
    }

    private string GetHudData() {
        var buffer = string.Empty;

        foreach ( var player in _sessionPlayerManager.ActivePlayers ) {
            var playerName = player.Value.NetworkPlayerData.Name;
            var activePlayerData = player.Value;

            buffer += $"{playerName} - {activePlayerData.Score} \n";
        }
        return buffer;
    }
}
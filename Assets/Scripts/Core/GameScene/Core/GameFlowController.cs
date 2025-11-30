using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Zenject;

public class GameFlowController : MonoBehaviour {
    private GameSessionService _gameSessionService;
    private bool _gameIsStarted = false;

    private readonly List<IDisposable> _subscriptions = new();

    public void Initialize( GameSessionService gameSessionService ) {
        _gameSessionService = gameSessionService;

        _subscriptions.Add( EventBus.Subscribe<StartButtonClicked>( e => OnStartRequested() ) );
        _subscriptions.Add( EventBus.Subscribe<OnStopGameButtonClicked>( e => OnStopRequested() ) );
        _subscriptions.Add( EventBus.Subscribe<OnExitGameButtonClicked>( e => OnQuitRequested() ) );
        _subscriptions.Add( EventBus.Subscribe<GameStoppedEvent>( e => OnGameEnded() ) );
        _subscriptions.Add( EventBus.Subscribe<PlayerWinGameEvent>( e => OnPlayerWinGame( e.playerId ) ) );
    }

    private void OnDestroy() {
        foreach ( var sub in _subscriptions ) { sub.Dispose(); }
    }

    private void Awake() {
        if ( !NetcodeHelper.IsServer ) { this.enabled = false; return; }
    }

    private void OnStartRequested() {
        if ( _gameIsStarted ) {
            _gameSessionService.StartRound();
        } else {
            _gameSessionService.StartGame();
        }
    }

    private void OnPlayerWinGame( ulong _ ) {
        OnGameEnded();
    }

    private void OnGameEnded() {
        _gameIsStarted = false;
    }

    private void OnStopRequested() {
        _gameSessionService.StopGame();
    }

    private void OnQuitRequested() {
        _gameSessionService.QuitToMenu();
    }
}
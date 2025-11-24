using Unity.Netcode;
using UnityEngine;
using Zenject;

public class GameFlowController_Server : MonoBehaviour {
    [SerializeField] private GameView_Global _view;

    private GameSessionService_Server _gameSessionService;
    private bool _gameIsStarted = false;

    public void Initialize( GameSessionService_Server gameSessionService ) {
        _gameSessionService = gameSessionService;

        GameEvents.OnStartBtn += OnStartRequested;
        GameEvents.OnStopGameBtn += OnStopRequested;
        GameEvents.OnQuitToMenuBtn += OnQuitRequested;

        GameEvents.OnGameStopped += OnGameEnded;
        GameEvents.OnPlayerWinGame += OnPlayerWinGame;
    }

    private void OnDestroy() {
        GameEvents.OnStartBtn -= OnStartRequested;
        GameEvents.OnStopGameBtn -= OnStopRequested;
        GameEvents.OnQuitToMenuBtn -= OnQuitRequested;
        GameEvents.OnGameStopped -= OnGameEnded;
        GameEvents.OnPlayerWinGame -= OnPlayerWinGame;
    }

    private void OnStartRequested() {
        if ( NetworkManager.Singleton.IsServer ) {
            if ( _gameIsStarted ) {
                _gameSessionService.StartRound();
            } else {
                _gameSessionService.StartGame();
            }
        }
    }

    private void OnPlayerWinGame( ulong _ ) {
        OnGameEnded();
    }

    private void OnGameEnded() {
        _gameIsStarted = false;
    }

    private void OnStopRequested() {
        if ( NetworkManager.Singleton.IsServer ) {
            _gameSessionService.StopGame();
        }
    }

    private void OnQuitRequested() {
        if ( NetworkManager.Singleton.IsHost || NetworkManager.Singleton.IsServer ) {
            _gameSessionService.QuitToMenu();
        }
    }
}

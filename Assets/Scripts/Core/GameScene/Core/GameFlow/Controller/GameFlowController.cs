using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zenject;

public class GameFlowController : IDisposable {
    private MapService _mapService;
    private PlayerSpawnService _playerSpawnService;
    private NetworkPlayerManager _playerManager;
    private SessionPlayerManager _sessionPlayerManager;

    private LobbyPresenter _lobbyPresenter;
    private RoundManager _roundManager;

    private PlayerWinRoundView _playerWinRoundView;
    private PlayerWinGameView _playerWinGameView;

    private CardManager _cardManager;

    private StartGameHandler _startGameHandler;
    private StartRoundHandler _startRoundHandler;
    private EndGameHandler _gameEndHandler;
    private EndRoundHandler _roundEndHandler;
    private GameWinHandler _gameWinHandler;

    private bool _isFirstRound = true;

    [Inject]
    public GameFlowController(
        MapService mapService,
        PlayerSpawnService playerSpawner,
        NetworkPlayerManager playerManager,
        SessionPlayerManager sessionPlayerManager,
        RoundManager roundController,
        PlayerWinRoundView playerWinRoundView,
        PlayerWinGameView playerWinGameView,
        LobbyPresenter lobbyController,
        CardManager cardManager ) {
        _mapService = mapService;
        _playerSpawnService = playerSpawner;
        _playerManager = playerManager;
        _sessionPlayerManager = sessionPlayerManager;
        _lobbyPresenter = lobbyController;
        _roundManager = roundController;
        _playerWinRoundView = playerWinRoundView;
        _playerWinGameView = playerWinGameView;
        _cardManager = cardManager;

        _startGameHandler = new( _playerManager, _sessionPlayerManager, _lobbyPresenter );
        _startRoundHandler = new( _cardManager, _mapService, _playerSpawnService, _roundManager, _sessionPlayerManager );
        _roundEndHandler = new( _playerSpawnService, _sessionPlayerManager, _playerWinRoundView );
        _gameEndHandler = new( _mapService, _lobbyPresenter, _playerSpawnService, _sessionPlayerManager );
        _gameWinHandler = new( _playerWinGameView );

        _roundManager.OnRoundEnd += HandleRoundEnd;
        _roundManager.OnPlayerWin += HandlePlayerWinGame;

        EventBus.Subscribe<StartButtonClicked>( OnStartRequested );
    }

    public void Dispose() {
        EventBus.Unsubscribe<StartButtonClicked>( OnStartRequested );
        _roundManager.OnRoundEnd -= HandleRoundEnd;
        _roundManager.OnPlayerWin -= HandlePlayerWinGame;
    }

    private void OnStartRequested( StartButtonClicked e ) {
        StartGame();
    }

    public async void StartGame() {
        _startGameHandler.HandleStartGame();
        _isFirstRound = true;
        await StartRound();
    }

    public async Task StartRound() {
        await _startRoundHandler.HandleStartRound( _isFirstRound );
        _isFirstRound = false;
    }

    private async void HandleRoundEnd( ulong winnerID, string name ) {
        await _roundEndHandler.HandleRoundEnd( winnerID, name );
        await StartRound();
    }

    private async void HandlePlayerWinGame( ulong winnerId, string name ) {
        await _gameWinHandler.HandlePlayerWinGame( winnerId, name );
        EndGame();
    }

    private void EndGame() {
        _gameEndHandler.HandleEndGame();
    }

    public void StopGame() {
        // TODO
    }

    public void QuitToMenu() {
        // TODO
    }
}
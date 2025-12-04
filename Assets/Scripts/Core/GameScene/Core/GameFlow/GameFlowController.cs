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
    private RoundManager _roundController;

    private PlayerWinRoundView _playerWinRoundView;
    private PlayerWinGameView _playerWinGameView;

    private CardManager _cardManager;

    private StartGameHandler _startGameHandler;
    private EndGameHandler _gameEndHandler;
    private EndRoundHandler _roundEndHandler;
    private GameWinHandler _gameWinHandler;

    private bool _gameIsStarted = false;
    private bool _isFirstRound = true;
    private TaskCompletionSource<bool> _waitCardPickTask;

    private readonly List<IDisposable> _subscriptions = new();

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
        _roundController = roundController;
        _playerWinRoundView = playerWinRoundView;
        _playerWinGameView = playerWinGameView;
        _cardManager = cardManager;

        _startGameHandler = new( _playerManager, _sessionPlayerManager, _lobbyPresenter );
        _roundEndHandler = new( _playerSpawnService, _sessionPlayerManager, _playerWinRoundView );
        _gameEndHandler = new( _mapService, _lobbyPresenter, _playerSpawnService, _sessionPlayerManager );
        _gameWinHandler = new( _playerWinGameView );

        _roundController.OnRoundEnd += HandleRoundEndWithWinner;
        _roundController.OnPlayerWin += HandlePlayerWinGame;
        _startGameHandler.StartRoundEvent += StartRound;
        _roundEndHandler.StartRoundEvent += StartRound;
        _gameWinHandler.EndGameEvent += EndGame;

        _subscriptions.Add( EventBus.Subscribe<StartButtonClicked>( e => OnStartRequested() ) );
        _subscriptions.Add( EventBus.Subscribe<PlayerCardPickEvent>( e => HandlePlayerPickCard( e.playerID ) ) );
    }

    public void Dispose() {
        foreach ( var sub in _subscriptions ) { sub.Dispose(); }
        _subscriptions.Clear();
        _roundController.OnRoundEnd -= HandleRoundEndWithWinner;
        _roundController.OnPlayerWin -= HandlePlayerWinGame;
        _startGameHandler.StartRoundEvent -= StartRound;
        _roundEndHandler.StartRoundEvent -= StartRound;
        _gameWinHandler.EndGameEvent -= EndGame;
    }

    private async void OnStartRequested() {
        if ( _gameIsStarted ) {
            await StartRound();
        } else {
            StartGame();
        }
    }

    public async void StartGame() {
        _startGameHandler.HandleStartGame();
        _isFirstRound = true;
    }

    public async Task StartRound() { // MyTodo Вынести в отдельный класс
        if ( _isFirstRound == true ) {
            _isFirstRound = false;

        } else {
            _cardManager.StartCardChooseFromServer();
            _waitCardPickTask = new TaskCompletionSource<bool>();
            await _waitCardPickTask.Task;
        }

        _mapService.LoadRandomMap();
        _playerSpawnService.SpawnPlayersOnMap();
        _roundController.StartRound();
    }

    private async void HandlePlayerPickCard( ulong playerId ) {
        _sessionPlayerManager.AddPlayerWhoChoseCard( playerId );

        if ( _sessionPlayerManager.IsAllLosePlayersChoseCard ) {
            _sessionPlayerManager.ResetPlayerWhoChoseCard();
            _waitCardPickTask?.SetResult( true );
        }
    }

    private async void HandleRoundEndWithWinner( ulong winnerID, string name ) {
        _roundEndHandler.HandleRoundEnd( winnerID, name );
    }

    private async void HandlePlayerWinGame( ulong playerId, string name ) {
        _gameWinHandler.HandlePlayerWinGame( playerId, name );
    }

    private void EndGame( ulong winnerId ) {
        _gameEndHandler.HandleEndGame( winnerId );
        _gameIsStarted = false;
    }

    public void StopGame() {
        // TODO
    }

    public void QuitToMenu() {
        // TODO
    }
}
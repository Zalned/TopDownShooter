using System.Threading.Tasks;
using Unity.Netcode;
using Zenject;

public class GameSessionService {
    private GameNetworkHandler _networkHandler;
    private MapService_Server _mapService;
    private PlayerSpawnService _playerSpawnService;
    private PlayerManager _playerManager;
    private SessionPlayerManager _sessionPlayerManager;

    private LobbyPresenter _lobbyController;
    private RoundManager _roundController;

    private PlayerWinRoundView _playerWinRoundView;
    private PlayerWinGameView _playerWinGameView;

    private const int SHOW_WIN_ROUND_UI_TIME = 5;
    private const int SHOW_WIN_GAME_UI_TIME = 5;

    [Inject]
    public GameSessionService(
        GameNetworkHandler networkHandler,
        MapService_Server mapService,
        PlayerSpawnService playerSpawner,
        PlayerManager playerManager,
        SessionPlayerManager sessionPlayerManager,
        RoundManager roundController,
        PlayerWinRoundView playerWinRoundView,
        PlayerWinGameView playerWinGameView,
        LobbyPresenter lobbyController ) {
        _networkHandler = networkHandler;
        _mapService = mapService;
        _playerSpawnService = playerSpawner;
        _playerManager = playerManager;
        _sessionPlayerManager = sessionPlayerManager;
        _lobbyController = lobbyController;
        _roundController = roundController;

        _playerWinRoundView = playerWinRoundView;
        _playerWinGameView = playerWinGameView;

        _roundController.OnRoundEnd += HandleRoundEnd;
        _roundController.OnPlayerWin += HandlePlayerWinGame;
    }

    public void StartGame() {
        _sessionPlayerManager.SetSessionPlayers( _playerManager.RegistredPlayers );
        _sessionPlayerManager.SetActivePlayers( _sessionPlayerManager.SessionPlayers );
        StartRound();
        _lobbyController.Hide();
        GameEvents.OnGameStarted?.Invoke();
    }

    public void StartRound() {
        _mapService.LoadRandomMap();
        _playerSpawnService.SpawnPlayersOnMap();
        _roundController.StartRound();
    }

    private async void HandleRoundEnd( ulong winnerID, string name ) {
        _playerSpawnService.DespawnPlayersOnMap();

        _playerWinRoundView.SetPlayerWinNameClientRpc( name );
        await ShowPlayerWinRoundUI();

        StartRound();
    }

    private async void HandlePlayerWinGame( ulong playerId, string name ) {
        _playerWinGameView.SetPlayerWinNameClientRpc( name );
        await ShowPlayerWinGameUI();

        GameEvents.OnPlayerWinGame.Invoke( playerId );
        EndGame( playerId );
    }

    private async Task ShowPlayerWinRoundUI() {
        _playerWinRoundView.ShowClientRpc();
        await Task.Delay( SHOW_WIN_ROUND_UI_TIME * 1000 );
        _playerWinRoundView.HideClientRpc();
    }

    private async Task ShowPlayerWinGameUI() {
        _playerWinGameView.ShowClientRpc();
        await Task.Delay( SHOW_WIN_GAME_UI_TIME * 1000 );
        _playerWinGameView.HideClientRpc();
    }

    private void EndGame( ulong winnerId ) {
        _mapService.DespawnMap();
        _lobbyController.Show();
        _playerSpawnService.DespawnPlayersOnMap();

        _sessionPlayerManager.ResetSessionPlayers();
        _sessionPlayerManager.ResetActivePlayers();
    }

    public void StopGame() {
        GameEvents.OnGameStopped.Invoke();
    }

    public void QuitToMenu() {
        _networkHandler.QuitToMenuClientRpc();
        NetworkManager.Singleton.Shutdown();
    }
}
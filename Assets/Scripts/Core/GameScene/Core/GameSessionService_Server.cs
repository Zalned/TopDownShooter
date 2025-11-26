using System.Threading.Tasks;
using Unity.Netcode;
using Zenject;

public class GameSessionService_Server {
    private GameNetworkHandler_Server _networkHandler;
    private MapService_Server _mapService;
    private PlayerSpawnController_Server _playerSpawnController;
    private PlayerManager_Server _playerManager;
    private SessionPlayerManager_Server _sessionPlayerManager;

    private LobbyController_Server _lobbyController;
    private RoundController_Server _roundController;

    private PlayerWinRoundView _playerWinRoundView;
    private PlayerWinGameView _playerWinGameView;

    private const int SHOW_WIN_ROUND_UI_TIME = 5;
    private const int SHOW_WIN_GAME_UI_TIME = 5;

    [Inject]
    public GameSessionService_Server(
        GameNetworkHandler_Server networkHandler,
        MapService_Server mapService,
        PlayerSpawnController_Server playerSpawner,
        PlayerManager_Server playerManager,
        SessionPlayerManager_Server sessionPlayerManager,
        RoundController_Server roundController,
        PlayerWinRoundView playerWinRoundView,
        PlayerWinGameView playerWinGameView, 
        LobbyController_Server lobbyController ) {
        _networkHandler = networkHandler;
        _mapService = mapService;
        _playerSpawnController = playerSpawner;
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
        _playerSpawnController.SpawnPlayersOnMap();
        _roundController.StartRound();
    }

    private async void HandleRoundEnd( ulong winnerID ) {
        _playerSpawnController.DespawnPlayersOnMap();

        _playerWinRoundView.SetPlayerWinNameClientRpc( winnerID.ToString() );
        await ShowPlayerWinRoundUI();

        StartRound();
    }

    private async void HandlePlayerWinGame( ulong playerId ) {
        _playerWinGameView.SetPlayerWinNameClientRpc( playerId.ToString() );
        await ShowPlayerWinGameUI();

        EndGame( playerId );
    }

    private async Task ShowPlayerWinRoundUI() {
        _playerWinRoundView.ShowClientRpc();
        await Task.Delay( SHOW_WIN_ROUND_UI_TIME * 1000 );
        _playerWinRoundView.ShowClientRpc();
    }

    private async Task ShowPlayerWinGameUI() {
        _playerWinGameView.ShowClientRpc();
        await Task.Delay( SHOW_WIN_GAME_UI_TIME * 1000 );
        _playerWinGameView.HideClientRpc();
    }

    private void EndGame( ulong winnerId ) {
        _mapService.DespawnMap();
        _lobbyController.Show();
        _playerSpawnController.DespawnPlayersOnMap();

        _sessionPlayerManager.ResetSessionPlayers();
        _sessionPlayerManager.ResetActivePlayers();
    }

    public void StopGame() { 
        GameEvents.OnGameStopped.Invoke();
    }

    public void QuitToMenu() {
        _networkHandler.QuitToMenuForClients();
        NetworkManager.Singleton.Shutdown();
    }
}
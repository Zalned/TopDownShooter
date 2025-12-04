using System;
using System.Threading.Tasks;

public class EndRoundHandler {
    private readonly PlayerSpawnService _playerSpawnService;
    private readonly SessionPlayerManager _sessionPlayerManager;
    private readonly PlayerWinRoundView _playerWinRoundView;
    private const int SHOW_WIN_ROUND_UI_TIME = 3 * 1000;

    public event Func<Task> StartRoundEvent;

    public EndRoundHandler(
        PlayerSpawnService playerSpawnService,
        SessionPlayerManager sessionPlayerManager,
        PlayerWinRoundView playerWinRoundView ) {
        _playerSpawnService = playerSpawnService;
        _sessionPlayerManager = sessionPlayerManager;
        _playerWinRoundView = playerWinRoundView;
    }

    public async void HandleRoundEnd( ulong winnerID, string name ) {
        _sessionPlayerManager.InitializeLosePlayers( winnerID );
        _playerSpawnService.DespawnPlayersOnMap();
        _playerWinRoundView.SetPlayerWinNameClientRpc( name );
        await ShowPlayerWinRoundUI();
    }

    private async Task ShowPlayerWinRoundUI() {
        _playerWinRoundView.ShowClientRpc();
        await Task.Delay( SHOW_WIN_ROUND_UI_TIME );
        _playerWinRoundView.HideClientRpc();
    }

}
using System.Threading.Tasks;
using UnityEngine;
using System;

public class StartRoundHandler {
    private readonly CardManager _cardManager;
    private readonly MapService _mapService;
    private readonly PlayerSpawnService _playerSpawnService;
    private readonly RoundManager _roundManager;
    private readonly SessionPlayerManager _sessionPlayerManager;

    public StartRoundHandler(
        CardManager cardManager,
        MapService mapService,
        PlayerSpawnService playerSpawnService,
        RoundManager roundManager,
        SessionPlayerManager sessionPlayerManager ) {
        _cardManager = cardManager;
        _mapService = mapService;
        _playerSpawnService = playerSpawnService;
        _roundManager = roundManager;
        _sessionPlayerManager = sessionPlayerManager;
    }

    public async Task HandleStartRound( bool isFirstRound ) {
        if ( !isFirstRound ) {
            var tcs = new TaskCompletionSource<bool>();

            Action handler = null;
            handler = () => {
                _sessionPlayerManager.OnAllPlayersPickCard -= handler;
                tcs.SetResult( true );
            };
            _sessionPlayerManager.OnAllPlayersPickCard += handler;

            _cardManager.StartCardChooseFromServer();
            await tcs.Task;
        }

        _mapService.LoadRandomMap();
        _playerSpawnService.SpawnPlayersOnMap();
        _roundManager.StartRound();
    }
}


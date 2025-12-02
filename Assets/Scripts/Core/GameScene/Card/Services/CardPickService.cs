using System;
using UnityEngine;
using Zenject;

public class CardPickService : IDisposable {
    private SessionPlayerManager _sessionPlayerManager;

    [Inject]
    public CardPickService( SessionPlayerManager sessionPlayerManager ) {
        _sessionPlayerManager = sessionPlayerManager;

        EventBus.Subscribe<OnPlayerPickCard>( HandlePlayerPickCard );
    }
    public void Dispose() {
        EventBus.Unsubscribe<OnPlayerPickCard>( HandlePlayerPickCard );
    }

    private void HandlePlayerPickCard( OnPlayerPickCard evt ) {
        var activePlayer = _sessionPlayerManager.GetActivePlayerByID( evt.playerID );
        activePlayer.CardDeck.Add( evt.CardSO );
    }
}
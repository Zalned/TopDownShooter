using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class CardManager : NetworkBehaviour {
    [SerializeField] private CardMenuView _cardPickView;

    private List<CardData> _registredCards = new();
    private int _maxCardsToChoose;

    private SessionPlayerManager _sessionPlayerManager;
    private CardListGenerator _cardListGenerator = new();

    public override void OnNetworkSpawn( ) {
        _registredCards = CardDataManager.GetCardDatas();
    }

    public void Initialize( SessionPlayerManager sessionPlayerManager ) {
        _sessionPlayerManager = sessionPlayerManager;

        var sessionConfig = Resources.Load<SessionConfigSO>( Defines.ConfigPaths.SESSION );
        _maxCardsToChoose = sessionConfig.CountCardToChoose;

        if ( IsServer ) {
            EventBus.Subscribe<PlayerCardPickEvent>( HandlePlayerChoseCardFromServer );
        }
    }

    public override void OnDestroy() {
        if ( IsServer ) {
            EventBus.Unsubscribe<PlayerCardPickEvent>( HandlePlayerChoseCardFromServer );
        }
    }

    public CardData GetRegistredCardByID( int id ) {
        return _registredCards[ id ];
    }

    public List<CardData> GetRegistredCardsByIDs( int[] ids ) {
        List<CardData> cards = new();
        if ( ids == null ) { return cards; }

        foreach ( var id in ids ) {
            cards.Add( GetRegistredCardByID( id ) );
        }
        return cards;
    }

    public void StartCardChooseFromServer() {
        HandleStartCardChooseClientRpc();
        HandleStartCardChooseServerRpc();
    }

    [ClientRpc]
    private void HandleStartCardChooseClientRpc() {
        _cardPickView.Show();
    }

    [ServerRpc]
    private void HandleStartCardChooseServerRpc() {
        var activePlayersIDs = _sessionPlayerManager.ActivePlayers.Keys;
        var losePlayersIDs = _sessionPlayerManager.LosePlayers.Keys;

        foreach ( var activePlayerID in activePlayersIDs ) {
            if ( _sessionPlayerManager.LosePlayers.Keys.Contains( activePlayerID ) ) {

                var cardIds = _cardListGenerator.Generate( _maxCardsToChoose, _registredCards.Count );
                SendCardsIDsTargetClientRpc( activePlayerID, cardIds );
                HandleLosePlayersStartChooseCardsTargetClientRpc( activePlayerID );

            } else {
                // Выполняем ту же логику что для игрока выбравшего карточку
                HandlePlayerChoseCardTargetClientRpc( activePlayerID );
            }
        }
    }

    [ClientRpc]
    private void SendCardsIDsTargetClientRpc( ulong playerID, int[] cardDataIds ) {
        if ( NetcodeHelper.LocalClientId == playerID ) {
            _cardPickView.InstallRandomCards( cardDataIds );
        }
    }

    [ClientRpc]
    private void HandleLosePlayersStartChooseCardsTargetClientRpc( ulong playerID ) {
        if ( NetcodeHelper.LocalClientId == playerID ) {
            _cardPickView.ShowPickMenu();
        }
    }

    private void HandlePlayerChoseCardFromServer( PlayerCardPickEvent evt ) {
        _sessionPlayerManager.AddPlayerWhoChoseCard( evt.playerID );

        if ( _sessionPlayerManager.IsAllLosePlayersChoseCard ) {
            HandleAllPlayersChoseCardClientRpc();
            HandleAllPlayersChoseCardServer();
        } else {
            HandlePlayerChoseCardTargetClientRpc( evt.playerID );
        }
    }

    private void HandleAllPlayersChoseCardServer() {
        _sessionPlayerManager.ClearPlayersWhoChoseCard();
    }

    [ClientRpc]
    private void HandleAllPlayersChoseCardClientRpc() {
        _cardPickView.Hide();
        _cardPickView.HideWaitingMenu();
        _cardPickView.HidePickMenu();
        _cardPickView.ClearCards();
    }

    [ClientRpc]
    private void HandlePlayerChoseCardTargetClientRpc( ulong playerID ) {
        if ( NetcodeHelper.LocalClientId == playerID ) {
            _cardPickView.HidePickMenu();
            _cardPickView.ShowWaitingMenu();
        }
    }
}
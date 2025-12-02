using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CardManager : NetworkBehaviour {
    [SerializeField] private CardPickView _cardPickView;
    [Space]
    [SerializeField] private List<GameObject> _cards = new();

    private int _maxCards;
    private CardListGenerator _cardListGenerator = new();

    private void Awake() {
        Debug.Log( "CardManager Awake" );
        NumberCards();
        var sessionConfig = Resources.Load<SessionConfigSO>( Defines.ConfigPaths.SESSION_CONFIG );
        _maxCards = sessionConfig.CountCardToChoose;

        if ( IsServer ) {
            Debug.Log( "CardManager subscribes on server" );
            EventBus.Subscribe<OnPlayerPickCard>( OnPlayerPickCard );
            DebugEvents.OnShowCardPickViewBtn += HandlePickCardClientRpc; // DEBUG
        } else {
            Debug.Log( "CardManager IS NOT SERVER" );
        }
    }

    public override void OnDestroy() {
        if ( IsServer ) {
            EventBus.Unsubscribe<OnPlayerPickCard>( OnPlayerPickCard );
            DebugEvents.OnShowCardPickViewBtn -= HandlePickCardClientRpc; // DEBUG
        }
    }

    [ClientRpc]
    private void HandlePickCardClientRpc() {
        Debug.Log( "HandlePickCardClientRpc" );
        RequestCardListServerRpc( NetcodeHelper.LocalClientId );
        _cardPickView.Show();
    }

    [Rpc( SendTo.Server, InvokePermission = RpcInvokePermission.Everyone )]
    public void RequestCardListServerRpc( ulong clientId ) {
        Debug.Log( "RequestCardListServerRpc" );
        int[] ids = _cardListGenerator.Generate( _maxCards );
        SendCardListClientRpc( ids, clientId );
    }

    [ClientRpc]
    private void SendCardListClientRpc( int[] ids, ulong targetClientId ) {
        Debug.Log( "SendCardListClientRpc" );
        if ( NetcodeHelper.LocalClientId != targetClientId ) return;
        _cardPickView.ShowCards( ids );
    }


    // MyNote: Предполагается, что метод вызывается на сервере
    private void OnPlayerPickCard( OnPlayerPickCard evt ) {
        OnPlayerPickCardClientRpc();
    }
    [ClientRpc]
    public void OnPlayerPickCardClientRpc() {
        _cardPickView.Hide();
        _cardPickView.ClearCards();
    }

    private void NumberCards() {
        for ( int i = 0; i < _cards.Count; i++ ) {
            var card = _cards[ i ].GetComponent<Card>();
            card.SetCardID( i );
        }
    }

    public Card GetCardByID( int id ) {
        return _cards[ id ].GetComponent<Card>();
    }
}
using System.Collections.Generic;
using UnityEngine;

public class ActivePlayerData {
    public int Score { get; private set; } = 0;
    public NetworkPlayerData NetworkPlayerData { get; private set; }
    public int[] CardDeckIDs { get; private set; }
    public List<CardSO> CardDeck { get; private set; } = new();

    public ActivePlayerData( NetworkPlayerData data ) {
        NetworkPlayerData = data;
    }

    //public void AddCard( CardSO card ) {
    //    Debug.Log( $"Adding card {card.Name} to player {NetworkPlayerData.Name}" );
    //    CardDeck.Add( card );
    //}

    public void AddScore() {
        Score++;
    }
}
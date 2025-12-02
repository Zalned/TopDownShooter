using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CardListGenerator : NetworkBehaviour {
    public int[] Generate( int numberOfCards ) {
        var cardIDs = new int[] { numberOfCards };
        for ( int i = 0; i < numberOfCards; i++ ) {
            cardIDs[ i ] = Random.Range( 0, numberOfCards );
        }
        return cardIDs;
    }
}
using System.Collections.Generic;
using UnityEngine;

public class CardListGenerator {
    public int[] Generate( int cardsCountToChoose, int cardsCount ) {
        if ( cardsCountToChoose <= 0 ) {
            throw new System.ArgumentOutOfRangeException( nameof( cardsCountToChoose ), "Must be > 0" );
        }

        if ( cardsCountToChoose > cardsCount ) {
            Debug.LogWarning( $"[{nameof( CardListGenerator )}] Cards to choose > registered cards" );
            cardsCountToChoose = cardsCount;
        }

        var result = new int[ cardsCountToChoose ];
        var used = new HashSet<int>();

        int index = 0;
        while ( index < cardsCountToChoose ) {
            int randomID = Random.Range( 0, cardsCount );
            if ( used.Add( randomID ) ) {
                result[ index ] = randomID;
                index++;
            }
        }

        return result;
    }
}
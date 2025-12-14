using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardStatsTextInitializer {
    public void InitializeCardStatTexts(
        List<CardStat> cardStats,
        Transform textParent,
        TextMeshProUGUI textExample ) {

        foreach ( var stat in cardStats ) {
            var statText = Object.Instantiate( textExample, textParent ); 

            statText.gameObject.SetActive( true );
            statText.text = stat.StatText;

            if ( stat.IsPositiveMod ) {
                statText.color = Color.green;
            } else {
                statText.color = Color.red;
            }
        }
    }
}
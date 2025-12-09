using System.Collections.Generic;

public static class CardDataManager {
    private static CardDatas _cardDatas = new();

    public static List<CardData> GetCardDatas() {
        List<CardData> cards = new();

        foreach ( var cardData in _cardDatas.Cards ) {
            if ( !cardData.Enabled ) { continue; }
            cards.Add( cardData );
        }

        return cards;
    }
}

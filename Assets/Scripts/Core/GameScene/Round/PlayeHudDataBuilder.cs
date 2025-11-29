using Unity.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHudDataBuilder {
    public HudPlayerDataArray GetHudData( List<ActivePlayerData> activePlayerDatas ) {
        int count = activePlayerDatas.Count;
        HudPlayerData[] data = new HudPlayerData[ count ];

        int i = 0;
        foreach ( var player in activePlayerDatas ) {
            data[ i ].PlayerText = new FixedString64Bytes( $"{player.NetworkPlayerData.Name} - {player.Score}" );
            data[ i ].PlayerColor = player.NetworkPlayerData.TeamColor;
            i++;
            Debug.Log( $"Builded data: {player.NetworkPlayerData.Name}" );
        }
        return new HudPlayerDataArray { Data = data };
    }
}
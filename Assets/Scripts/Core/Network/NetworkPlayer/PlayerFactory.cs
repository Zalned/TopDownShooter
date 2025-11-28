using UnityEngine;

public class PlayerFactory {
    public NetworkPlayerData CreatePlayer( ulong clientId, PlayerConnectionPayload payloadData) {
        Color ramdomPlayerColor = GetRandomColor();

        return new NetworkPlayerData( clientId, ramdomPlayerColor, payloadData.PlayerName );
    }

    private Color GetRandomColor() { // TODO Временно рандомный
        return new Color( Random.Range( 0f, 1f ), Random.Range( 0f, 1f ), Random.Range( 0f, 1f ) );
    }
}
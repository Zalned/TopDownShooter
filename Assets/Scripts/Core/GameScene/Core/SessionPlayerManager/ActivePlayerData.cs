using System.Collections.Generic;

public class ActivePlayerData {
    public int Score { get; private set; } = 0;
    public NetworkPlayerData NetworkPlayerData { get; private set; }
    public int[] CardDeckIDs { get; private set; }

    public ActivePlayerData( NetworkPlayerData data ) {
        NetworkPlayerData = data;
    }

    public void AddScore() {
        Score++;
    }
}
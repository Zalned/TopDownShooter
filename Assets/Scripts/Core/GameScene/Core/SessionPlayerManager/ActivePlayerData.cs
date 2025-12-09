using System.Collections.Generic;

public class ActivePlayerData {
    public int Score { get; private set; } = 0;
    public NetworkPlayerData NetworkPlayerData { get; private set; }
    public List<int> CardDeckIDs { get; private set; } = new();

    public ActivePlayerData( NetworkPlayerData data ) {
        NetworkPlayerData = data;
    }

    public void AddScore() {
        Score++;
    }
}
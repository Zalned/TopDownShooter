public class ActivePlayerData {
    public int Score { get; private set; } = 0;
    public NetworkPlayerData NetworkPlayerData { get; private set; }

    public ActivePlayerData( NetworkPlayerData data ) {
        NetworkPlayerData = data;
    }

    public void AddScore() {
        Score++;
    }
}
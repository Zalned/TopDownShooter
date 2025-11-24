using System;

public static class NetworkEvents {
    public static Action OnNetworkStarted;

    public static Action<NetworkPlayerData> OnPlayerJoined; 
    public static Action<NetworkPlayerData> OnPlayerLeft;

    public static Action<ulong> OnClientConnected;
    public static Action<ulong> OnClientDisconnected;

    public static Action<string> StartHostRequest;
    public static Action StopHostRequest;

    public static Action<string> StartClientRequest;

    public static Action OnHostStarted;

    public static Action<string, Exception> OnNetworkError;
}
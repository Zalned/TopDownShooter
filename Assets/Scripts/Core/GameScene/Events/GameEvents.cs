using System;
using UnityEngine;

public static class GameEvents {
    public static Action OnStartBtn;
    public static Action OnStopGameBtn;
    public static Action OnQuitToMenuBtn;

    public static Action OnGameStarted;
    public static Action OnGameStopped;

    public static Action<ulong, GameObject> OnPlayerDie;

    public static Action<ulong> OnPlayerWinGame;
}
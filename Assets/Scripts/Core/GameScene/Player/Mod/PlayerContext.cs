using UnityEngine;

public class PlayerContext {
    public GameObject GO;
    public PlayerRuntimeStats Stats;

    public PlayerContext( GameObject go, PlayerRuntimeStats stats ) {
        GO = go;
        Stats = stats;
    }
}
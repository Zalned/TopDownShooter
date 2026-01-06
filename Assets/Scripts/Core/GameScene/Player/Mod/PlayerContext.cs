using UnityEngine;

public class PlayerContext {
    public GameObject GO;
    public PlayerModel Model;

    public PlayerContext( GameObject go, PlayerModel model ) {
        GO = go;
        Model = model;
    }

    public PlayerStats Stats => Model.PlayerStats;

    public void AddHealth( float value ) {
        Model.CurrentHealth.Value += value;
    }
}
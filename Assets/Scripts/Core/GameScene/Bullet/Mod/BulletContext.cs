using UnityEngine;

public class BulletContext {
    public GameObject GO;
    public BulletRuntimeStats Stats;

    public BulletContext( GameObject go, BulletRuntimeStats stats, CardContext _cardCtx) {
        GO = go;
        Stats = stats;
    }
}
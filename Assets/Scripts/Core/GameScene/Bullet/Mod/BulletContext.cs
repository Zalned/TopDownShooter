using UnityEngine;

public class BulletContext {
    public GameObject GO;
    public BulletRuntimeStats Stats;

    public BulletContext( GameObject go, BulletRuntimeStats stats) {
        GO = go;
        Stats = stats;
    }
}
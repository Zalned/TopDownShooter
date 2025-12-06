using UnityEngine;

public interface IBulletMod : ISimpleMod {
    void OnSpawn() { }
    void OnTick() { }
    void OnHit( Transform hit ) { }
    void OnDestroy() { }
}
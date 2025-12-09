using UnityEngine;

public interface IBulletMod : IMod {
    void OnSpawn() { }
    void OnTick() { }
    void OnHit( Transform hit ) { }
    void OnDestroy() { }
}
public interface IBulletMod : IMod {
    void OnSpawn() { }
    void OnTick() { }
    void OnHit( BulletHitContext bulletHitContext ) { }
    void OnDestroy() { }
}
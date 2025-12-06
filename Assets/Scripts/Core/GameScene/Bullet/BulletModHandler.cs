using UnityEngine;

public class BulletModHandler {
    private BulletContext _ctx;
    private BulletRuntimeStats _config;

    public BulletModHandler( BulletContext ctx ) {
        _config = ctx.Stats;
    }

    public void OnSpawn() {
        foreach ( var mod in _config.Mods ) { mod.OnSpawn(); }
    }

    public void OnTick() {
        foreach ( var mod in _config.Mods ) { mod.OnTick(); }
    }

    public void OnHit( Transform hit ) {
        foreach ( var mod in _config.Mods ) { mod.OnHit( hit ); }
    }

    public void OnDestroy() {
        foreach ( var mod in _config.Mods ) { mod.OnDestroy(); }
    }
}
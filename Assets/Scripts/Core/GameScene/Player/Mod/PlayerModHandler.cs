public class PlayerModHandler {
    PlayerRuntimeStats _config;

    public PlayerModHandler( PlayerContext ctx ) {
        _config = ctx.Stats.RuntimeConfig.Player;
    }

    public void OnSpawn() {
        foreach ( var mod in _config.Mods ) { mod.OnInitialize(); }
    }

    public void OnTick() {
        foreach ( var mod in _config.Mods ) { mod.OnTick(); }
    }

    public void OnDamage() {
        foreach ( var mod in _config.Mods ) { mod.OnDamage(); }
    }

    public void OnShot() {
        foreach ( var mod in _config.Mods ) { mod.OnShoot(); }
    }

    public void OnDash() {
        foreach ( var mod in _config.Mods ) { mod.OnDash(); }
    }
}
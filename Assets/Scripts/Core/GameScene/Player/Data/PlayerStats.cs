using UnityEngine;

public class PlayerStats {
    public PlayerRuntimeConfig RuntimeConfig { get; private set; }

    public PlayerStats() {
        var basePlayerConfig = Resources.Load<BasePlayerConfigSO>( Defines.ConfigPaths.PLAYER );
        var baseBulletConfig = Resources.Load<BaseBulletConfigSO>( Defines.ConfigPaths.BULLET );
        RuntimeConfig = new PlayerRuntimeConfig( basePlayerConfig, baseBulletConfig );
    }
}
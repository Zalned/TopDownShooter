[System.Serializable]
public class PlayerRuntimeConfig {
    public PlayerRuntimeStats Player { get; private set; }
    public BulletRuntimeStats Bullet { get; private set; }

    public PlayerRuntimeConfig( BasePlayerConfigSO basePlayerCfg, BaseBulletConfigSO baseBulletCfg ) {
        Player = new( basePlayerCfg );
        Bullet = new( baseBulletCfg );
    }
}
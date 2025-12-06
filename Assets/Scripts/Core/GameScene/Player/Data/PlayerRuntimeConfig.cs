[System.Serializable]
public class PlayerRuntimeConfig {
    public PlayerRuntimeStats Player { get; private set; } = new();
    public BulletRuntimeStats Bullet { get; private set; } = new();
    public void SetPlayerRuntimeStats( PlayerRuntimeStats stats ) => Player = stats;
    public void SetBulletRuntimeStats( BulletRuntimeStats stats ) => Bullet = stats;

    public PlayerRuntimeConfig( BasePlayerConfigSO basePlayerCfg, BaseBulletConfigSO baseBulletCfg ) {
        Player.SetStats( basePlayerCfg );
        Bullet.SetStats( baseBulletCfg );
    }
}
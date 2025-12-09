public static class Defines {
    public static class SceneNames {
        public const string MainMenu = "MainMenu";
        public const string GameScene = "GameScene";
    }

    public static class ObjectPaths {
        public const string PLAYER_PREFAB = "Prefabs/Player/Player";
        public const string BULLET_PREFAB = "Prefabs/Bullet/Bullet";
        public const string CLIENT_BULLET_PREFAB = "Prefabs/Bullet/ClientBullet";
        public const string PLAYER_CAMERA_PREFAB = "Prefabs/Camera/PlayerCamera";
        public const string CARD_PREFAB = "Prefabs/Card/Card";
    }

    public static class ConfigPaths {
        public const string SESSION_CONFIG = "Configs/SessionConfig";
        public const string BULLET_CONFIG = "Configs/BulletConfig";
        public const string PLAYER_CONFIG = "Configs/PlayerConfig";
        public const string SETTINGS_CONFIG = "Configs/SettingsConfig";
    }

    public static class Tags {
        public const string Player = "Player";
        public const string Enemy = "Enemy";
        public const string Bullet = "Bullet";
    }

    public static class Layers {
        public const string Environment = "Environment";
        public const string PlayerObject = "PlayerObject";
        public const string Bullet = "Bullet";
    }
}
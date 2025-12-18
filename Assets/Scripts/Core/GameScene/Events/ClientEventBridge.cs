public static class ClientEventBridge {
    public static void RaisePlayerSpawned() {
        EventBus.Publish( new PlayerSpawnedClientEvent() );
    }
}

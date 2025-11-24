public static class HostDataTransfer {
    public static PlayerConnectionPayload HostData {  get; private set; }

    public static void SetHostData(PlayerConnectionPayload data ) {
        HostData = data;
    }
}
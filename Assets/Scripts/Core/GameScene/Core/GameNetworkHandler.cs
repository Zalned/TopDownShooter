using Unity.Netcode;

public class GameNetworkHandler : NetworkBehaviour {
    private SceneLoader _sceneLoader;

    public void Initialize( SceneLoader sceneLoader ) {
        _sceneLoader = sceneLoader;
    }

    private void Awake() {
        if ( !NetcodeHelper.IsServer ) { this.enabled = false; return; }
    }

    [ClientRpc]
    public void QuitToMenuClientRpc() {
        _sceneLoader.LoadMenuScene();
    }
}

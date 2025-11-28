using UnityEngine;
using Unity.Netcode;
using Zenject;

public class GameNetworkHandler : NetworkBehaviour {
    private SceneLoader _sceneLoader;

    public void Initialize( SceneLoader sceneLoader ) {
        _sceneLoader = sceneLoader;
    }

    private void Awake() {
        if ( !NetcodeHelper.IsServer ) { this.enabled = false; return; }
    }

    public void QuitToMenuForClients() {
        if ( NetcodeHelper.IsHost || NetcodeHelper.IsServer ) {
            QuitToMenuClientRpc();
        }
    }

    [ClientRpc]
    private void QuitToMenuClientRpc() {
        _sceneLoader.LoadMenuScene();
    }
}

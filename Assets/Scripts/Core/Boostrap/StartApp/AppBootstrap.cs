using UnityEngine;
using Zenject;

public class AppBootstrap : MonoBehaviour {
    private void Awake() {
        ProjectContext.Instance.Container.Resolve<SceneLoader>().LoadMenuScene();
    }
}
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller {
    [SerializeField] private SettingsView_Client _settingsView_Client;

    public override void InstallBindings() {
        Container.Bind<NetworkLifecycleService>().AsSingle().NonLazy();
        Container.Bind<ConnectionService>().AsSingle().NonLazy();
        Container.Bind<ConnectionHandler>().AsSingle().NonLazy();
        Container.Bind<ConnectionErrorHandler>().AsSingle().NonLazy();

        Container.Bind<SettingsService_Client>().AsSingle().WithArguments( _settingsView_Client );
        Container.Bind<SceneLoader>().AsSingle().NonLazy();

        Container.BindInterfacesAndSelfTo<ProjectBootstrap>().AsSingle().NonLazy();
    }
}
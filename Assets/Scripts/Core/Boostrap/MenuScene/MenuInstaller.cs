using UnityEngine;
using Zenject;

public class MenuInstaller : MonoInstaller {
    [SerializeField] private MainMenuView _mainMenuView;
    [SerializeField] private HostMenuView _hostMenuView;
    [SerializeField] private JoinMenuView _joinMenuView;
    [SerializeField] private InputNameView _inputNameView;

    public override void InstallBindings() {
        Container.Bind<MenuConnectionStarter>().AsSingle();

        Container.Bind<MainMenuView>().FromInstance( _mainMenuView ).AsSingle();
        Container.Bind<HostMenuView>().FromInstance( _hostMenuView ).AsSingle();
        Container.Bind<JoinMenuView>().FromInstance( _joinMenuView ).AsSingle();
        Container.Bind<InputNameView>().FromInstance( _inputNameView ).AsSingle();

        Container.Bind<MainMenuController>().AsSingle().NonLazy();
        Container.Bind<JoinMenuController>().AsSingle();
        Container.Bind<HostMenuController>().AsSingle();
        Container.Bind<InputNameController>().AsSingle();

        Container.BindInterfacesAndSelfTo<MenuBootstrap_Zenject>().AsSingle();
    }
}
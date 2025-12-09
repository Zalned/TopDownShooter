public interface IPlayerMod : IMod {
    void OnInitialize();
    void OnTick();
    void OnDamage();
    void OnDash();
}
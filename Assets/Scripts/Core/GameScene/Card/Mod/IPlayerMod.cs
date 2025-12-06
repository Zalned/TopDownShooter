public interface IPlayerMod : ISimpleMod {
    void OnInitialize();
    void OnTick();
    void OnDamage();
    void OnDash();
}
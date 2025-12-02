public interface IEffect {
    void Install( PlayerStats stats, CardContext ctx );
    void Uninstall(); // Not used
    string Id { get; }
}
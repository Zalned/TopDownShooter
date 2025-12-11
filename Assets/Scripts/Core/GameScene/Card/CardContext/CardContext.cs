public class CardContext {
    public ExplosionService ExplosionService { get; private set; } = new();

    public CardContext( ) {
        ExplosionService = new ExplosionService();
    }
}
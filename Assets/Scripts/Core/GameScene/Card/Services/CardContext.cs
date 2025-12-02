using Zenject;

public class CardContext {
    public ExplosionService ExplosionService { get; private set; } = new();

    [Inject]
    public CardContext( ExplosionService explosionService ) {
        ExplosionService = explosionService;
    }
}

using System;

public class CardContext {
    public ExplosionService ExplosionService { get; private set; }
    public LifeStealService LifeStealService { get; private set; }

    public CardContext(  ) {
        ExplosionService = new ExplosionService();
        LifeStealService = new LifeStealService();
    }
}
using System;

public class CardContext {
    public ExplosionService ExplosionService { get; private set; }
    public LifeStealService LifeStealService { get; private set; }

    public CardContext(  ) {
        ExplosionService = new ExplosionService();
    }

    public void InitializeLifeStealService( Action<float> lifeStealCallback ) {
        LifeStealService = new LifeStealService( lifeStealCallback );
    }
}
using System;

public class LifeStealService {
    private Action<float> _lifeStealCallback;

    public LifeStealService( Action<float> lifeStealCallback ) {
        _lifeStealCallback = lifeStealCallback;
    }

    public void LifeSteal(float dmg) {
        _lifeStealCallback.Invoke( dmg );
    }
}
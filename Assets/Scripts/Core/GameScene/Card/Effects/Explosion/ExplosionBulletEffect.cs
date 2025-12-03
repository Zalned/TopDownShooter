using UnityEngine;

public class ExplosionOnHitEffect : IEffect {
    public string Id => "explosion";

    private CardContext _ctx;
    private PlayerStats _stats;

    private const float ExplosionMultiple = 2;

    public void Install( PlayerStats stats, CardContext ctx ) {
        _ctx = ctx;
        _stats = stats;
        CardProvideEvents.OnHit += OnHit;
    }

    private void OnHit( ServerBullet bullet, Collider hit ) { // MyTodo
        float damage = _stats.RuntimeConfig.Bullet.Damage;
        float explosionRadius = damage * ExplosionMultiple; // При комбинировании карточек радиус должен увеличиваться
        _ctx.ExplosionService.Explode( hit.gameObject.transform.position, explosionRadius, damage );
    }

    public void Uninstall() {
        CardProvideEvents.OnHit -= OnHit;
    }
}

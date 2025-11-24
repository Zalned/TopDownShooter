public interface ITickService {
    void Register( ITickable tickable );
    void Unregister( ITickable tickable );
    void Tick();
}

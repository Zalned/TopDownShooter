public interface ITickDispatcher {
    public void Init( ITickService tickService );
    public void Tick();
}
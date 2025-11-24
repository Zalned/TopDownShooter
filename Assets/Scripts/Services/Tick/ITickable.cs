public interface ITickable {
    public void RegisterIn( ITickService tickService ) { tickService.Register( this ); }

    void Tick() { }

    string DescribeTickable() => GetType().ToString();
}

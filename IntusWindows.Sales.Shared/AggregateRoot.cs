namespace IntusWindows.Sales.Shared;

public abstract class AggregateRoot
{
    private readonly List<IDomainEvent> Changes;

    public int Version { get; private set; }

    public AggregateRoot()
    {
        this.Changes = new List<IDomainEvent>();
    }
    public IEnumerable<IDomainEvent> GetChanges() => Changes.AsEnumerable();

    public void ClearChanges() => this.Changes.Clear();



    protected abstract void ChangeStateByUsingDomainEvent(IDomainEvent domainEvent);

    protected abstract void ValidateState();

    protected void ApplyDomainEvent(IDomainEvent domainEvent)
    {
        ChangeStateByUsingDomainEvent(domainEvent);
        ValidateState();
        this.Changes.Add(domainEvent);
        this.Version++;

    }

    public void Load(IEnumerable<IDomainEvent> history)
    {
        foreach (var item in history)
        {
            this.ApplyDomainEvent(item);
        }
        this.ClearChanges();
    }


}
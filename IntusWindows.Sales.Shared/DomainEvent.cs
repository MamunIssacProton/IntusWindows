using System;
namespace IntusWindows.Sales.Shared;

public class DomainEvent<T>
{
    private List<Action<T>> Actions { get; } = new();

    public void Register(Action<T> callback)
    {
        if (this.Actions.Exists(del => del.Method == callback.Method))
            return;

        this.Actions.Add(callback);
    }

    public void Publish(T args)
    {
        foreach (var item in Actions)
        {
            item.Invoke(args);
        }
    }

}


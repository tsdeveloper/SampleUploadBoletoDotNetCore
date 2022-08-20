namespace Core.Iterator;

public interface IIterator<T>
{
    T Next();
    T Current { get; }
    bool IsLeft();
}
internal  class Iterator<T> : IIterator<T>
{
    private readonly IAggregate<T> _aggregate;
    private int index = 0;

    public Iterator(IAggregate<T> aggregate)
    {
        _aggregate = aggregate;
    }
    
    public T Next()
    {
        index++;
        return (IsLeft() ? _aggregate[index] : default)!;
    }

    public T Current => _aggregate[index];

    public bool IsLeft() => index < _aggregate.Count;
}
using System.Collections;

namespace DepartureBoard.Misc;

public class DtoBuffer<T> : IEnumerable
{
    private readonly List<object> _items;

    public DtoBuffer() => _items = [];

    public DtoBuffer(IEnumerable<object> items) => _items = items.ToList();
    
    public IEnumerator GetEnumerator()
    {
        lock (_locker)
        {
            return _items.GetEnumerator();
        }
    }

    private readonly Lock _locker = new Lock();
    
    public void Add(object item)
    {
        lock (_locker)
        {
            _items.Add(item);
        }
    }

    public void Remove(object item)
    {
        lock (_locker)
        {
            _items.Remove(item);
        }
    }
    
    public object this[int i]
    {
        get
        {
            lock (_locker)
            {
                return _items[i];
            }
        }
        set
        {
            lock (_locker)
            {
                _items[i] = value;
            }
        }
    }

    public List<object> ToList()
    {
        lock (_locker)
        {
            return _items.ToList();
        }
    }
}
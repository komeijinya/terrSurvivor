using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;

public partial class WeightedDictionary<TKey,TValue> where TKey : notnull
{
    private readonly Dictionary<TKey, (TValue Value,double Weight)> _items;
    private double _totalWeight;
    private readonly Random _random;


    public double TotalWeight => _totalWeight;

    public int Count => _items.Count;

    public IEnumerable<TKey> Keys => _items.Keys;

    public IEnumerable<TValue> Values => _items.Values.Select(x => x.Value);

    public IEnumerable<(TValue,double Weight)> WeightedValues => _items.Values;

    public WeightedDictionary()
    {
        _items = new Dictionary<TKey, (TValue,double)>();
        _totalWeight = 0;
        _random = new Random();
    }

    public WeightedDictionary(int capacity)
    {
        _items = new Dictionary<TKey, (TValue,double)>(capacity);
        _totalWeight = 0;
        _random = new Random();
    }

    public void AddOrUpdate(TKey key ,TValue value , double weight)
    {
        if(weight <= 0)
        {
            return;
        }

        if(_items.TryGetValue(key,out var existingItem))
        {
            _totalWeight -= existingItem.Weight;
            _items[key] = (value,weight);
        }
        else
        {
            _items[key] = (value,weight);
        }

        _totalWeight += weight;
    }

    public bool Remove(TKey key)
    {
        if(_items.TryGetValue(key,out var item))
        {
            _totalWeight -= item.Weight;
            return _items.Remove(key);
        }

        return false;
    }

    public TValue GetRandom(IEnumerable<TValue> exclusions)
    {
        GD.Print(exclusions.Count());
        if(_items.Count == 0)
        {
            return default;
        }

        var exclusionSet = exclusions as HashSet<TValue> ?? new HashSet<TValue>(exclusions);
        var candidates = new List<(TValue Value,double Weight)>();
        double remainingTotalWeight = 0;

        foreach(var item in _items.Values)
        {
            if(!exclusionSet.Contains(item.Value))
            {
                candidates.Add(item);
                remainingTotalWeight += item.Weight;
            }
        }
        double randomValue = _random.NextDouble() * _totalWeight;
        double cumlativeWeight = 0;

        foreach (var candidate in candidates)
        {
            cumlativeWeight += candidate.Weight;
            if(randomValue <= cumlativeWeight)
            {
                return candidate.Value;
            }
        }

        return _items.Values.Last().Value;
    }

    public TValue GetRandom()
    {
        if(_items.Count == 0)
        {
            return default;
        }

        if(_items.Count == 1)
        {
            return _items.Values.First().Value;
        }

        double randomValue = _random.NextDouble() * _totalWeight;
        double cumlativeWeight = 0;

        foreach (var (Value,Weight) in _items.Values)
        {
            cumlativeWeight += Weight;
            if(randomValue <= cumlativeWeight)
            {
                return Value;
            }
        }

        return _items.Values.Last().Value;
    }

    public TValue GetValue(TKey key)
    {
        return _items.TryGetValue(key,out var item) ? item.Value : default;
    }

public WeightedDictionary<TKey, TValue> Clone()
{
    var clone = new WeightedDictionary<TKey, TValue>(_items.Count);
    
    foreach (var kvp in _items)
    {
        clone._items[kvp.Key] = kvp.Value;
    }
    
    clone._totalWeight = _totalWeight;
    return clone;
}
}
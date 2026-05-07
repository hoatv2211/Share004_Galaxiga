using System;
using System.Collections;
using System.Collections.Generic;

public class ReadOnlyDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
{
	public ReadOnlyDictionary(IDictionary<TKey, TValue> dictionary)
	{
		this._dictionary = dictionary;
	}

	void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
	{
		throw ReadOnlyDictionary<TKey, TValue>.ReadOnlyException();
	}

	public bool ContainsKey(TKey key)
	{
		return this._dictionary.ContainsKey(key);
	}

	public ICollection<TKey> Keys
	{
		get
		{
			return this._dictionary.Keys;
		}
	}

	bool IDictionary<TKey, TValue>.Remove(TKey key)
	{
		throw ReadOnlyDictionary<TKey, TValue>.ReadOnlyException();
	}

	public bool TryGetValue(TKey key, out TValue value)
	{
		return this._dictionary.TryGetValue(key, out value);
	}

	public ICollection<TValue> Values
	{
		get
		{
			return this._dictionary.Values;
		}
	}

	public TValue this[TKey key]
	{
		get
		{
			return this._dictionary[key];
		}
	}

	TValue IDictionary<TKey, TValue>.this[TKey key]
	{
		get
		{
			return this[key];
		}
		set
		{
			throw ReadOnlyDictionary<TKey, TValue>.ReadOnlyException();
		}
	}

	void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
	{
		throw ReadOnlyDictionary<TKey, TValue>.ReadOnlyException();
	}

	void ICollection<KeyValuePair<TKey, TValue>>.Clear()
	{
		throw ReadOnlyDictionary<TKey, TValue>.ReadOnlyException();
	}

	public bool Contains(KeyValuePair<TKey, TValue> item)
	{
		return this._dictionary.Contains(item);
	}

	public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
	{
		this._dictionary.CopyTo(array, arrayIndex);
	}

	public int Count
	{
		get
		{
			return this._dictionary.Count;
		}
	}

	public bool IsReadOnly
	{
		get
		{
			return true;
		}
	}

	bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
	{
		throw ReadOnlyDictionary<TKey, TValue>.ReadOnlyException();
	}

	public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
	{
		return this._dictionary.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.GetEnumerator();
	}

	private static Exception ReadOnlyException()
	{
		return new NotSupportedException("This dictionary is read-only");
	}

	private readonly IDictionary<TKey, TValue> _dictionary;
}

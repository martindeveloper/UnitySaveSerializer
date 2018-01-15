using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryBox<T>
{
	public T Value;
}

public class MemoryStorage : IEnumerable<KeyValuePair<string, object>>
{
	private Dictionary<string, object> Values;

	public MemoryStorage ()
	{
		Values = new Dictionary<string, object> ();
	}

	public void Clear ()
	{
		Values.Clear ();
	}

	public bool ContainsKey (string key)
	{
		return Values.ContainsKey (key);
	}

	public void Insert<T> (string key, T value)
	{
		Values [key] = (object)value;
	}

	public T GetValue<T> (string key) where T : struct
	{
		if (!ContainsKey (key)) {
			return default(T);
		}

		object rawObject = Values [key];

		return (T)rawObject;
	}

	public T Get<T> (string key) where T : class
	{
		if (!ContainsKey (key)) {
			return default(T);
		}

		object rawObject = Values [key];

		T typedObject = rawObject as T;

		if (typedObject != null) {
			return typedObject;
		}

		return default(T);
	}

	IEnumerator IEnumerable.GetEnumerator ()
	{  
		return GetEnumerator ();  
	}

	public IEnumerator<KeyValuePair<string, object>> GetEnumerator ()
	{  
		foreach (KeyValuePair<string, object> keyValue in Values) {
			yield return keyValue;
		}
	}
}

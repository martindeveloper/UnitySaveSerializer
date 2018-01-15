using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TestBench : MonoBehaviour
{
	private IObjectConverter Converter;
	private MemoryStorageTransformer Transformer;
	private MemoryStorage Storage;

	public void Start ()
	{
		Converter = new ObjectConverterBase64 ();
		Transformer = new MemoryStorageTransformer (Converter);

		Storage = new MemoryStorage ();

		Storage.Insert ("Key1", "string value");
		Storage.Insert ("Key2", 2.4f);
		Storage.Insert ("Key3", 1);
		Storage.Insert ("Key4", true);

		string transformedString = Transformer.MemoryStorageToString (Storage);

		Debug.LogFormat ("{0}\n{1}\n", "Transformed string:", transformedString);

		MemoryStorage transformedStorage = Transformer.StringToMemoryStorage (transformedString);

		Debug.Log (transformedStorage.Get<string> ("Key1"));
		Debug.Log (transformedStorage.GetValue<float> ("Key2"));
		Debug.Log (transformedStorage.GetValue<int> ("Key3"));
		Debug.Log (transformedStorage.GetValue<bool> ("Key4"));

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Text.RegularExpressions;

public class MemoryStorageTransformer
{
	private IObjectConverter Converter;

	public MemoryStorageTransformer (IObjectConverter converter)
	{
		Converter = converter;
	}

	public string MemoryStorageToString (MemoryStorage storage)
	{
		StringBuilder stringBuffer = new StringBuilder ();

		foreach (KeyValuePair<string, object> keyValue in storage) {
			stringBuffer.AppendFormat (@"{0}=""{1}"";", keyValue.Key, Converter.Serialize (keyValue.Value));
		}

		return stringBuffer.ToString ().TrimEnd (';');
	}

	public MemoryStorage StringToMemoryStorage (string source)
	{
		MemoryStorage memoryStorage = new MemoryStorage ();

		string[] pairs = source.Split (';');

		Regex keyValueMatcher = new Regex (@"(\w+)=""([\w\+\=\/\:]+)""");

		foreach (string pair in pairs) {
			MatchCollection matches = keyValueMatcher.Matches (pair);

			foreach (Match match in matches) {
				string key = match.Groups [1].Value;
				string rawValue = match.Groups [2].Value;
				object objectValue = Converter.Deserialize (rawValue);

				memoryStorage.Insert (key, objectValue);
			}
		}

		return memoryStorage;
	}
}

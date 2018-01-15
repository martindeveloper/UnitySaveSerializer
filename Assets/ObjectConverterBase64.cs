using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class ObjectConverterBase64 : IObjectConverter
{
	public string Serialize (object sourceObject)
	{
		byte[] bytes;
		long length = 0;
		MemoryStream memoryStream = new MemoryStream ();
		BinaryFormatter binaryFormatter = new BinaryFormatter ();

		binaryFormatter.Serialize (memoryStream, sourceObject);
		length = memoryStream.Length;
		bytes = memoryStream.GetBuffer ();

		string encodedData = bytes.Length + ":" + Convert.ToBase64String (bytes, 0, bytes.Length, Base64FormattingOptions.None);

		return encodedData;
	}

	public object Deserialize (string sourceString)
	{
		int delimiterPosition = sourceString.IndexOf (':');
		int length = Convert.ToInt32 (sourceString.Substring (0, delimiterPosition));

		byte[] memoryData = Convert.FromBase64String (sourceString.Substring (delimiterPosition + 1));
		MemoryStream memoryStream = new MemoryStream (memoryData, 0, length);
		BinaryFormatter binaryFormatter = new BinaryFormatter ();

		object rawObject = binaryFormatter.Deserialize (memoryStream);

		return rawObject;
	}
}

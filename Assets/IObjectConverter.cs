using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectConverter
{
	string Serialize (object sourceObject);

	object Deserialize (string sourceString);
}

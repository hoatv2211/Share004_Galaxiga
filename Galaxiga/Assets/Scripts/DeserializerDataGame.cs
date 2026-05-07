using System;
using UnityEngine;

public class DeserializerDataGame : MonoBehaviour
{
	private void Awake()
	{
		Deserializer.Deserialize(this.set);
	}

	public SerializableSet set;
}

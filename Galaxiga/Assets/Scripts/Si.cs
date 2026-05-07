using System;
using UnityEngine;

public class Si : MonoBehaviour
{
	private void Awake()
	{
		Deserializer.Deserialize(this.set);
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public SerializableSet set;
}

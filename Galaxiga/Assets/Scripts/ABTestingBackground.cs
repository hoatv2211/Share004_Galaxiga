using System;
using UnityEngine;

public class ABTestingBackground : MonoBehaviour
{
	private void Awake()
	{
		this.backgroundParallax.SetActive(true);
	}

	public GameObject backgroundParallax;
}

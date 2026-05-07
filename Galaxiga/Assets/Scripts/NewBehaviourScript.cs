using System;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		base.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y, 10f));
	}
}

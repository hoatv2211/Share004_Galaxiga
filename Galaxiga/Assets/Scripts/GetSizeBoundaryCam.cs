using System;
using UnityEngine;

public class GetSizeBoundaryCam : MonoBehaviour
{
	private void Awake()
	{
		this.mainCamera = base.GetComponent<Camera>();
	}

	private void Start()
	{
		this.orthorgrhicValue = this.mainCamera.orthographicSize;
		GameContext.orthorgrhicSize = this.orthorgrhicValue;
		GameContext.sizeBoudaryCam = this.orthorgrhicValue * (float)Screen.width / (float)Screen.height;
		UnityEngine.Debug.Log("size boudary cam " + GameContext.sizeBoudaryCam);
	}

	private void Update()
	{
	}

	private Camera mainCamera;

	private float orthorgrhicValue;
}

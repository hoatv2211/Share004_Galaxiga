using System;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
	private void Awake()
	{
		this.SetMatchWidthOrHeight();
		UnityEngine.Debug.Log("aspect " + this.mainCamera.aspect);
	}

	private void SetMatchWidthOrHeight()
	{
		if (this.mainCamera.aspect >= 0.62f)
		{
			for (int i = 0; i < this.canvasScalers.Length; i++)
			{
				this.canvasScalers[i].matchWidthOrHeight = 1f;
			}
		}
	}

	public Camera mainCamera;

	public CanvasScaler[] canvasScalers;
}

using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

public class MutliFingersScreenTouch : MonoBehaviour
{
	private void OnEnable()
	{
		EasyTouch.On_TouchStart += this.On_TouchStart;
	}

	private void OnDestroy()
	{
		EasyTouch.On_TouchStart -= this.On_TouchStart;
	}

	private void On_TouchStart(Gesture gesture)
	{
		if (gesture.pickedObject == null)
		{
			Vector3 touchToWorldPoint = gesture.GetTouchToWorldPoint(5f);
			UnityEngine.Object.Instantiate<GameObject>(this.touchGameObject, touchToWorldPoint, Quaternion.identity).GetComponent<FingerTouch>().InitTouch(gesture.fingerIndex);
		}
	}

	public GameObject touchGameObject;
}

using System;
using UnityEngine;

public class FXQ_RotateShapeParticle : MonoBehaviour
{
	private void Start()
	{
		base.transform.localEulerAngles = this.m_StartRotation;
	}

	private void Update()
	{
		base.transform.localEulerAngles = base.transform.localEulerAngles + this.m_RotationOvertime * Time.deltaTime;
	}

	public Vector3 m_StartRotation;

	public Vector3 m_RotationOvertime;
}

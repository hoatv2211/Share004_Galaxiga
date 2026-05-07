using System;
using UnityEngine;

[DisallowMultipleComponent]
public sealed class UbhTentacleBullet : MonoBehaviour
{
	private void Awake()
	{
		this.m_rootTransform = new GameObject("Root").GetComponent<Transform>();
		this.m_rootTransform.SetParent(base.transform, false);
		if (this.m_numOfTentacles < 2)
		{
			UnityEngine.Debug.LogWarning("NumOfTentacles need 2 or more.");
			return;
		}
		float num = 360f / (float)this.m_numOfTentacles;
		for (int i = 0; i < this.m_numOfTentacles; i++)
		{
			Quaternion rotation = Quaternion.identity;
			UbhUtil.AXIS axisMove = this.m_axisMove;
			if (axisMove != UbhUtil.AXIS.X_AND_Y)
			{
				if (axisMove == UbhUtil.AXIS.X_AND_Z)
				{
					rotation = Quaternion.Euler(new Vector3(0f, num * (float)i, 0f));
				}
			}
			else
			{
				rotation = Quaternion.Euler(new Vector3(0f, 0f, num * (float)i));
			}
			for (int j = 0; j < this.m_numOfBulletsForOneTentacle; j++)
			{
				Transform component = UnityEngine.Object.Instantiate<GameObject>(this.m_centerBullet, this.m_rootTransform).GetComponent<Transform>();
				UbhUtil.AXIS axisMove2 = this.m_axisMove;
				if (axisMove2 != UbhUtil.AXIS.X_AND_Y)
				{
					if (axisMove2 == UbhUtil.AXIS.X_AND_Z)
					{
						component.position += rotation * Vector3.forward * ((float)(j + 1) * this.m_distanceBetweenBullets);
					}
				}
				else
				{
					component.position += rotation * Vector3.up * ((float)(j + 1) * this.m_distanceBetweenBullets);
				}
			}
		}
		this.m_centerBullet.SetActive(this.m_enableCenterBullet);
	}

	public void UpdateRotate()
	{
		UbhUtil.AXIS axisMove = this.m_axisMove;
		if (axisMove != UbhUtil.AXIS.X_AND_Y)
		{
			if (axisMove == UbhUtil.AXIS.X_AND_Z)
			{
				this.m_rootTransform.AddEulerAnglesY(-this.m_rotationSpeed * UbhSingletonMonoBehavior<UbhTimer>.instance.deltaTime);
			}
		}
		else
		{
			this.m_rootTransform.AddEulerAnglesZ(this.m_rotationSpeed * UbhSingletonMonoBehavior<UbhTimer>.instance.deltaTime);
		}
	}

	public GameObject m_centerBullet;

	[Range(2f, 360f)]
	public int m_numOfTentacles = 3;

	[Range(1f, 50f)]
	public int m_numOfBulletsForOneTentacle = 3;

	public float m_distanceBetweenBullets = 0.5f;

	public bool m_enableCenterBullet = true;

	public UbhUtil.AXIS m_axisMove;

	public float m_rotationSpeed = 90f;

	private Transform m_rootTransform;
}

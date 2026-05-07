using System;
using SkyGameKit;
using UnityEngine;

public class BulletDrone : MonoBehaviour
{
	private void Awake()
	{
		this.sgkBullet = base.GetComponent<SgkBullet>();
	}

	public void Start()
	{
		this.sgkBullet.power = this.droneData.CurrentDamage;
	}

	public DroneData droneData;

	private SgkBullet sgkBullet;
}

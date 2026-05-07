using System;
using UnityEngine;

public class ItemTurret : MonoBehaviour
{
	public ItemTurret.TurretType turretType;

	public enum TurretType
	{
		Bullet,
		Laser,
		HomingMissile
	}
}

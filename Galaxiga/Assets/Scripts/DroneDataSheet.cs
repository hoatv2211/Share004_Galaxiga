using System;
using System.Collections.Generic;

[Serializable]
public class DroneDataSheet
{
	public static DroneDataSheet Get(int droneID)
	{
		return DroneDataSheet.dictionary[droneID];
	}

	public static Dictionary<int, DroneDataSheet> GetDictionary()
	{
		return DroneDataSheet.dictionary;
	}

	public int droneID;

	public string droneName;

	public int power;

	public int specPower;

	public float durationSkill;

	public float fireRate;

	public float fireRateSkill;

	public float cooldownSkill;

	public string rank;

	public int numberCardCraft;

	public int numberBulletMainGun;

	public int accurateMainGun;

	private static Dictionary<int, DroneDataSheet> dictionary = new Dictionary<int, DroneDataSheet>();
}

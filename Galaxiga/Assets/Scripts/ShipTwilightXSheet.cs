using System;
using System.Collections.Generic;

[Serializable]
public class ShipTwilightXSheet
{
	public static ShipTwilightXSheet Get(int power)
	{
		return ShipTwilightXSheet.dictionary[power];
	}

	public static Dictionary<int, ShipTwilightXSheet> GetDictionary()
	{
		return ShipTwilightXSheet.dictionary;
	}

	public int power;

	public int mainDamage;

	public float mainFireRate;

	public int subDamage;

	public float subFireRate;

	public int numberBulletMainGun;

	public float accurateMainGun;

	public int numberBulletSubGun1;

	public float accurateSubGun1;

	private static Dictionary<int, ShipTwilightXSheet> dictionary = new Dictionary<int, ShipTwilightXSheet>();
}

using System;
using System.Collections.Generic;

[Serializable]
public class ShipBataFD01Sheet
{
	public static ShipBataFD01Sheet Get(int power)
	{
		return ShipBataFD01Sheet.dictionary[power];
	}

	public static Dictionary<int, ShipBataFD01Sheet> GetDictionary()
	{
		return ShipBataFD01Sheet.dictionary;
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

	private static Dictionary<int, ShipBataFD01Sheet> dictionary = new Dictionary<int, ShipBataFD01Sheet>();
}

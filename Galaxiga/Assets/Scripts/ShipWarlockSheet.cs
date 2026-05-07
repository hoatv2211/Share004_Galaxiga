using System;
using System.Collections.Generic;

[Serializable]
public class ShipWarlockSheet
{
	public static ShipWarlockSheet Get(int power)
	{
		return ShipWarlockSheet.dictionary[power];
	}

	public static Dictionary<int, ShipWarlockSheet> GetDictionary()
	{
		return ShipWarlockSheet.dictionary;
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

	private static Dictionary<int, ShipWarlockSheet> dictionary = new Dictionary<int, ShipWarlockSheet>();
}

using System;
using System.Collections.Generic;

[Serializable]
public class ShipGreataxeSheet
{
	public static ShipGreataxeSheet Get(int power)
	{
		return ShipGreataxeSheet.dictionary[power];
	}

	public static Dictionary<int, ShipGreataxeSheet> GetDictionary()
	{
		return ShipGreataxeSheet.dictionary;
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

	private static Dictionary<int, ShipGreataxeSheet> dictionary = new Dictionary<int, ShipGreataxeSheet>();
}

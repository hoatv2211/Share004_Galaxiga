using System;
using System.Collections.Generic;

[Serializable]
public class ShipSkywraithSheet
{
	public static ShipSkywraithSheet Get(int power)
	{
		return ShipSkywraithSheet.dictionary[power];
	}

	public static Dictionary<int, ShipSkywraithSheet> GetDictionary()
	{
		return ShipSkywraithSheet.dictionary;
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

	private static Dictionary<int, ShipSkywraithSheet> dictionary = new Dictionary<int, ShipSkywraithSheet>();
}

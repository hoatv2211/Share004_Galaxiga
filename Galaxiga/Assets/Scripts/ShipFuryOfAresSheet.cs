using System;
using System.Collections.Generic;

[Serializable]
public class ShipFuryOfAresSheet
{
	public static ShipFuryOfAresSheet Get(int power)
	{
		return ShipFuryOfAresSheet.dictionary[power];
	}

	public static Dictionary<int, ShipFuryOfAresSheet> GetDictionary()
	{
		return ShipFuryOfAresSheet.dictionary;
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

	private static Dictionary<int, ShipFuryOfAresSheet> dictionary = new Dictionary<int, ShipFuryOfAresSheet>();
}

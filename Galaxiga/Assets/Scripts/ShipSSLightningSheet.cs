using System;
using System.Collections.Generic;

[Serializable]
public class ShipSSLightningSheet
{
	public static ShipSSLightningSheet Get(int power)
	{
		return ShipSSLightningSheet.dictionary[power];
	}

	public static Dictionary<int, ShipSSLightningSheet> GetDictionary()
	{
		return ShipSSLightningSheet.dictionary;
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

	private static Dictionary<int, ShipSSLightningSheet> dictionary = new Dictionary<int, ShipSSLightningSheet>();
}

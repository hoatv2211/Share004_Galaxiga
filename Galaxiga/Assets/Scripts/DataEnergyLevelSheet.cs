using System;
using System.Collections.Generic;

[Serializable]
public class DataEnergyLevelSheet
{
	public static DataEnergyLevelSheet Get(int level)
	{
		return DataEnergyLevelSheet.dictionary[level];
	}

	public static Dictionary<int, DataEnergyLevelSheet> GetDictionary()
	{
		return DataEnergyLevelSheet.dictionary;
	}

	public int level;

	public int energyEasy;

	public int energyNormal;

	public int energyHard;

	public int backupEnergy;

	private static Dictionary<int, DataEnergyLevelSheet> dictionary = new Dictionary<int, DataEnergyLevelSheet>();
}

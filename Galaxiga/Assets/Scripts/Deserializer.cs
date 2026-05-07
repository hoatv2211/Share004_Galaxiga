using System;

public class Deserializer
{
	public static void Deserialize(SerializableSet set)
	{
		int i = 0;
		int num = set.LevelHardEnemys.Length;
		while (i < num)
		{
			LevelHardEnemySheet.GetDictionary().Add(set.LevelHardEnemys[i].id, set.LevelHardEnemys[i]);
			i++;
		}
		int j = 0;
		int num2 = set.ShopEndlessModes.Length;
		while (j < num2)
		{
			ShopEndlessModeSheet.GetDictionary().Add(set.ShopEndlessModes[j].id, set.ShopEndlessModes[j]);
			j++;
		}
		int k = 0;
		int num3 = set.QuestEndlessModes.Length;
		while (k < num3)
		{
			QuestEndlessModeSheet.GetDictionary().Add(set.QuestEndlessModes[k].idQuest, set.QuestEndlessModes[k]);
			k++;
		}
		int l = 0;
		int num4 = set.RewardOnedTimeEndlessModes.Length;
		while (l < num4)
		{
			RewardOnedTimeEndlessModeSheet.GetDictionary().Add(set.RewardOnedTimeEndlessModes[l].wave, set.RewardOnedTimeEndlessModes[l]);
			l++;
		}
		int m = 0;
		int num5 = set.DataEnemy1s.Length;
		while (m < num5)
		{
			DataEnemy1Sheet.GetDictionary().Add(set.DataEnemy1s[m].id, set.DataEnemy1s[m]);
			m++;
		}
		int n = 0;
		int num6 = set.DataBoxs.Length;
		while (n < num6)
		{
			DataBoxSheet.GetDictionary().Add(set.DataBoxs[n].idReward, set.DataBoxs[n]);
			n++;
		}
		int num7 = 0;
		int num8 = set.ShopEnergyDatas.Length;
		while (num7 < num8)
		{
			ShopEnergyDataSheet.GetDictionary().Add(set.ShopEnergyDatas[num7].energyID, set.ShopEnergyDatas[num7]);
			num7++;
		}
		int num9 = 0;
		int num10 = set.DataEnergyLevels.Length;
		while (num9 < num10)
		{
			DataEnergyLevelSheet.GetDictionary().Add(set.DataEnergyLevels[num9].level, set.DataEnergyLevels[num9]);
			num9++;
		}
		int num11 = 0;
		int num12 = set.ShopGemDatas.Length;
		while (num11 < num12)
		{
			ShopGemDataSheet.GetDictionary().Add(set.ShopGemDatas[num11].packID, set.ShopGemDatas[num11]);
			num11++;
		}
		int num13 = 0;
		int num14 = set.ShopCoinDatas.Length;
		while (num13 < num14)
		{
			ShopCoinDataSheet.GetDictionary().Add(set.ShopCoinDatas[num13].packID, set.ShopCoinDatas[num13]);
			num13++;
		}
		int num15 = 0;
		int num16 = set.DroneCostUpgrades.Length;
		while (num15 < num16)
		{
			DroneCostUpgradeSheet.GetDictionary().Add(set.DroneCostUpgrades[num15].level, set.DroneCostUpgrades[num15]);
			num15++;
		}
		int num17 = 0;
		int num18 = set.DroneDatas.Length;
		while (num17 < num18)
		{
			DroneDataSheet.GetDictionary().Add(set.DroneDatas[num17].droneID, set.DroneDatas[num17]);
			num17++;
		}
		int num19 = 0;
		int num20 = set.ShopCoinItems.Length;
		while (num19 < num20)
		{
			ShopCoinItemSheet.GetDictionary().Add(set.ShopCoinItems[num19].idItem, set.ShopCoinItems[num19]);
			num19++;
		}
		int num21 = 0;
		int num22 = set.DroneDataMaxs.Length;
		while (num21 < num22)
		{
			DroneDataMaxSheet.GetDictionary().Add(set.DroneDataMaxs[num21].rankID, set.DroneDataMaxs[num21]);
			num21++;
		}
		int num23 = 0;
		int num24 = set.LevelEnemys.Length;
		while (num23 < num24)
		{
			LevelEnemySheet.GetDictionary().Add(set.LevelEnemys[num23].levelPlanet, set.LevelEnemys[num23]);
			num23++;
		}
		int num25 = 0;
		int num26 = set.BaseEnemyDatas.Length;
		while (num25 < num26)
		{
			BaseEnemyDataSheet.GetDictionary().Add(set.BaseEnemyDatas[num25].id, set.BaseEnemyDatas[num25]);
			num25++;
		}
		int num27 = 0;
		int num28 = set.LevelDetails.Length;
		while (num27 < num28)
		{
			LevelDetailSheet.GetDictionary().Add(set.LevelDetails[num27].levelPlanet, set.LevelDetails[num27]);
			num27++;
		}
		int num29 = 0;
		int num30 = set.WaveEnemys.Length;
		while (num29 < num30)
		{
			WaveEnemySheet.GetDictionary().Add(set.WaveEnemys[num29].numWave, set.WaveEnemys[num29]);
			num29++;
		}
		int num31 = 0;
		int num32 = set.PlaneCostUpgrades.Length;
		while (num31 < num32)
		{
			PlaneCostUpgradeSheet.GetDictionary().Add(set.PlaneCostUpgrades[num31].level, set.PlaneCostUpgrades[num31]);
			num31++;
		}
		int num33 = 0;
		int num34 = set.PlaneDatas.Length;
		while (num33 < num34)
		{
			PlaneDataSheet.GetDictionary().Add(set.PlaneDatas[num33].planeID, set.PlaneDatas[num33]);
			num33++;
		}
		int num35 = 0;
		int num36 = set.ShipBataFD01s.Length;
		while (num35 < num36)
		{
			ShipBataFD01Sheet.GetDictionary().Add(set.ShipBataFD01s[num35].power, set.ShipBataFD01s[num35]);
			num35++;
		}
		int num37 = 0;
		int num38 = set.ShipFuryOfAress.Length;
		while (num37 < num38)
		{
			ShipFuryOfAresSheet.GetDictionary().Add(set.ShipFuryOfAress[num37].power, set.ShipFuryOfAress[num37]);
			num37++;
		}
		int num39 = 0;
		int num40 = set.ShipSkywraiths.Length;
		while (num39 < num40)
		{
			ShipSkywraithSheet.GetDictionary().Add(set.ShipSkywraiths[num39].power, set.ShipSkywraiths[num39]);
			num39++;
		}
		int num41 = 0;
		int num42 = set.ShipTwilightXs.Length;
		while (num41 < num42)
		{
			ShipTwilightXSheet.GetDictionary().Add(set.ShipTwilightXs[num41].power, set.ShipTwilightXs[num41]);
			num41++;
		}
		int num43 = 0;
		int num44 = set.ShipGreataxes.Length;
		while (num43 < num44)
		{
			ShipGreataxeSheet.GetDictionary().Add(set.ShipGreataxes[num43].power, set.ShipGreataxes[num43]);
			num43++;
		}
		int num45 = 0;
		int num46 = set.ShipSSLightnings.Length;
		while (num45 < num46)
		{
			ShipSSLightningSheet.GetDictionary().Add(set.ShipSSLightnings[num45].power, set.ShipSSLightnings[num45]);
			num45++;
		}
		int num47 = 0;
		int num48 = set.ShipWarlocks.Length;
		while (num47 < num48)
		{
			ShipWarlockSheet.GetDictionary().Add(set.ShipWarlocks[num47].power, set.ShipWarlocks[num47]);
			num47++;
		}
		int num49 = 0;
		int num50 = set.CostGemUpgradePlaneDrones.Length;
		while (num49 < num50)
		{
			CostGemUpgradePlaneDroneSheet.GetDictionary().Add(set.CostGemUpgradePlaneDrones[num49].level, set.CostGemUpgradePlaneDrones[num49]);
			num49++;
		}
		int num51 = 0;
		int num52 = set.DataRewardEndlessModes.Length;
		while (num51 < num52)
		{
			DataRewardEndlessModeSheet.GetDictionary().Add(set.DataRewardEndlessModes[num51].wave, set.DataRewardEndlessModes[num51]);
			num51++;
		}
		int num53 = 0;
		int num54 = set.DataRewardEndlessModeOneTimes.Length;
		while (num53 < num54)
		{
			DataRewardEndlessModeOneTimeSheet.GetDictionary().Add(set.DataRewardEndlessModeOneTimes[num53].wave, set.DataRewardEndlessModeOneTimes[num53]);
			num53++;
		}
		int num55 = 0;
		int num56 = set.DataRewardBossModeEasys.Length;
		while (num55 < num56)
		{
			DataRewardBossModeEasySheet.GetDictionary().Add(set.DataRewardBossModeEasys[num55].bossID, set.DataRewardBossModeEasys[num55]);
			num55++;
		}
		int num57 = 0;
		int num58 = set.DataRewardBossModeNormals.Length;
		while (num57 < num58)
		{
			DataRewardBossModeNormalSheet.GetDictionary().Add(set.DataRewardBossModeNormals[num57].bossID, set.DataRewardBossModeNormals[num57]);
			num57++;
		}
		int num59 = 0;
		int num60 = set.DataRewardBossModeHards.Length;
		while (num59 < num60)
		{
			DataRewardBossModeHardSheet.GetDictionary().Add(set.DataRewardBossModeHards[num59].bossID, set.DataRewardBossModeHards[num59]);
			num59++;
		}
		int num61 = 0;
		int num62 = set.DataRewardOneTimeBossModeEasys.Length;
		while (num61 < num62)
		{
			DataRewardOneTimeBossModeEasySheet.GetDictionary().Add(set.DataRewardOneTimeBossModeEasys[num61].bossID, set.DataRewardOneTimeBossModeEasys[num61]);
			num61++;
		}
		int num63 = 0;
		int num64 = set.DataRewardOneTimeBossModeNormals.Length;
		while (num63 < num64)
		{
			DataRewardOneTimeBossModeNormalSheet.GetDictionary().Add(set.DataRewardOneTimeBossModeNormals[num63].bossID, set.DataRewardOneTimeBossModeNormals[num63]);
			num63++;
		}
		int num65 = 0;
		int num66 = set.DataRewardOneTimeBossModeHards.Length;
		while (num65 < num66)
		{
			DataRewardOneTimeBossModeHardSheet.GetDictionary().Add(set.DataRewardOneTimeBossModeHards[num65].bossID, set.DataRewardOneTimeBossModeHards[num65]);
			num65++;
		}
		int num67 = 0;
		int num68 = set.DailyQuests.Length;
		while (num67 < num68)
		{
			DailyQuestSheet.GetDictionary().Add(set.DailyQuests[num67].idQuest, set.DailyQuests[num67]);
			num67++;
		}
		int num69 = 0;
		int num70 = set.PlaneDataMaxs.Length;
		while (num69 < num70)
		{
			PlaneDataMaxSheet.GetDictionary().Add(set.PlaneDataMaxs[num69].rankID, set.PlaneDataMaxs[num69]);
			num69++;
		}
		int num71 = 0;
		int num72 = set.DataEventNoels.Length;
		while (num71 < num72)
		{
			DataEventNoelSheet.GetDictionary().Add(set.DataEventNoels[num71].idBox, set.DataEventNoels[num71]);
			num71++;
		}
		int num73 = 0;
		int num74 = set.RewardModeLevelEasys.Length;
		while (num73 < num74)
		{
			RewardModeLevelEasySheet.GetDictionary().Add(set.RewardModeLevelEasys[num73].level, set.RewardModeLevelEasys[num73]);
			num73++;
		}
		int num75 = 0;
		int num76 = set.RewardModeLevelNormals.Length;
		while (num75 < num76)
		{
			RewardModeLevelNormalSheet.GetDictionary().Add(set.RewardModeLevelNormals[num75].level, set.RewardModeLevelNormals[num75]);
			num75++;
		}
		int num77 = 0;
		int num78 = set.RewardModeLevelHards.Length;
		while (num77 < num78)
		{
			RewardModeLevelHardSheet.GetDictionary().Add(set.RewardModeLevelHards[num77].level, set.RewardModeLevelHards[num77]);
			num77++;
		}
		int num79 = 0;
		int num80 = set.VIPDatas.Length;
		while (num79 < num80)
		{
			VIPDataSheet.GetDictionary().Add(set.VIPDatas[num79].vipID, set.VIPDatas[num79]);
			num79++;
		}
		int num81 = 0;
		int num82 = set.GemReviveDatas.Length;
		while (num81 < num82)
		{
			GemReviveDataSheet.GetDictionary().Add(set.GemReviveDatas[num81].idPrice, set.GemReviveDatas[num81]);
			num81++;
		}
		int num83 = 0;
		int num84 = set.DailyGiftFirstMonths.Length;
		while (num83 < num84)
		{
			DailyGiftFirstMonthSheet.GetDictionary().Add(set.DailyGiftFirstMonths[num83].day, set.DailyGiftFirstMonths[num83]);
			num83++;
		}
		int num85 = 0;
		int num86 = set.DailyGiftMonthAfters.Length;
		while (num85 < num86)
		{
			DailyGiftMonthAfterSheet.GetDictionary().Add(set.DailyGiftMonthAfters[num85].day, set.DailyGiftMonthAfters[num85]);
			num85++;
		}
	}
}

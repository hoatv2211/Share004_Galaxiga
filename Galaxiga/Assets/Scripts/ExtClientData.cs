using System;
using System.Collections.Generic;

[Serializable]
public class ExtClientData
{
	public int gem;

	public int coin;

	public int energy;

	public int ultraCardPlane;

	public int ultraCardDrone;

	public List<PlaneInfo> dataPlane;

	public List<DroneInfo> dataDrone;

	public List<DataLevel> dataLevelCampaign;

	public List<DataLevelBossMode> dataLevelBossModes;

	public List<DataQuest> dataDailyQuests;

	public List<bool> stateItemShopCoin;

	public List<bool> stateClaimRewardVIPOneTime;

	public ItemBooster booster;

	public OtherDataClient otherDataClient;
}

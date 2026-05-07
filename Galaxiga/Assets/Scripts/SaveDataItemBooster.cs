using System;
using BayatGames.SaveGamePro;

public class SaveDataItemBooster
{
	public static SaveDataItemBooster Instance
	{
		get
		{
			if (SaveDataItemBooster._ins == null)
			{
				SaveDataItemBooster._ins = new SaveDataItemBooster();
				SaveDataItemBooster._ins.Init();
			}
			return SaveDataItemBooster._ins;
		}
	}

	public void Save(ItemBooster _itemBooster)
	{
		SaveGame.Save<ItemBooster>("dataBooster.txt", _itemBooster);
	}

	public void Init()
	{
		this.itemBooster = new ItemBooster();
		this.itemBooster.numberItemBoosterPower5 = CacheGame.NumberItemPowerUpBullet5;
		this.itemBooster.numberItemBoosterPower10 = CacheGame.NumberItemPowerUpBullet10;
		this.itemBooster.numberItemBoosterShield = CacheGame.NumberItemPowerShield;
		this.itemBooster.numberItemBoosterHeart = CacheGame.NumberItemPowerHeart;
		this.itemBooster.numberItemBoosterOverdrive = CacheGame.GetNumberSkillPlane();
	}

	public void SetData(ItemBooster itemBooster)
	{
		CacheGame.NumberItemPowerUpBullet5 = (itemBooster.numberItemBoosterPower5 = CacheGame.NumberItemPowerUpBullet5);
		CacheGame.NumberItemPowerUpBullet10 = itemBooster.numberItemBoosterPower10;
		CacheGame.NumberItemPowerShield = itemBooster.numberItemBoosterShield;
		CacheGame.NumberItemPowerHeart = itemBooster.numberItemBoosterHeart;
		CacheGame.SetNumberSkillPlane(itemBooster.numberItemBoosterOverdrive);
	}

	public static SaveDataItemBooster _ins;

	public ItemBooster itemBooster;

	private const string DATA_BOOSTER = "dataBooster.txt";
}

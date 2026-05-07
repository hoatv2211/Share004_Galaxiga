using System;

public class RewardItemBullet5 : RewardView
{
	public override void SetView(int numberItem)
	{
		this.textNumberReward.text = "x" + numberItem;
		GameContext.totalItemPowerUpBullet5 += numberItem;
		CacheGame.NumberItemPowerUpBullet5 = GameContext.totalItemPowerUpBullet5;
	}
}

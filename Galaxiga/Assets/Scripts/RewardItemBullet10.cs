using System;

public class RewardItemBullet10 : RewardView
{
	public override void SetView(int number)
	{
		GameContext.totalItemPowerUpBullet10 += number;
		CacheGame.NumberItemPowerUpBullet10 = GameContext.totalItemPowerUpBullet10;
		this.numberReward = number;
		base.SetView();
	}
}

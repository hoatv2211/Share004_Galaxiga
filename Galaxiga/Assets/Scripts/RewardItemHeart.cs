using System;

public class RewardItemHeart : RewardView
{
	public new void SetView(int number)
	{
		CacheGame.NumberItemPowerHeart += number;
		this.numberReward = number;
		GameContext.totalItemPowerUpHeart += number;
		base.SetView();
	}
}

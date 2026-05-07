using System;

public class RewardShield : RewardView
{
	public void SetData(int _number)
	{
		if (this.textNumberReward != null)
		{
			this.textNumberReward.text = "x" + _number;
		}
		GameContext.totalItemPowerShield += _number;
		CacheGame.NumberItemPowerShield = GameContext.totalItemPowerShield;
		this.numberReward = _number;
	}
}

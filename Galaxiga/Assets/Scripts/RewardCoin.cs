using System;

public class RewardCoin : RewardView
{
	public override void SetView()
	{
		CacheGame.AddCoins(this.numberReward);
		base.SetView();
	}

	public override void SetView2(int _numberReward)
	{
		base.SetView(_numberReward);
	}
}

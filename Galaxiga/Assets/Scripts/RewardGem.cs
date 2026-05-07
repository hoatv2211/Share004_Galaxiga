using System;

public class RewardGem : RewardView
{
	public override void SetView()
	{
		CacheGame.AddGems(this.numberReward);
		base.SetView();
	}

	public override void SetView2(int _numberReward)
	{
		base.SetView(_numberReward);
	}
}

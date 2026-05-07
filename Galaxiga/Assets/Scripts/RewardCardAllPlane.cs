using System;

public class RewardCardAllPlane : RewardView
{
	public override void SetView()
	{
		CacheGame.AddCardAllPlane(this.numberReward);
		base.SetView();
	}

	public override void SetView2(int _numberReward)
	{
		base.SetView2(_numberReward);
	}
}

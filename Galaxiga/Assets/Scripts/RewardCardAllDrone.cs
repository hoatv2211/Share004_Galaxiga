using System;

public class RewardCardAllDrone : RewardView
{
	public override void SetView()
	{
		CacheGame.AddCardAllDrone(this.numberReward);
		base.SetView();
	}

	public override void SetView2(int _numberReward)
	{
		base.SetView2(_numberReward);
	}
}

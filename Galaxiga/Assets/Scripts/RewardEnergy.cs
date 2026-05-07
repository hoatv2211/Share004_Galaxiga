using System;

public class RewardEnergy : RewardView
{
	public override void SetView()
	{
		ShopEnergy.current.AddEnergy(this.numberReward);
		base.SetView(this.numberReward);
	}
}

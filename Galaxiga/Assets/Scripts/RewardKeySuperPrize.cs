using System;

public class RewardKeySuperPrize : RewardView
{
	public override void SetView(int numberKey)
	{
		int num = CacheGame.KeySuperRize;
		num += numberKey;
		CacheGame.KeySuperRize = num;
		this.numberReward = numberKey;
		base.SetView();
	}

	public override void SetView2(int _numberReward)
	{
		base.SetView2(_numberReward);
	}
}

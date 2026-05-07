using System;

public class RewardSkillPlane : RewardView
{
	public override void SetView()
	{
		CacheGame.AddSkillPlane(this.numberReward);
		base.SetView();
	}

	public override void SetView2(int _numberReward)
	{
		base.SetView2(_numberReward);
	}
}

using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class ItemRewardInGame : RewardView
{
	public override void SetView()
	{
		DOTween.Restart("REWARD_IN_GAME", true, -1f);
		DOTween.Play("REWARD_IN_GAME");
		if (this.textNumberReward != null)
		{
			this.textNumberReward.text = "x" + GameUtil.FormatNumber(this.numberReward);
		}
		if (this.rewardType == PrizeManager.RewardType.Card_Plane || this.rewardType == PrizeManager.RewardType.Unlock_Plane)
		{
			DataGame.Current.SetSpriteImagePlane(this.imgPlane, this._planeID);
		}
		else if (this.rewardType == PrizeManager.RewardType.Card_Drone)
		{
			DataGame.Current.SetSpriteImageDrone(this.imgDrone, this._droneID);
		}
	}

	public void Hide()
	{
		DOTween.PlayBackwards("REWARD_IN_GAME");
	}

	[ShowIf("rewardType", PrizeManager.RewardType.Card_Plane, true)]
	public Image imgPlane;

	[ShowIf("rewardType", PrizeManager.RewardType.Card_Drone, true)]
	public Image imgDrone;
}

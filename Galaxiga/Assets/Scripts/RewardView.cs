using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RewardView : MonoBehaviour
{
	private void Awake()
	{
		this.btn = base.GetComponent<Button>();
		if (this.btn != null)
		{
			this.btn.onClick.AddListener(new UnityAction(this.ShowPopupInfoItem));
		}
	}

	private void ShowPopupInfoItem()
	{
		if (this.rewardType == PrizeManager.RewardType.Card_Plane)
		{
			InfoItemReward.current._planeID = this._planeID;
		}
		else if (this.rewardType == PrizeManager.RewardType.Card_Drone)
		{
			InfoItemReward.current._droneID = this._droneID;
		}
		InfoItemReward.current.ShowPopupInfoItem(this.rewardType, this.numberReward);
	}

	public virtual void SetView()
	{
		if (this.textNumberReward != null)
		{
			this.textNumberReward.text = "x" + this.numberReward;
		}
	}

	public virtual void SetView2(int _numberReward)
	{
		if (this.textNumberReward != null)
		{
			this.textNumberReward.text = "x" + GameUtil.FormatNumber(_numberReward);
		}
		this.numberReward = _numberReward;
	}

	public virtual void SetView(int _numberReward)
	{
		if (this.textNumberReward != null)
		{
			this.textNumberReward.text = GameUtil.FormatNumber(_numberReward);
		}
		this.numberReward = _numberReward;
	}

	public virtual void SetView2(GameContext.Plane planeID, int numberCard)
	{
		if (this.textNumberReward != null)
		{
			this.textNumberReward.text = "x" + numberCard;
		}
		this.numberReward = numberCard;
	}

	public virtual void SetView2(GameContext.Drone droneID, int numberCard)
	{
		this.numberReward = numberCard;
	}

	public void ShowPanelFxSuperPrize()
	{
		this.panelFxSuperPrize.SetActive(true);
	}

	public PrizeManager.RewardType rewardType;

	[HideIf("rewardType", PrizeManager.RewardType.Unlock_Plane, true)]
	public GameObject panelFxSuperPrize;

	[HideIf("rewardType", PrizeManager.RewardType.Unlock_Plane, true)]
	public Text textNumberReward;

	[HideIf("rewardType", PrizeManager.RewardType.Unlock_Plane, true)]
	public int numberReward;

	[HideInInspector]
	public GameContext.Plane _planeID;

	[HideInInspector]
	public GameContext.Drone _droneID;

	private Button btn;
}

using System;
using DG.Tweening;
using I2.Loc;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

public class ViewRewardBoss : MonoBehaviour
{
	private void Awake()
	{
		this.GetDataRewardBoss();
		this.textBossName.text = DataRewardBossModeEasySheet.Get(this.bossID).nameBoss;
		this.skeletonGraphicBoxEasy = this.objBoxEasy.GetComponent<SkeletonGraphic>();
		this.skeletonGraphicBoxNormal = this.objBoxNormal.GetComponent<SkeletonGraphic>();
		this.skeletonGraphicBoxHard = this.objBoxHard.GetComponent<SkeletonGraphic>();
		this.CheckReward();
	}

	private void GetDataRewardBoss()
	{
		this.rewardBossEasy = new ViewRewardBoss.RewardBoss(DataRewardOneTimeBossModeEasySheet.Get(this.bossID).gem, DataRewardOneTimeBossModeEasySheet.Get(this.bossID).coin, DataRewardOneTimeBossModeEasySheet.Get(this.bossID).cardAllPlane, DataRewardOneTimeBossModeEasySheet.Get(this.bossID).cardAllDrone);
		this.rewardBossNormal = new ViewRewardBoss.RewardBoss(DataRewardOneTimeBossModeNormalSheet.Get(this.bossID).gem, DataRewardOneTimeBossModeNormalSheet.Get(this.bossID).coin, DataRewardOneTimeBossModeNormalSheet.Get(this.bossID).cardAllPlane, DataRewardOneTimeBossModeNormalSheet.Get(this.bossID).cardAllDrone);
		this.rewardBossHard = new ViewRewardBoss.RewardBoss(DataRewardOneTimeBossModeHardSheet.Get(this.bossID).gem, DataRewardOneTimeBossModeHardSheet.Get(this.bossID).coin, DataRewardOneTimeBossModeHardSheet.Get(this.bossID).cardAllPlane, DataRewardOneTimeBossModeHardSheet.Get(this.bossID).cardAllDrone);
	}

	private void Start()
	{
		this.lenghtArrTweening = this.arrTweenReward.Length;
		this.SelectBoxEasy();
	}

	public void SelectBoxEasy()
	{
		this.currentBox = ViewRewardBoss.Box.easy;
		this.MoveImgSelect(this.boxEasy);
		this.SetView(this.rewardBossEasy);
	}

	public void SelectBoxNormal()
	{
		this.currentBox = ViewRewardBoss.Box.normal;
		this.MoveImgSelect(this.boxNormal);
		this.SetView(this.rewardBossNormal);
	}

	public void SelectBoxHard()
	{
		this.currentBox = ViewRewardBoss.Box.hard;
		this.MoveImgSelect(this.boxHard);
		this.SetView(this.rewardBossHard);
	}

	private void SetView(ViewRewardBoss.RewardBoss rewardBoss)
	{
		this.CheckClaimReward();
		this.rewardCoin.numberReward = rewardBoss.coin;
		this.rewardCoin.textNumberReward.text = "x" + rewardBoss.coin;
		this.rewardGem.numberReward = rewardBoss.gem;
		this.rewardGem.textNumberReward.text = "x" + rewardBoss.gem;
		this.rewardCardAllPlane.numberReward = rewardBoss.numberCardAllPlane;
		this.rewardCardAllPlane.textNumberReward.text = "x" + rewardBoss.numberCardAllPlane;
		this.rewardCardAllDrone.numberReward = rewardBoss.numberCardAllDrone;
		this.rewardCardAllDrone.textNumberReward.text = "x" + rewardBoss.numberCardAllDrone;
		for (int i = 0; i < this.lenghtArrTweening; i++)
		{
			this.arrTweenReward[i].DORestart(false);
			this.arrTweenReward[i].DOPlay();
		}
	}

	private void CheckClaimReward()
	{
		this.isChecking = true;
		ViewRewardBoss.Box box = this.currentBox;
		if (box != ViewRewardBoss.Box.easy)
		{
			if (box != ViewRewardBoss.Box.normal)
			{
				if (box == ViewRewardBoss.Box.hard)
				{
					this.wasPassedBoss = SaveDataLevelBossMode.WasPassedBoss(this.bossID, GameContext.ModeLevel.Hard);
					this.wasClaimed = SaveDataLevelBossMode.WasCollectedRewardOneTime(this.bossID, GameContext.ModeLevel.Hard);
				}
			}
			else
			{
				this.wasPassedBoss = SaveDataLevelBossMode.WasPassedBoss(this.bossID, GameContext.ModeLevel.Normal);
				this.wasClaimed = SaveDataLevelBossMode.WasCollectedRewardOneTime(this.bossID, GameContext.ModeLevel.Normal);
			}
		}
		else
		{
			this.wasPassedBoss = SaveDataLevelBossMode.WasPassedBoss(this.bossID, GameContext.ModeLevel.Easy);
			this.wasClaimed = SaveDataLevelBossMode.WasCollectedRewardOneTime(this.bossID, GameContext.ModeLevel.Easy);
		}
		if (!this.wasPassedBoss || this.wasClaimed)
		{
			this.imgBtnClaim.sprite = this.sprClaimDisable;
		}
		else
		{
			this.imgBtnClaim.sprite = this.sprBtnClaim;
		}
		this.isChecking = false;
	}

	private void CheckReward()
	{
		bool flag = SaveDataLevelBossMode.WasPassedBoss(this.bossID, GameContext.ModeLevel.Easy);
		bool flag2 = SaveDataLevelBossMode.WasCollectedRewardOneTime(this.bossID, GameContext.ModeLevel.Easy);
		bool flag3 = SaveDataLevelBossMode.WasPassedBoss(this.bossID, GameContext.ModeLevel.Normal);
		bool flag4 = SaveDataLevelBossMode.WasCollectedRewardOneTime(this.bossID, GameContext.ModeLevel.Normal);
		bool flag5 = SaveDataLevelBossMode.WasPassedBoss(this.bossID, GameContext.ModeLevel.Hard);
		bool flag6 = SaveDataLevelBossMode.WasCollectedRewardOneTime(this.bossID, GameContext.ModeLevel.Hard);
		if (flag)
		{
			if (!flag2)
			{
				this.skeletonGraphicBoxEasy.freeze = false;
			}
			else
			{
				this.objBoxEasy.SetActive(false);
				this.objBoxEasyDisable.SetActive(true);
			}
		}
		if (flag3)
		{
			if (!flag4)
			{
				this.skeletonGraphicBoxNormal.freeze = false;
			}
			else
			{
				this.objBoxNormal.SetActive(false);
				this.objBoxNormalDisable.SetActive(true);
			}
		}
		if (flag5)
		{
			if (!flag6)
			{
				this.skeletonGraphicBoxHard.freeze = false;
			}
			else
			{
				this.objBoxHard.SetActive(false);
				this.objBoxHardDisable.SetActive(true);
			}
		}
	}

	public void ClaimReward()
	{
		if (!this.isChecking)
		{
			if (this.wasPassedBoss)
			{
				if (!this.wasClaimed)
				{
					ViewRewardBoss.Box box = this.currentBox;
					if (box != ViewRewardBoss.Box.easy)
					{
						if (box != ViewRewardBoss.Box.normal)
						{
							if (box == ViewRewardBoss.Box.hard)
							{
								this.CollectReward(this.rewardBossHard);
								SaveDataLevelBossMode.SetCollectRewardOneTime(this.bossID, GameContext.ModeLevel.Hard);
								this.objBoxHard.SetActive(false);
								this.objBoxHardDisable.SetActive(true);
							}
						}
						else
						{
							this.CollectReward(this.rewardBossNormal);
							SaveDataLevelBossMode.SetCollectRewardOneTime(this.bossID, GameContext.ModeLevel.Normal);
							this.objBoxNormal.SetActive(false);
							this.objBoxNormalDisable.SetActive(true);
						}
					}
					else
					{
						this.CollectReward(this.rewardBossEasy);
						SaveDataLevelBossMode.SetCollectRewardOneTime(this.bossID, GameContext.ModeLevel.Easy);
						this.objBoxEasy.SetActive(false);
						this.objBoxEasyDisable.SetActive(true);
					}
					BossModeManager.current.CheckDeactiveAnimBtnRewardOneTime();
					this.imgBtnClaim.sprite = this.sprClaimDisable;
				}
				else
				{
					Notification.Current.ShowNotification(ScriptLocalization.notify_already_claimed);
				}
			}
			else
			{
				Notification.Current.ShowNotification(ScriptLocalization.notify_defeat_the_boss_first);
			}
		}
	}

	private void CollectReward(ViewRewardBoss.RewardBoss rewardBoss)
	{
		this.wasClaimed = true;
		CacheGame.AddCoins(rewardBoss.coin);
		CacheGame.AddGems(rewardBoss.gem);
		CurrencyLog.LogGemIn(rewardBoss.gem, CurrencyLog.In.Other, "Reward_One_Time_Boss_Mode");
		CurrencyLog.LogGoldIn(rewardBoss.coin, CurrencyLog.In.Other, "Reward_One_Time_Boss_Mode");
		CacheGame.AddCardAllPlane(rewardBoss.numberCardAllPlane);
		CacheGame.AddCardAllDrone(rewardBoss.numberCardAllDrone);
		GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Coin, rewardBoss.coin);
		GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Gem, rewardBoss.gem);
		GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Card_All_Plane, rewardBoss.numberCardAllPlane);
		GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Card_All_Drone, rewardBoss.numberCardAllDrone);
		GetRewardSuccess.current.ShowPanelGetRewardSuccess();
	}

	private void MoveImgSelect(Transform pos)
	{
		this.imgSelect.DOLocalMoveX(pos.localPosition.x, 0.15f, false);
	}

	public int bossID;

	public Text textBossName;

	public Image imgBtnClaim;

	public Sprite sprBtnClaim;

	public Sprite sprClaimDisable;

	public Transform imgSelect;

	public Transform boxEasy;

	public Transform boxNormal;

	public Transform boxHard;

	public RewardGem rewardGem;

	public RewardCoin rewardCoin;

	public RewardCardAllPlane rewardCardAllPlane;

	public RewardCardAllDrone rewardCardAllDrone;

	public DOTweenAnimation[] arrTweenReward;

	public GameObject objBoxEasy;

	public GameObject objBoxNormal;

	public GameObject objBoxHard;

	public GameObject objBoxEasyDisable;

	public GameObject objBoxNormalDisable;

	public GameObject objBoxHardDisable;

	private ViewRewardBoss.RewardBoss rewardBossEasy;

	private ViewRewardBoss.RewardBoss rewardBossNormal;

	private ViewRewardBoss.RewardBoss rewardBossHard;

	private SkeletonGraphic skeletonGraphicBoxEasy;

	private SkeletonGraphic skeletonGraphicBoxNormal;

	private SkeletonGraphic skeletonGraphicBoxHard;

	private ViewRewardBoss.Box currentBox;

	private int lenghtArrTweening;

	private bool isChecking;

	private bool wasClaimed;

	private bool wasPassedBoss;

	private class RewardBoss
	{
		public RewardBoss(int _gem, int _coin, int _cardAllPlane, int _cardAllDrone)
		{
			this.gem = _gem;
			this.coin = _coin;
			this.numberCardAllPlane = _cardAllPlane;
			this.numberCardAllDrone = _cardAllDrone;
		}

		public int gem;

		public int coin;

		public int numberCardAllPlane;

		public int numberCardAllDrone;
	}

	private enum Box
	{
		easy,
		normal,
		hard
	}
}

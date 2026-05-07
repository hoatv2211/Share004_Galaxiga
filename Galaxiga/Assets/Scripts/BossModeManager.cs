using System;
using DG.Tweening;
using I2.Loc;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

public class BossModeManager : MonoBehaviour
{
	public static BossModeManager current
	{
		get
		{
			if (BossModeManager._ins == null)
			{
				BossModeManager._ins = UnityEngine.Object.FindObjectOfType<BossModeManager>();
			}
			return BossModeManager._ins;
		}
	}

	private void Awake()
	{
		if (GameContext.currentModeGamePlay == GameContext.ModeGamePlay.Boss)
		{
			this.ShowPanelModeBoss();
		}
	}

	private void Start()
	{
		this.CheckActiveAnimBtnRewarOneTime();
	}

	public void ShowPanelModeBoss()
	{
		if (GameContext.maxLevelUnlocked > 5)
		{
			if (NewTutorial.current.currentStepTutorial_OpenBossMode == 1)
			{
				NewTutorial.current.OpenBossMode_Step1();
			}
			GameContext.currentModeGamePlay = GameContext.ModeGamePlay.Boss;
			this.panelModeBoss.SetActive(true);
			DOTween.Restart("PANEL_MODE_BOSS", true, -1f);
			DOTween.Play("PANEL_MODE_BOSS");
			EscapeManager.Current.AddAction(new Action(this.HidePanelModeBoss));
			SelectLevelManager.current.AddAction(new Action(this.HidePanelModeBoss));
		}
		else
		{
			Notification.Current.ShowNotification(string.Format(ScriptLocalization.notify_you_need_pass_stage_in_mode_campaign, 5));
		}
	}

	private void HidePanelModeBoss()
	{
		GameContext.currentModeGamePlay = GameContext.ModeGamePlay.Campaign;
		EscapeManager.Current.RemoveAction(new Action(this.HidePanelModeBoss));
		SelectLevelManager.current.RemoveAction(new Action(this.HidePanelModeBoss));
		DOTween.PlayBackwards("PANEL_MODE_BOSS");
		base.StartCoroutine(GameContext.Delay(0.1f, delegate
		{
			this.panelModeBoss.SetActive(false);
		}));
	}

	public void ShowPanelRewardOneTime()
	{
		this.panelRewardOneTime.SetActive(true);
		DOTween.Restart("Reward_One_Time_Boss", true, -1f);
		DOTween.Play("Reward_One_Time_Boss");
		EscapeManager.Current.AddAction(new Action(this.HidePanelRewardOneTime));
		SelectLevelManager.current.AddAction(new Action(this.HidePanelRewardOneTime));
	}

	public void HidePanelRewardOneTime()
	{
		EscapeManager.Current.RemoveAction(new Action(this.HidePanelRewardOneTime));
		SelectLevelManager.current.RemoveAction(new Action(this.HidePanelRewardOneTime));
		DOTween.PlayBackwards("Reward_One_Time_Boss");
		base.StartCoroutine(GameContext.Delay(0.1f, delegate
		{
			this.panelRewardOneTime.SetActive(false);
		}));
	}

	private void ActiveAnimBtnRewardOneTime()
	{
		this.skeletonGraphicBtnRewardOneTime.freeze = false;
		this.objNotificationRed.SetActive(true);
	}

	public void CheckDeactiveAnimBtnRewardOneTime()
	{
		if (this.numberRewardNeedClaim > 0)
		{
			this.numberRewardNeedClaim--;
			if (this.numberRewardNeedClaim == 0)
			{
				this.skeletonGraphicBtnRewardOneTime.freeze = true;
				this.objNotificationRed.SetActive(false);
			}
		}
	}

	public void CheckActiveAnimBtnRewarOneTime()
	{
		for (int i = 0; i <= GameContext.maxLevelBosssUnlocked; i++)
		{
			bool flag = SaveDataLevelBossMode.WasPassedBoss(i + 1, GameContext.ModeLevel.Easy);
			bool flag2 = SaveDataLevelBossMode.WasPassedBoss(i + 1, GameContext.ModeLevel.Normal);
			bool flag3 = SaveDataLevelBossMode.WasPassedBoss(i + 1, GameContext.ModeLevel.Hard);
			if (flag && !SaveDataLevelBossMode.WasCollectedRewardOneTime(i + 1, GameContext.ModeLevel.Easy))
			{
				this.numberRewardNeedClaim++;
				break;
			}
			if (flag2 && !SaveDataLevelBossMode.WasCollectedRewardOneTime(i + 1, GameContext.ModeLevel.Normal))
			{
				this.numberRewardNeedClaim++;
				break;
			}
			if (flag3 && !SaveDataLevelBossMode.WasCollectedRewardOneTime(i + 1, GameContext.ModeLevel.Hard))
			{
				this.numberRewardNeedClaim++;
				break;
			}
		}
		if (this.numberRewardNeedClaim > 0)
		{
			this.ActiveAnimBtnRewardOneTime();
		}
	}

	public void VictoryCheckAnimBtnReward()
	{
		if (!SaveDataLevelBossMode.WasCollectedRewardOneTime(GameContext.currentLevel, GameContext.currentModeLevel))
		{
			this.ActiveAnimBtnRewardOneTime();
		}
	}

	public static BossModeManager _ins;

	public GameObject panelModeBoss;

	public GameObject panelRewardOneTime;

	public ScrollRect scroRewardOneTime;

	public SkeletonGraphic skeletonGraphicBtnRewardOneTime;

	public GameObject objNotificationRed;

	private int numberRewardNeedClaim;
}

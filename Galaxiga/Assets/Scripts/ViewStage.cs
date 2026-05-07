using System;
using DG.Tweening;
using Hellmade.Sound;
using I2.Loc;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ViewStage : MonoBehaviour
{
	private void Awake()
	{
		this.textLevel.text = string.Empty + this.level;
		this.btnLevel = base.GetComponent<Button>();
		if (!this.isStageBoss)
		{
			if (this.gate)
			{
				this.CheckGate();
			}
			this.CheckLevel();
		}
		else
		{
			this.CheckLevelBoss();
		}
	}

	private void Start()
	{
		this.btnLevel.onClick.AddListener(new UnityAction(this.SelectLevel));
	}

	private void CheckLevel()
	{
		if (GameContext.maxLevelUnlocked >= this.level)
		{
			this.SetUnlockLevel();
		}
	}

	private void SetUnlockLevel()
	{
		this.objStar.SetActive(true);
		this.imgStage.sprite = SelectLevelManager.current.sprBtnLevelUnlock;
		int num = SaveDataLevel.NumberStar(this.level);
		this.textLevel.color = this.colorText;
		if (num == 1)
		{
			this.starEasy.SetActive(true);
		}
		else if (num == 2)
		{
			this.starEasy.SetActive(true);
			this.starNormal.SetActive(true);
		}
		else if (num == 3)
		{
			this.starEasy.SetActive(true);
			this.starNormal.SetActive(true);
			this.starHard.SetActive(true);
		}
		if (GameContext.maxLevelUnlocked == this.level)
		{
			SelectLevelManager.current.SetPositionFxLevel(this._element);
			SelectLevelManager.current.SnapToLevelCampaign(this._element);
			base.StartCoroutine(GameContext.Delay(1f, delegate
			{
				this.doTweenAnimation.DOPlay();
			}));
		}
	}

	public void StopDotween()
	{
		this.doTweenAnimation.DORestart(false);
		this.doTweenAnimation.DOPause();
	}

	public void SetForcusLevel()
	{
		SelectLevelManager.current.SetPositionFxLevel(this._element);
		this.doTweenAnimation.DOPlay();
	}

	private void CheckLevelBoss()
	{
		if (GameContext.maxLevelUnlocked > this.level * 5 && GameContext.maxLevelBosssUnlocked >= this.level)
		{
			this.UnlockLevelBoss();
		}
	}

	private void UnlockLevelBoss()
	{
		this.objStar.SetActive(true);
		this.imgStage.sprite = SelectLevelManager.current.sprBtnLevelBosslUnlock;
		int num = SaveDataLevelBossMode.NumberStars(this.level);
		this.textLevel.color = this.colorText;
		if (num == 1)
		{
			this.starEasy.SetActive(true);
		}
		else if (num == 2)
		{
			this.starEasy.SetActive(true);
			this.starNormal.SetActive(true);
		}
		else if (num == 3)
		{
			this.starEasy.SetActive(true);
			this.starNormal.SetActive(true);
			this.starHard.SetActive(true);
		}
		if (num < 3)
		{
			if (GameContext.maxLevelBosssUnlocked == this.level && GameContext.maxLevelBosssUnlocked > 3)
			{
				SelectLevelManager.current.SnapToLevelBoss(this._element);
			}
			base.StartCoroutine(GameContext.Delay(1f, delegate
			{
				this.doTweenAnimation.DOPlay();
			}));
		}
	}

	private void SelectLevel()
	{
		EazySoundManager.PlayUISound(AudioCache.UISound.tap);
		if (this.level <= GameContext.maxLevelUnlocked)
		{
			GameContext.currentLevel = this.level;
			if (!this.isStageBoss)
			{
				if (this.gate)
				{
					if (this.canPlay)
					{
						GameContext.numberStarCurrentLvl = SaveDataLevel.NumberStar(this.level);
						RewardStageManager.current.ShowPanelReward();
					}
					else
					{
						SelectLevelManager.current.ShowPopupCollectStar();
					}
				}
				else
				{
					GameContext.numberStarCurrentLvl = SaveDataLevel.NumberStar(this.level);
					RewardStageManager.current.ShowPanelReward();
				}
			}
			else
			{
				if (NewTutorial.current.currentStepTutorial_OpenBossMode == 5)
				{
					NewTutorial.current.OpenBossMode_Step5();
				}
				GameContext.numberStarCurrentLvl = SaveDataLevelBossMode.NumberStars(this.level);
				RewardStageManager.current.ShowPanelReward();
			}
		}
		else
		{
			Notification.Current.ShowNotification(ScriptLocalization.unlock_level_in_order);
		}
	}

	private void CheckGate()
	{
		if (GameContext.maxLevelUnlocked > this.level)
		{
			this.objGate.SetActive(false);
			this.canPlay = true;
			int num = SaveDataLevel.NumberStar(this.level);
			if (this.arrLockPlane.Length > 0)
			{
				if (num != 1)
				{
					if (num == 3)
					{
						this.arrLockPlane[2].SetActive(false);
					}
				}
				else
				{
					this.arrLockPlane[0].SetActive(false);
				}
			}
		}
		else
		{
			int num2 = this.level - GameContext.maxLevelUnlocked;
			if (num2 < 15)
			{
				this.objNumberStar.SetActive(true);
				int num3 = SaveDataLevel.TotalStarAllLevel();
				int num4 = this.level / 15 * 20;
				this.textNumberStar.text = num3 + "/" + num4;
				SelectLevelManager.current.textNumberStar.text = num3 + "/" + num4;
				if (num3 >= num4)
				{
					this.canPlay = true;
				}
				if (GameContext.maxLevelUnlocked == this.level && this.canPlay)
				{
					this.objGate.SetActive(false);
				}
			}
		}
	}

	public int level;

	public bool isStageBoss;

	public GameObject objStar;

	public GameObject starEasy;

	public GameObject starNormal;

	public GameObject starHard;

	public Image imgStage;

	public Text textLevel;

	public DOTweenAnimation doTweenAnimation;

	private Button btnLevel;

	public RectTransform _element;

	public Color colorText;

	public bool gate;

	[ShowIf("gate", false)]
	public GameObject objGate;

	[ShowIf("gate", false)]
	public GameObject objNumberStar;

	[ShowIf("gate", false)]
	public Text textNumberStar;

	[ShowIf("gate", false)]
	public GameObject[] arrLockPlane;

	private SelectLevelManager selectLevel;

	private bool canPlay;
}

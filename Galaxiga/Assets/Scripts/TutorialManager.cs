using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
	public static TutorialManager current
	{
		get
		{
			if (TutorialManager._current == null)
			{
				TutorialManager._current = UnityEngine.Object.FindObjectOfType<TutorialManager>();
			}
			return TutorialManager._current;
		}
	}

	public bool IsSceneHome()
	{
		return this.scene == TutorialManager.Scene.Home;
	}

	public bool IsSceneSelectLevel()
	{
		return this.scene == TutorialManager.Scene.Select_Level;
	}

	private void Awake()
	{
	}

	private void Start()
	{
		if (this.IsSceneSelectLevel())
		{
			if (GameContext.maxLevelUnlocked < 4)
			{
				GameContext.isTutorial = true;
				this.objBarBottom.SetActive(false);
				this.objModeLevel.SetActive(false);
				this.SetForcusBtnLevel();
				this.imgViewSliderLeve.enabled = false;
			}
			else
			{
				if (GameContext.isCompleteTutorialSceneHome && GameContext.maxLevelUnlocked <= 4)
				{
					this.SetForcusBtn(this.btnLevel4);
				}
				if (CacheGame.IsCompleteTutorialSceneSelectLevel)
				{
					GameContext.isTutorial = false;
				}
				else
				{
					this.objBtnNextLvl.SetActive(false);
					this.objBtnRestart.SetActive(false);
				}
			}
			if (GameContext.needShowForcusBtnEndless)
			{
				this.SetForcusBtnEndlessMode();
			}
			else if (GameContext.needShowForcusBtnBoss)
			{
				this.SetForcusBtnBossFight();
			}
		}
		else if (this.IsSceneHome() && !GameContext.isCompleteTutorialUpgradePlane)
		{
			this.SetForcusBtnPlane();
		}
	}

	public void SetForcusBtnLevel()
	{
		if (GameContext.maxLevelUnlocked == 1)
		{
			this.SetForcusBtn(this.btnLevel1);
		}
		else if (GameContext.maxLevelUnlocked == 2)
		{
			this.SetForcusBtn(this.btnLevel2);
		}
		else if (GameContext.maxLevelUnlocked == 3)
		{
			this.SetForcusBtn(this.btnLevel3);
		}
		else if (GameContext.maxLevelUnlocked == 4)
		{
			this.SetForcusBtnBackToHome();
		}
	}

	private void SetForcusBtn(Transform transform, Transform pos)
	{
		this.objForcus.transform.parent = transform;
		this.objForcus.transform.position = pos.position;
		this.objForcus.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
		if (!this.objForcus.activeInHierarchy)
		{
			this.objForcus.SetActive(true);
		}
	}

	private void SetForcusBtn(Transform transform)
	{
		this.objForcus.transform.parent = transform;
		this.objForcus.transform.localPosition = Vector3.zero;
		if (this.IsSceneSelectLevel())
		{
			this.objForcus.transform.localScale = new Vector3(2f, 2f, 2f);
		}
		else if (this.IsSceneHome())
		{
			this.objForcus.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
		}
		if (!this.objForcus.activeInHierarchy)
		{
			this.objForcus.SetActive(true);
		}
	}

	public void SetForcusBtnPlay()
	{
		this.SetForcusBtn(this.btnPlay);
	}

	public void SetForcusBtnBackOnPanelVictoryManager()
	{
		this.SetForcusBtn(this.btnNextLevel);
	}

	public void SetForcusBtnBackOnPanelDefeat()
	{
		this.SetForcusBtn(this.btnResart);
	}

	public void SetForcusBtnBackToHome()
	{
		this.objModeLevel.SetActive(true);
		this.objBarBottom.SetActive(true);
		this.imgViewSliderLeve.enabled = true;
		this.SetForcusBtn(this.btnBackToHome);
	}

	private void SetForcusBtnStage4()
	{
		this.SetForcusBtn(this.btnLevel4);
	}

	public void SetForcusBtnPlane()
	{
		this.SetForcusBtn(this.btnPlane);
	}

	public void SetForcusBtnUpgradePlane()
	{
		this.SetForcusBtn(this.posBtnUpgradePlane, this.btnUpgradePlane);
	}

	public void SetForcusBtnBackOnPanelPlane()
	{
		this.SetForcusBtn(this.posBtnBackOnPanelPlane, this.btnBackOnPanelPlane);
	}

	public void SetForcusBtnQuestOnPanelPlane()
	{
		this.SetForcusBtn(this.posBtnQuest, this.btnQuest);
	}

	public void SetForcusBtnCollectRewardPanelQuest()
	{
		this.SetForcusBtn(this.posBtnCollectQuestUpgradePlane, this.btnCollectQuestUpgradePlane);
	}

	public void SetForcusBtnPlaneHome()
	{
		this.SetForcusBtn(this.posBtnPlaneHome, this.btnPlaneHome);
		this.isForcusBtnPlaneHome = true;
	}

	public void SetForcusBtnStartGame()
	{
		if (this.isForcusBtnPlaneHome)
		{
			ShopManager.Instance.ShowStarterPack();
			this.SetForcusBtn(this.btnStartGame);
		}
	}

	public void SetForcusBtnEndlessMode()
	{
	}

	public void SetForcusBtnBossFight()
	{
		this.SetForcusBtn(this.btnBossFight);
	}

	public void HideObjForcus()
	{
		this.objForcus.transform.parent = null;
		this.objForcus.SetActive(false);
	}

	public void SetStepTutorial(TutorialManager.StepTutorial stepTutorial)
	{
		switch (stepTutorial)
		{
		case TutorialManager.StepTutorial.show_popup_reward_starship:
			if (this.IsSceneHome())
			{
				this.ShowPopupClaimStarship();
			}
			break;
		}
	}

	private void ShowPopupClaimStarship()
	{
	}

	public static TutorialManager _current;

	public TutorialManager.Scene scene;

	public GameObject objForcus;

	[HideIf("IsSceneHome", true)]
	public GameObject objBtnNextLvl;

	[HideIf("IsSceneHome", true)]
	public GameObject objBtnRestart;

	[HideIf("IsSceneHome", true)]
	public GameObject objModeLevel;

	[HideIf("IsSceneHome", true)]
	public GameObject objBarBottom;

	[HideIf("IsSceneHome", true)]
	public Transform btnBackOnPanelVictory;

	[HideIf("IsSceneHome", true)]
	public Transform btnBackOnPanelDefeat;

	[HideIf("IsSceneHome", true)]
	public Transform btnPlay;

	[HideIf("IsSceneHome", true)]
	public Transform btnBackToHome;

	[HideIf("IsSceneHome", true)]
	public Transform btnLevel1;

	[HideIf("IsSceneHome", true)]
	public Transform btnLevel2;

	[HideIf("IsSceneHome", true)]
	public Transform btnLevel3;

	[HideIf("IsSceneHome", true)]
	public Transform btnLevel4;

	[HideIf("IsSceneHome", true)]
	public Transform btnEndless;

	[HideIf("IsSceneHome", true)]
	public Transform btnBossFight;

	[HideIf("IsSceneHome", true)]
	public Transform btnNextLevel;

	[HideIf("IsSceneHome", true)]
	public Transform btnResart;

	[HideIf("IsSceneHome", true)]
	public Image imgViewSliderLeve;

	[HideIf("IsSceneSelectLevel", true)]
	public Transform btnPlane;

	[HideIf("IsSceneSelectLevel", true)]
	public Transform btnUpgradePlane;

	[HideIf("IsSceneSelectLevel", true)]
	public Transform btnBackOnPanelPlane;

	[HideIf("IsSceneSelectLevel", true)]
	public Transform btnQuest;

	[HideIf("IsSceneSelectLevel", true)]
	public Transform btnCollectQuestUpgradePlane;

	[HideIf("IsSceneSelectLevel", true)]
	public Transform btnPlaneHome;

	[HideIf("IsSceneSelectLevel", true)]
	public Transform btnStartGame;

	[HideIf("IsSceneSelectLevel", true)]
	public Transform posBtnQuest;

	[HideIf("IsSceneSelectLevel", true)]
	public Transform posBtnCollectQuestUpgradePlane;

	[HideIf("IsSceneSelectLevel", true)]
	public Transform posBtnPlaneHome;

	[HideIf("IsSceneSelectLevel", true)]
	public Transform posBtnUpgradePlane;

	[HideIf("IsSceneSelectLevel", true)]
	public Transform posBtnBackOnPanelPlane;

	private bool isForcusBtnPlaneHome;

	private int currentStepTutorial;

	public enum Scene
	{
		Home,
		Select_Level
	}

	public enum StepTutorial
	{
		show_popup_reward_starship,
		switch_starship,
		upgrade_starship,
		claim_daily_quest,
		play_stage_4,
		show_popup_reward_drone,
		equip_drone,
		play_stage_5,
		use_skill_drone,
		show_popup_starship,
		select_starship,
		click_button_evol,
		evol_starship
	}
}

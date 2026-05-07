using System;
using System.Collections;
using DG.Tweening;
using Hellmade.Sound;
using I2.Loc;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

public class EvolvePlaneManager : MonoBehaviour
{
	private void Awake()
	{
		EvolvePlaneManager.current = this;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void GetDataCard(EvolvePlaneManager.DataPlane dataPlane)
	{
		this.cardPlane = dataPlane.planeData.CardEvolvePlane;
		this.totalCard = this.cardPlane + GameContext.totalUltraStarshipCard;
		this.numberCardToEvolvePlane = dataPlane.planeData.NumberCardToEvolve;
	}

	private void SetInforPlane(EvolvePlaneManager.DataPlane dataPlane, bool needCheck)
	{
		this.currentRank = dataPlane.planeData.CurrentRank;
		if (this.currentRank != GameContext.Rank.SSS.ToString())
		{
			this.nextRank = this.NextRank(this.currentRank);
		}
		else
		{
			this.nextRank = GameContext.Rank.SSS.ToString();
		}
		dataPlane.SetAnimPlane(this.nextRank);
		dataPlane.SetAnimPlane();
		if (this.saveDataPlane == dataPlane && needCheck)
		{
			return;
		}
		this.saveDataPlane = dataPlane;
		DataGame.Current.SetSpriteImagePlane(this.imgPlaneCard, dataPlane.planeData.plane);
		DataGame.Current.SetImageRank(this.imgCurrentRank, this.currentRank);
		DataGame.Current.SetImageRank(this.imgNextRank, this.nextRank);
		for (int i = 0; i < this.imgDecorCurrentRank.Length; i++)
		{
			GameContext.SetColor(this.imgDecorCurrentRank[i], this.currentRank);
		}
		for (int j = 0; j < this.imgDecorNextRank.Length; j++)
		{
			GameContext.SetColor(this.imgDecorNextRank[j], this.nextRank);
		}
		this.SetPriceEvolvePlane(this.currentRank);
		this.SetTextCard(dataPlane);
		this.textNameSkill.text = this.NameAbility(this.nextRank);
		this.textDescriptionSkill.text = this.InfoAbility(this.nextRank);
		int dps = dataPlane.planeData.DPS;
		int nextRankDPS = dataPlane.planeData.NextRankDPS;
		if (dataPlane.planeData.Level < 200)
		{
			this.textCurrentPower.text = string.Concat(new object[]
			{
				ScriptLocalization.power,
				": ",
				dps,
				string.Format("<color=#13be07ff>+{0}</color>", nextRankDPS - dps)
			});
		}
		else
		{
			this.textCurrentPower.text = ScriptLocalization.power + ": " + dps;
		}
		this.textCurrentPower.text.ToUpper();
		this.textNextMaxLevel.text = string.Empty + dataPlane.planeData.NextMaxLevel;
	}

	private void SetTextCard(EvolvePlaneManager.DataPlane dataPlane)
	{
		this.textNumberCardPlane.text = string.Empty + dataPlane.planeData.CardEvolvePlane;
		this.textNumberCardAllPlane.text = string.Empty + GameContext.totalUltraStarshipCard;
		if (GameContext.totalUltraStarshipCard > 0)
		{
			this.sliderLineCardAllPlane.gameObject.SetActive(true);
			this.sliderLineCardAllPlane.maxValue = (float)this.numberCardToEvolvePlane;
			this.sliderLineCardAllPlane.value = (float)this.numberCardToEvolvePlane - ((float)this.cardPlane + (float)(this.totalCard - this.cardPlane) / 2f);
		}
		else
		{
			this.sliderLineCardAllPlane.gameObject.SetActive(false);
		}
		if (this.cardPlane > 0)
		{
			this.sliderLineCardPlane.gameObject.SetActive(true);
			this.sliderLineCardPlane.maxValue = (float)this.numberCardToEvolvePlane;
			this.sliderLineCardPlane.value = (float)this.cardPlane / 2f;
		}
		else
		{
			this.sliderLineCardPlane.gameObject.SetActive(false);
		}
		this.textSliderCard.text = this.totalCard + "/" + this.numberCardToEvolvePlane;
		this.sliderCardPlane.maxValue = (float)this.numberCardToEvolvePlane;
		this.sliderCardPlane.value = (float)this.cardPlane;
		this.sliderCardAllPlane.maxValue = (float)this.numberCardToEvolvePlane;
		this.sliderCardAllPlane.value = (float)this.totalCard;
	}

	private void SetPriceEvolvePlane(string currentRank)
	{
		if (currentRank == GameContext.Rank.C.ToString())
		{
			this.priceEvolvePlane = PlaneDataMaxSheet.Get(1).costEvolve;
			this.imgSkill.sprite = this.sprSkillRankB;
		}
		else if (currentRank == GameContext.Rank.B.ToString())
		{
			this.priceEvolvePlane = PlaneDataMaxSheet.Get(2).costEvolve;
			this.imgSkill.sprite = this.sprSkillRankA;
		}
		else if (currentRank == GameContext.Rank.A.ToString())
		{
			this.priceEvolvePlane = PlaneDataMaxSheet.Get(3).costEvolve;
			this.imgSkill.sprite = this.sprSkillRankS;
		}
		else if (currentRank == GameContext.Rank.S.ToString())
		{
			this.priceEvolvePlane = PlaneDataMaxSheet.Get(4).costEvolve;
			this.imgSkill.sprite = this.sprSkillRankSS;
		}
		else if (currentRank == GameContext.Rank.SS.ToString())
		{
			this.priceEvolvePlane = PlaneDataMaxSheet.Get(5).costEvolve;
			this.imgSkill.sprite = this.sprSkillRankSSS;
		}
		else
		{
			this.priceEvolvePlane = 0;
		}
		this.imgSkill.SetNativeSize();
		this.textCostEvolvePlane.text = string.Empty + this.priceEvolvePlane;
	}

	private string NextRank(string currentRank)
	{
		string result = string.Empty;
		if (currentRank == GameContext.Rank.C.ToString())
		{
			result = GameContext.Rank.B.ToString();
		}
		else if (currentRank == GameContext.Rank.B.ToString())
		{
			result = GameContext.Rank.A.ToString();
		}
		else if (currentRank == GameContext.Rank.A.ToString())
		{
			result = GameContext.Rank.S.ToString();
		}
		else if (currentRank == GameContext.Rank.S.ToString())
		{
			result = GameContext.Rank.SS.ToString();
		}
		else if (currentRank == GameContext.Rank.SS.ToString())
		{
			result = GameContext.Rank.SSS.ToString();
		}
		else
		{
			result = "SSS";
		}
		return result;
	}

	private string InfoAbility(string currentRank)
	{
		string result = string.Empty;
		switch (GameContext.GetRankEnum(currentRank))
		{
		case GameContext.Rank.B:
			result = ScriptLocalization.start_game_with_power_3;
			break;
		case GameContext.Rank.A:
			result = ScriptLocalization.start_game_with_power_5;
			break;
		case GameContext.Rank.S:
			result = ScriptLocalization.increase_damage_15;
			break;
		case GameContext.Rank.SS:
			result = ScriptLocalization.start_game_with_power_8;
			break;
		case GameContext.Rank.SSS:
			result = ScriptLocalization.a_force_shield_protect_the_starship;
			break;
		default:
			result = string.Empty;
			break;
		}
		return result;
	}

	private string NameAbility(string currentRank)
	{
		string result = string.Empty;
		if (currentRank == GameContext.Rank.B.ToString())
		{
			result = ScriptLocalization.power_up_starship;
		}
		else if (currentRank == GameContext.Rank.A.ToString())
		{
			result = ScriptLocalization.power_up_starship;
		}
		else if (currentRank == GameContext.Rank.S.ToString())
		{
			result = ScriptLocalization.increase_damage;
		}
		else if (currentRank == GameContext.Rank.SS.ToString())
		{
			result = ScriptLocalization.power_up_starship;
		}
		else if (currentRank == GameContext.Rank.SSS.ToString())
		{
			result = ScriptLocalization.force_shield;
		}
		else
		{
			result = string.Empty;
		}
		return result;
	}

	public void ShowPanelEvolvePlane()
	{
		if (this.isOwnedCurrentPlane)
		{
			if (NewTutorial.current.currentStepTutorial_EvovlePlane == 5)
			{
				NewTutorial.current.EvovlePlane_Step5();
			}
			this.GetDataCard(this.arrPlane[this.indexArrPlane]);
			this.CheckShowHideButtonEvolve();
			this.panelEvolvePlane.SetActive(true);
			if (this.saveDataPlane != null)
			{
				this.SetTextCard(this.saveDataPlane);
				this.SetInforPlane(this.arrPlane[this.indexArrPlane], false);
			}
			else
			{
				this.SetInforPlane(this.arrPlane[this.indexArrPlane], true);
			}
			if (this.panelEvolSuccess.activeInHierarchy)
			{
				this.panelEvolSuccess.SetActive(false);
			}
			if (this.currentRank == "SSS")
			{
				this.btnEvolvePlane.SetActive(false);
				this.panelInfoPlane.SetActive(false);
				this.panelMaxEvolution.SetActive(true);
			}
			else
			{
				this.panelInfoPlane.SetActive(true);
				this.panelMaxEvolution.SetActive(false);
			}
			DOTween.Restart("EVOLVE_PLANE", true, -1f);
			DOTween.Play("EVOLVE_PLANE");
			EscapeManager.Current.AddAction(new Action(this.HidePanelEvolvePlane));
		}
		else
		{
			this.ShowPopupEvolutionCards();
		}
	}

	public void HidePanelEvolvePlane()
	{
		if (NewTutorial.current.currentStepTutorial_EvovlePlane == 11)
		{
			NewTutorial.current.EvovlePlane_Step11();
		}
		EscapeManager.Current.RemoveAction(new Action(this.HidePanelEvolvePlane));
		DOTween.PlayBackwards("EVOLVE_PLANE");
		this.arrPlane[this.indexArrPlane].HideAnim();
		base.StartCoroutine(this.DelayHidePanel());
		if (this.fxRank.activeInHierarchy)
		{
			this.fxRank.SetActive(false);
		}
		DOTween.Restart("FX_RANK", true, -1f);
		DOTween.Pause("FX_RANK");
	}

	private IEnumerator DelayHidePanel()
	{
		yield return new WaitForSeconds(0.05f);
		this.panelEvolvePlane.SetActive(false);
		yield break;
	}

	public void StartEvolvePlane()
	{
		if (ShopContext.currentGem >= this.priceEvolvePlane)
		{
			if (NewTutorial.current.currentStepTutorial_EvovlePlane == 7)
			{
				NewTutorial.current.EvovlePlane_Step7();
			}
			CacheGame.MinusGems(this.priceEvolvePlane);
			CurrencyLog.LogGemOut(this.priceEvolvePlane, CurrencyLog.Out.EvolvePlane, this.arrPlane[this.indexArrPlane].planeData.plane.ToString());
			int num = this.cardPlane - this.numberCardToEvolvePlane;
			if (num < 0)
			{
				int numberCardAllPlane = GameContext.totalUltraStarshipCard + num;
				CacheGame.SetNumberCardAllPlane(numberCardAllPlane);
				this.arrPlane[this.indexArrPlane].planeData.CardEvolvePlane = 0;
			}
			else
			{
				this.arrPlane[this.indexArrPlane].planeData.CardEvolvePlane = num;
			}
			this.btnEvolvePlane.GetComponent<Button>().interactable = false;
			this.fxHolderEvolvePlane.SetActive(true);
			DOTween.Restart("FX_EVOLVE_PLANE", true, -1f);
			DOTween.Play("FX_EVOLVE_PLANE");
			EazySoundManager.PlayUISound(AudioCache.UISound.evolve_1);
		}
		else
		{
			UnityEngine.Debug.Log("Not Enougt Gems");
			ShopManager.Instance.ShowPopupNotEnoughGem();
		}
	}

	public void EvolvePlane()
	{
		if (!this.isEvol)
		{
			this.isEvol = true;
			this.fxHolderEvolvePlane.SetActive(false);
			this.fxExplosionEvolvePlane.SetActive(true);
			this.arrPlane[this.indexArrPlane].planeData.CurrentRank = this.NextRank(this.currentRank);
			PlaneManager.current.EvolutionPlaneComplete();
			base.StartCoroutine(this.EvolutionSuccess());
			UnityEngine.Debug.Log("evolve now");
		}
	}

	private IEnumerator EvolutionSuccess()
	{
		yield return new WaitForSeconds(0.25f);
		EazySoundManager.PlayUISound(AudioCache.UISound.evolve_3);
		this.isEvol = false;
		this.fxExplosionEvolvePlane.SetActive(false);
		this.ActivePanelFXEvolvePlaneSuccess();
		this.panelEvolSuccess.SetActive(true);
		this.CheckCard();
		if (this.currentRank == "SSS")
		{
			this.panelInfoPlane.SetActive(false);
			this.panelMaxEvolution.SetActive(true);
			this.btnEvolvePlane.SetActive(false);
			this.iconAdd.SetActive(false);
			this.iconEvolve.SetActive(false);
		}
		yield break;
	}

	private void CheckShowHideButtonEvolve()
	{
		if (this.totalCard >= this.numberCardToEvolvePlane)
		{
			this.btnEvolvePlane.SetActive(true);
			this.btnEvolvePlane.GetComponent<Button>().interactable = true;
		}
		else
		{
			this.btnEvolvePlane.SetActive(false);
		}
	}

	public void CheckEvolutionPlane(bool isOwnedPlane, GameContext.Plane plane)
	{
		this.isOwnedCurrentPlane = isOwnedPlane;
		this.currentPlane = plane;
		switch (plane)
		{
		case GameContext.Plane.BataFD01:
			this.indexArrPlane = 0;
			break;
		case GameContext.Plane.FuryOfAres:
			this.indexArrPlane = 1;
			break;
		case GameContext.Plane.SkyWraith:
			this.indexArrPlane = 2;
			break;
		case GameContext.Plane.TwilightX:
			this.indexArrPlane = 4;
			break;
		case GameContext.Plane.Greataxe:
			this.indexArrPlane = 3;
			break;
		case GameContext.Plane.SSLightning:
			this.indexArrPlane = 5;
			break;
		case GameContext.Plane.Warlock:
			this.indexArrPlane = 6;
			break;
		}
		if (isOwnedPlane)
		{
			this.GetDataCard(this.arrPlane[this.indexArrPlane]);
			if (this.totalCard >= this.numberCardToEvolvePlane)
			{
				this.iconEvolve.SetActive(true);
				this.iconAdd.SetActive(false);
			}
			else
			{
				this.iconEvolve.SetActive(false);
				this.iconAdd.SetActive(true);
			}
		}
		else
		{
			this.iconAdd.SetActive(false);
			this.iconEvolve.SetActive(false);
		}
	}

	private void CheckCard()
	{
		this.GetDataCard(this.arrPlane[this.indexArrPlane]);
		if (this.totalCard >= this.numberCardToEvolvePlane)
		{
			this.iconEvolve.SetActive(true);
			this.iconAdd.SetActive(false);
			this.btnEvolvePlane.SetActive(true);
			this.btnEvolvePlane.GetComponent<Button>().interactable = true;
		}
		else
		{
			this.iconEvolve.SetActive(false);
			this.iconAdd.SetActive(true);
			this.btnEvolvePlane.SetActive(false);
		}
		this.SetInforPlane(this.arrPlane[this.indexArrPlane], false);
	}

	private void ActivePanelFXEvolvePlaneSuccess()
	{
		this.panelFXEvolvePlaneSuccess.SetActive(true);
		DOTween.Restart("FX_EVOLVE_PLANE_2", true, -1f);
		DOTween.Play("FX_EVOLVE_PLANE_2");
		this.fxRank.SetActive(true);
		DOTween.Restart("FX_RANK", true, -1f);
		DOTween.Play("FX_RANK");
		EscapeManager.Current.AddAction(new Action(this.DeactivePlaneFXEvolvePlaneSuccess));
	}

	public void DeactivePlaneFXEvolvePlaneSuccess()
	{
		this.panelFXEvolvePlaneSuccess.SetActive(false);
		EscapeManager.Current.RemoveAction(new Action(this.DeactivePlaneFXEvolvePlaneSuccess));
	}

	public void ShowPopupEvolutionCards()
	{
		EscapeManager.Current.AddAction(new Action(this.HidePopupEvolutionCard));
		this.popupEvolCard.SetActive(true);
		DOTween.Restart("EVOLUTION_CARD", true, -1f);
	}

	public void HidePopupEvolutionCard()
	{
		EscapeManager.Current.RemoveAction(new Action(this.HidePopupEvolutionCard));
		DOTween.PlayBackwards("EVOLUTION_CARD");
		base.StartCoroutine(GameContext.Delay(0.1f, delegate
		{
			this.popupEvolCard.SetActive(false);
		}));
	}

	public static EvolvePlaneManager current;

	public Image imgPlaneCard;

	public Image imgCurrentRank;

	public Image imgNextRank;

	public Image[] imgDecorCurrentRank;

	public Image[] imgDecorNextRank;

	public Text textNumberCardPlane;

	public Text textNumberCardAllPlane;

	public Text textSliderCard;

	public Slider sliderCardPlane;

	public Slider sliderCardAllPlane;

	public Slider sliderLineCardPlane;

	public Slider sliderLineCardAllPlane;

	public Image imgSkill;

	public Text textNameSkill;

	public Text textDescriptionSkill;

	public Text textCurrentPower;

	public Text textNextMaxLevel;

	public Text textCostEvolvePlane;

	public Sprite sprSkillRankB;

	public Sprite sprSkillRankA;

	public Sprite sprSkillRankS;

	public Sprite sprSkillRankSS;

	public Sprite sprSkillRankSSS;

	public GameObject panelEvolvePlane;

	public GameObject btnEvolvePlane;

	public GameObject iconAdd;

	public GameObject iconEvolve;

	public GameObject panelFXEvolvePlaneSuccess;

	public GameObject fxHolderEvolvePlane;

	public GameObject fxExplosionEvolvePlane;

	public GameObject fxRank;

	public GameObject panelEvolSuccess;

	public GameObject panelInfoPlane;

	public GameObject panelMaxEvolution;

	[Header("Popup Evolution Card")]
	public GameObject popupEvolCard;

	private GameContext.Plane currentPlane;

	public EvolvePlaneManager.DataPlane[] arrPlane;

	private int priceEvolvePlane;

	private int totalCard;

	private int cardPlane;

	private int numberCardToEvolvePlane;

	private string currentRank;

	private string nextRank = string.Empty;

	private EvolvePlaneManager.DataPlane saveDataPlane;

	private bool isEvol;

	private int indexArrPlane = -1;

	private bool isOwnedCurrentPlane;

	[Serializable]
	public class DataPlane
	{
		public void SetAnimPlane()
		{
			string skin = "E" + DataGame.Current.ConverRankPlaneToIndex(this.planeData.CurrentRank);
			this.skeletonGraphicPlane.gameObject.SetActive(true);
			this.skeletonGraphicPlane.Skeleton.SetSkin(skin);
		}

		public void SetAnimPlane(string nextRank)
		{
			string skin = "E" + DataGame.Current.ConverRankPlaneToIndex(nextRank);
			this.skeletonGraphicNextPlane.gameObject.SetActive(true);
			this.skeletonGraphicNextPlane.Skeleton.SetSkin(skin);
		}

		public void HideAnim()
		{
			this.skeletonGraphicPlane.gameObject.SetActive(false);
			this.skeletonGraphicNextPlane.gameObject.SetActive(false);
		}

		public PlaneData planeData;

		public SkeletonGraphic skeletonGraphicPlane;

		public SkeletonGraphic skeletonGraphicNextPlane;
	}
}

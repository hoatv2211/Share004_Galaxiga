using System;
using System.Collections;
using DG.Tweening;
using Hellmade.Sound;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class EvolveDroneManager : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private void GetDataCard(DroneData droneData)
	{
		this.cardDrone = droneData.CardEvolveDrone;
		this.totalCard = this.cardDrone + GameContext.totalUltraDroneCard;
		this.numberCardToEvolveDrone = droneData.NumberCardToEvolve;
	}

	private void SetInforDrone(DroneData droneData)
	{
		this.currentRank = droneData.CurrentRank;
		string rank = this.NextRank(this.currentRank);
		DataGame.Current.SetImageRank(this.imgCurrentRank, this.currentRank);
		DataGame.Current.SetImageRank(this.imgNextRank, rank);
		for (int i = 0; i < this.imgDecorCurrentRank.Length; i++)
		{
			GameContext.SetColor(this.imgDecorCurrentRank[i], this.currentRank);
		}
		for (int j = 0; j < this.imgDecorNextRank.Length; j++)
		{
			GameContext.SetColor(this.imgDecorNextRank[j], rank);
		}
		this.SetPriceEvolveDrone(this.currentRank);
		this.textNumberCardDrone.text = string.Empty + this.cardDrone;
		this.textNumberCardAllDrone.text = string.Empty + GameContext.totalUltraDroneCard;
		this.textSliderCard.text = this.totalCard + "/" + this.numberCardToEvolveDrone;
		if (this.cardDrone > 0)
		{
			this.sliderLineCardDrone.gameObject.SetActive(true);
			this.sliderLineCardDrone.maxValue = (float)this.numberCardToEvolveDrone;
			this.sliderLineCardDrone.value = (float)this.cardDrone / 2f;
		}
		else
		{
			this.sliderLineCardDrone.gameObject.SetActive(false);
		}
		if (GameContext.totalUltraDroneCard > 0)
		{
			this.sliderLineCardAllDrone.gameObject.SetActive(true);
			this.sliderLineCardAllDrone.maxValue = (float)this.numberCardToEvolveDrone;
			this.sliderLineCardAllDrone.value = (float)this.numberCardToEvolveDrone - ((float)this.cardDrone + (float)(this.totalCard - this.cardDrone) / 2f);
		}
		else
		{
			this.sliderLineCardAllDrone.gameObject.SetActive(false);
		}
		this.sliderCardDrone.maxValue = (float)this.numberCardToEvolveDrone;
		this.sliderCardDrone.value = (float)this.cardDrone;
		this.sliderCardAllDrone.maxValue = (float)this.numberCardToEvolveDrone;
		this.sliderCardAllDrone.value = (float)this.totalCard;
		this.textNameSkill.text = ScriptLocalization.notify_coming_soon;
		this.textDescriptionSkill.text = ScriptLocalization.notify_coming_soon;
		int dps = droneData.DPS;
		int nexDPSRank = droneData.NexDPSRank;
		if (this.arrDrones[this.indexArrDrone].droneData.Level < 200)
		{
			this.textCurrentPower.text = string.Concat(new object[]
			{
				ScriptLocalization.power,
				": ",
				dps,
				string.Format("<color=#13be07ff>+{0}</color>", nexDPSRank - dps)
			});
		}
		else
		{
			this.textCurrentPower.text = ScriptLocalization.power + ": " + dps;
		}
		this.textCurrentPower.text.ToUpper();
		this.textNextMaxLevel.text = string.Empty + droneData.NextMaxLevel;
	}

	private void SetPriceEvolveDrone(string currentRank)
	{
		if (currentRank == GameContext.Rank.C.ToString())
		{
			this.priceEvolveDrone = DroneDataMaxSheet.Get(1).costEvolve;
		}
		else if (currentRank == GameContext.Rank.B.ToString())
		{
			this.priceEvolveDrone = DroneDataMaxSheet.Get(2).costEvolve;
		}
		else if (currentRank == GameContext.Rank.A.ToString())
		{
			this.priceEvolveDrone = DroneDataMaxSheet.Get(3).costEvolve;
		}
		else if (currentRank == GameContext.Rank.S.ToString())
		{
			this.priceEvolveDrone = DroneDataMaxSheet.Get(4).costEvolve;
		}
		else if (currentRank == GameContext.Rank.SS.ToString())
		{
			this.priceEvolveDrone = DroneDataMaxSheet.Get(5).costEvolve;
		}
		else
		{
			this.priceEvolveDrone = 0;
		}
		this.textCostEvolvePlane.text = string.Empty + this.priceEvolveDrone;
	}

	private string NextRank(string currentRank)
	{
		string result = string.Empty;
		if (currentRank == GameContext.Rank.A.ToString())
		{
			result = GameContext.Rank.S.ToString();
		}
		else if (currentRank == GameContext.Rank.B.ToString())
		{
			result = GameContext.Rank.A.ToString();
		}
		else if (currentRank == GameContext.Rank.C.ToString())
		{
			result = GameContext.Rank.B.ToString();
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
			result = string.Empty;
		}
		return result;
	}

	private int NextPower(DroneData droneData)
	{
		return 0;
	}

	private int NextSpecPower(DroneData droneData)
	{
		return 0;
	}

	private string InfoAbility(string currentRank)
	{
		string result = string.Empty;
		if (currentRank == GameContext.Rank.B.ToString())
		{
			result = ScriptLocalization.decrease_fire_rate_by + " 20%";
		}
		else if (currentRank == GameContext.Rank.A.ToString())
		{
			result = ScriptLocalization.decrease_cooldown_skill_by + " 12%";
		}
		else if (currentRank == GameContext.Rank.S.ToString())
		{
			result = ScriptLocalization.increase_damage + " 15%";
		}
		else if (currentRank == GameContext.Rank.SS.ToString())
		{
			result = ScriptLocalization.decrease_cooldown_skill_by + " 18%";
		}
		else if (currentRank == GameContext.Rank.SSS.ToString())
		{
			result = ScriptLocalization.increase_damage_of_skill + " 15%";
		}
		else
		{
			result = string.Empty;
		}
		return result;
	}

	private string NameAbility(string currentRank)
	{
		string result = string.Empty;
		if (currentRank == GameContext.Rank.B.ToString())
		{
			result = ScriptLocalization.decrease_fire_rate;
		}
		else if (currentRank == GameContext.Rank.A.ToString())
		{
			result = ScriptLocalization.decrease_cooldown;
		}
		else if (currentRank == GameContext.Rank.S.ToString())
		{
			result = ScriptLocalization.increase_power;
		}
		else if (currentRank == GameContext.Rank.SS.ToString())
		{
			result = ScriptLocalization.decrease_cooldown;
		}
		else if (currentRank == GameContext.Rank.SSS.ToString())
		{
			result = ScriptLocalization.increase_power_of_skill;
		}
		else
		{
			result = string.Empty;
		}
		return result;
	}

	public void ShowPanelEvolveDrone()
	{
		if (this.isOwnedCurrentDrone)
		{
			this.arrDrones[this.indexArrDrone].SetSpriteImage(this.imgDrone);
			this.arrDrones[this.indexArrDrone].SetSpriteImage(this.imgDroneCard);
			this.arrDrones[this.indexArrDrone].SetSpriteImage(this.imgDroneEvolution);
			this.CheckShowHideButtonEvolve();
			this.SetInforDrone(this.arrDrones[this.indexArrDrone].droneData);
			this.panelEvolveDrone.SetActive(true);
			if (this.panelEvolSuccess.activeInHierarchy)
			{
				this.panelEvolSuccess.SetActive(false);
			}
			if (this.currentRank == "SSS")
			{
				this.btnEvolveDrone.SetActive(false);
				this.panelInfoDrone.SetActive(false);
				this.panelMaxEvolution.SetActive(true);
			}
			else
			{
				this.panelInfoDrone.SetActive(true);
				this.panelMaxEvolution.SetActive(false);
			}
			DOTween.Restart("EVOLVE_DRONE", true, -1f);
			DOTween.Play("EVOLVE_DRONE");
			EscapeManager.Current.AddAction(new Action(this.HidePanelEvolveDrone));
		}
		else
		{
			this.evolvePlaneManager.ShowPopupEvolutionCards();
		}
	}

	public void HidePanelEvolveDrone()
	{
		EscapeManager.Current.RemoveAction(new Action(this.HidePanelEvolveDrone));
		DOTween.PlayBackwards("EVOLVE_DRONE");
		base.StartCoroutine(this.DelayHidePanel());
		if (this.fxRank.activeInHierarchy)
		{
			this.fxRank.SetActive(false);
		}
		DOTween.Restart("FX_RANK_DRONE", true, -1f);
		DOTween.Pause("FX_RANK_DRONE");
	}

	private IEnumerator DelayHidePanel()
	{
		yield return new WaitForSeconds(0.05f);
		this.panelEvolveDrone.SetActive(false);
		yield break;
	}

	public void StartEvolveDrone()
	{
		if (ShopContext.currentGem >= this.priceEvolveDrone)
		{
			CacheGame.MinusGems(this.priceEvolveDrone);
			CurrencyLog.LogGemOut(this.priceEvolveDrone, CurrencyLog.Out.EvolveDrone, this.arrDrones[this.indexArrDrone].droneData.droneID.ToString());
			int num = this.cardDrone - this.numberCardToEvolveDrone;
			if (num < 0)
			{
				int numberCardAllDrone = GameContext.totalUltraDroneCard + num;
				CacheGame.SetNumberCardAllDrone(numberCardAllDrone);
				this.arrDrones[this.indexArrDrone].droneData.CardEvolveDrone = 0;
			}
			else
			{
				this.arrDrones[this.indexArrDrone].droneData.CardEvolveDrone = num;
			}
			EazySoundManager.PlayUISound(AudioCache.UISound.evolve_1);
			this.btnEvolveDrone.GetComponent<Button>().interactable = false;
			this.fxHolderEvolveDrone.SetActive(true);
			DOTween.Restart("FX_EVOLVE_DRONE", true, -1f);
			DOTween.Play("FX_EVOLVE_DRONE");
		}
		else
		{
			ShopManager.Instance.ShowPopupNotEnoughGem();
		}
	}

	public void EvolveDrone()
	{
		UnityEngine.Debug.Log("evolve now");
		this.fxHolderEvolveDrone.SetActive(false);
		this.fxExplosionEvolveDrone.SetActive(true);
		base.StartCoroutine(this.EvolutionSuccess());
	}

	private IEnumerator EvolutionSuccess()
	{
		this.arrDrones[this.indexArrDrone].droneData.CurrentRank = this.NextRank(this.currentRank);
		UnityEngine.Debug.Log("Evolve success. rank drone " + this.arrDrones[this.indexArrDrone].droneData.CurrentRank);
		DroneManager.current.EvolutionDroneComplete();
		yield return new WaitForSeconds(0.25f);
		EazySoundManager.PlayUISound(AudioCache.UISound.evolve_3);
		this.fxExplosionEvolveDrone.SetActive(false);
		this.ActivePanelFXEvolveDroneSuccess();
		this.panelEvolSuccess.SetActive(true);
		this.CheckCard();
		if (this.currentRank == "SSS")
		{
			this.panelMaxEvolution.SetActive(true);
			this.btnEvolveDrone.SetActive(false);
			this.panelInfoDrone.SetActive(false);
			this.iconEvolve.SetActive(false);
			this.iconAdd.SetActive(false);
		}
		yield break;
	}

	private void CheckShowHideButtonEvolve()
	{
		if (this.totalCard >= this.numberCardToEvolveDrone)
		{
			this.btnEvolveDrone.SetActive(true);
			this.btnEvolveDrone.GetComponent<Button>().interactable = true;
		}
		else
		{
			this.btnEvolveDrone.SetActive(false);
		}
	}

	public void CheckEvolutionDrone(bool isOwnedDrone, GameContext.Drone drone)
	{
		this.isOwnedCurrentDrone = isOwnedDrone;
		this.indexArrDrone = drone - GameContext.Drone.GatlingGun;
		if (isOwnedDrone)
		{
			this.GetDataCard(this.arrDrones[this.indexArrDrone].droneData);
			if (this.totalCard >= this.numberCardToEvolveDrone)
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
			this.iconEvolve.SetActive(false);
			this.iconAdd.SetActive(true);
		}
	}

	private void CheckCard()
	{
		this.GetDataCard(this.arrDrones[this.indexArrDrone].droneData);
		if (this.totalCard >= this.numberCardToEvolveDrone)
		{
			this.iconEvolve.SetActive(true);
			this.iconAdd.SetActive(false);
			this.btnEvolveDrone.SetActive(true);
			this.btnEvolveDrone.GetComponent<Button>().interactable = true;
		}
		else
		{
			this.iconEvolve.SetActive(false);
			this.iconAdd.SetActive(true);
			this.btnEvolveDrone.SetActive(false);
		}
		this.SetInforDrone(this.arrDrones[this.indexArrDrone].droneData);
	}

	private void ActivePanelFXEvolveDroneSuccess()
	{
		this.panelFXEvolveDroneSuccess.SetActive(true);
		DOTween.Restart("FX_EVOLVE_DRONE_2", true, -1f);
		DOTween.Play("FX_EVOLVE_DRONE_2");
		this.fxRank.SetActive(true);
		DOTween.Restart("FX_RANK_DRONE", true, -1f);
		DOTween.Play("FX_RANK_DRONE");
		EscapeManager.Current.AddAction(new Action(this.DeactivePlaneFXEvolvePlaneSuccess));
	}

	public void DeactivePlaneFXEvolvePlaneSuccess()
	{
		this.panelFXEvolveDroneSuccess.SetActive(false);
		EscapeManager.Current.RemoveAction(new Action(this.DeactivePlaneFXEvolvePlaneSuccess));
	}

	public Image imgDrone;

	public Image imgDroneEvolution;

	public Image imgDroneCard;

	public Image imgCurrentRank;

	public Image imgNextRank;

	public Image[] imgDecorCurrentRank;

	public Image[] imgDecorNextRank;

	public Text textNumberCardDrone;

	public Text textNumberCardAllDrone;

	public Text textSliderCard;

	public Slider sliderCardDrone;

	public Slider sliderCardAllDrone;

	public Slider sliderLineCardDrone;

	public Slider sliderLineCardAllDrone;

	public Image imgSkill;

	public Text textNameSkill;

	public Text textDescriptionSkill;

	public Text textCurrentPower;

	public Text textNextMaxLevel;

	public Text textCostEvolvePlane;

	public GameObject panelEvolveDrone;

	public GameObject btnEvolveDrone;

	public GameObject iconAdd;

	public GameObject iconEvolve;

	public GameObject panelFXEvolveDroneSuccess;

	public GameObject fxHolderEvolveDrone;

	public GameObject fxExplosionEvolveDrone;

	public GameObject fxRank;

	public GameObject panelEvolSuccess;

	public GameObject panelInfoDrone;

	public GameObject panelMaxEvolution;

	public EvolvePlaneManager evolvePlaneManager;

	public EvolveDroneManager.DataDrone[] arrDrones;

	private int priceEvolveDrone;

	private int totalCard;

	private int cardDrone;

	private int numberCardToEvolveDrone;

	private string currentRank;

	private int indexArrDrone = -1;

	private bool isOwnedCurrentDrone;

	[Serializable]
	public class DataDrone
	{
		public void SetSpriteImage(Image imgDrone)
		{
			imgDrone.sprite = this.sprDrone;
		}

		public DroneData droneData;

		public Sprite sprDrone;
	}
}

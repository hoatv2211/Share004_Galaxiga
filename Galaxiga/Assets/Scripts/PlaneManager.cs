using System;
using System.Collections;
using DG.Tweening;
using Hellmade.Sound;
using I2.Loc;
using SnapScrollView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlaneManager : MonoBehaviour
{
	private void Awake()
	{
		PlaneManager.current = this;
		this.actionCardChange = delegate(Component sender, object param)
		{
			this.CheckEnableEvolveAllPlane();
		};
		EventDispatcher.Instance.RegisterListener(EventID.UltraStarshipCard, this.actionCardChange);
		this.currentPlaneEquiped = CacheGame.GetPlaneEquiped();
		this.CheckAllPlane();
	}

	private void Start()
	{
		this.btnNextPage.onClick.AddListener(new UnityAction(this.NextPage));
		this.btnPrevPage.onClick.AddListener(new UnityAction(this.PrevPage));
		this.SetAnimatorPlaneHome(this.currentPlaneEquiped);
	}

	public void CheckAllPlane()
	{
		int num = this.arrPlanes.Length;
		for (int i = 0; i < num; i++)
		{
			this.arrPlanes[i].CheckPlane();
			this.arrPlanes[i].CheckEquipPlane(this.objEquipped);
		}
	}

	public void CheckPlane(GameContext.Plane plane)
	{
		int num = this.arrPlanes.Length;
		for (int i = 0; i < num; i++)
		{
			if (this.arrPlanes[i].planeID == plane)
			{
				this.arrPlanes[i].CheckPlane();
				break;
			}
		}
	}

	public void CheckEvolAllPlane()
	{
		int num = this.arrPlanes.Length;
		for (int i = 0; i < num; i++)
		{
			this.arrPlanes[i].CheckEnableEvolve();
		}
	}

	public void CheckPlaneStarterPack()
	{
		this.arrPlanes[2].inforPlane.CheckPlane();
		this.arrPlanes[2].CheckPlane();
	}

	public void CheckEnableEvolveAllPlane()
	{
		int num = this.arrPlanes.Length;
		for (int i = 0; i < num; i++)
		{
			this.arrPlanes[i].CheckEnableEvolve();
		}
		this.SetTextCardEvolvePlane();
	}

	[EnumAction(typeof(GameContext.Plane))]
	public void SelectPlane(int planeID)
	{
		if (this.savePlane != planeID)
		{
			this.savePlane = planeID;
			this.currentPlane = (GameContext.Plane)planeID;
			this.currentIndexPlane = planeID;
			this.arrPlanes[this.currentIndexPlane].SelectPlane(this.objSelect);
			this.scrollSnapPlane.MoveToIndex(this.arrPlanes[this.currentIndexPlane].indexHSS);
			this.SetActionButtonTrySwitchPlane();
			this.evolvePlaneManager.CheckEvolutionPlane(this.arrPlanes[this.currentIndexPlane].isOwned, this.currentPlane);
			this.SetIconAbiRank();
		}
		this.SetTextCardEvolvePlane();
		if (NewTutorial.current.currentStepTutorial_UpgradePlane == 5 && this.currentPlane == GameContext.Plane.FuryOfAres)
		{
			NewTutorial.current.UpgradePlane_Step5();
		}
	}

	public void SetTextCardEvolvePlane()
	{
		int num = GameContext.totalUltraStarshipCard + this.arrPlanes[this.currentIndexPlane].CardPlane;
		this.textCurrentCardCraft.text = num + "/" + this.arrPlanes[this.currentIndexPlane].TotalCard;
		this.sliderCardEvolvePlane.maxValue = (float)this.arrPlanes[this.currentIndexPlane].TotalCard;
		this.sliderCardEvolvePlane.value = (float)num;
	}

	public void EvolutionPlaneComplete()
	{
		this.CheckEnableEvolveAllPlane();
		this.arrPlanes[this.currentIndexPlane].EvolutionComplete();
		this.SetIconAbiRank();
		UnityEngine.Debug.Log("rank plane " + this.arrPlanes[this.currentIndexPlane].planeID.ToString() + " " + this.arrPlanes[this.currentIndexPlane].Rank);
		if (this.currentIndexPlane == this.currentPlaneEquiped)
		{
			this.SetAnimatorPlaneHome(this.currentIndexPlane);
		}
	}

	private void SetActionButtonTrySwitchPlane()
	{
		this.btnTrySwitchPlane.onClick.RemoveAllListeners();
		if (this.arrPlanes[this.currentIndexPlane].isUnlock)
		{
			this.btnTrySwitchPlane.gameObject.SetActive(true);
			if (this.arrPlanes[this.currentIndexPlane].isOwned)
			{
				if (this.currentPlaneEquiped == (int)this.currentPlane)
				{
					this.textBtnSwitchPlane.text = ScriptLocalization.selected;
					this.btnTrySwitchPlane.interactable = false;
				}
				else
				{
					this.textBtnSwitchPlane.text = ScriptLocalization.switch_drone;
					this.btnTrySwitchPlane.onClick.AddListener(new UnityAction(this.SwitchPlane));
					this.btnTrySwitchPlane.interactable = true;
				}
			}
			else
			{
				this.btnTrySwitchPlane.interactable = true;
				this.textBtnSwitchPlane.text = ScriptLocalization.try_starship;
				this.btnTrySwitchPlane.onClick.AddListener(new UnityAction(this.TestPlane));
			}
		}
		else
		{
			this.btnTrySwitchPlane.interactable = true;
			this.textBtnSwitchPlane.text = ScriptLocalization.try_starship;
			this.btnTrySwitchPlane.onClick.AddListener(new UnityAction(this.TestPlane));
		}
	}

	public void TestPlane()
	{
		GameContext.sceneNameTryPlane = SceneContext.sceneName;
		GameContext.Reset();
		GameContext.ResetLevel();
		GameContext.currentLive = 100;
		GameContext.isTryPlane = true;
		GameContext.planeTry = this.currentPlane;
		GameContext.currentNumberSkillPlane = 5;
		LoadingScenes.Current.LoadLevel("LevelTry");
	}

	public void CraftPlane()
	{
		UnityEngine.Debug.Log("Craft Plane " + this.currentPlane);
	}

	private void SwitchPlane()
	{
		UnityEngine.Debug.Log("Switch Plane " + this.currentPlane);
		if (NewTutorial.current.currentStepTutorial_UpgradePlane == 7)
		{
			NewTutorial.current.UpgradePlane_Step7();
		}
		EazySoundManager.PlayUISound(AudioCache.Sound.switch_airship);
		CacheGame.SetPlaneEquip(this.currentPlane);
		this.currentPlaneEquiped = (int)this.currentPlane;
		GameContext.currentPlaneIDEquiped = this.currentPlaneEquiped;
		this.SetActionButtonTrySwitchPlane();
		this.arrPlanes[this.currentIndexPlane].CheckEquipPlane(this.objEquipped);
		this.SetAnimatorPlaneHome(this.currentPlaneEquiped);
	}

	private void SetInfoPlane()
	{
		this.arrPlanes[this.currentIndexPlane].inforPlane.CheckPlane();
	}

	public void SelectTabStarship()
	{
		if (this.currentTab != 0)
		{
			this.currentTab = 0;
			this.imgTabStarship.sprite = this.spriteTab;
			this.imgTabDrone.sprite = this.spriteTabHide;
			this.objPanelStarship.SetActive(true);
			this.objPanelDrone.SetActive(false);
			this.objBarStarship.SetActive(true);
			this.objBarDrone.SetActive(false);
		}
	}

	public void SelectTabDrone()
	{
		if (this.currentTab != 1)
		{
			this.currentTab = 1;
			this.imgTabDrone.sprite = this.spriteTab;
			this.imgTabStarship.sprite = this.spriteTabHide;
			this.objPanelDrone.SetActive(true);
			this.objPanelStarship.SetActive(false);
			this.objBarDrone.SetActive(true);
			this.objBarStarship.SetActive(false);
			if (this.isFirstOpenTabDrone)
			{
				this.isFirstOpenTabDrone = false;
				this.droneManager.SelectTabDrone();
			}
		}
	}

	public void ShowPanelSelectDrone()
	{
		if (NewTutorial.current.currentStepTutorial_EquipDrone == 3)
		{
			NewTutorial.current.EquipDrone_Step3();
		}
		if (this.needUpdateToServer)
		{
			this.needUpdateToServer = false;
		}
		this.panelPlane.SetActive(true);
		this.SelectTabDrone();
		DOTween.Restart("PLANE_MANAGER", true, -1f);
	}

	public void ShowPanelSelectPlane(GameContext.Plane plane)
	{
		if (this.needUpdateToServer)
		{
			this.needUpdateToServer = false;
		}
		EscapeManager.Current.AddAction(new Action(this.HidePanelSelectPlane));
		this.SelectTabStarship();
		this.panelPlane.transform.position = Vector3.zero;
		this.panelPlane.transform.localScale = Vector3.one;
		this.panelPlane.SetActive(true);
		if (!this.isAddListener)
		{
			this.scrollSnapPlane.onLerpComplete.AddListener(new UnityAction(this.OnLerpComplete));
			this.isAddListener = true;
			this.maxPage = this.scrollSnapPlane.CalculateMaxIndex();
		}
		this.SelectPlane((int)plane);
	}

	public void ShowPanelSelectPlane()
	{
		if (NewTutorial.current.currentStepTutorial_UpgradePlane == 3)
		{
			NewTutorial.current.UpgradePlane_Step3();
		}
		if (NewTutorial.current.currentStepTutorial_EvovlePlane == 3)
		{
			NewTutorial.current.EvovlePlane_Step3();
		}
		if (this.needUpdateToServer)
		{
			this.needUpdateToServer = false;
		}
		EscapeManager.Current.AddAction(new Action(this.HidePanelSelectPlane));
		this.SelectTabStarship();
		this.panelPlane.SetActive(true);
		if (!this.isAddListener)
		{
			this.scrollSnapPlane.onLerpComplete.AddListener(new UnityAction(this.OnLerpComplete));
			this.maxPage = this.scrollSnapPlane.CalculateMaxIndex();
			this.isAddListener = true;
		}
		DOTween.Restart("PLANE_MANAGER", true, -1f);
		this.SelectPlane(this.currentPlaneEquiped);
	}

	private void OnLerpComplete()
	{
		this.SelectPlaneFollowID(this.scrollSnapPlane.cellIndex);
		UnityEngine.Debug.Log("Lerp complete " + this.scrollSnapPlane.cellIndex);
	}

	public void NextPage()
	{
		this.currentIndexPage = this.scrollSnapPlane.cellIndex;
		if (this.currentIndexPage < this.maxPage)
		{
			this.currentIndexPage++;
			this.SelectPlaneFollowID(this.currentIndexPage);
		}
	}

	public void PrevPage()
	{
		this.currentIndexPage = this.scrollSnapPlane.cellIndex;
		if (this.currentIndexPage > 0)
		{
			this.currentIndexPage--;
			this.SelectPlaneFollowID(this.currentIndexPage);
		}
	}

	private void SelectPlaneFollowID(int idPlane)
	{
		this.SelectPlane(idPlane);
	}

	private IEnumerator DelaySelectPlane()
	{
		yield return new WaitForSeconds(0.1f);
		this.SelectPlane(this.currentPlaneEquiped);
		yield break;
	}

	private IEnumerator DelayHidePanelPlane()
	{
		yield return new WaitForSeconds(0.05f);
		this.panelPlane.SetActive(false);
		//if (this.needUpdateToServer)
		//{
		//	AccountManager.Instance.UpdateToServer("PlaneManager - Update To Server");
		//}
		yield break;
	}

	public void HidePanelSelectPlane()
	{
		EscapeManager.Current.RemoveAction(new Action(this.HidePanelSelectPlane));
		this.currentTab = -1;
		DOTween.PlayBackwards("PLANE_MANAGER");
		base.StartCoroutine(this.DelayHidePanelPlane());
		if (NewTutorial.current.currentStepTutorial_UpgradePlane == 13)
		{
			NewTutorial.current.UpgradePlane_Step13();
		}
		if (NewTutorial.current.currentStepTutorial_EquipDrone == 9)
		{
			NewTutorial.current.EquipDrone_Step9();
		}
	}

	public void SetOwenedPlane(bool isIAP)
	{
		if (isIAP)
		{
			CacheGame.SetUnlockPlane(this.currentPlane);
			this.arrPlanes[this.currentIndexPlane].objFadeLock.SetActive(false);
		}
		if (!this.needUpdateToServer)
		{
			this.needUpdateToServer = true;
		}
		CacheGame.SetOwnedPlane(this.currentPlane);
		this.arrPlanes[this.currentIndexPlane].CheckPlane();
		this.SetActionButtonTrySwitchPlane();
		this.SetInfoPlane();
		this.ShowPopupOwnedPlane(this.currentPlane);
		this.evolvePlaneManager.CheckEvolutionPlane(this.arrPlanes[this.currentIndexPlane].isOwned, this.currentPlane);
	}

	public void SetOwenedPlane()
	{
		if (this.currentIndexPlane >= 0 && this.currentIndexPlane < this.arrPlanes.Length)
		{
			this.SetActionButtonTrySwitchPlane();
			this.SetInfoPlane();
			this.ShowPopupOwnedPlane(this.currentPlane);
			this.evolvePlaneManager.CheckEvolutionPlane(this.arrPlanes[this.currentIndexPlane].isOwned, this.currentPlane);
			if (!this.needUpdateToServer)
			{
				this.needUpdateToServer = true;
			}
		}
	}

	public void CheckEvolutionPlane(GameContext.Plane planeID)
	{
		this.evolvePlaneManager.CheckEvolutionPlane(true, planeID);
	}

	public void BuyPlaneCoin()
	{
		int priceCoins = this.arrPlanes[this.currentIndexPlane].PriceCoins;
		if (ShopContext.currentCoin >= priceCoins)
		{
			CacheGame.MinusCoins(priceCoins);
			CurrencyLog.LogGoldOut(priceCoins, CurrencyLog.Out.BuyPlane, this.arrPlanes[this.currentIndexPlane].planeID.ToString());
			this.SetOwenedPlane(false);
		}
		else
		{
			ShopManager.Instance.ShowPopupNotEnoughCoin();
		}
	}

	public void BuyPlaneGem()
	{
		int priceGems = this.arrPlanes[this.currentIndexPlane].PriceGems;
		if (ShopContext.currentGem >= priceGems)
		{
			CacheGame.MinusGems(priceGems);
			CurrencyLog.LogGemOut(priceGems, CurrencyLog.Out.BuyPlane, this.arrPlanes[this.currentIndexPlane].planeID.ToString());
			this.SetOwenedPlane(false);
		}
		else
		{
			ShopManager.Instance.ShowPopupNotEnoughGem();
		}
	}

	private void SetAnimatorPlaneHome(int planeID)
	{
		if (HomeManager.current != null)
		{
			HomeManager.current.ChangeImagePlane(planeID);
		}
	}

	public void HidePlaneUI()
	{
		if (this.panelPlane.activeInHierarchy)
		{
			this.panelPlane.SetActive(false);
		}
	}

	public void ShowPopupOwnedPlane(GameContext.Plane _planeID)
	{
		this.SetViewPlane(_planeID);
		this.popupUnlockPlane.SetActive(true);
		DOTween.Restart("OWNED_PLANE", true, -1f);
		EscapeManager.Current.AddAction(new Action(this.HidePopupOwnedPlane));
	}

	public void HidePopupOwnedPlane()
	{
		EscapeManager.Current.RemoveAction(new Action(this.HidePopupOwnedPlane));
		DOTween.PlayBackwards("OWNED_PLANE");
		base.StartCoroutine(GameContext.Delay(0.1f, delegate
		{
			this.popupUnlockPlane.SetActive(false);
		}));
	}

	private void SetViewPlane(GameContext.Plane planeID)
	{
		this.imgPlane.sprite = DataGame.Current.SpritePlane(planeID);
		DataGame.Current.SetImageRank(this.imgRank, DataGame.InfoPlane.CurrentRank);
		this.textNamePlaneUnlock.text = this.arrPlanes[(int)planeID].inforPlane.NamePlane;
		this.textPower.text = this.arrPlanes[(int)planeID].inforPlane.textPower.text;
		this.sliderDamage.value = this.arrPlanes[(int)planeID].sliderDamage.value;
		this.sliderFireRate.value = this.arrPlanes[(int)planeID].sliderFireRate.value;
		this.sliderSuperDamage.value = this.arrPlanes[(int)planeID].sliderSuperDamage.value;
		this.textNameSkill.text = DataGame.Current.NameSkillPlane((int)planeID);
		this.textDescriptionSkill.text = DataGame.Current.DesSkillPlane((int)planeID);
	}

	public void ShowPopupRankPlaneDroneInfo()
	{
		EscapeManager.Current.AddAction(new Action(this.HidePopupRankPlaneDroneInfo));
		if (this.currentTab == 0)
		{
			this.popupStarshipRankInfo.SetActive(true);
			DOTween.Restart("RANK_PLANE_INFO", true, -1f);
		}
		else
		{
			this.popupDroneRankInfo.SetActive(true);
			DOTween.Restart("RANK_DRONE_INFO", true, -1f);
		}
	}

	public void HidePopupRankPlaneDroneInfo()
	{
		EscapeManager.Current.RemoveAction(new Action(this.HidePopupRankPlaneDroneInfo));
		if (this.currentTab == 0)
		{
			DOTween.PlayBackwards("RANK_PLANE_INFO");
			base.StartCoroutine(GameContext.Delay(0.1f, delegate
			{
				this.popupStarshipRankInfo.SetActive(false);
			}));
		}
		else
		{
			DOTween.PlayBackwards("RANK_DRONE_INFO");
			base.StartCoroutine(GameContext.Delay(0.1f, delegate
			{
				this.popupDroneRankInfo.SetActive(false);
			}));
		}
	}

	private void SetIconAbiRank()
	{
		int currentIDRank = this.arrPlanes[this.currentIndexPlane].currentIDRank;
		for (int i = 0; i < this.arrImgAblityRank.Length; i++)
		{
			if (i < currentIDRank)
			{
				this.arrImgAblityRank[i].color = Color.white;
			}
			else
			{
				this.arrImgAblityRank[i].color = Color.grey;
			}
		}
	}

	public EvolvePlaneManager evolvePlaneManager;

	public DroneManager droneManager;

	public static PlaneManager current;

	public ScrollSnap scrollSnapPlane;

	public Text textCurrentCardCraft;

	public Button btnTrySwitchPlane;

	public Text textBtnSwitchPlane;

	public Slider sliderCardEvolvePlane;

	public GameObject panelPlane;

	public GameObject objPanelStarship;

	public GameObject objPanelDrone;

	public GameObject objBarStarship;

	public GameObject objBarDrone;

	public SetSiblingRectransfom setSiblingRectransfom;

	public Transform objEquipped;

	public Transform objSelect;

	public PlaneManager.Planes[] arrPlanes;

	public Button btnNextPage;

	public Button btnPrevPage;

	public Image imgTabStarship;

	public Image imgTabDrone;

	public Sprite spriteTab;

	public Sprite spriteTabHide;

	[Header("Popup Unlock Plane")]
	public GameObject popupUnlockPlane;

	public Text textNamePlaneUnlock;

	public Text textPower;

	public Text textNameSkill;

	public Text textDescriptionSkill;

	public Slider sliderDamage;

	public Slider sliderFireRate;

	public Slider sliderSuperDamage;

	public Image imgPlane;

	public Image imgRank;

	[Header("Popup Rank Starship")]
	public GameObject popupStarshipRankInfo;

	[Header("Popup Rank Drone")]
	public GameObject popupDroneRankInfo;

	[HideInInspector]
	public bool needUpdateToServer;

	public Image[] arrImgAblityRank;

	private GameContext.Plane currentPlane;

	[HideInInspector]
	public int currentIndexPlane;

	private int currentPlaneEquiped = -1;

	private Action<Component, object> actionCardChange;

	private int savePlane = -1;

	private int currentTab = -1;

	private bool isFirstOpenTabDrone = true;

	private bool isAddListener;

	private int currentIndexPage;

	private int maxPage;

	[Serializable]
	public class Planes
	{
		public string Name
		{
			get
			{
				return this.inforPlane.planeData.name;
			}
		}

		public int PriceCoins
		{
			get
			{
				return this.inforPlane.planeData.priceCoin;
			}
		}

		public int PriceGems
		{
			get
			{
				return this.inforPlane.planeData.priceGem;
			}
		}

		public int LevelUnlock
		{
			get
			{
				return this.inforPlane.planeData.LevelUnlock;
			}
		}

		public int TotalCard
		{
			get
			{
				return this.inforPlane.planeData.NumberCardToEvolve;
			}
		}

		public int CardPlane
		{
			get
			{
				return this.inforPlane.planeData.CardEvolvePlane;
			}
		}

		public int Vip
		{
			get
			{
				return this.inforPlane.planeData.Vip;
			}
		}

		public string Rank
		{
			get
			{
				string currentRank = this.inforPlane.planeData.CurrentRank;
				this.currentIDRank = DataGame.Current.ConverRankPlaneToIndex(currentRank);
				return currentRank;
			}
		}

		public void SelectPlane(Transform transformObjSelect)
		{
			transformObjSelect.parent = this.imgRank.gameObject.transform;
			transformObjSelect.localPosition = Vector3.zero;
		}

		private void CheckStateLockPlane()
		{
			this.isUnlock = CacheGame.IsUnlockPlane(this.planeID);
			if (this.isUnlock)
			{
				this.objFadeLock.SetActive(false);
				this.objLock.SetActive(true);
			}
			else if (CacheGame.IsPassLevel(this.inforPlane.planeData.ModeLevelUnlock, this.LevelUnlock))
			{
				CacheGame.SetUnlockPlane(this.planeID);
				this.objFadeLock.SetActive(false);
				this.isUnlock = true;
				this.objLock.SetActive(true);
			}
			else
			{
				this.objFadeLock.SetActive(true);
			}
		}

		private void CheckStateOwned()
		{
			this.isOwned = CacheGame.IsOwnedPlane(this.planeID);
			if (this.isOwned)
			{
				this.objLock.SetActive(false);
				this.textLevelPlane.gameObject.SetActive(true);
				this.textLevelPlane.text = string.Empty + this.inforPlane.planeData.Level;
			}
		}

		public void CheckEquipPlane(Transform transformObjEquiped)
		{
			if (CacheGame.GetPlaneEquiped() == (int)this.planeID)
			{
				transformObjEquiped.parent = this.imgRank.transform;
				transformObjEquiped.localPosition = Vector3.zero;
				if (this.objEvolve.activeInHierarchy)
				{
					this.objEvolve.GetComponent<RectTransform>().SetAsFirstSibling();
				}
			}
		}

		public void CheckEnableEvolve()
		{
			if (this.isOwned)
			{
				if (this.Rank != GameContext.Rank.SSS.ToString() && this.inforPlane.planeData.Level < 200)
				{
					int cardEvolvePlane = this.inforPlane.planeData.CardEvolvePlane;
					if (GameContext.totalUltraStarshipCard + cardEvolvePlane >= this.TotalCard)
					{
						this.enableEvolve = true;
						this.objEvolve.SetActive(true);
						if (SceneContext.sceneName == "Home")
						{
							WarningManager.Current.ShowWarningEvolvePlane();
						}
					}
					else
					{
						this.enableEvolve = false;
						this.objEvolve.SetActive(false);
					}
				}
				else
				{
					this.enableEvolve = false;
				}
			}
		}

		public void SetValueSlider()
		{
			float value = this.sliderDamage.value;
			float value2 = this.sliderFireRate.value;
			float value3 = this.sliderSuperDamage.value;
			int num = DataGame.Current.ConverRankPlaneToIndex(this.inforPlane.planeData.BaseRank);
			int num2 = DataGame.Current.ConverRankPlaneToIndex(this.inforPlane.planeData.CurrentRank);
			this.sliderDamage.value = value + (float)(num2 - num);
			this.sliderFireRate.value = value2 + (float)(num2 - num);
			this.sliderSuperDamage.value = value3 + (float)(num2 - num);
		}

		public void SetValueSlider2()
		{
			this.sliderDamage.value += 1f;
			this.sliderFireRate.value += 1f;
			this.sliderSuperDamage.value += 1f;
		}

		public void SetTextLevelPlane(int level)
		{
			this.textLevelPlane.text = string.Empty + level;
		}

		public void SetImageRank()
		{
			DataGame.Current.SetImageRank2(this.imgRank, this.Rank);
		}

		public void EvolutionComplete()
		{
			this.inforPlane.SetAnimPlane();
			this.inforPlane.SetTextView();
			this.inforPlane.ShowPanelUpgradePlane();
			this.SetImageRank();
			this.SetValueSlider2();
			UnityEngine.Debug.Log("Set value slider");
		}

		public void CheckPlane()
		{
			this.CheckStateLockPlane();
			this.CheckStateOwned();
			this.CheckEnableEvolve();
			this.SetImageRank();
			this.SetValueSlider();
		}

		public GameContext.Plane planeID;

		public ViewInforPlane inforPlane;

		public int indexHSS;

		public GameObject objFadeLock;

		public GameObject objLock;

		public GameObject objEvolve;

		public Text textLevelPlane;

		public Image imgRank;

		public Slider sliderDamage;

		public Slider sliderFireRate;

		public Slider sliderSuperDamage;

		[HideInInspector]
		public bool isOwned;

		[HideInInspector]
		public bool isUnlock;

		[HideInInspector]
		public bool enableEvolve;

		[HideInInspector]
		public int currentIDRank;
	}
}

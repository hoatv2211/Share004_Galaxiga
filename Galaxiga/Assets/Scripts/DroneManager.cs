using System;
using DG.Tweening;
using Hellmade.Sound;
using I2.Loc;
using SnapScrollView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DroneManager : MonoBehaviour
{
	private void Awake()
	{
		DroneManager.current = this;
		this.actionCardChange = delegate(Component sender, object param)
		{
			this.CheckEnableEvolveAllDrone();
		};
		EventDispatcher.Instance.RegisterListener(EventID.UltraDroneCard, this.actionCardChange);
		this.currentIDDroneEquippedLeft = CacheGame.GetDroneLeftEquiped();
		this.currentIDDroneEquippedRight = CacheGame.GetDroneRightEquiped();
		this.CheckAllDrone();
		if (SceneContext.sceneName == "Home")
		{
			this.SetAnimatorDroneEquipedHome();
		}
	}

	private void Start()
	{
		this.btnNextPage.onClick.AddListener(new UnityAction(this.NextPage));
		this.btnPrevPage.onClick.AddListener(new UnityAction(this.PrevPage));
	}

	public void CheckAllDrone()
	{
		for (int i = 0; i < this.arrDrone.Length; i++)
		{
			this.arrDrone[i].CheckDrone();
			this.arrDrone[i].SetImageRank();
		}
	}

	public void CheckDrone(GameContext.Drone drone)
	{
		for (int i = 0; i < this.arrDrone.Length; i++)
		{
			if (this.arrDrone[i].droneID == drone)
			{
				this.arrDrone[i].CheckDrone();
				this.arrDrone[i].SetImageRank();
				break;
			}
		}
	}

	public void CheckDroneStarterPack()
	{
		this.arrDrone[0].CheckDrone();
		this.arrDrone[0].SetImageRank();
		this.arrDrone[2].CheckDrone();
		this.arrDrone[2].SetImageRank();
	}

	[EnumAction(typeof(GameContext.Drone))]
	public void SelectDrone(int droneID)
	{
		if (this.saveDrone != droneID)
		{
			this.saveDrone = droneID;
			this.currentDrone = (GameContext.Drone)droneID;
			this.currentIndexDrone = droneID - 1;
			this.arrDrone[this.currentIndexDrone].SelectDrone(this.objSelectDrone);
			this.scrollSnapDrone.MoveToIndex(this.arrDrone[this.currentIndexDrone].indexHSS);
			this.SetTextInfoDrone();
			this.ChangeActionButton();
			this.evolveDroneManager.CheckEvolutionDrone(this.arrDrone[this.currentIndexDrone].isOwned, this.arrDrone[this.currentIndexDrone].droneID);
		}
	}

	private void SetTextInfoDrone()
	{
		if (this.arrDrone[this.currentIndexDrone].isOwned)
		{
			int num = GameContext.totalUltraDroneCard + this.arrDrone[this.currentIndexDrone].droneData.CardEvolveDrone;
			this.textNumberCardDroneCraft.text = num + "/" + this.arrDrone[this.currentIndexDrone].droneData.NumberCardToEvolve;
			this.sliderNumberCardDrone.maxValue = (float)this.arrDrone[this.currentIndexDrone].droneData.NumberCardToEvolve;
			this.sliderNumberCardDrone.value = (float)num;
		}
		else
		{
			this.textNumberCardDroneCraft.text = this.arrDrone[this.currentIndexDrone].droneData.CardEvolveDrone + "/" + this.arrDrone[this.currentIndexDrone].droneData.NumberCardCraft;
			this.sliderNumberCardDrone.maxValue = (float)this.arrDrone[this.currentIndexDrone].droneData.NumberCardCraft;
			this.sliderNumberCardDrone.value = (float)this.arrDrone[this.currentIndexDrone].droneData.CardEvolveDrone;
		}
	}

	private void ChangeActionButton()
	{
		this.btnSelectCraftDrone.onClick.RemoveAllListeners();
		if (this.arrDrone[this.currentIndexDrone].isOwned)
		{
			this.btnSelectCraftDrone.interactable = true;
			if (this.currentIDDroneEquippedLeft == (int)this.currentDrone || this.currentIDDroneEquippedRight == (int)this.currentDrone)
			{
				this.textNameBtnSelectCraftDrone.text = ScriptLocalization.unequip;
				this.btnSelectCraftDrone.onClick.AddListener(new UnityAction(this.UnequipDrone));
			}
			else
			{
				this.textNameBtnSelectCraftDrone.text = ScriptLocalization.switch_drone;
				this.btnSelectCraftDrone.onClick.AddListener(new UnityAction(this.EquipDrone));
			}
		}
		else
		{
			this.textNameBtnSelectCraftDrone.text = ScriptLocalization.craft;
			if (this.arrDrone[this.currentIndexDrone].enableCraft)
			{
				this.btnSelectCraftDrone.interactable = true;
				this.btnSelectCraftDrone.onClick.AddListener(new UnityAction(this.StartCraftDrone));
			}
			else
			{
				this.btnSelectCraftDrone.interactable = false;
			}
		}
	}

	private void UnequipDrone()
	{
		if (this.currentIDDroneEquippedLeft == (int)this.currentDrone)
		{
			this.currentIDDroneEquippedLeft = 0;
			this.DisableAnimatorDroneHome(true);
			this.arrDrone[this.currentIndexDrone].UnequipDrone(true, this.objSwitchedLeft);
		}
		if (this.currentIDDroneEquippedRight == (int)this.currentDrone)
		{
			this.currentIDDroneEquippedRight = 0;
			this.DisableAnimatorDroneHome(false);
			this.arrDrone[this.currentIndexDrone].UnequipDrone(false, this.objSwitchedRight);
		}
		this.textNameBtnSelectCraftDrone.text = ScriptLocalization.switch_drone;
		this.btnSelectCraftDrone.onClick.AddListener(new UnityAction(this.EquipDrone));
	}

	private void EquipDrone()
	{
		bool animatorDroneHome;
		if (this.currentIDDroneEquippedLeft != 0)
		{
			if (this.currentIDDroneEquippedRight != 0)
			{
				this.arrDrone[this.currentIndexDrone].EquipDrone(true, this.objSwitchedLeft);
				this.currentIDDroneEquippedLeft = (int)this.currentDrone;
				animatorDroneHome = true;
			}
			else
			{
				this.arrDrone[this.currentIndexDrone].EquipDrone(false, this.objSwitchedRight);
				this.currentIDDroneEquippedRight = (int)this.currentDrone;
				animatorDroneHome = false;
			}
		}
		else
		{
			this.arrDrone[this.currentIndexDrone].EquipDrone(true, this.objSwitchedLeft);
			this.currentIDDroneEquippedLeft = (int)this.currentDrone;
			animatorDroneHome = true;
		}
		if (SceneContext.sceneName == "Home")
		{
			this.SetAnimatorDroneHome(animatorDroneHome);
		}
		this.textNameBtnSelectCraftDrone.text = ScriptLocalization.unequip;
		this.btnSelectCraftDrone.onClick.RemoveAllListeners();
		this.btnSelectCraftDrone.onClick.AddListener(new UnityAction(this.UnequipDrone));
		EazySoundManager.PlayUISound(AudioCache.Sound.switch_airship);
	}

	public void StartCraftDrone()
	{
		this.fxHoldDrone.SetActive(true);
		this.arrDrone[this.currentIndexDrone].StartFxCraft();
		EazySoundManager.PlayUISound(AudioCache.UISound.evolve_1);
	}

	public void CraftDrone()
	{
		UnityEngine.Debug.Log("CraftDrone");
		this.arrDrone[this.currentIndexDrone].CraftDrone(new Action(this.CraftSuccess));
	}

	private void CraftSuccess()
	{
		UnityEngine.Debug.Log("Craft Success");
		this.fxHoldDrone.SetActive(false);
		this.arrDrone[this.currentIndexDrone].CheckEnableEvolve();
		this.CheckShowWarningEvolve();
		this.arrDrone[this.currentIndexDrone].StopFxCraft();
		this.arrDrone[this.currentIndexDrone].CheckOwned();
		this.craftDroneSuccess.ShowPopupCraftDroneSuccess(this.arrDrone[this.currentIndexDrone].droneID);
		EazySoundManager.PlayUISound(AudioCache.UISound.evolve_3);
		this.textNameBtnSelectCraftDrone.text = ScriptLocalization.switch_drone;
		this.btnSelectCraftDrone.onClick.RemoveAllListeners();
		this.btnSelectCraftDrone.onClick.AddListener(new UnityAction(this.EquipDrone));
		this.evolveDroneManager.CheckEvolutionDrone(true, this.arrDrone[this.currentIndexDrone].droneID);
		this.SetTextInfoDrone();
	}

	public void EvolutionDroneComplete()
	{
		this.CheckEnableEvolveAllDrone();
		this.arrDrone[this.currentIndexDrone].infoDrone.EvolutionDroneComplete();
		this.arrDrone[this.currentIndexDrone].SetImageRank();
		this.arrDrone[this.currentIndexDrone].SetValueSlider2();
	}

	public void CheckEnableEvolveAllDrone()
	{
		int num = this.arrDrone.Length;
		for (int i = 0; i < num; i++)
		{
			this.arrDrone[i].CheckEnableEvolve();
		}
		this.CheckShowWarningEvolve();
		this.SetTextInfoDrone();
	}

	public void CheckEnableEvolveDrone()
	{
		this.arrDrone[this.currentIndexDrone].CheckEnableCraft();
		this.SetTextInfoDrone();
		this.ChangeActionButton();
	}

	private void CheckShowWarningEvolve()
	{
		if (SceneContext.sceneName != "Home")
		{
			return;
		}
		int num = this.arrDrone.Length;
		for (int i = 0; i < num; i++)
		{
			if (this.arrDrone[i].enableEvovle)
			{
				WarningManager.Current.ShowWarningEvolveDrone();
				break;
			}
			WarningManager.Current.HideWarningEvolveDrone();
		}
	}

	public void SelectTabDrone()
	{
		bool flag = false;
		if (!this.isAddListener)
		{
			this.isAddListener = true;
			this.maxPage = this.scrollSnapDrone.CalculateMaxIndex();
			this.scrollSnapDrone.onLerpComplete.AddListener(new UnityAction(this.OnLerpComplete));
		}
		if (this.currentIDDroneEquippedLeft == 0)
		{
			if (this.currentIDDroneEquippedRight != 0)
			{
				this.SelectDrone(this.currentIDDroneEquippedRight);
			}
			else
			{
				for (int i = 0; i < this.arrDrone.Length; i++)
				{
					if (this.arrDrone[i].isOwned)
					{
						this.SelectDrone((int)this.arrDrone[i].droneID);
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					this.SelectDrone(1);
				}
			}
		}
		else
		{
			this.SelectDrone(this.currentIDDroneEquippedLeft);
		}
		this.ChangeActionButton();
	}

	private void SetAnimatorDroneHome(bool isLeft)
	{
		if (isLeft)
		{
			this.animatorDroneHomeLeft.enabled = true;
			this.objDroneLeft.SetActive(true);
			this.animatorDroneHomeLeft.runtimeAnimatorController = this.arrDrone[this.currentIndexDrone].anim;
		}
		else
		{
			this.animatorDroneHomeRight.enabled = true;
			this.objDroneRight.SetActive(true);
			this.animatorDroneHomeRight.runtimeAnimatorController = this.arrDrone[this.currentIndexDrone].anim;
		}
	}

	private void DisableAnimatorDroneHome(bool isLeft)
	{
		if (isLeft)
		{
			this.animatorDroneHomeLeft.enabled = false;
			this.imgDroneLeft.sprite = this.sprDroneUneqip;
			this.objDroneLeft.SetActive(false);
		}
		else
		{
			this.animatorDroneHomeRight.enabled = false;
			this.imgDroneRight.sprite = this.sprDroneUneqip;
			this.objDroneRight.SetActive(false);
		}
	}

	private void SetAnimatorDroneEquipedHome()
	{
		if (this.currentIDDroneEquippedLeft != 0)
		{
			this.animatorDroneHomeLeft.enabled = true;
			this.animatorDroneHomeLeft.runtimeAnimatorController = this.arrDrone[this.currentIDDroneEquippedLeft - 1].anim;
			this.objDroneLeft.SetActive(true);
			this.arrDrone[this.currentIDDroneEquippedLeft - 1].SetEquipped(this.objSwitchedLeft);
		}
		if (this.currentIDDroneEquippedRight != 0)
		{
			this.animatorDroneHomeRight.enabled = true;
			this.animatorDroneHomeRight.runtimeAnimatorController = this.arrDrone[this.currentIDDroneEquippedRight - 1].anim;
			this.objDroneRight.SetActive(true);
			this.arrDrone[this.currentIDDroneEquippedRight - 1].SetEquipped(this.objSwitchedRight);
		}
	}

	public void ComingSoon()
	{
		Notification.Current.ShowNotification(ScriptLocalization.notify_coming_soon);
	}

	public void NextPage()
	{
		this.currentIndexPage = this.scrollSnapDrone.cellIndex;
		if (this.currentIndexPage < this.maxPage)
		{
			this.currentIndexPage++;
			this.SelectDrone(this.currentIndexPage + 1);
		}
	}

	public void PrevPage()
	{
		this.currentIndexPage = this.scrollSnapDrone.cellIndex;
		if (this.currentIndexPage > 0)
		{
			this.currentIndexPage--;
			this.SelectDrone(this.currentIndexPage + 1);
		}
	}

	private void OnLerpComplete()
	{
		this.SelectDrone(this.scrollSnapDrone.cellIndex + 1);
		UnityEngine.Debug.Log("Lerp complete " + this.scrollSnapDrone.cellIndex);
	}

	public void PlayFxUpgrade()
	{
		if (!this.fxUprade.gameObject.activeInHierarchy)
		{
			this.fxUprade.gameObject.SetActive(true);
		}
		this.fxUprade.Play();
	}

	public static DroneManager current;

	public ScrollSnap scrollSnapDrone;

	public Text textNumberCardDroneCraft;

	public Slider sliderNumberCardDrone;

	public GameObject objDroneLeft;

	public GameObject objDroneRight;

	public Animator animatorDroneHomeLeft;

	public Animator animatorDroneHomeRight;

	public Button btnSelectCraftDrone;

	public Text textNameBtnSelectCraftDrone;

	public Image imgDroneLeft;

	public Image imgDroneRight;

	public Sprite sprDroneUneqip;

	public EvolveDroneManager evolveDroneManager;

	public CraftDroneSuccess craftDroneSuccess;

	public GameObject fxHoldDrone;

	public GameObject fxCraftDrone;

	public ParticleSystem fxUprade;

	public Transform objSelectDrone;

	public Transform objSwitchedLeft;

	public Transform objSwitchedRight;

	public Button btnNextPage;

	public Button btnPrevPage;

	public DroneManager.Drone[] arrDrone;

	private GameContext.Drone currentDrone;

	private int currentIDDroneEquippedLeft;

	private int currentIDDroneEquippedRight;

	private int currentIndexDrone;

	private Action<Component, object> actionCardChange;

	private int saveDrone = -1;

	private bool isAddListener;

	private int currentIndexPage;

	private int maxPage;

	[Serializable]
	public class Drone
	{
		public string Rank
		{
			get
			{
				return this.droneData.CurrentRank;
			}
		}

		public void SetValueSlider()
		{
			float value = this.sliderDamage.value;
			float value2 = this.sliderFireRate.value;
			float value3 = this.sliderSuperDamage.value;
			int num = DataGame.Current.ConverRankPlaneToIndex(this.droneData.BaseRank);
			int num2 = DataGame.Current.ConverRankPlaneToIndex(this.droneData.CurrentRank);
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

		public void SelectDrone(Transform transformObjSelectDrone)
		{
			transformObjSelectDrone.parent = this.imgRank.transform;
			transformObjSelectDrone.localPosition = Vector3.zero;
		}

		public void CheckOwned()
		{
			this.isOwned = CacheGame.IsOwnedDrone(this.droneID);
			if (this.isOwned)
			{
				this.objLock.SetActive(false);
				this.objLevel.SetActive(true);
			}
			else
			{
				this.objLock.SetActive(true);
				this.objLevel.SetActive(false);
			}
		}

		public void CheckEnableCraft()
		{
			if (this.isOwned)
			{
				this.CheckEnableEvolve();
				this.objFade.SetActive(false);
			}
			else
			{
				int cardEvolveDrone = this.droneData.CardEvolveDrone;
				if (cardEvolveDrone >= this.droneData.NumberCardCraft)
				{
					this.enableCraft = true;
					this.enableEvovle = true;
					this.objFade.SetActive(false);
					this.iconEvolve.SetActive(true);
					if (SceneContext.sceneName == "Home")
					{
						WarningManager.Current.ShowWarningEvolveDrone();
					}
				}
				else
				{
					this.enableEvovle = false;
					this.iconEvolve.SetActive(false);
					if (cardEvolveDrone > 0)
					{
						this.objFade.SetActive(false);
						this.objLock.SetActive(true);
					}
					else
					{
						this.objFade.SetActive(true);
					}
				}
			}
		}

		public void CheckEnableEvolve()
		{
			if (!this.isOwned)
			{
				return;
			}
			if (this.droneData.CurrentRank != GameContext.Rank.SSS.ToString())
			{
				int num = this.droneData.CardEvolveDrone + GameContext.totalUltraDroneCard;
				if (num >= this.droneData.NumberCardToEvolve)
				{
					this.iconEvolve.SetActive(true);
					this.enableEvovle = true;
					if (SceneContext.sceneName == "Home")
					{
						WarningManager.Current.ShowWarningEvolveDrone();
					}
				}
				else
				{
					this.iconEvolve.SetActive(false);
					this.enableEvovle = false;
				}
			}
		}

		public void CraftDrone(Action CraftComplete)
		{
			UnityEngine.Debug.Log("Start craft drone " + this.droneID.ToString());
			int cardEvolveDrone = this.droneData.CardEvolveDrone;
			int numberCardCraft = this.droneData.NumberCardCraft;
			int cardEvolveDrone2 = cardEvolveDrone - numberCardCraft;
			this.droneData.CardEvolveDrone = cardEvolveDrone2;
			CacheGame.SetOwnedDrone(this.droneID);
			this.infoDrone.ShowPanelUpgrade();
			this.isOwned = true;
			CraftComplete();
		}

		public void StartFxCraft()
		{
			switch (this.droneID)
			{
			case GameContext.Drone.GatlingGun:
				DOTween.Play("GATLING_GUN_FX");
				break;
			case GameContext.Drone.AutoGatlingGun:
				DOTween.Play("AUTO_GATLING_GUN_FX");
				break;
			case GameContext.Drone.Laser:
				DOTween.Play("LASER_FX");
				break;
			case GameContext.Drone.Nighturge:
				DOTween.Play("NIGHTURGE_FX");
				break;
			case GameContext.Drone.GodOfThunder:
				DOTween.Play("GOD_OF_THUNDER_FX");
				break;
			case GameContext.Drone.Terigon:
				DOTween.Play("TERIGON_FX");
				break;
			}
		}

		public void StopFxCraft()
		{
			switch (this.droneID)
			{
			case GameContext.Drone.GatlingGun:
				DOTween.Pause("GATLING_GUN_FX");
				break;
			case GameContext.Drone.AutoGatlingGun:
				DOTween.Pause("AUTO_GATLING_GUN_FX");
				break;
			case GameContext.Drone.Laser:
				DOTween.Pause("LASER_FX");
				break;
			case GameContext.Drone.Nighturge:
				DOTween.Play("NIGHTURGE_FX");
				break;
			case GameContext.Drone.GodOfThunder:
				DOTween.Play("GOD_OF_THUNDER_FX");
				break;
			case GameContext.Drone.Terigon:
				DOTween.Play("TERIGON_FX");
				break;
			}
		}

		public void SetImageRank()
		{
			DataGame.Current.SetImageRank2(this.imgRank, this.Rank);
		}

		public void CheckDrone()
		{
			this.CheckOwned();
			this.CheckEnableCraft();
			this.SetValueSlider();
		}

		public void SetEquipped(Transform transformObjEquipped)
		{
			transformObjEquipped.parent = this.imgRank.transform;
			transformObjEquipped.localPosition = Vector3.zero;
			transformObjEquipped.gameObject.SetActive(true);
			this.iconEvolve.GetComponent<RectTransform>().SetAsLastSibling();
		}

		public void EquipDrone(bool isDroneLeft, Transform transformObjEquipped)
		{
			if (isDroneLeft)
			{
				CacheGame.SetDroneLeftEquip(this.droneID);
			}
			else
			{
				CacheGame.SetDroneRightEquip(this.droneID);
			}
			if (!transformObjEquipped.gameObject.activeInHierarchy)
			{
				transformObjEquipped.gameObject.SetActive(true);
			}
			transformObjEquipped.parent = this.imgRank.transform;
			transformObjEquipped.localPosition = Vector3.zero;
			if (this.iconEvolve.activeInHierarchy)
			{
				this.iconEvolve.GetComponent<RectTransform>().SetAsLastSibling();
			}
			if (NewTutorial.current.currentStepTutorial_EquipDrone == 5)
			{
				NewTutorial.current.EquipDrone_Step5();
			}
		}

		public void UnequipDrone(bool isDroneLeft, Transform transformObjEquipped)
		{
			if (isDroneLeft)
			{
				CacheGame.SetDroneLeftEquip(GameContext.Drone.NoDrone);
			}
			else
			{
				CacheGame.SetDroneRightEquip(GameContext.Drone.NoDrone);
			}
			transformObjEquipped.gameObject.SetActive(false);
		}

		public GameContext.Drone droneID;

		public DroneData droneData;

		public ViewInfoDrone infoDrone;

		public int indexHSS;

		public GameObject objLock;

		public GameObject objFade;

		public GameObject iconEvolve;

		public GameObject objLevel;

		public RuntimeAnimatorController anim;

		public Image imgRank;

		[HideInInspector]
		public bool isOwned;

		[HideInInspector]
		public bool enableCraft;

		[HideInInspector]
		public bool enableEvovle;

		public Slider sliderDamage;

		public Slider sliderFireRate;

		public Slider sliderSuperDamage;
	}
}

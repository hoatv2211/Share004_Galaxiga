using System;
using System.Collections;
using DG.Tweening;
using Hellmade.Sound;
using I2.Loc;
using SkyGameKit;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

public class BackupPlaneManager : MonoBehaviour
{
	private void Awake()
	{
		BackupPlaneManager.current = this;
		int num = this.arrPlanes.Length;
		for (int i = 0; i < num; i++)
		{
			this.arrPlanes[i].CheckPlane();
			if (this.arrPlanes[i].canBackup)
			{
				this.backupAvailable = true;
			}
		}
	}

	private void Start()
	{
		this.numberRevive = 3;
		this.CheckFreeRevive();
		if (!GameContext.isTryPlane)
		{
			if (GameContext.currentModeGamePlay == GameContext.ModeGamePlay.Campaign)
			{
				this.energyBackup = DataEnergyLevelSheet.Get(GameContext.currentLevel).backupEnergy;
			}
			else if (GameContext.currentModeGamePlay == GameContext.ModeGamePlay.Endless)
			{
				this.energyBackup = 5;
			}
		}
		this.textNumberEnergy.text = string.Empty + this.energyBackup;
	}

	private void CheckFreeRevive()
	{
		if (GameContext.numberFreeRespawnPlayer > 0)
		{
			this.btnFreeRevive.SetActive(true);
		}
		else
		{
			this.btnWatchVideoToRevive.SetActive(true);
		}
	}

	private void CheckPriceRivivePlayer()
	{
		switch (GameContext.currentModeGamePlay)
		{
		case GameContext.ModeGamePlay.Endless:
			if (this.numberRevive == 3)
			{
				this.priceRevive = 10;
			}
			else if (this.numberRevive == 2)
			{
				this.priceRevive = 20;
			}
			else if (this.numberRevive == 1)
			{
				this.priceRevive = 30;
			}
			break;
		case GameContext.ModeGamePlay.Campaign:
		{
			int level = GemReviveDataSheet.Get(1).level;
			int level2 = GemReviveDataSheet.Get(2).level;
			int level3 = GemReviveDataSheet.Get(3).level;
			int level4 = GemReviveDataSheet.Get(4).level;
			if (GameContext.currentLevel <= level)
			{
				if (this.numberRevive == 3)
				{
					this.priceRevive = GemReviveDataSheet.Get(1).price1;
				}
				else if (this.numberRevive == 2)
				{
					this.priceRevive = GemReviveDataSheet.Get(1).price2;
				}
				else if (this.numberRevive == 1)
				{
					this.priceRevive = GemReviveDataSheet.Get(1).price3;
				}
			}
			else if (GameContext.currentLevel > level && GameContext.currentLevel <= level2)
			{
				if (this.numberRevive == 3)
				{
					this.priceRevive = GemReviveDataSheet.Get(2).price1;
				}
				else if (this.numberRevive == 2)
				{
					this.priceRevive = GemReviveDataSheet.Get(2).price2;
				}
				else if (this.numberRevive == 1)
				{
					this.priceRevive = GemReviveDataSheet.Get(2).price3;
				}
			}
			else if (GameContext.currentLevel > level2 && GameContext.currentLevel <= level3)
			{
				if (this.numberRevive == 3)
				{
					this.priceRevive = GemReviveDataSheet.Get(3).price1;
				}
				else if (this.numberRevive == 2)
				{
					this.priceRevive = GemReviveDataSheet.Get(3).price2;
				}
				else if (this.numberRevive == 1)
				{
					this.priceRevive = GemReviveDataSheet.Get(3).price3;
				}
			}
			else if (GameContext.currentLevel > level3 && GameContext.currentLevel <= level4)
			{
				if (this.numberRevive == 3)
				{
					this.priceRevive = GemReviveDataSheet.Get(4).price1;
				}
				else if (this.numberRevive == 2)
				{
					this.priceRevive = GemReviveDataSheet.Get(4).price2;
				}
				else if (this.numberRevive == 1)
				{
					this.priceRevive = GemReviveDataSheet.Get(4).price3;
				}
			}
			else if (GameContext.currentLevel > level4)
			{
				if (this.numberRevive == 3)
				{
					this.priceRevive = GemReviveDataSheet.Get(5).price1;
				}
				else if (this.numberRevive == 2)
				{
					this.priceRevive = GemReviveDataSheet.Get(5).price2;
				}
				else if (this.numberRevive == 1)
				{
					this.priceRevive = GemReviveDataSheet.Get(5).price3;
				}
			}
			break;
		}
		case GameContext.ModeGamePlay.Boss:
			if (this.numberRevive == 3)
			{
				this.priceRevive = 5;
			}
			else if (this.numberRevive == 2)
			{
				this.priceRevive = 8;
			}
			else if (this.numberRevive == 1)
			{
				this.priceRevive = 12;
			}
			break;
		}
		if (GameContext.isPurchasePremiumPack)
		{
			this.priceRevive = (int)Mathf.Round((float)this.priceRevive * 0.7f);
		}
		UnityEngine.Debug.Log("Price Revive " + this.priceRevive);
	}

	[EnumAction(typeof(GameContext.Plane))]
	public void SelectPlane(int planeID)
	{
		if (this.saveIDPlane != planeID)
		{
			this.saveIDPlane = planeID;
			this.indexCurrentPlane = planeID;
			this.arrPlanes[this.indexCurrentPlane].SelectPlane(this.objSelect);
			if (this.arrPlanes[this.indexCurrentPlane].canBackup)
			{
				this.imgBtnBackUp.sprite = this.sprtBtnBackup;
			}
			else
			{
				this.imgBtnBackUp.sprite = this.sprtBtnBackupDisable;
			}
		}
	}

	public void GameOver()
	{
		if (Time.timeScale < 1f)
		{
			Time.timeScale = 1f;
		}
		UIGameManager.current.ShowFxDefeat();
		GameContext.isMissionFail = true;
		if (!GameContext.isPassCurrentLevel)
		{
			SpaceForceFirebaseLogger.LevelFail(GameContext.currentModeLevel, GameContext.currentLevel);
		}
		base.StartCoroutine(this.DelayExitGameScreen());
	}

	public void ShowPanelRevive()
	{
		if (!this.isShowingPanelRevive && !GameContext.isMissionFail)
		{
			this.isShowingPanelRevive = true;
			if (!this.backupAvailable)
			{
				this.textBackupAvailable.SetActive(true);
			}
			if (this.numberRevive > 0)
			{
				this.CheckPriceRivivePlayer();
				this.textNumberRevives.text = this.numberRevive + "/" + 3;
				this.textPriceRevive.text = string.Empty + this.priceRevive;
				this.panelRevive.SetActive(true);
				this.numberRevive--;
				this.StartCountDownRevive();
				base.StartCoroutine(GameContext.Delay(0.2f, delegate
				{
					if (SgkSingleton<LevelManager>.Instance != null)
					{
						this.sliderProcessing.maxValue = (float)SgkSingleton<LevelManager>.Instance.listSequenceWave.Count;
						this.CountTo(SgkSingleton<LevelManager>.Instance.CurrentSequenceWaveIndex);
					}
				}));
				DOTween.Restart("GAME_OVER", true, -1f);
			}
			else
			{
				this.GameOver();
			}
		}
	}

	private void HidePanelRevive(Action actionT)
	{
		this.isShowingPanelRevive = false;
		DOTween.PlayBackwards("GAME_OVER");
		base.StartCoroutine(this.DelayHide(this.panelRevive, actionT));
	}

	public void FreeReivePlane()
	{
		if (this.isCountdownRevive)
		{
			if (GameContext.numberFreeRespawnPlayer > 0)
			{
				this.StopCountDownRevive();
				GameContext.numberFreeRespawnPlayer--;
				this.HidePanelRevive(new Action(this.RevivePlaneNow));
				this.btnFreeRevive.SetActive(false);
			}
			else
			{
				Notification.Current.ShowNotification(ScriptLocalization.notify_free_respawn_once_per_day);
			}
		}
	}

	public void WatchVideoToRevive()
	{
		if (this.isCountdownRevive)
		{
			
				Notification.Current.ShowNotification("The video not available");
			
		}
	}

	public void WatchVideoToReviveComplete()
	{
		this.isVideoToReviveComplete = true;
		if (this.panelBackup.activeInHierarchy)
		{
			this.panelBackup.SetActive(false);
		}
		this.HidePanelRevive(new Action(this.RevivePlaneNow));
		this.btnWatchVideoToRevive.SetActive(false);
	}

	public void RevivePlane()
	{
		if (this.isCountdownRevive)
		{
			if (ShopContext.currentGem >= this.priceRevive)
			{
				this.StopCountDownRevive();
				ShopContext.currentGem -= this.priceRevive;
				CacheGame.SetGems(ShopContext.currentGem);
				this.HidePanelRevive(new Action(this.RevivePlaneNow));
				CurrencyLog.LogGemOut(this.priceRevive, CurrencyLog.Out.Revive, "Revive_" + GameContext.currentLevel);
			}
			else
			{
				Notification.Current.ShowNotification(ScriptLocalization.notify_you_need_buy_more);
			}
		}
	}

	private void RevivePlaneNow()
	{
		if (!this.isRevivedPlane)
		{
			this.isRevivedPlane = true;
			Time.timeScale = 1f;
			PlaneIngameManager.current.RevivePlane();
			UIGameManager.current.ShowUIPlayGame();
		}
	}

	private IEnumerator DelayExitGameScreen()
	{
		yield return new WaitForSeconds(2.5f);
		CameraManager.curret.ExitGameScreen("SelectLevel");
		yield break;
	}

	private void ShowPanelBackup()
	{
		int num = this.arrPlanes.Length;
		for (int i = 0; i < num; i++)
		{
			if (this.arrPlanes[i].canBackup)
			{
				this.SelectPlane((int)this.arrPlanes[i].planeID);
				this.canBackUp = true;
				break;
			}
		}
		UIGameManager.current.HideUI();
		if (!this.canBackUp)
		{
			this.imgBtnBackUp.sprite = this.sprtBtnBackupDisable;
		}
		else
		{
			this.imgBtnBackUp.sprite = this.sprtBtnBackup;
		}
		this.timerDayleft = (float)CheckNewDay.Current.CurrentSecondDayleft();
		base.StartCoroutine("CoroutineTimeDayleft");
		this.backupAvailable = false;
		EscapeManager.Current.AddAction(new Action(this.Leave));
		this.panelBackup.SetActive(true);
		for (int j = 0; j < num; j++)
		{
			this.arrPlanes[j].SetAnimPlane();
		}
		DOTween.Restart("BACKUP_PANE", true, -1f);
	}

	private void HidePanelBackup(Action actionT)
	{
		base.StopCoroutine("CoroutineTimeDayleft");
		DOTween.PlayBackwards("BACKUP_PANE");
		base.StartCoroutine(this.DelayHide(this.panelBackup, actionT));
	}

	public void BackUpPlane()
	{
		if (this.arrPlanes[this.indexCurrentPlane].canBackup)
		{
			if (GameContext.totalEnergy >= this.energyBackup)
			{
				CacheGame.MinusEnergy(this.energyBackup);
				this.arrPlanes[this.indexCurrentPlane].BackupPlane();
				this.HidePanelBackup(new Action(this.BackupPlaneNow));
				if (ShopManager.Instance != null)
				{
					ShopManager.Instance.UpdateTextEnergy();
				}
			}
			else
			{
				Notification.Current.ShowNotification(ScriptLocalization.notify_you_need_buy_more_energy);
				ShopEnergy.current.ShowPanelShopEnergy();
			}
		}
		else if (this.saveIDPlane == -1)
		{
			Notification.Current.ShowNotification(ScriptLocalization.notify_select_starship_to_backup);
		}
		else
		{
			Notification.Current.ShowNotification(ScriptLocalization.notify_cannot_use_this_starship_to_backup);
		}
	}

	private void BackupPlaneNow()
	{
		PlaneIngameManager.current.BackupPlane(this.arrPlanes[this.indexCurrentPlane].planeID);
		UnityEngine.Debug.Log("Spawn plane backup");
		Time.timeScale = 1f;
		UIGameManager.current.ShowUIPlayGame();
	}

	public void Leave()
	{
		this.HidePanelBackup(new Action(this.GameOver));
		EscapeManager.Current.RemoveAction(new Action(this.Leave));
	}

	public void StartCountDownRevive()
	{
		this.isVideoToReviveComplete = false;
		if (this.countDownRevive != null)
		{
			base.StopCoroutine(this.countDownRevive);
		}
		this.countDownRevive = base.StartCoroutine(this.CountDown());
	}

	public void StopCountDownRevive()
	{
		this.timeCountDown = 5;
		this.isVideoToReviveComplete = true;
		base.StopCoroutine(this.countDownRevive);
	}

	private IEnumerator CountDown()
	{
		this.isCountdownRevive = true;
		GameScreenManager.current.PauseMusic();
		this.objCountDown.SetActive(true);
		this.timeCountDown = 5;
		this.textCountDown.text = string.Empty + this.timeCountDown;
		while (this.timeCountDown > 0)
		{
			yield return new WaitForSecondsRealtime(1f);
			this.timeCountDown--;
			this.textCountDown.text = string.Empty + this.timeCountDown;
			EazySoundManager.PlayUISound(AudioCache.UISound.tik_tok);
		}
		this.isCountdownRevive = false;
		if (this.backupAvailable)
		{
			this.HidePanelRevive(new Action(this.ShowPanelBackup));
		}
		else if (!this.isVideoToReviveComplete)
		{
			this.HidePanelRevive(new Action(this.GameOver));
		}
		yield break;
	}

	public void HidePanelReiviveGameOver()
	{
		this.HidePanelRevive(new Action(this.GameOver));
	}

	private IEnumerator DelayHide(GameObject obj)
	{
		yield return new WaitForSecondsRealtime(0.1f);
		obj.SetActive(false);
		yield break;
	}

	private IEnumerator DelayHide(GameObject obj, Action HideComplete)
	{
		yield return new WaitForSecondsRealtime(0.1f);
		obj.SetActive(false);
		HideComplete();
		yield break;
	}

	private void CountTo(int target)
	{
		int num = 0;
		int num2 = (int)Mathf.Lerp((float)num, (float)target, 0.1f);
		this.sliderProcessing.value = (float)num2;
		this.sliderProcessing.value = (float)target;
		Time.timeScale = 0f;
	}

	private IEnumerator CoroutineTimeDayleft()
	{
		while (this.timerDayleft > 0f)
		{
			string textFormat = TimeSpan.FromSeconds((double)this.timerDayleft).ToString("hh\\:mm\\:ss");
			this.textTimeDayleft.text = textFormat;
			yield return new WaitForSecondsRealtime(1f);
			this.timerDayleft -= 1f;
		}
		this.timerDayleft = 86400f;
		CheckNewDay.Current.NewDay();
		base.StartCoroutine("CoroutineTimeDayleft");
		yield break;
	}

	public static BackupPlaneManager current;

	public GameObject panelRevive;

	public GameObject panelBackup;

	public GameObject objCountDown;

	public Image imgBtnBackUp;

	public Text textNumberEnergy;

	public Text textCountDown;

	public Text textPriceRevive;

	public GameObject textBackupAvailable;

	public Text textNumberRevives;

	public Text textTimeDayleft;

	public Slider sliderProcessing;

	public Sprite sprtBtnBackup;

	public Sprite sprtBtnBackupDisable;

	public GameObject btnFreeRevive;

	public GameObject btnWatchVideoToRevive;

	public Transform objSelect;

	public BackupPlaneManager.Planes[] arrPlanes;

	private int numberRevive;

	private int priceRevive;

	private bool backupAvailable;

	private int timeCountDown;

	private int energyBackup;

	private float timerDayleft;

	private int saveIDPlane = -1;

	private int indexCurrentPlane;

	private bool isShowingPanelRevive;

	private bool isVideoToReviveComplete;

	[HideInInspector]
	public bool isRevivedPlane;

	private bool canBackUp;

	private Coroutine countDownRevive;

	private bool isCountdownRevive = true;

	[Serializable]
	public class Planes
	{
		public string Rank
		{
			get
			{
				return this.planeData.CurrentRank;
			}
		}

		public void CheckOwned()
		{
			this.isOwned = CacheGame.IsOwnedPlane(this.planeID);
			this.textLevel.text = string.Empty + this.planeData.Level;
			if (this.isOwned)
			{
				this.objLevel.SetActive(true);
			}
			else
			{
				this.objLock.SetActive(true);
			}
		}

		public void SelectPlane(Transform _objSelect)
		{
			_objSelect.parent = this.imgRank.transform;
			_objSelect.localPosition = Vector3.zero;
			_objSelect.localScale = Vector3.one;
		}

		public void CheckBackup()
		{
			if (!this.isOwned)
			{
				return;
			}
			if (GameContext.currentPlaneIDEquiped == (int)this.planeID)
			{
				this.objInactive.SetActive(true);
				this.canBackup = false;
				this.objLevel.SetActive(false);
			}
			else
			{
				this.currentNumberBackup = CacheGame.GetNumberBackUpPlane(this.planeID);
				if (this.currentNumberBackup > 0)
				{
					this.objInactive.SetActive(false);
					this.canBackup = true;
				}
				else
				{
					this.objInactive.SetActive(true);
					this.canBackup = false;
				}
				this.textNumberBackup.text = this.currentNumberBackup + "/" + 3;
			}
		}

		public void BackupPlane()
		{
			if (!this.isOwned || !this.canBackup)
			{
				return;
			}
			if (this.currentNumberBackup > 0)
			{
				this.currentNumberBackup--;
				CacheGame.SetNumberBackUpPlane(this.planeID, this.currentNumberBackup);
				this.CheckBackup();
			}
		}

		public void SetColorRank()
		{
			DataGame.Current.SetImageRank2(this.imgRank, this.Rank);
		}

		public void SetAnimPlane()
		{
			try
			{
				string skin = "E" + DataGame.Current.ConverRankPlaneToIndex(this.Rank);
				this.skeletonGraphicPlane.Skeleton.SetSkin(skin);
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.LogError(ex.Message);
			}
		}

		public void CheckPlane()
		{
			this.SetColorRank();
			this.CheckOwned();
			this.CheckBackup();
		}

		public GameContext.Plane planeID;

		public PlaneData planeData;

		public Image imgRank;

		public GameObject objLock;

		public GameObject objLevel;

		public GameObject objInactive;

		public Text textLevel;

		public Text textNumberBackup;

		public SkeletonGraphic skeletonGraphicPlane;

		[HideInInspector]
		public bool isOwned;

		[HideInInspector]
		public bool canBackup;

		private int currentNumberBackup = 3;
	}
}

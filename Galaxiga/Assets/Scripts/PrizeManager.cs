using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using I2.Loc;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PrizeManager : MonoBehaviour
{
	private int priceOneBox
	{
		get
		{
			if (GameContext.currentVIP > 2 && GameContext.currentVIP < 6)
			{
				return 19;
			}
			if (GameContext.currentVIP > 6)
			{
				return 16;
			}
			return 20;
		}
	}

	private float timeToFreeBox
	{
		get
		{
			if (GameContext.currentVIP > 1)
			{
				return 0.8f;
			}
			return 1f;
		}
	}

	private void Awake()
	{
		PrizeManager.current = this;
		this.particleSpawnReward = this.fxSpawnReward.GetComponent<ParticleSystem>();
		this.rectTransformFXSpawnReward = this.fxSpawnReward.GetComponent<RectTransform>();
		this.buttonWatchVideo = this.btnWatchVideoToFreeBox.GetComponent<Button>();
		this.CheckTimeFreeBox();
	}

	private void Start()
	{
		this.totalRewardType = 13f;
		base.StartCoroutine(GameContext.Delay(2f, delegate
		{
			this.CheckNumberMultiBox();
		}));
	}

	private void Update()
	{
		if (this.needScroll)
		{
			this.scrollBestReward.horizontalNormalizedPosition += Time.deltaTime * 0.05f;
		}
	}

	public void CheckKeySuperPrize()
	{
		int keySuperRize = CacheGame.KeySuperRize;
		this.textNumberKeySuperPrize.text = keySuperRize + "/" + this.numberKeyToOpenSuperPrize;
		int num = this.numberKeyToOpenSuperPrize - keySuperRize;
		if (num < 0)
		{
			num = 0;
		}
		this.textCollectKeySupperPrize.text = string.Format(ScriptLocalization.collect_key_to_get_super_bonus, num);
		this.btnOpenSuperPrize.onClick.RemoveAllListeners();
		if (keySuperRize >= this.numberKeyToOpenSuperPrize)
		{
			this.imgBtnOpenSuperPrize.sprite = this.sprBtnOpenSuperPrize;
			this.btnOpenSuperPrize.onClick.AddListener(new UnityAction(this.OpenSuperBox));
		}
		else
		{
			this.imgBtnOpenSuperPrize.sprite = this.sprBtnOpenSuperPrizeDisable;
			this.btnOpenSuperPrize.onClick.AddListener(new UnityAction(this.ShowPanelRewardSuperPrize));
		}
	}

	private void SetTextOpenBox()
	{
		this.textOpenOneBox.text = string.Format(ScriptLocalization.open_box, "x" + 1);
		this.textOpenOneBoxAgain.text = string.Format(ScriptLocalization.open_box, "x" + 1);
		this.textOpenMultiBox.text = string.Format(ScriptLocalization.open_box, "x" + this.numberBoxOpen);
		this.textOpenAgainMultiBox.text = string.Format(ScriptLocalization.open_box, "x" + this.numberBoxOpen);
	}

	public void CheckNumberMultiBox()
	{
		if (ShopContext.currentGem >= this.priceOneBox)
		{
			int num = ShopContext.currentGem / this.priceOneBox;
			if (num >= 10)
			{
				this.numberBoxOpen = 10;
			}
			else
			{
				this.numberBoxOpen = num;
			}
		}
		else
		{
			this.numberBoxOpen = 10;
		}
		this.textNumberBox.text = "x" + this.numberBoxOpen;
		this.SetTextOpenBox();
		this.textPriceOneBox.text = string.Empty + this.priceOneBox;
		this.textPriceOneBoxAgain.text = string.Empty + this.priceOneBox;
		this.priceMultiBox = (float)(this.numberBoxOpen * this.priceOneBox);
		if (this.numberBoxOpen >= 5)
		{
			this.priceMultiBox *= 0.9f;
		}
		else if (this.numberBoxOpen > 1 && this.numberBoxOpen < 5)
		{
			this.priceMultiBox *= 0.95f;
		}
		this.textPriceMultiBox.text = string.Empty + (int)this.priceMultiBox;
		this.textPriceMultiBoxAgain.text = string.Empty + (int)this.priceMultiBox;
	}

	private void RandomReward()
	{
		float num = 0f;
		float num2 = UnityEngine.Random.Range(0f, 100f);
		int num3 = 0;
		while ((float)num3 < this.totalRewardType)
		{
			float percent = DataBoxSheet.Get(num3).percent;
			float num4 = num;
			num = num4 + percent;
			if (num2 >= num4 && num2 < num)
			{
				this.currentIDReward = num3;
				break;
			}
			num3++;
		}
	}

	private PrizeManager.Reward RandomValueReward()
	{
		float num = UnityEngine.Random.Range(0f, 100f);
		float percentValue = DataBoxSheet.Get(this.currentIDReward).percentValue1;
		float percentValue2 = DataBoxSheet.Get(this.currentIDReward).percentValue2;
		bool isSuperPrize = false;
		int number;
		if (num <= percentValue)
		{
			number = DataBoxSheet.Get(this.currentIDReward).value1;
			isSuperPrize = true;
		}
		else if (num > percentValue && num <= percentValue2)
		{
			number = DataBoxSheet.Get(this.currentIDReward).value2;
		}
		else
		{
			number = DataBoxSheet.Get(this.currentIDReward).value3;
		}
		return new PrizeManager.Reward(this.currentIDReward, number, isSuperPrize);
	}

	private PrizeManager.Reward RewardSuperPrize()
	{
		this.currentIDReward = this.arrRewardSuperPrize[UnityEngine.Random.Range(0, this.arrRewardSuperPrize.Length)];
		int number = 0;
		switch (this.currentIDReward)
		{
		case 0:
			number = 100;
			break;
		case 1:
			number = 100;
			break;
		case 4:
			number = 300;
			break;
		case 5:
			number = 300000;
			break;
		}
		return new PrizeManager.Reward(this.currentIDReward, number, true);
	}

	public void RandomOneBox()
	{
		if (this.isOpenSuperBox)
		{
			this.rewardOneBox = this.RewardSuperPrize();
		}
		else
		{
			this.RandomReward();
			this.rewardOneBox = this.RandomValueReward();
		}
		UnityEngine.Debug.Log(string.Concat(new object[]
		{
			"Name Reward: ",
			DataBoxSheet.Get(this.rewardOneBox.idReward).nameReward,
			", So Luong: ",
			this.rewardOneBox.numberReward
		}));
	}

	public void RandomMultiBox()
	{
		this.listRewardMultiBox = new List<PrizeManager.Reward>();
		for (int i = 0; i < this.numberBoxOpen; i++)
		{
			this.RandomReward();
			this.listRewardMultiBox.Add(this.RandomValueReward());
		}
	}

	private void SpawnReward(PrizeManager.Reward reward)
	{
		int idReward = reward.idReward;
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.arrRewards[idReward], this.groupReward.position, Quaternion.identity);
		gameObject.transform.parent = this.groupReward;
		gameObject.transform.localScale = Vector3.one;
		if (reward.isSuperPrize)
		{
			gameObject.GetComponent<RewardView>().ShowPanelFxSuperPrize();
		}
		switch (idReward)
		{
		case 0:
		{
			RewardCardAllPlane component = gameObject.GetComponent<RewardCardAllPlane>();
			component.numberReward = reward.numberReward;
			component.SetView();
			break;
		}
		case 1:
		{
			RewardCardAllDrone component2 = gameObject.GetComponent<RewardCardAllDrone>();
			component2.numberReward = reward.numberReward;
			component2.SetView();
			break;
		}
		case 2:
		{
			RewardCardPlane component3 = gameObject.GetComponent<RewardCardPlane>();
			component3.SetView(DataGame.Current.RandomPlaneID, reward.numberReward);
			break;
		}
		case 3:
		{
			RewardCardDrone component4 = gameObject.GetComponent<RewardCardDrone>();
			component4.SetView(DataGame.Current.RandomDroneID(), reward.numberReward);
			break;
		}
		case 4:
		{
			RewardGem component5 = gameObject.GetComponent<RewardGem>();
			component5.numberReward = reward.numberReward;
			component5.SetView();
			if (this.isOpenOneBox)
			{
				CurrencyLog.LogGemIn(reward.numberReward, CurrencyLog.In.Prize, "One_Box_Normal");
			}
			else if (this.isOpenMultiBox)
			{
				CurrencyLog.LogGemIn(reward.numberReward, CurrencyLog.In.Prize, "Multi_Box_Normal");
			}
			else if (this.isOpenSuperBox)
			{
				CurrencyLog.LogGemIn(reward.numberReward, CurrencyLog.In.Prize, "Super_Box");
			}
			break;
		}
		case 5:
		{
			RewardCoin component6 = gameObject.GetComponent<RewardCoin>();
			component6.numberReward = reward.numberReward;
			component6.SetView();
			if (this.isOpenOneBox)
			{
				CurrencyLog.LogGoldIn(reward.numberReward, CurrencyLog.In.Prize, "Box_Normal");
			}
			else if (this.isOpenMultiBox)
			{
				CurrencyLog.LogGoldIn(reward.numberReward, CurrencyLog.In.Prize, "Multi_Box_Normal");
			}
			else if (this.isOpenSuperBox)
			{
				CurrencyLog.LogGoldIn(reward.numberReward, CurrencyLog.In.Prize, "Super_Box");
			}
			break;
		}
		case 6:
		{
			RewardEnergy component7 = gameObject.GetComponent<RewardEnergy>();
			component7.numberReward = reward.numberReward;
			component7.SetView();
			break;
		}
		case 7:
		{
			RewardKeySuperPrize component8 = gameObject.GetComponent<RewardKeySuperPrize>();
			component8.SetView(reward.numberReward);
			this.CheckKeySuperPrize();
			break;
		}
		case 8:
		{
			RewardItemHeart component9 = gameObject.GetComponent<RewardItemHeart>();
			component9.SetView(reward.numberReward);
			break;
		}
		case 9:
		{
			RewardItemBullet10 component10 = gameObject.GetComponent<RewardItemBullet10>();
			component10.SetView(reward.numberReward);
			break;
		}
		case 10:
		{
			RewardItemBullet5 component11 = gameObject.GetComponent<RewardItemBullet5>();
			break;
		}
		case 11:
		{
			RewardShield component12 = gameObject.GetComponent<RewardShield>();
			component12.SetData(reward.numberReward);
			break;
		}
		case 12:
		{
			RewardSkillPlane component13 = gameObject.GetComponent<RewardSkillPlane>();
			component13.numberReward = reward.numberReward;
			component13.SetView();
			break;
		}
		}
	}

	public void SpawnOneReward()
	{
		this.SpawnReward(this.rewardOneBox);
		this.btnBackToPanelPrize.interactable = true;
	}

	public IEnumerator SpawnMultiReward()
	{
		for (int i = 0; i < this.listRewardMultiBox.Count; i++)
		{
			this.SpawnReward(this.listRewardMultiBox[i]);
			yield return new WaitForSeconds(this.timeDelaySpawnReward);
		}
		this.btnBackToPanelPrize.interactable = true;
		yield break;
	}

	public void OpenOneBox()
	{
		if (ShopContext.currentGem >= this.priceOneBox)
		{
			this.isOpenOneBox = true;
			this.isOpenMultiBox = false;
			this.isOpenSuperBox = false;
			this.btnBackToPanelPrize.interactable = false;
			CacheGame.MinusGems(this.priceOneBox);
			CurrencyLog.LogGemOut(this.priceOneBox, CurrencyLog.Out.Prize, "Buy_One_Box");
			this.RandomOneBox();
			this.CheckNumberMultiBox();
			base.StartCoroutine(this.StartEffectOpenBox());
		}
		else
		{
			ShopManager.Instance.ShowPopupNotEnoughGem();
		}
	}

	public void OpenMultiBox()
	{
		if ((float)ShopContext.currentGem >= this.priceMultiBox)
		{
			this.isOpenMultiBox = true;
			this.isOpenOneBox = false;
			this.isOpenSuperBox = false;
			this.btnBackToPanelPrize.interactable = false;
			CacheGame.MinusGems((int)this.priceMultiBox);
			CurrencyLog.LogGemOut((int)this.priceMultiBox, CurrencyLog.Out.Prize, "Buy_Multi_Box");
			this.RandomMultiBox();
			this.CheckNumberMultiBox();
			base.StartCoroutine(this.StartEffectOpenBox());
		}
		else
		{
			ShopManager.Instance.ShowPopupNotEnoughGem();
		}
	}

	private IEnumerator OpenBoxAgain()
	{
		this.rectTransformFXSpawnReward.parent = this.panelOpenBox.transform;
		DOTween.Rewind("BOX_MOVE", true);
		yield return new WaitForSeconds(0.25f);
		DOTween.Restart("BOX_SHAKE", true, -1f);
		DOTween.Play("BOX_SHAKE");
		this.fxBoxShake.Play();
		base.StartCoroutine(this.StartEffectOpenBox());
		yield break;
	}

	public void OpenOneBoxAgain()
	{
		if (ShopContext.currentGem >= this.priceOneBox)
		{
			this.isOpenOneBox = true;
			this.isOpenMultiBox = false;
			this.isOpenSuperBox = false;
			this.btnBackToPanelPrize.interactable = false;
			CacheGame.MinusGems(this.priceOneBox);
			CurrencyLog.LogGemOut(this.priceOneBox, CurrencyLog.Out.Prize, "Buy_One_Box");
			this.RandomOneBox();
			this.panelReward.SetActive(false);
			IEnumerator enumerator = this.groupReward.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					UnityEngine.Object.Destroy(transform.gameObject);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			this.CheckNumberMultiBox();
			base.StartCoroutine(this.OpenBoxAgain());
		}
		else
		{
			ShopManager.Instance.ShowPopupNotEnoughGem();
		}
	}

	public void OpenMultiBoxAgain()
	{
		if ((float)ShopContext.currentGem >= this.priceMultiBox)
		{
			this.isOpenMultiBox = true;
			this.isOpenOneBox = false;
			this.isOpenSuperBox = false;
			this.btnBackToPanelPrize.interactable = false;
			this.timeDelaySpawnReward = 0.4f;
			CacheGame.MinusGems((int)this.priceMultiBox);
			CurrencyLog.LogGemOut((int)this.priceMultiBox, CurrencyLog.Out.Prize, "Buy_Multi_Box");
			this.RandomMultiBox();
			this.panelReward.SetActive(false);
			IEnumerator enumerator = this.groupReward.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					UnityEngine.Object.Destroy(transform.gameObject);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			this.CheckNumberMultiBox();
			base.StartCoroutine(this.OpenBoxAgain());
		}
		else
		{
			ShopManager.Instance.ShowPopupNotEnoughGem();
		}
	}

	public void OpenSuperBox()
	{
		this.isOpenSuperBox = true;
		this.isOpenMultiBox = false;
		this.isOpenOneBox = false;
		this.btnBackToPanelPrize.interactable = false;
		this.RandomOneBox();
		base.StartCoroutine(this.StartEffectOpenBox());
	}

	public IEnumerator StartEffectOpenBox()
	{
		this.panelOpenBox.SetActive(true);
		this.SetAnimBoxOpen();
		yield return new WaitForSeconds(1.122f);
		this.fxOpenBox.SetActive(true);
		yield return new WaitForSeconds(0.2f);
		this.SetAnimBoxOpenIdle();
		yield return new WaitForSeconds(0.2f);
		DOTween.Restart("BOX_MOVE", true, -1f);
		DOTween.Play("BOX_MOVE");
		yield return new WaitForSeconds(0.5f);
		if (this.isOpenSuperBox)
		{
			this.panelButtonOpenBoxAgain.SetActive(false);
		}
		else
		{
			this.panelButtonOpenBoxAgain.SetActive(true);
		}
		this.panelReward.SetActive(true);
		if (this.isOpenMultiBox)
		{
			base.StartCoroutine(this.SpawnMultiReward());
		}
		else
		{
			this.SpawnOneReward();
		}
		if (this.isOpenSuperBox)
		{
			this.isOpenSuperBox = false;
			int num = CacheGame.KeySuperRize;
			num -= this.numberKeyToOpenSuperPrize;
			CacheGame.KeySuperRize = num;
			this.CheckKeySuperPrize();
		}
		yield break;
	}

	public void ClosePanelOpenBox()
	{
		this.panelReward.SetActive(false);
		this.timeDelaySpawnReward = 0.4f;
		IEnumerator enumerator = this.groupReward.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				UnityEngine.Object.Destroy(transform.gameObject);
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
		this.rectTransformFXSpawnReward.parent = this.panelOpenBox.transform;
		DOTween.Rewind("BOX_MOVE", true);
		this.panelOpenBox.SetActive(false);
	}

	public void IgnoreFX()
	{
		this.timeDelaySpawnReward = 0f;
	}

	public void ShowPanelPrize()
	{
		this.needScroll = true;
		this.CheckKeySuperPrize();
		this.SetTextOpenBox();
		this.panelPrize.SetActive(true);
		DOTween.Restart("HOME_PRIZE", true, -1f);
		DOTween.Play("HOME_PRIZE");
		EscapeManager.Current.AddAction(new Action(this.HidePanelPrize));
		if (NewTutorial.current.currentStepTutorial_OpenBox == 3)
		{
			NewTutorial.current.OpenBox_Step3();
		}
	}

	public void HidePanelPrize()
	{
		if (NewTutorial.current.currentStepTutorial_OpenBox == 7)
		{
			NewTutorial.current.OpenBox_Step7();
		}
		EscapeManager.Current.RemoveAction(new Action(this.HidePanelPrize));
		this.needScroll = false;
		DOTween.PlayBackwards("HOME_PRIZE");
		base.StartCoroutine(GameContext.Delay(0.1f, delegate
		{
			this.panelPrize.SetActive(false);
		}));
		this.SaveTimeLeftFreeBox();
	}

	public void ShowPanelRewardSuperPrize()
	{
		EscapeManager.Current.AddAction(new Action(this.HidePanelRewardSuperPrize));
		this.panelRewardSuperPrize.SetActive(true);
		DOTween.Restart("HOME_REWARD_SUPER_PRIZE", true, -1f);
		DOTween.Play("HOME_REWARD_SUPER_PRIZE");
	}

	public void HidePanelRewardSuperPrize()
	{
		DOTween.PlayBackwards("HOME_REWARD_SUPER_PRIZE");
		base.StartCoroutine(GameContext.Delay(0.2f, delegate
		{
			this.panelRewardSuperPrize.SetActive(false);
		}));
		EscapeManager.Current.RemoveAction(new Action(this.HidePanelRewardSuperPrize));
	}

	private void SetAnimBoxOpen()
	{
		if (this.isOpenSuperBox)
		{
			this.skeletonGraphicBox.AnimationState.SetAnimation(0, this.animOpenSuperBox, false);
		}
		else
		{
			this.skeletonGraphicBox.AnimationState.SetAnimation(0, this.animOpenNormalBox, false);
		}
	}

	private void SetAnimBoxOpenIdle()
	{
		if (!this.isOpenSuperBox)
		{
			this.skeletonGraphicBox.AnimationState.SetAnimation(0, this.animBoxNormalOpenIdle, true);
		}
		else
		{
			this.skeletonGraphicBox.AnimationState.SetAnimation(0, this.animBoxSuperOpenIdle, true);
		}
	}

	private void CheckTimeFreeBox()
	{
		this.timeLeftToFreeBox = (float)GameContext.currentSecondsTimeLeftWatchVideoToOpenBox;
		if (GameContext.availableWatchVideoToOpenBox)
		{
			this.ShowButtonWatchVideo();
		}
		else
		{
			if (GameContext.firstTimeInSceneHome)
			{
				GameContext.firstTimeInSceneHome = false;
				this.timeSpan = GameContext.GetTimeSpan(DateTime.Now, GameContext.lastTimeQuitGame);
			}
			else
			{
				this.timeSpan = GameContext.GetTimeSpan(DateTime.Now, GameContext.lastTimeInSceneHome);
			}
			if (this.timeSpan.TotalHours >= (double)this.timeToFreeBox)
			{
				this.ShowButtonWatchVideo();
			}
			else
			{
				int num = (int)this.timeSpan.TotalSeconds;
				if ((float)num >= this.timeLeftToFreeBox)
				{
					this.ShowButtonWatchVideo();
				}
				else
				{
					this.ShowButtonOpenX1Box();
					this.timeLeftToFreeBox -= (float)num;
					this.StartCountdown();
				}
			}
		}
	}

	private void ShowButtonWatchVideo()
	{
		GameContext.availableWatchVideoToOpenBox = true;
		if (this.skeletonGraphicIconBoxHomeScreen != null)
		{
			this.skeletonGraphicIconBoxHomeScreen.freeze = false;
		}
		this.textOpenOneBox.text = string.Format(ScriptLocalization.watch_the_video_to_get_free_box, "x" + 1);
		this.textBtnWatch.text = ScriptLocalization.watch;
		this.btnOpenOneBox.SetActive(false);
		this.btnWatchVideoToFreeBox.SetActive(true);
		this.objTextNextVideo.SetActive(false);
		this.objTextWatchVideo.SetActive(true);
		this.buttonWatchVideo.onClick.RemoveAllListeners();
		this.buttonWatchVideo.onClick.AddListener(new UnityAction(this.WatchVideoToOpenBox));
		this.timeLeftToFreeBox = 0f;
		if (SceneContext.sceneName == "Home")
		{
			WarningManager.Current.ShowNotificationPrize();
		}
	}

	private void ShowButtonOpenX1Box()
	{
		GameContext.availableWatchVideoToOpenBox = false;
		if (this.skeletonGraphicIconBoxHomeScreen != null)
		{
			this.skeletonGraphicIconBoxHomeScreen.freeze = true;
		}
		this.btnOpenOneBox.SetActive(true);
		this.btnWatchVideoToFreeBox.SetActive(false);
		this.objTextNextVideo.SetActive(true);
		this.objTextWatchVideo.SetActive(false);
	}

	public void WatchVideoToOpenBox()
	{
		GameContext.actionVideo = GameContext.ActionVideoAds.Box;
	}

	public void WatchVideoToOpenBoxComplete()
	{
		this.textBtnWatch.text = ScriptLocalization.open;
		this.textOpenOneBox.text = string.Format(ScriptLocalization.open_box, "x" + 1);
		this.buttonWatchVideo.onClick.RemoveAllListeners();
		this.buttonWatchVideo.onClick.AddListener(new UnityAction(this.OpenFreeBox));
		if (SceneContext.sceneName == "Home")
		{
			WarningManager.Current.HideNotificationPrize();
		}
	}

	public void OpenFreeBox()
	{
		this.btnBackToPanelPrize.interactable = false;
		this.RandomOneBox();
		base.StartCoroutine(this.StartEffectOpenBox());
		this.timeLeftToFreeBox = this.timeToFreeBox * 3600f;
		this.StartCountdown();
		this.ShowButtonOpenX1Box();
	}

	private IEnumerator CoroutineFreeBoxCountdown()
	{
		while (this.timeLeftToFreeBox > 0f)
		{
			this.textTimeFreeBox.text = GameUtil.GetFormatTimeFrom(this.timeLeftToFreeBox);
			yield return new WaitForSecondsRealtime(1f);
			this.timeLeftToFreeBox -= 1f;
		}
		this.StopCountdown();
		this.ShowButtonWatchVideo();
		yield break;
	}

	public void SaveTimeLeftFreeBox()
	{
		CacheGame.TimeLeftFreeBox = (int)this.timeLeftToFreeBox;
		GameContext.currentSecondsTimeLeftWatchVideoToOpenBox = (int)this.timeLeftToFreeBox;
	}

	private void StartCountdown()
	{
		if (!this.coroutineIsRunning)
		{
			this.coroutineIsRunning = true;
			base.StartCoroutine("CoroutineFreeBoxCountdown");
		}
	}

	private void StopCountdown()
	{
		if (this.coroutineIsRunning)
		{
			this.coroutineIsRunning = false;
			base.StopCoroutine("CoroutineFreeBoxCountdown");
		}
	}

	public void ShowBtnFreeTutorial()
	{
		this.btnFreeTutorial.SetActive(true);
		this.btnWatchVideoToFreeBox.SetActive(false);
		this.btnOpenOneBox.SetActive(false);
	}

	public void HideBtnFreeTutorial()
	{
		this.btnFreeTutorial.SetActive(false);
		this.btnWatchVideoToFreeBox.SetActive(true);
		this.btnOpenOneBox.SetActive(false);
	}

	public void OpenFreeTutorialBox()
	{
		this.RandomOneBox();
		base.StartCoroutine(this.StartEffectOpenBox());
		this.CheckTimeFreeBox();
		if (NewTutorial.current.currentStepTutorial_OpenBox == 5)
		{
			NewTutorial.current.OpenBox_Step5();
			this.HideBtnFreeTutorial();
		}
	}

	public static PrizeManager current;

	public GameObject panelPrize;

	public Text textNumberBox;

	public Text textOpenOneBox;

	public Text textOpenOneBoxAgain;

	public Text textOpenMultiBox;

	public Text textOpenAgainMultiBox;

	public Text textPriceOneBox;

	public Text textPriceMultiBox;

	public Text textPriceOneBoxAgain;

	public Text textPriceMultiBoxAgain;

	public Text textNumberKeySuperPrize;

	public Text textCollectKeySupperPrize;

	public Text textTimeFreeBox;

	public GameObject panelBox;

	public GameObject panelOpenBox;

	public GameObject panelReward;

	public GameObject fxOpenBox;

	public GameObject fxSpawnReward;

	public Transform groupReward;

	public GameObject[] arrRewards;

	public ParticleSystem fxBoxShake;

	public Button btnOpenSuperPrize;

	public Button btnBackToPanelPrize;

	public Image imgBtnOpenSuperPrize;

	public Sprite sprBtnOpenSuperPrize;

	public Sprite sprBtnOpenSuperPrizeDisable;

	public GameObject panelButtonOpenBoxAgain;

	[Header("Panel Reward Super Prize")]
	public GameObject panelRewardSuperPrize;

	public SkeletonGraphic skeletonGraphicBox;

	[SpineAnimation("", "", true, false)]
	public string animOpenNormalBox;

	[SpineAnimation("", "", true, false)]
	public string animBoxNormalOpenIdle;

	[SpineAnimation("", "", true, false)]
	public string animOpenSuperBox;

	[SpineAnimation("", "", true, false)]
	public string animBoxSuperOpenIdle;

	public GameObject btnOpenOneBox;

	public GameObject btnWatchVideoToFreeBox;

	public GameObject btnFreeTutorial;

	public GameObject objTextNextVideo;

	public GameObject objTextWatchVideo;

	public Text textBtnWatch;

	public SkeletonGraphic skeletonGraphicIconBoxHomeScreen;

	public ScrollRect scrollBestReward;

	private int numberKeyToOpenSuperPrize = 20;

	private float totalRewardType;

	private int currentIDReward;

	private int numberBoxOpen;

	private float priceMultiBox;

	private PrizeManager.Reward rewardOneBox;

	private List<PrizeManager.Reward> listRewardMultiBox;

	private ParticleSystem particleSpawnReward;

	private RectTransform rectTransformFXSpawnReward;

	private bool isOpenOneBox;

	private bool isOpenMultiBox;

	private bool isOpenSuperBox;

	private int[] arrRewardSuperPrize = new int[]
	{
		0,
		1,
		4,
		5
	};

	private Button buttonWatchVideo;

	private bool needScroll;

	private float timeDelaySpawnReward = 0.4f;

	private TimeSpan timeSpan;

	private float timeLeftToFreeBox;

	private bool coroutineIsRunning;

	[Serializable]
	public enum RewardType
	{
		Card_All_Plane,
		Card_All_Drone,
		Card_Random_Plane,
		Card_Random_Drone,
		Gem,
		Coin,
		Energy,
		Key_Super_Prize,
		Item_Heart,
		Item_Bullet_10,
		Item_Bullet_5,
		Item_Shield,
		Special_Skill,
		Box,
		Card_Plane,
		Card_Drone,
		Unlock_Plane,
		Blue_Star,
		Ticket_Galaxy_1,
		Ticket_Galaxy_2,
		Ticket_Galaxy_3,
		Ticket_Galaxy_4
	}

	private class Reward
	{
		public Reward(int _id, int _number, bool _isSuperPrize)
		{
			this.idReward = _id;
			this.numberReward = _number;
			this.isSuperPrize = _isSuperPrize;
		}

		public int idReward;

		public int numberReward;

		public bool isSuperPrize;
	}
}

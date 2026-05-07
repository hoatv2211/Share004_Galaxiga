using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DailyGiftManager : MonoBehaviour
{
	private void Awake()
	{
		DailyGiftManager.Current = this;
		this.CheckNewMonth();
		if (GameContext.canCollectDailyGift)
		{
			this.SetImageBtnCollectActive();
			this.arrGift[this.dayCollectGift].SetCanCollectGift();
		}
		else
		{
			this.SetImageBtnCollectDeactive();
			this.arrGift[this.dayCollectGift].SetActionGiftCollected();
		}
		this.SetGiftCollected();
	}

	private void SetGiftCollected()
	{
		if (this.dayCollectGift > 0)
		{
			for (int i = 0; i < this.dayCollectGift; i++)
			{
				this.arrGift[i].SetViewGiftCollected();
			}
		}
	}

	private void CheckNewMonth()
	{
		this.dayCollectGift = CacheGame.NumberDayCollectGift;
		if (this.dayCollectGift == 30)
		{
			CacheGame.NumberDayCollectGift = 0;
			this.isNewMonth = true;
		}
		if (this.dayCollectGift >= 17)
		{
			this.scrollRectGift.verticalNormalizedPosition = 0.02f;
		}
	}

	public void SetActionButtonCollect(UnityAction actionT)
	{
		this.btnCollect.onClick.RemoveAllListeners();
		this.btnCollect.onClick.AddListener(actionT);
	}

	public void AddDayCollectGift()
	{
		this.dayCollectGift++;
		CacheGame.NumberDayCollectGift = this.dayCollectGift;
	}

	public void SetPositionFXDailyGift(GameObject objGift)
	{
		this.objFXDailyGift.transform.parent = objGift.transform;
		this.objFXDailyGift.transform.localPosition = Vector3.zero;
		this.objFXDailyGift.transform.localScale = new Vector3(100f, 100f, 100f);
		this.objFXDailyGift.SetActive(true);
	}

	public void HideFxDailyGift()
	{
		this.objFXDailyGift.SetActive(false);
	}

	public void SetImageBtnCollectActive()
	{
		this.imgBtnCollect.sprite = this.sprBtnActive;
	}

	public void SetImageBtnCollectDeactive()
	{
		this.imgBtnCollect.sprite = this.sprBtnDeactive;
	}

	public void ShowPanelDailyGift()
	{
		EscapeManager.Current.AddAction(new Action(this.HidePanelDailyGift));
		this.panelDailyGift.SetActive(true);
		DOTween.Restart("Daily_Gift", true, -1f);
		DOTween.Play("Daily_Gift");
		base.StartCoroutine(this.DelaySetScrollRect());
	}

	public void HidePanelDailyGift()
	{
		if (NewTutorial.current.currentStepTutorial_RewardDailyGift == 9)
		{
			NewTutorial.current.RewardDailyGift_Step9();
		}
		DOTween.PlayBackwards("Daily_Gift");
		EscapeManager.Current.RemoveAction(new Action(this.HidePanelDailyGift));
		base.StartCoroutine(GameContext.Delay(0.25f, delegate
		{
			this.scrollRectGift.verticalNormalizedPosition = 1f;
			this.panelDailyGift.SetActive(false);
		}));
	}

	private IEnumerator DelaySetScrollRect()
	{
		yield return new WaitForSeconds(0.15f);
		this.scrollRectGift.verticalNormalizedPosition = 1f;
		yield break;
	}

	public static DailyGiftManager Current;

	public Sprite sprIconCardAllPlane;

	public Sprite sprIconCardAllDrone;

	public Sprite sprIconPower;

	public Sprite sprIconShield;

	public Sprite sprIconHeart;

	public Sprite sprIconSkillPlane;

	public Sprite sprIconGem;

	public Sprite sprIconCoin;

	public Sprite sprKeySuperPrize;

	[HideInInspector]
	public bool isNewMonth;

	public int dayCollectGift;

	public Button btnCollect;

	public GameObject objFXDailyGift;

	public Image imgBtnCollect;

	public Sprite sprBtnActive;

	public Sprite sprBtnDeactive;

	public GameObject panelDailyGift;

	public ScrollRect scrollRectGift;

	public ViewDailyGift[] arrGift;

	[Serializable]
	public enum Gift
	{
		Gem,
		Coin,
		Card_All_Plane,
		Card_All_Drone,
		Card_Random_Plane,
		Card_Random_Drone,
		Item_Heart,
		Item_Bullet_5,
		Item_Bullet_10,
		Item_Shield,
		Special_Skill,
		Key_Super_Prize,
		Plane,
		Drone
	}
}

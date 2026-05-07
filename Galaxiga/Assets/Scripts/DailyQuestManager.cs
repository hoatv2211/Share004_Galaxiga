using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DailyQuestManager : MonoBehaviour
{
	public static DailyQuestManager Current
	{
		get
		{
			if (DailyQuestManager._instance == null)
			{
				DailyQuestManager._instance = UnityEngine.Object.FindObjectOfType<DailyQuestManager>();
			}
			return DailyQuestManager._instance;
		}
	}

	private void Awake()
	{
		this.scrollRect.onValueChanged.AddListener(new UnityAction<Vector2>(this.OnValueChange));
		this.CheckShowNotification();
	}

	private void OnValueChange(Vector2 vector2)
	{
		if ((double)this.scrollRect.verticalNormalizedPosition <= 0.1)
		{
			this.objFade.SetActive(false);
		}
		else
		{
			this.objFade.SetActive(true);
		}
	}

	private void Start()
	{
		if (GameContext.isPurchasePremiumPack && !SaveDataQuest.IsCollectReward(DailyQuestManager.Quest.premium))
		{
			this.objPremium.SetActive(true);
			this.rewardDailyQuests[11].ShowQuest();
		}
	}

	public void SetBottomDailyReward()
	{
		this.rewardDailyQuests[12].SetBottomDailyReward();
	}

	public void ShowTimeResetDay()
	{
		this.scrollRect.verticalNormalizedPosition = 1f;
		int num = this.rewardDailyQuests.Length;
		for (int i = 0; i < num; i++)
		{
			this.rewardDailyQuests[i].ShowQuest();
		}
		this.timerDayleft = (float)CheckNewDay.Current.CurrentSecondDayleft();
		base.StartCoroutine("CoroutineCountdownTimeResetDay");
	}

	public void HideTimeResetDay()
	{
		base.StopCoroutine("CoroutineCountdownTimeResetDay");
	}

	private IEnumerator CoroutineCountdownTimeResetDay()
	{
		while (this.timerDayleft > 0f)
		{
			string textFormat = TimeSpan.FromSeconds((double)this.timerDayleft).ToString("hh\\:mm\\:ss");
			this.textResetTime.text = textFormat;
			yield return new WaitForSecondsRealtime(1f);
			this.timerDayleft -= 1f;
		}
		this.timerDayleft = 86400f;
		CheckNewDay.Current.NewDay();
		base.StartCoroutine("CoroutineCountdownTimeResetDay");
		yield break;
	}

	public void CompleteQuest()
	{
		this.currentNumberQuestComplete++;
		UnityEngine.Debug.Log("number quest complete " + this.currentNumberQuestComplete);
		this.CheckShowNotification();
	}

	public void GetRewardQuestComplete()
	{
		if (this.currentNumberQuestComplete > 0)
		{
			this.currentNumberQuestComplete--;
		}
		UnityEngine.Debug.Log("number quest complete " + this.currentNumberQuestComplete);
		this.CheckShowNotification();
	}

	public void CheckShowNotification()
	{
		if (this.currentNumberQuestComplete > 0)
		{
			WarningManager.Current.ShowNotificationQuest();
		}
		else
		{
			WarningManager.Current.HideNotificationQuest();
			bool flag = SaveDataQuest.IsCollectReward(DailyQuestManager.Quest.tv_trailer);
			if (!SaveDataQuest.IsCollectReward(DailyQuestManager.Quest.daily_reward))
			{
				WarningManager.Current.ShowNotificationQuest();
			}
			else if (!flag)
			{
				WarningManager.Current.ShowNotificationTvTrailer();
			}
		}
	}

	public void CheckQuest(DailyQuestManager.Quest quest, int progress)
	{
		this.rewardDailyQuests[(int)quest].CheckQuestIsComplete(quest, progress);
	}

	public void SetObjTemp()
	{
		this.recTransformObjTemp.SetAsLastSibling();
	}

	private static DailyQuestManager _instance;

	public ScrollRect scrollRect;

	public Text textResetTime;

	public GameObject objPremium;

	public GameObject objFade;

	public RectTransform recTransformObjTemp;

	public RewardDailyQuest[] rewardDailyQuests;

	private int currentNumberQuestComplete;

	private float timerDayleft;

	[Serializable]
	public enum Quest
	{
		tv_trailer,
		investor,
		mechanic,
		upgrade_drone,
		hunter_easy,
		hunter_normal,
		hunter_hard,
		coiner,
		free_energy1,
		free_energy2,
		im_rick,
		premium,
		daily_reward
	}
}

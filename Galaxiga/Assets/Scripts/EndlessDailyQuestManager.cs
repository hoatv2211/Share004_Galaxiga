using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EndlessDailyQuestManager : MonoBehaviour
{
	public static EndlessDailyQuestManager current
	{
		get
		{
			if (EndlessDailyQuestManager._current == null)
			{
				EndlessDailyQuestManager._current = UnityEngine.Object.FindObjectOfType<EndlessDailyQuestManager>();
			}
			return EndlessDailyQuestManager._current;
		}
	}

	public void CheckQuest(EndlessDailyQuestManager.Quest quest, int progress)
	{
		this.endlessRewardDailyQuests[(int)quest].CheckCompleteQuest(quest, progress);
	}

	private void OnEnable()
	{
		this.ShowTimeResetDay();
	}

	private void OnDisable()
	{
		this.HideTimeResetDay();
	}

	public void ShowTimeResetDay()
	{
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

	public void CheckCompleteQuest()
	{
		this.currentNumberQuestComplete = 0;
		for (int i = 0; i < this.endlessRewardDailyQuests.Length; i++)
		{
			if (this.endlessRewardDailyQuests[i].isCompleteQuest)
			{
				this.currentNumberQuestComplete++;
			}
		}
		this.CheckShowNotification();
		UnityEngine.Debug.Log("currentNumberQuestComplete: " + this.currentNumberQuestComplete);
	}

	public void CheckShowNotification()
	{
		if (this.currentNumberQuestComplete > 0)
		{
			this.objWarning.SetActive(true);
		}
		else
		{
			this.objWarning.SetActive(false);
		}
	}

	private static EndlessDailyQuestManager _current;

	public Text textResetTime;

	public EndlessRewardDailyQuest[] endlessRewardDailyQuests;

	public RectTransform recTransformObjTemp;

	public GameObject objWarning;

	private float timerDayleft;

	private int currentNumberQuestComplete;

	public enum Quest
	{
		complete_quest,
		fly_through_waves0,
		defeat_enemies0,
		defeat_boss0,
		purchase_stars_in_mystic_shop0,
		use_skill_times0
	}
}

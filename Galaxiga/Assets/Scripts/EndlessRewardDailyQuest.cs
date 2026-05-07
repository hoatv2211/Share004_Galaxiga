using System;
using I2.Loc;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class EndlessRewardDailyQuest : MonoBehaviour
{
	public bool isQuest0()
	{
		return this.quest == EndlessDailyQuestManager.Quest.complete_quest;
	}

	private void Awake()
	{
		this.rectTransform = base.GetComponent<RectTransform>();
	}

	private void OnEnable()
	{
		this.LoadData();
		this.ShowQuest();
	}

	public void LoadData()
	{
		int num = (int)(this.quest + 1);
		this.idQuest = QuestEndlessModeSheet.Get(num).idQuest;
		this.target1 = QuestEndlessModeSheet.Get(num).target1;
		this.target2 = QuestEndlessModeSheet.Get(num).target2;
		this.target3 = QuestEndlessModeSheet.Get(num).target3;
		this.reward1 = QuestEndlessModeSheet.Get(num).reward1;
		this.reward2 = QuestEndlessModeSheet.Get(num).reward2;
		this.reward3 = QuestEndlessModeSheet.Get(num).reward3;
		this.idReward = QuestEndlessModeSheet.Get(num).idReward;
		this.levelQuest = SaveDataQuestEndless.LevelQuest(this.quest);
		switch (this.levelQuest)
		{
		case 0:
			this.currentTarget = this.target1;
			this.currentReward = this.reward1;
			break;
		case 1:
			this.currentTarget = this.target2;
			this.currentReward = this.reward2;
			break;
		case 2:
			this.currentTarget = this.target3;
			this.currentReward = this.reward3;
			break;
		default:
			this.currentTarget = this.target3;
			this.currentReward = this.reward3;
			break;
		}
		this.sliderQuest.maxValue = (float)(this.currentTarget + 1);
	}

	public void ShowQuest()
	{
		this.currentProgessQuest = SaveDataQuestEndless.dataQuestEndlessContainer.dataQuestEndless[(int)this.quest].processQuest;
		this.CheckCompleteQuest(this.quest, this.currentProgessQuest);
	}

	public void HideQuest()
	{
		this.isCompleteQuest = false;
		this.btnCollect.SetActive(false);
		this.finishQuest.SetActive(true);
		this.SetView();
	}

	public void SetView()
	{
		switch (this.quest)
		{
		case EndlessDailyQuestManager.Quest.complete_quest:
			this.txtInforQuest.text = string.Format(ScriptLocalization.endless_quest_0_infor, this.currentTarget);
			this.txtReward0.text = this.currentReward.Split(new char[]
			{
				','
			})[0];
			this.txtReward1.text = this.currentReward.Split(new char[]
			{
				','
			})[1];
			this.txtReward2.text = this.currentReward.Split(new char[]
			{
				','
			})[2];
			break;
		case EndlessDailyQuestManager.Quest.fly_through_waves0:
			this.txtInforQuest.text = string.Format(ScriptLocalization.endless_quest_1_infor, this.currentTarget);
			this.txtReward.text = this.currentReward;
			break;
		case EndlessDailyQuestManager.Quest.defeat_enemies0:
			this.txtInforQuest.text = string.Format(ScriptLocalization.endless_quest_2_infor, this.currentTarget);
			this.txtReward.text = this.currentReward;
			break;
		case EndlessDailyQuestManager.Quest.defeat_boss0:
			this.txtInforQuest.text = string.Format(ScriptLocalization.endless_quest_3_infor, this.currentTarget);
			this.txtReward.text = this.currentReward;
			break;
		case EndlessDailyQuestManager.Quest.purchase_stars_in_mystic_shop0:
			this.txtInforQuest.text = string.Format(ScriptLocalization.endless_quest_4_infor, this.currentTarget);
			this.txtReward.text = this.currentReward;
			break;
		case EndlessDailyQuestManager.Quest.use_skill_times0:
			this.txtInforQuest.text = string.Format(ScriptLocalization.endless_quest_5_infor, this.currentTarget);
			this.txtReward.text = this.currentReward;
			break;
		}
		for (int i = 0; i < this.imgStar.Length; i++)
		{
			if (i < this.levelQuest)
			{
				this.imgStar[i].sprite = this.sprActiveStar;
			}
			else
			{
				this.imgStar[i].sprite = this.sprInactiveStar;
			}
		}
	}

	public void CheckCompleteQuest(EndlessDailyQuestManager.Quest quest, int currentProgress)
	{
		if (this.levelQuest < 3)
		{
			if (currentProgress >= this.currentTarget)
			{
				this.isCompleteQuest = true;
				this.imgBtnCollect.sprite = this.sprBtnComplete;
				this.txtSliderQuest.text = currentProgress + "/" + this.currentTarget;
				this.txtBtnCollect.text = ScriptLocalization.collect;
				this.sliderQuest.value = this.sliderQuest.maxValue;
				EndlessDailyQuestManager.current.CheckCompleteQuest();
			}
			else
			{
				this.imgBtnCollect.sprite = this.sprBtnProcessing;
				this.txtSliderQuest.text = currentProgress + "/" + this.currentTarget;
				this.sliderQuest.value = (float)(currentProgress + 1);
			}
			this.SetView();
		}
		else
		{
			this.imgBtnCollect.sprite = this.sprBtnComplete;
			this.txtSliderQuest.text = ScriptLocalization.complete;
			this.txtBtnCollect.text = ScriptLocalization.collect;
			this.sliderQuest.value = this.sliderQuest.maxValue;
			this.HideQuest();
		}
	}

	public void CollectReward()
	{
		UnityEngine.Debug.Log("CollectReward:" + this.isCompleteQuest);
		if (this.isCompleteQuest)
		{
			this.isCompleteQuest = false;
			switch (this.quest)
			{
			case EndlessDailyQuestManager.Quest.complete_quest:
			{
				int coins = int.Parse(this.currentReward.Split(new char[]
				{
					','
				})[0]);
				int gems = int.Parse(this.currentReward.Split(new char[]
				{
					','
				})[1]);
				int number = int.Parse(this.currentReward.Split(new char[]
				{
					','
				})[2]);
				CacheGame.AddCoins(coins);
				CacheGame.AddGems(gems);
				EndlessModeManager.current.AddBlueStar(number);
				UnityEngine.Object.Instantiate<GameObject>(this.listFxTakeGift[0], base.transform.position, Quaternion.identity);
				UnityEngine.Object.Instantiate<GameObject>(this.listFxTakeGift[1], base.transform.position, Quaternion.identity);
				UnityEngine.Object.Instantiate<GameObject>(this.listFxTakeGift[2], base.transform.position, Quaternion.identity);
				break;
			}
			case EndlessDailyQuestManager.Quest.fly_through_waves0:
				this.valueReward = int.Parse(this.currentReward);
				EndlessModeManager.current.AddBlueStar(this.valueReward);
				UnityEngine.Object.Instantiate<GameObject>(this.listFxTakeGift[2], base.transform.position, Quaternion.identity);
				break;
			case EndlessDailyQuestManager.Quest.defeat_enemies0:
				this.valueReward = int.Parse(this.currentReward);
				CacheGame.AddCoins(this.valueReward);
				UnityEngine.Object.Instantiate<GameObject>(this.listFxTakeGift[0], base.transform.position, Quaternion.identity);
				break;
			case EndlessDailyQuestManager.Quest.defeat_boss0:
				this.valueReward = int.Parse(this.currentReward);
				EndlessModeManager.current.AddBlueStar(this.valueReward);
				UnityEngine.Object.Instantiate<GameObject>(this.listFxTakeGift[2], base.transform.position, Quaternion.identity);
				break;
			case EndlessDailyQuestManager.Quest.purchase_stars_in_mystic_shop0:
				this.valueReward = int.Parse(this.currentReward);
				EndlessModeManager.current.AddBlueStar(this.valueReward);
				UnityEngine.Object.Instantiate<GameObject>(this.listFxTakeGift[2], base.transform.position, Quaternion.identity);
				break;
			case EndlessDailyQuestManager.Quest.use_skill_times0:
				this.valueReward = int.Parse(this.currentReward);
				CacheGame.AddCoins(this.valueReward);
				UnityEngine.Object.Instantiate<GameObject>(this.listFxTakeGift[0], base.transform.position, Quaternion.identity);
				break;
			}
			EndlessDailyQuestManager.current.CheckCompleteQuest();
			if (this.quest != EndlessDailyQuestManager.Quest.complete_quest)
			{
				SaveDataQuestEndless.SetProcessQuest(EndlessDailyQuestManager.Quest.complete_quest, 1);
			}
			if (this.levelQuest < 3)
			{
				SaveDataQuestEndless.SetLevelQuest(this.quest, 1);
			}
			this.LoadData();
			this.ShowQuest();
		}
	}

	public EndlessDailyQuestManager.Quest quest;

	private RectTransform rectTransform;

	public Text txtInforQuest;

	public Text txtBtnCollect;

	public Text txtSliderQuest;

	[HideIf("isQuest0", true)]
	public Text txtReward;

	[ShowIf("isQuest0", true)]
	public Text txtReward0;

	[ShowIf("isQuest0", true)]
	public Text txtReward1;

	[ShowIf("isQuest0", true)]
	public Text txtReward2;

	public Slider sliderQuest;

	public Sprite sprBtnComplete;

	public Sprite sprBtnProcessing;

	public Image imgBtnCollect;

	public GameObject btnCollect;

	public Image[] imgStar;

	public Sprite sprActiveStar;

	public Sprite sprInactiveStar;

	public GameObject finishQuest;

	public GameObject[] listFxTakeGift;

	public int idQuest;

	public int levelQuest;

	public int currentProgessQuest;

	public bool isCompleteQuest;

	private bool isCollected;

	public int currentTarget;

	private int target1;

	private int target2;

	private int target3;

	public string currentReward;

	private string reward1;

	private string reward2;

	private string reward3;

	private int valueReward;

	private string idReward;
}

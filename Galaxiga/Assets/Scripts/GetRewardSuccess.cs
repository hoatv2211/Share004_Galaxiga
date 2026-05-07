using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GetRewardSuccess : MonoBehaviour
{
	public static GetRewardSuccess current
	{
		get
		{
			if (GetRewardSuccess._ins == null)
			{
				GetRewardSuccess._ins = UnityEngine.Object.FindObjectOfType<GetRewardSuccess>();
			}
			return GetRewardSuccess._ins;
		}
	}

	private void Start()
	{
		GetRewardSuccess.listItemRewards = new List<GetRewardSuccess.ItemReward>();
		this.listRewards = new List<GameObject>();
	}

	public void AddItemToView(PrizeManager.RewardType _idReward, int _number)
	{
		if (GetRewardSuccess.listItemRewards == null)
		{
			GetRewardSuccess.listItemRewards = new List<GetRewardSuccess.ItemReward>();
		}
		GetRewardSuccess.ItemReward item = new GetRewardSuccess.ItemReward((int)_idReward, _number);
		GetRewardSuccess.listItemRewards.Add(item);
	}

	public void AddItemToView(PrizeManager.RewardType _idReward, int _number, GameContext.Plane _plane)
	{
		if (GetRewardSuccess.listItemRewards == null)
		{
			GetRewardSuccess.listItemRewards = new List<GetRewardSuccess.ItemReward>();
		}
		GetRewardSuccess.ItemReward item = new GetRewardSuccess.ItemReward((int)_idReward, _number, _plane);
		GetRewardSuccess.listItemRewards.Add(item);
	}

	public void AddItemToView(PrizeManager.RewardType _idReward, GameContext.Plane _plane)
	{
		if (GetRewardSuccess.listItemRewards == null)
		{
			GetRewardSuccess.listItemRewards = new List<GetRewardSuccess.ItemReward>();
		}
		GetRewardSuccess.ItemReward item = new GetRewardSuccess.ItemReward((int)_idReward, _plane);
		GetRewardSuccess.listItemRewards.Add(item);
	}

	public void AddItemToView(PrizeManager.RewardType _idReward, int _number, GameContext.Drone _drone)
	{
		if (GetRewardSuccess.listItemRewards == null)
		{
			GetRewardSuccess.listItemRewards = new List<GetRewardSuccess.ItemReward>();
		}
		GetRewardSuccess.ItemReward item = new GetRewardSuccess.ItemReward((int)_idReward, _number, _drone);
		GetRewardSuccess.listItemRewards.Add(item);
	}

	private void ClearLisrItem()
	{
		GetRewardSuccess.listItemRewards.Clear();
	}

	public void ShowPanelGetRewardSuccess()
	{
		UnityEngine.Debug.Log("Show panel get reward success");
		EscapeManager.Current.AddAction(new Action(this.HidePanel));
		this.coroutineDelay = base.StartCoroutine(GameContext.Delay(3.5f, delegate
		{
			this.HidePanel();
		}));
		this.panelGetRewardSuccess.gameObject.SetActive(true);
		for (int i = 0; i < GetRewardSuccess.listItemRewards.Count; i++)
		{
			GetRewardSuccess.ItemReward itemReward = GetRewardSuccess.listItemRewards[i];
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.arrObjRewardInGames[itemReward.idReward], this.transformRewardsParent);
			this.listRewards.Add(gameObject);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = Vector3.one;
			ItemRewardInGame component = gameObject.GetComponent<ItemRewardInGame>();
			component.numberReward = itemReward.numberReward;
			if (itemReward.idReward == 14 || itemReward.idReward == 16 || itemReward.idReward == 2)
			{
				component._planeID = itemReward.planID;
			}
			else if (itemReward.idReward == 15 || itemReward.idReward == 3)
			{
				component._droneID = itemReward.droneID;
			}
			component.SetView();
		}
		DOTween.Restart("GET_REWARD_SUCCESS", true, -1f);
		DOTween.Play("GET_REWARD_SUCCESS");
	}

	public void HidePanel()
	{
		if (this.coroutineDelay != null)
		{
			base.StopCoroutine(this.coroutineDelay);
		}
		EscapeManager.Current.RemoveAction(new Action(this.HidePanel));
		for (int i = 0; i < this.listRewards.Count; i++)
		{
			UnityEngine.Object.Destroy(this.listRewards[i]);
		}
		this.listRewards.Clear();
		this.ClearLisrItem();
		base.StartCoroutine(GameContext.Delay(0.1f, delegate
		{
			this.panelGetRewardSuccess.SetActive(false);
		}));
	}

	public static GetRewardSuccess _ins;

	public GameObject panelGetRewardSuccess;

	public Transform transformRewardsParent;

	public GameObject[] arrObjRewardInGames;

	public static List<GetRewardSuccess.ItemReward> listItemRewards;

	private List<GameObject> listRewards;

	private Coroutine coroutineDelay;

	public class ItemReward
	{
		public ItemReward(int _idReward, int _numberReward)
		{
			this.idReward = _idReward;
			this.numberReward = _numberReward;
		}

		public ItemReward(int _idReward, int _numberReward, GameContext.Plane _planeID)
		{
			this.idReward = _idReward;
			this.numberReward = _numberReward;
			this.planID = _planeID;
		}

		public ItemReward(int _idReward, GameContext.Plane _planeID)
		{
			this.idReward = _idReward;
			this.planID = _planeID;
		}

		public ItemReward(int _idReward, int _numberReward, GameContext.Drone _droneID)
		{
			this.idReward = _idReward;
			this.numberReward = _numberReward;
			this.droneID = _droneID;
		}

		public int idReward;

		public int numberReward;

		public GameContext.Plane planID;

		public GameContext.Drone droneID;
	}
}

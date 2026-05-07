using System;
using UnityEngine;

public class RewardNewPlane : MonoBehaviour
{
	public void BtnClaim()
	{
		if (NewTutorial.current.currentStepTutorial_UpgradePlane == 1)
		{
			NewTutorial.current.UpgradePlane_Step1();
		}
		else if (NewTutorial.current.currentStepTutorial_EquipDrone == 1)
		{
			NewTutorial.current.EquipDrone_Step1();
		}
		else if (NewTutorial.current.currentStepTutorial_OpenBox == 1)
		{
			NewTutorial.current.OpenBox_Step1();
			this.RewardBox();
		}
		else if (NewTutorial.current.currentStepTutorial_EvovlePlane == 1)
		{
			NewTutorial.current.EvovlePlane_Step1();
			this.RewardCard();
		}
		this.popupReWard.SetActive(false);
	}

	private void RewardPlane()
	{
	}

	private void RewardDrone()
	{
	}

	private void RewardBox()
	{
		PrizeManager.current.ShowBtnFreeTutorial();
	}

	private void RewardCard()
	{
		CacheGame.AddCardAllPlane(80);
		CacheGame.AddGems(100);
	}

	[SerializeField]
	private GameObject popupReWard;
}

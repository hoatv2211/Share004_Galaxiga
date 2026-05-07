using System;
using DG.Tweening;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class ShowInfoPlaneDrone : MonoBehaviour
{
	public static ShowInfoPlaneDrone Current
	{
		get
		{
			if (ShowInfoPlaneDrone._instance == null)
			{
				ShowInfoPlaneDrone._instance = UnityEngine.Object.FindObjectOfType<ShowInfoPlaneDrone>();
			}
			return ShowInfoPlaneDrone._instance;
		}
	}

	public void ShowInfoPlane(GameContext.Plane planeID)
	{
		this.isShowPopupInfoPlane = true;
		this.objSkillPlane.SetActive(true);
		DataGame.Current.GetInfoPlane(planeID);
		DataGame.Current.SetSpriteImagePlane(this.imgPlaneDrone, planeID);
		DataGame.Current.SetImageRank2(this.imgRank, DataGame.InfoPlane.CurrentRank);
		this.textNamePlaneDrone.text = DataGame.InfoPlane.PlaneName;
		this.textPower.text = ScriptLocalization.power + ": " + DataGame.InfoPlane.DPS;
		this.sliderDamage.value = PlaneManager.current.arrPlanes[(int)planeID].sliderDamage.value;
		this.sliderFireRate.value = PlaneManager.current.arrPlanes[(int)planeID].sliderFireRate.value;
		this.sliderSuperDamage.value = PlaneManager.current.arrPlanes[(int)planeID].sliderSuperDamage.value;
		this.textNameSkill.text = DataGame.InfoPlane.NameSkill;
		this.textDescriptionSkill.text = string.Empty + DataGame.InfoPlane.DescriptionSkill;
		this.popupInfo.SetActive(true);
		DOTween.Restart("SHOW_INFO_PLANE_DRONE", true, -1f);
		DOTween.Play("SHOW_INFO_PLANE_DRONE");
		EscapeManager.Current.AddAction(new Action(this.HidePopupInfo));
	}

	public void ShowInfoDrone(GameContext.Drone droneID)
	{
		this.isShowPopupInfoPlane = false;
		this.objSkillDrone.SetActive(true);
		DataGame.Current.GetInfoDrone(droneID);
		DataGame.Current.SetSpriteImageDrone(this.imgPlaneDrone, droneID);
		DataGame.Current.SetImageRank2(this.imgRank, DataGame.InfoDrone.CurrentRank);
		this.textNamePlaneDrone.text = DataGame.InfoDrone.DroneName;
		this.textPower.text = string.Empty + DataGame.InfoDrone.CurrentPower;
		this.textNameSkill.text = DataGame.InfoDrone.NameSkill;
		this.textDescriptionSkill.text = string.Empty + DataGame.InfoDrone.DescriptionSkill;
		DataGame.Current.SetSpriteImageSkillDrone(this.imgSkillDrone, droneID);
		this.popupInfo.SetActive(true);
		DOTween.Restart("SHOW_INFO_PLANE_DRONE", true, -1f);
		DOTween.Play("SHOW_INFO_PLANE_DRONE");
		EscapeManager.Current.AddAction(new Action(this.HidePopupInfo));
	}

	public void HidePopupInfo()
	{
		EscapeManager.Current.RemoveAction(new Action(this.HidePopupInfo));
		DOTween.PlayBackwards("SHOW_INFO_PLANE_DRONE");
		base.StartCoroutine(GameContext.Delay(0.1f, delegate
		{
			if (this.isShowPopupInfoPlane)
			{
				this.objSkillPlane.SetActive(false);
			}
			else
			{
				this.objSkillDrone.SetActive(false);
			}
			this.popupInfo.SetActive(false);
		}));
	}

	private static ShowInfoPlaneDrone _instance;

	public GameObject popupInfo;

	public GameObject objSkillPlane;

	public GameObject objSkillDrone;

	public Image imgPlaneDrone;

	public Image imgRank;

	public Text textNamePlaneDrone;

	public Text textPower;

	public Text textNameSkill;

	public Text textDescriptionSkill;

	public Image imgSkillDrone;

	public Slider sliderDamage;

	public Slider sliderFireRate;

	public Slider sliderSuperDamage;

	private bool isShowPopupInfoPlane;
}

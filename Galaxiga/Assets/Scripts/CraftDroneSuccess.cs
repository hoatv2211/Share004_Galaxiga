using System;
using System.Collections;
using DG.Tweening;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class CraftDroneSuccess : MonoBehaviour
{
	public void ShowPopupCraftDroneSuccess(GameContext.Drone droneID)
	{
		this.SetInfoDrone(droneID - GameContext.Drone.GatlingGun);
	}

	private void SetInfoDrone(int index)
	{
		DataGame dataGame = DataGame.Current;
		this.textNameDrone.text = dataGame.arrDrones[index].DroneName;
		this.textPower.text = ScriptLocalization.power + ": " + dataGame.arrDrones[index].droneData.DPS;
		this.textNameSkill.text = string.Empty + dataGame.arrDrones[index].NameSkill;
		this.textDescriptionSkill.text = dataGame.arrDrones[index].DescriptionSkill;
		dataGame.SetSpriteImageSkillDrone(this.imgSkill, index + GameContext.Drone.GatlingGun);
		dataGame.SetSpriteImageDrone(this.imgDrone, index + GameContext.Drone.GatlingGun);
		this.ShowPopupCraftSuccess();
		DataGame.Current.SetImageRank2(this.imgRank, dataGame.arrDrones[index].BaseRank);
	}

	public void ShowPopupCraftSuccess()
	{
		EscapeManager.Current.AddAction(new Action(this.HidePopupCraftSuccess));
		this.panelFXCraftSuccess.SetActive(true);
		DOTween.Restart("FX_CRAFT_DRONE_SUCCESS", true, -1f);
		DOTween.Play("FX_CRAFT_DRONE_SUCCESS");
		this.popupCraftSuccess.SetActive(true);
		DOTween.Restart("CRAFT_DRONE_SUCCESS", true, -1f);
		DOTween.Play("CRAFT_DRONE_SUCCESS");
	}

	public void HidePopupCraftSuccess()
	{
		EscapeManager.Current.RemoveAction(new Action(this.HidePopupCraftSuccess));
		DOTween.PlayBackwards("CRAFT_DRONE_SUCCESS");
		base.StartCoroutine(this.DelayHide());
	}

	private IEnumerator DelayHide()
	{
		yield return new WaitForSeconds(0.1f);
		this.popupCraftSuccess.SetActive(false);
		yield break;
	}

	public Text textNameDrone;

	public Text textPower;

	public Text textNameSkill;

	public Text textDescriptionSkill;

	public Image imgDrone;

	public Image imgSkill;

	public GameObject popupCraftSuccess;

	public GameObject panelFXCraftSuccess;

	public Image imgRank;
}

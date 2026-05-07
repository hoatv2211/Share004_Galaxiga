using System;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class InfoRankStarship : MonoBehaviour
{
	[EnumAction(typeof(GameContext.Rank))]
	public void ShowPopupInfoRank(int idRank)
	{
		if (this.currentIDRank != idRank)
		{
			this.currentIDRank = idRank;
			string text = string.Empty;
			string text2 = string.Empty;
			switch (idRank)
			{
			case 0:
				text = DataGame.Current.NameSkillPlane(this.planeManager.currentIndexPlane);
				text2 = DataGame.Current.DesSkillPlane(this.planeManager.currentIndexPlane);
				break;
			case 1:
				text = ScriptLocalization.power_up_starship;
				text2 = ScriptLocalization.start_game_with_power_3;
				break;
			case 2:
				text = ScriptLocalization.power_up_starship;
				text2 = ScriptLocalization.start_game_with_power_5;
				break;
			case 3:
				text = ScriptLocalization.increase_damage;
				text2 = ScriptLocalization.increase_damage_15;
				break;
			case 4:
				text = ScriptLocalization.power_up_starship;
				text2 = ScriptLocalization.start_game_with_power_8;
				break;
			case 5:
				text = ScriptLocalization.force_shield;
				text2 = ScriptLocalization.a_force_shield_protect_the_starship;
				break;
			}
			this.textNameAbi.text = text;
			this.textInfoAbi.text = text2;
			this.iconAbi[idRank].SetActive(true);
			DataGame dataGame = DataGame.Current;
			Image img = this.imgRank;
			GameContext.Rank rank = (GameContext.Rank)idRank;
			dataGame.SetImageRank(img, rank.ToString());
			EscapeManager.Current.AddAction(new Action(this.HidePanel));
			this.panelInfoRank.SetActive(true);
		}
		else
		{
			this.HidePanel();
		}
	}

	public void HidePanel()
	{
		this.panelInfoRank.SetActive(false);
		this.iconAbi[this.currentIDRank].SetActive(false);
		this.currentIDRank = -1;
	}

	public GameObject panelInfoRank;

	public Text textNameAbi;

	public Text textInfoAbi;

	public Image imgRank;

	public GameObject[] iconAbi;

	public PlaneManager planeManager;

	private int currentIDRank = -1;
}

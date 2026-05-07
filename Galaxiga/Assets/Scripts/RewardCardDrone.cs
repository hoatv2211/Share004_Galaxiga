using System;
using UnityEngine.UI;

public class RewardCardDrone : RewardView
{
	public override void SetView()
	{
		string text = this.droneName;
		if (text != null)
		{
			if (!(text == "Galing Gun"))
			{
				if (!(text == "Auto Gatling Gun"))
				{
					if (!(text == "Laser"))
					{
						if (!(text == "Nighturge"))
						{
							if (!(text == "God Of Thunder"))
							{
								if (text == "Terigon")
								{
									this._droneID = GameContext.Drone.Terigon;
								}
							}
							else
							{
								this._droneID = GameContext.Drone.GodOfThunder;
							}
						}
						else
						{
							this._droneID = GameContext.Drone.Nighturge;
						}
					}
					else
					{
						this._droneID = GameContext.Drone.Laser;
					}
				}
				else
				{
					this._droneID = GameContext.Drone.AutoGatlingGun;
				}
			}
			else
			{
				this._droneID = GameContext.Drone.GatlingGun;
			}
		}
		DataGame.Current.SetSpriteImageDrone(this.imgDrone, this._droneID);
		DataGame.Current.SetImageRank2(this.bg, DataGame.Current.RankDrone(this._droneID));
		DataGame.Current.AddCardDrone(this._droneID, this.numberCard);
	}

	public void SetData(string _droneName, int _numberCard)
	{
		this.droneName = _droneName;
		this.numberCard = _numberCard;
		this.textNumberReward.text = "x" + this.numberCard;
		this.numberReward = _numberCard;
	}

	public void SetView(GameContext.Drone droneID, int _numberCard)
	{
		this.imgDrone.sprite = DataGame.Current.SpriteDrone(droneID);
		this.textNumberReward.text = "x" + _numberCard;
		DataGame.Current.AddCardDrone(droneID, _numberCard);
		this.numberReward = _numberCard;
		this._droneID = droneID;
	}

	public override void SetView2(GameContext.Drone droneID, int numberCard)
	{
		DataGame.Current.SetSpriteImageDrone(this.imgDrone, droneID);
		this.textNumberReward.text = "x" + numberCard;
		this.numberReward = numberCard;
		this._droneID = droneID;
	}

	public Image bg;

	public Image imgDrone;

	private string droneName;

	private int numberCard;
}

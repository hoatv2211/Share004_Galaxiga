using System;
using UnityEngine.UI;

public class RewardCardPlane : RewardView
{
	public override void SetView()
	{
		string text = this.planeName;
		switch (text)
		{
		case "Bata FD 01":
			this._planeID = GameContext.Plane.BataFD01;
			break;
		case "Sky Wraith":
			this._planeID = GameContext.Plane.SkyWraith;
			break;
		case "Fury Of Ares":
			this._planeID = GameContext.Plane.FuryOfAres;
			break;
		case "Greataxe":
			this._planeID = GameContext.Plane.Greataxe;
			break;
		case "Twilight X":
			this._planeID = GameContext.Plane.TwilightX;
			break;
		case "Warlock":
			this._planeID = GameContext.Plane.Warlock;
			break;
		case "SS Lightning":
			this._planeID = GameContext.Plane.SSLightning;
			break;
		}
		DataGame.Current.SetSpriteImagePlane(this.imgPlane, this._planeID);
		DataGame.Current.AddCardPlane(this._planeID, this.numberCard);
		DataGame.Current.SetImageRank2(this.bg, DataGame.Current.RankPlane(this._planeID));
	}

	public void SetData(string _planeName, int _numberCard)
	{
		this.planeName = _planeName;
		this.numberCard = _numberCard;
		if (this.textNumberReward != null)
		{
			this.textNumberReward.text = "x" + _numberCard;
		}
		this.numberReward = _numberCard;
	}

	public void SetView(GameContext.Plane planeID, int _numberCard)
	{
		this.imgPlane.sprite = DataGame.Current.SpritePlane(planeID);
		if (this.textNumberReward != null)
		{
			this.textNumberReward.text = "x" + _numberCard;
		}
		DataGame.Current.AddCardPlane(planeID, _numberCard);
		this.numberReward = _numberCard;
		this._planeID = planeID;
	}

	public override void SetView2(GameContext.Plane planeID, int _numberCard)
	{
		base.SetView2(planeID, _numberCard);
		this.imgPlane.sprite = DataGame.Current.SpritePlane(planeID);
		this.numberReward = _numberCard;
		this._planeID = planeID;
	}

	public Image bg;

	public Image imgPlane;

	private string planeName;

	private int numberCard;
}

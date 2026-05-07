using System;
using UnityEngine.UI;

public class RewardUnlockPlane : RewardView
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
		DataGame.Current.SetImageRank2(this.bg, DataGame.Current.RankPlane(this._planeID));
		CacheGame.SetUnlockPlane(this._planeID);
	}

	public Image bg;

	public Image imgPlane;

	public string planeName;
}

using System;
using System.Collections.Generic;
using BayatGames.SaveGamePro;
using UnityEngine;

public class SaveDataPlane
{
	public static SaveDataPlane Instance
	{
		get
		{
			if (SaveDataPlane._instance == null)
			{
				SaveDataPlane._instance = new SaveDataPlane();
				SaveDataPlane._instance.Load();
			}
			return SaveDataPlane._instance;
		}
	}

	public void Load()
	{
		if (this.planeInfoContainer == null || this.planeInfoContainer.arrPlanes.Count <= 0)
		{
			this.Init();
		}
		UnityEngine.Debug.Log("number planes " + this.planeInfoContainer.arrPlanes.Count);
	}

	public void Save(PlaneInfoContainer _planeInfoContainer)
	{
		SaveGame.Save<PlaneInfoContainer>("dataPlane.txt", _planeInfoContainer);
	}

	public void Init()
	{
		int length = Enum.GetValues(typeof(GameContext.Plane)).Length;
		this.planeInfoContainer = new PlaneInfoContainer();
		for (int i = 0; i < length; i++)
		{
			PlaneInfo planeInfo = new PlaneInfo();
			planeInfo.planeID = (int)DataGame.Current.arrPlanes[i].planeID;
			planeInfo.name = DataGame.Current.arrPlanes[i].planeData.namePlane;
			planeInfo.level = DataGame.Current.arrPlanes[i].Level;
			planeInfo.rank = DataGame.Current.arrPlanes[i].CurrentRank;
			planeInfo.mainPower = DataGame.Current.arrPlanes[i].CurrentPower;
			planeInfo.subPower = DataGame.Current.arrPlanes[i].CurrentSubPower;
			planeInfo.superPower = DataGame.Current.arrPlanes[i].CurrentSpecialPower;
			planeInfo.isUnlock = DataGame.Current.arrPlanes[i].planeData.IsUnlock;
			planeInfo.isOwned = DataGame.Current.arrPlanes[i].planeData.IsOwned;
			planeInfo.cardPlane = DataGame.Current.arrPlanes[i].CurrentNumberCardToEvolution;
			this.planeInfoContainer.arrPlanes.Add(planeInfo);
		}
	}

	public void AddData(PlaneInfo planeInfo)
	{
		if (!this.planeInfoContainer.arrPlanes.Contains(planeInfo))
		{
			this.planeInfoContainer.arrPlanes.Add(planeInfo);
		}
	}

	public void SetData(List<PlaneInfo> arrPlanes)
	{
		float version = CacheGame.Version;
		int count = arrPlanes.Count;
		for (int i = 0; i < count; i++)
		{
			PlaneInfo planeInfo = arrPlanes[i];
			UnityEngine.Debug.LogWarning(string.Concat(new object[]
			{
				"----------",
				planeInfo.planeID,
				"||",
				planeInfo.name,
				"||",
				planeInfo.level,
				"||",
				planeInfo.rank
			}));
			if (version > 0f && version <= 5.8f)
			{
				if (i == 1)
				{
					if (planeInfo.planeID == 1 && planeInfo.name == "Sky Wraith")
					{
						planeInfo = new PlaneInfo();
						planeInfo = arrPlanes[2];
						planeInfo.planeID = 1;
					}
				}
				else if (i == 2 && planeInfo.planeID == 2 && planeInfo.name == "Fury Of Ares")
				{
					planeInfo = new PlaneInfo();
					planeInfo = arrPlanes[1];
					planeInfo.planeID = 2;
					if (planeInfo.rank == "C")
					{
						planeInfo.rank = "B";
					}
				}
			}
			UnityEngine.Debug.LogWarning(string.Concat(new object[]
			{
				"+++++++++",
				planeInfo.planeID,
				"||",
				planeInfo.name,
				"||",
				planeInfo.level,
				"||",
				planeInfo.rank
			}));
			GameContext.Plane planeID = (GameContext.Plane)planeInfo.planeID;
			GameContext.Rank rank = DataGame.Current.GetRank(planeInfo.rank);
			if (planeInfo.isOwned)
			{
				CacheGame.SetUnlockPlane(planeID);
				CacheGame.SetOwnedPlane(planeID);
			}
			if (planeInfo.isUnlock)
			{
				CacheGame.SetUnlockPlane(planeID);
			}
			DataGame.Current.SetRankPlane(planeID, rank);
			CacheGame.SetLevelPlane(planeID, planeInfo.level);
			DataGame.Current.arrPlanes[i].planeData.CardEvolvePlane = planeInfo.cardPlane;
		}
	}

	public PlaneInfoContainer planeInfoContainer = new PlaneInfoContainer();

	private static SaveDataPlane _instance;

	private const string DATA_PLANE = "dataPlane.txt";
}

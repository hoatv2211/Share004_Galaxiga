using System;
using System.Collections.Generic;
using BayatGames.SaveGamePro;
using UnityEngine;

public class SaveDataDrone
{
	public static SaveDataDrone Instance
	{
		get
		{
			if (SaveDataDrone._instance == null)
			{
				SaveDataDrone._instance = new SaveDataDrone();
				SaveDataDrone._instance.Load();
			}
			return SaveDataDrone._instance;
		}
	}

	public void Load()
	{
		if (this.droneInfoContainer == null || this.droneInfoContainer.arrDrones.Count <= 0)
		{
			this.Init();
		}
		UnityEngine.Debug.Log("number Drones " + this.droneInfoContainer.arrDrones.Count);
	}

	public void Save(DroneInfoContainer _droneInfoContainer)
	{
		SaveGame.Save<DroneInfoContainer>("dataDrone.txt", _droneInfoContainer);
	}

	public void Init()
	{
		int length = Enum.GetValues(typeof(GameContext.Drone)).Length;
		this.droneInfoContainer = new DroneInfoContainer();
		for (int i = 0; i < length - 1; i++)
		{
			this.AddData(new DroneInfo
			{
				droneID = (int)DataGame.Current.arrDrones[i].droneID,
				name = DataGame.Current.arrDrones[i].droneData.NameDrone,
				level = DataGame.Current.arrDrones[i].droneData.Level,
				rank = DataGame.Current.arrDrones[i].CurrentRank,
				mainPower = DataGame.Current.arrDrones[i].CurrentPower,
				superPower = DataGame.Current.arrDrones[i].CurrentSpecialPower,
				isOwned = DataGame.Current.arrDrones[i].droneData.IsOwned,
				cardDrone = DataGame.Current.arrDrones[i].droneData.CardEvolveDrone
			});
		}
	}

	public void AddData(DroneInfo droneInfo)
	{
		if (!this.droneInfoContainer.arrDrones.Contains(droneInfo))
		{
			this.droneInfoContainer.arrDrones.Add(droneInfo);
		}
	}

	public void SetData(List<DroneInfo> arrDrone)
	{
		int count = arrDrone.Count;
		for (int i = 0; i < count; i++)
		{
			DroneInfo droneInfo = arrDrone[i];
			GameContext.Drone droneID = (GameContext.Drone)droneInfo.droneID;
			GameContext.Rank rank = DataGame.Current.GetRank(droneInfo.rank);
			if (droneInfo.isOwned)
			{
				CacheGame.SetOwnedDrone(droneID);
			}
			DataGame.Current.SetRankDrone(droneID, rank);
			CacheGame.SetLevelDrone(droneID, droneInfo.level);
			DataGame.Current.arrDrones[i].droneData.CardEvolveDrone = droneInfo.cardDrone;
		}
	}

	public DroneInfoContainer droneInfoContainer;

	private static SaveDataDrone _instance;

	private const string DATA_DRONE = "dataDrone.txt";
}

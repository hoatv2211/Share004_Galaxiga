using System;
using UnityEngine;
using UnityEngine.AI;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_NavMeshObstacle : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(NavMeshObstacle);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			NavMeshObstacle navMeshObstacle = (NavMeshObstacle)value;
			writer.WriteProperty<float>("height", navMeshObstacle.height);
			writer.WriteProperty<float>("radius", navMeshObstacle.radius);
			writer.WriteProperty<Vector3>("velocity", navMeshObstacle.velocity);
			writer.WriteProperty<bool>("carving", navMeshObstacle.carving);
			writer.WriteProperty<bool>("carveOnlyStationary", navMeshObstacle.carveOnlyStationary);
			writer.WriteProperty<float>("carvingMoveThreshold", navMeshObstacle.carvingMoveThreshold);
			writer.WriteProperty<float>("carvingTimeToStationary", navMeshObstacle.carvingTimeToStationary);
			writer.WriteProperty<NavMeshObstacleShape>("shape", navMeshObstacle.shape);
			writer.WriteProperty<Vector3>("center", navMeshObstacle.center);
			writer.WriteProperty<Vector3>("size", navMeshObstacle.size);
			writer.WriteProperty<bool>("enabled", navMeshObstacle.enabled);
			writer.WriteProperty<string>("tag", navMeshObstacle.tag);
			writer.WriteProperty<string>("name", navMeshObstacle.name);
			writer.WriteProperty<HideFlags>("hideFlags", navMeshObstacle.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			NavMeshObstacle navMeshObstacle = SaveGameType.CreateComponent<NavMeshObstacle>();
			this.ReadInto(navMeshObstacle, reader);
			return navMeshObstacle;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			NavMeshObstacle navMeshObstacle = (NavMeshObstacle)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "height":
					navMeshObstacle.height = reader.ReadProperty<float>();
					break;
				case "radius":
					navMeshObstacle.radius = reader.ReadProperty<float>();
					break;
				case "velocity":
					navMeshObstacle.velocity = reader.ReadProperty<Vector3>();
					break;
				case "carving":
					navMeshObstacle.carving = reader.ReadProperty<bool>();
					break;
				case "carveOnlyStationary":
					navMeshObstacle.carveOnlyStationary = reader.ReadProperty<bool>();
					break;
				case "carvingMoveThreshold":
					navMeshObstacle.carvingMoveThreshold = reader.ReadProperty<float>();
					break;
				case "carvingTimeToStationary":
					navMeshObstacle.carvingTimeToStationary = reader.ReadProperty<float>();
					break;
				case "shape":
					navMeshObstacle.shape = reader.ReadProperty<NavMeshObstacleShape>();
					break;
				case "center":
					navMeshObstacle.center = reader.ReadProperty<Vector3>();
					break;
				case "size":
					navMeshObstacle.size = reader.ReadProperty<Vector3>();
					break;
				case "enabled":
					navMeshObstacle.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					navMeshObstacle.tag = reader.ReadProperty<string>();
					break;
				case "name":
					navMeshObstacle.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					navMeshObstacle.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}

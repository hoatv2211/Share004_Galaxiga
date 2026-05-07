using System;
using UnityEngine;
using UnityEngine.AI;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_NavMeshAgent : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(NavMeshAgent);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			NavMeshAgent navMeshAgent = (NavMeshAgent)value;
			writer.WriteProperty<Vector3>("destination", navMeshAgent.destination);
			writer.WriteProperty<float>("stoppingDistance", navMeshAgent.stoppingDistance);
			writer.WriteProperty<Vector3>("velocity", navMeshAgent.velocity);
			writer.WriteProperty<Vector3>("nextPosition", navMeshAgent.nextPosition);
			writer.WriteProperty<float>("baseOffset", navMeshAgent.baseOffset);
			writer.WriteProperty<bool>("autoTraverseOffMeshLink", navMeshAgent.autoTraverseOffMeshLink);
			writer.WriteProperty<bool>("autoBraking", navMeshAgent.autoBraking);
			writer.WriteProperty<bool>("autoRepath", navMeshAgent.autoRepath);
			writer.WriteProperty<bool>("isStopped", navMeshAgent.isStopped);
			writer.WriteProperty<NavMeshPath>("path", navMeshAgent.path);
			writer.WriteProperty<int>("agentTypeID", navMeshAgent.agentTypeID);
			writer.WriteProperty<int>("areaMask", navMeshAgent.areaMask);
			writer.WriteProperty<float>("speed", navMeshAgent.speed);
			writer.WriteProperty<float>("angularSpeed", navMeshAgent.angularSpeed);
			writer.WriteProperty<float>("acceleration", navMeshAgent.acceleration);
			writer.WriteProperty<bool>("updatePosition", navMeshAgent.updatePosition);
			writer.WriteProperty<bool>("updateRotation", navMeshAgent.updateRotation);
			writer.WriteProperty<bool>("updateUpAxis", navMeshAgent.updateUpAxis);
			writer.WriteProperty<float>("radius", navMeshAgent.radius);
			writer.WriteProperty<float>("height", navMeshAgent.height);
			writer.WriteProperty<ObstacleAvoidanceType>("obstacleAvoidanceType", navMeshAgent.obstacleAvoidanceType);
			writer.WriteProperty<int>("avoidancePriority", navMeshAgent.avoidancePriority);
			writer.WriteProperty<bool>("enabled", navMeshAgent.enabled);
			writer.WriteProperty<string>("tag", navMeshAgent.tag);
			writer.WriteProperty<string>("name", navMeshAgent.name);
			writer.WriteProperty<HideFlags>("hideFlags", navMeshAgent.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			NavMeshAgent navMeshAgent = SaveGameType.CreateComponent<NavMeshAgent>();
			this.ReadInto(navMeshAgent, reader);
			return navMeshAgent;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			NavMeshAgent navMeshAgent = (NavMeshAgent)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "destination":
					navMeshAgent.destination = reader.ReadProperty<Vector3>();
					break;
				case "stoppingDistance":
					navMeshAgent.stoppingDistance = reader.ReadProperty<float>();
					break;
				case "velocity":
					navMeshAgent.velocity = reader.ReadProperty<Vector3>();
					break;
				case "nextPosition":
					navMeshAgent.nextPosition = reader.ReadProperty<Vector3>();
					break;
				case "baseOffset":
					navMeshAgent.baseOffset = reader.ReadProperty<float>();
					break;
				case "autoTraverseOffMeshLink":
					navMeshAgent.autoTraverseOffMeshLink = reader.ReadProperty<bool>();
					break;
				case "autoBraking":
					navMeshAgent.autoBraking = reader.ReadProperty<bool>();
					break;
				case "autoRepath":
					navMeshAgent.autoRepath = reader.ReadProperty<bool>();
					break;
				case "isStopped":
					navMeshAgent.isStopped = reader.ReadProperty<bool>();
					break;
				case "path":
					navMeshAgent.path = reader.ReadProperty<NavMeshPath>();
					break;
				case "agentTypeID":
					navMeshAgent.agentTypeID = reader.ReadProperty<int>();
					break;
				case "areaMask":
					navMeshAgent.areaMask = reader.ReadProperty<int>();
					break;
				case "speed":
					navMeshAgent.speed = reader.ReadProperty<float>();
					break;
				case "angularSpeed":
					navMeshAgent.angularSpeed = reader.ReadProperty<float>();
					break;
				case "acceleration":
					navMeshAgent.acceleration = reader.ReadProperty<float>();
					break;
				case "updatePosition":
					navMeshAgent.updatePosition = reader.ReadProperty<bool>();
					break;
				case "updateRotation":
					navMeshAgent.updateRotation = reader.ReadProperty<bool>();
					break;
				case "updateUpAxis":
					navMeshAgent.updateUpAxis = reader.ReadProperty<bool>();
					break;
				case "radius":
					navMeshAgent.radius = reader.ReadProperty<float>();
					break;
				case "height":
					navMeshAgent.height = reader.ReadProperty<float>();
					break;
				case "obstacleAvoidanceType":
					navMeshAgent.obstacleAvoidanceType = reader.ReadProperty<ObstacleAvoidanceType>();
					break;
				case "avoidancePriority":
					navMeshAgent.avoidancePriority = reader.ReadProperty<int>();
					break;
				case "enabled":
					navMeshAgent.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					navMeshAgent.tag = reader.ReadProperty<string>();
					break;
				case "name":
					navMeshAgent.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					navMeshAgent.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}

using System;
using UnityEngine;
using UnityEngine.AI;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_NavMeshHit : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(NavMeshHit);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			NavMeshHit navMeshHit = (NavMeshHit)value;
			writer.WriteProperty<Vector3>("position", navMeshHit.position);
			writer.WriteProperty<Vector3>("normal", navMeshHit.normal);
			writer.WriteProperty<float>("distance", navMeshHit.distance);
			writer.WriteProperty<int>("mask", navMeshHit.mask);
			writer.WriteProperty<bool>("hit", navMeshHit.hit);
		}

		public override object Read(ISaveGameReader reader)
		{
			NavMeshHit navMeshHit = default(NavMeshHit);
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "position"))
					{
						if (!(text == "normal"))
						{
							if (!(text == "distance"))
							{
								if (!(text == "mask"))
								{
									if (text == "hit")
									{
										navMeshHit.hit = reader.ReadProperty<bool>();
									}
								}
								else
								{
									navMeshHit.mask = reader.ReadProperty<int>();
								}
							}
							else
							{
								navMeshHit.distance = reader.ReadProperty<float>();
							}
						}
						else
						{
							navMeshHit.normal = reader.ReadProperty<Vector3>();
						}
					}
					else
					{
						navMeshHit.position = reader.ReadProperty<Vector3>();
					}
				}
			}
			return navMeshHit;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}

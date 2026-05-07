using System;
using UnityEngine;
using UnityEngine.AI;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_OffMeshLink : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(OffMeshLink);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			OffMeshLink offMeshLink = (OffMeshLink)value;
			writer.WriteProperty<bool>("activated", offMeshLink.activated);
			writer.WriteProperty<float>("costOverride", offMeshLink.costOverride);
			writer.WriteProperty<bool>("biDirectional", offMeshLink.biDirectional);
			writer.WriteProperty<int>("area", offMeshLink.area);
			writer.WriteProperty<bool>("autoUpdatePositions", offMeshLink.autoUpdatePositions);
			writer.WriteProperty<Transform>("startTransform", offMeshLink.startTransform);
			writer.WriteProperty<Transform>("endTransform", offMeshLink.endTransform);
			writer.WriteProperty<bool>("enabled", offMeshLink.enabled);
			writer.WriteProperty<string>("tag", offMeshLink.tag);
			writer.WriteProperty<string>("name", offMeshLink.name);
			writer.WriteProperty<HideFlags>("hideFlags", offMeshLink.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			OffMeshLink offMeshLink = SaveGameType.CreateComponent<OffMeshLink>();
			this.ReadInto(offMeshLink, reader);
			return offMeshLink;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			OffMeshLink offMeshLink = (OffMeshLink)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "activated":
					offMeshLink.activated = reader.ReadProperty<bool>();
					break;
				case "costOverride":
					offMeshLink.costOverride = reader.ReadProperty<float>();
					break;
				case "biDirectional":
					offMeshLink.biDirectional = reader.ReadProperty<bool>();
					break;
				case "area":
					offMeshLink.area = reader.ReadProperty<int>();
					break;
				case "autoUpdatePositions":
					offMeshLink.autoUpdatePositions = reader.ReadProperty<bool>();
					break;
				case "startTransform":
					if (offMeshLink.startTransform == null)
					{
						offMeshLink.startTransform = reader.ReadProperty<Transform>();
					}
					else
					{
						reader.ReadIntoProperty<Transform>(offMeshLink.startTransform);
					}
					break;
				case "endTransform":
					if (offMeshLink.endTransform == null)
					{
						offMeshLink.endTransform = reader.ReadProperty<Transform>();
					}
					else
					{
						reader.ReadIntoProperty<Transform>(offMeshLink.endTransform);
					}
					break;
				case "enabled":
					offMeshLink.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					offMeshLink.tag = reader.ReadProperty<string>();
					break;
				case "name":
					offMeshLink.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					offMeshLink.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}

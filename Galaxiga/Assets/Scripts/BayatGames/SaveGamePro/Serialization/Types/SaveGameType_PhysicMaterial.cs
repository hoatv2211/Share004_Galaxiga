using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_PhysicMaterial : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(PhysicsMaterial);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			PhysicsMaterial physicMaterial = (PhysicsMaterial)value;
			writer.WriteProperty<float>("dynamicFriction", physicMaterial.dynamicFriction);
			writer.WriteProperty<float>("staticFriction", physicMaterial.staticFriction);
			writer.WriteProperty<float>("bounciness", physicMaterial.bounciness);
			writer.WriteProperty<PhysicsMaterialCombine>("frictionCombine", physicMaterial.frictionCombine);
			writer.WriteProperty<PhysicsMaterialCombine>("bounceCombine", physicMaterial.bounceCombine);
			writer.WriteProperty<string>("name", physicMaterial.name);
			writer.WriteProperty<HideFlags>("hideFlags", physicMaterial.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			PhysicsMaterial physicMaterial = new PhysicsMaterial();
			this.ReadInto(physicMaterial, reader);
			return physicMaterial;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			PhysicsMaterial physicMaterial = (PhysicsMaterial)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "dynamicFriction":
					physicMaterial.dynamicFriction = reader.ReadProperty<float>();
					break;
				case "staticFriction":
					physicMaterial.staticFriction = reader.ReadProperty<float>();
					break;
				case "bounciness":
					physicMaterial.bounciness = reader.ReadProperty<float>();
					break;
				case "frictionCombine":
					physicMaterial.frictionCombine = reader.ReadProperty<PhysicsMaterialCombine>();
					break;
				case "bounceCombine":
					physicMaterial.bounceCombine = reader.ReadProperty<PhysicsMaterialCombine>();
					break;
				case "name":
					physicMaterial.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					physicMaterial.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}

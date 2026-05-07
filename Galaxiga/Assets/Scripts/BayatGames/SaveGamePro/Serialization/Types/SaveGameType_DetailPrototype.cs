using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_DetailPrototype : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(DetailPrototype);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			DetailPrototype detailPrototype = (DetailPrototype)value;
			writer.WriteProperty<GameObject>("prototype", detailPrototype.prototype);
			writer.WriteProperty<Texture2D>("prototypeTexture", detailPrototype.prototypeTexture);
			writer.WriteProperty<float>("minWidth", detailPrototype.minWidth);
			writer.WriteProperty<float>("maxWidth", detailPrototype.maxWidth);
			writer.WriteProperty<float>("minHeight", detailPrototype.minHeight);
			writer.WriteProperty<float>("maxHeight", detailPrototype.maxHeight);
			writer.WriteProperty<float>("noiseSpread", detailPrototype.noiseSpread);
			writer.WriteProperty<float>("bendFactor", detailPrototype.bendFactor);
			writer.WriteProperty<Color>("healthyColor", detailPrototype.healthyColor);
			writer.WriteProperty<Color>("dryColor", detailPrototype.dryColor);
			writer.WriteProperty<DetailRenderMode>("renderMode", detailPrototype.renderMode);
			writer.WriteProperty<bool>("usePrototypeMesh", detailPrototype.usePrototypeMesh);
		}

		public override object Read(ISaveGameReader reader)
		{
			DetailPrototype detailPrototype = new DetailPrototype();
			this.ReadInto(detailPrototype, reader);
			return detailPrototype;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			DetailPrototype detailPrototype = (DetailPrototype)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "prototype":
					if (detailPrototype.prototype == null)
					{
						detailPrototype.prototype = reader.ReadProperty<GameObject>();
					}
					else
					{
						reader.ReadIntoProperty<GameObject>(detailPrototype.prototype);
					}
					break;
				case "prototypeTexture":
					if (detailPrototype.prototypeTexture == null)
					{
						detailPrototype.prototypeTexture = reader.ReadProperty<Texture2D>();
					}
					else
					{
						reader.ReadIntoProperty<Texture2D>(detailPrototype.prototypeTexture);
					}
					break;
				case "minWidth":
					detailPrototype.minWidth = reader.ReadProperty<float>();
					break;
				case "maxWidth":
					detailPrototype.maxWidth = reader.ReadProperty<float>();
					break;
				case "minHeight":
					detailPrototype.minHeight = reader.ReadProperty<float>();
					break;
				case "maxHeight":
					detailPrototype.maxHeight = reader.ReadProperty<float>();
					break;
				case "noiseSpread":
					detailPrototype.noiseSpread = reader.ReadProperty<float>();
					break;
				case "bendFactor":
					detailPrototype.bendFactor = reader.ReadProperty<float>();
					break;
				case "healthyColor":
					detailPrototype.healthyColor = reader.ReadProperty<Color>();
					break;
				case "dryColor":
					detailPrototype.dryColor = reader.ReadProperty<Color>();
					break;
				case "renderMode":
					detailPrototype.renderMode = reader.ReadProperty<DetailRenderMode>();
					break;
				case "usePrototypeMesh":
					detailPrototype.usePrototypeMesh = reader.ReadProperty<bool>();
					break;
				}
			}
		}
	}
}

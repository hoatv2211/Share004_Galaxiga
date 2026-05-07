using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Bounds : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Bounds);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Bounds bounds = (Bounds)value;
			writer.WriteProperty<Vector3>("center", bounds.center);
			writer.WriteProperty<Vector3>("size", bounds.size);
			writer.WriteProperty<Vector3>("extents", bounds.extents);
			writer.WriteProperty<Vector3>("min", bounds.min);
			writer.WriteProperty<Vector3>("max", bounds.max);
		}

		public override object Read(ISaveGameReader reader)
		{
			Bounds bounds = default(Bounds);
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "center"))
					{
						if (!(text == "size"))
						{
							if (!(text == "extents"))
							{
								if (!(text == "min"))
								{
									if (text == "max")
									{
										bounds.max = reader.ReadProperty<Vector3>();
									}
								}
								else
								{
									bounds.min = reader.ReadProperty<Vector3>();
								}
							}
							else
							{
								bounds.extents = reader.ReadProperty<Vector3>();
							}
						}
						else
						{
							bounds.size = reader.ReadProperty<Vector3>();
						}
					}
					else
					{
						bounds.center = reader.ReadProperty<Vector3>();
					}
				}
			}
			return bounds;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}

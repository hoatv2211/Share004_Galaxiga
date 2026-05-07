using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Matrix4x4 : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Matrix4x4);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Matrix4x4 matrix4x = (Matrix4x4)value;
			writer.WriteProperty<float>("m00", matrix4x.m00);
			writer.WriteProperty<float>("m10", matrix4x.m10);
			writer.WriteProperty<float>("m20", matrix4x.m20);
			writer.WriteProperty<float>("m30", matrix4x.m30);
			writer.WriteProperty<float>("m01", matrix4x.m01);
			writer.WriteProperty<float>("m11", matrix4x.m11);
			writer.WriteProperty<float>("m21", matrix4x.m21);
			writer.WriteProperty<float>("m31", matrix4x.m31);
			writer.WriteProperty<float>("m02", matrix4x.m02);
			writer.WriteProperty<float>("m12", matrix4x.m12);
			writer.WriteProperty<float>("m22", matrix4x.m22);
			writer.WriteProperty<float>("m32", matrix4x.m32);
			writer.WriteProperty<float>("m03", matrix4x.m03);
			writer.WriteProperty<float>("m13", matrix4x.m13);
			writer.WriteProperty<float>("m23", matrix4x.m23);
			writer.WriteProperty<float>("m33", matrix4x.m33);
		}

		public override object Read(ISaveGameReader reader)
		{
			Matrix4x4 matrix4x = default(Matrix4x4);
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "m00":
					matrix4x.m00 = reader.ReadProperty<float>();
					break;
				case "m10":
					matrix4x.m10 = reader.ReadProperty<float>();
					break;
				case "m20":
					matrix4x.m20 = reader.ReadProperty<float>();
					break;
				case "m30":
					matrix4x.m30 = reader.ReadProperty<float>();
					break;
				case "m01":
					matrix4x.m01 = reader.ReadProperty<float>();
					break;
				case "m11":
					matrix4x.m11 = reader.ReadProperty<float>();
					break;
				case "m21":
					matrix4x.m21 = reader.ReadProperty<float>();
					break;
				case "m31":
					matrix4x.m31 = reader.ReadProperty<float>();
					break;
				case "m02":
					matrix4x.m02 = reader.ReadProperty<float>();
					break;
				case "m12":
					matrix4x.m12 = reader.ReadProperty<float>();
					break;
				case "m22":
					matrix4x.m22 = reader.ReadProperty<float>();
					break;
				case "m32":
					matrix4x.m32 = reader.ReadProperty<float>();
					break;
				case "m03":
					matrix4x.m03 = reader.ReadProperty<float>();
					break;
				case "m13":
					matrix4x.m13 = reader.ReadProperty<float>();
					break;
				case "m23":
					matrix4x.m23 = reader.ReadProperty<float>();
					break;
				case "m33":
					matrix4x.m33 = reader.ReadProperty<float>();
					break;
				}
			}
			return matrix4x;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}

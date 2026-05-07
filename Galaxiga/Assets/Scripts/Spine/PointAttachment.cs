using System;

namespace Spine
{
	public class PointAttachment : Attachment
	{
		public PointAttachment(string name) : base(name)
		{
		}

		public float X
		{
			get
			{
				return this.x;
			}
			set
			{
				this.x = value;
			}
		}

		public float Y
		{
			get
			{
				return this.y;
			}
			set
			{
				this.y = value;
			}
		}

		public float Rotation
		{
			get
			{
				return this.rotation;
			}
			set
			{
				this.rotation = value;
			}
		}

		public void ComputeWorldPosition(Bone bone, out float ox, out float oy)
		{
			bone.LocalToWorld(this.x, this.y, out ox, out oy);
		}

		public float ComputeWorldRotation(Bone bone)
		{
			float num = MathUtils.CosDeg(this.rotation);
			float num2 = MathUtils.SinDeg(this.rotation);
			float num3 = num * bone.a + num2 * bone.b;
			float num4 = num * bone.c + num2 * bone.d;
			return MathUtils.Atan2(num4, num3) * 57.2957764f;
		}

		internal float x;

		internal float y;

		internal float rotation;
	}
}

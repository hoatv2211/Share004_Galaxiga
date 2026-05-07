using System;

namespace Spine
{
	public class Bone : IUpdatable
	{
		public Bone(BoneData data, Skeleton skeleton, Bone parent)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data", "data cannot be null.");
			}
			if (skeleton == null)
			{
				throw new ArgumentNullException("skeleton", "skeleton cannot be null.");
			}
			this.data = data;
			this.skeleton = skeleton;
			this.parent = parent;
			this.SetToSetupPose();
		}

		public BoneData Data
		{
			get
			{
				return this.data;
			}
		}

		public Skeleton Skeleton
		{
			get
			{
				return this.skeleton;
			}
		}

		public Bone Parent
		{
			get
			{
				return this.parent;
			}
		}

		public ExposedList<Bone> Children
		{
			get
			{
				return this.children;
			}
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

		public float ScaleX
		{
			get
			{
				return this.scaleX;
			}
			set
			{
				this.scaleX = value;
			}
		}

		public float ScaleY
		{
			get
			{
				return this.scaleY;
			}
			set
			{
				this.scaleY = value;
			}
		}

		public float ShearX
		{
			get
			{
				return this.shearX;
			}
			set
			{
				this.shearX = value;
			}
		}

		public float ShearY
		{
			get
			{
				return this.shearY;
			}
			set
			{
				this.shearY = value;
			}
		}

		public float AppliedRotation
		{
			get
			{
				return this.arotation;
			}
			set
			{
				this.arotation = value;
			}
		}

		public float AX
		{
			get
			{
				return this.ax;
			}
			set
			{
				this.ax = value;
			}
		}

		public float AY
		{
			get
			{
				return this.ay;
			}
			set
			{
				this.ay = value;
			}
		}

		public float AScaleX
		{
			get
			{
				return this.ascaleX;
			}
			set
			{
				this.ascaleX = value;
			}
		}

		public float AScaleY
		{
			get
			{
				return this.ascaleY;
			}
			set
			{
				this.ascaleY = value;
			}
		}

		public float AShearX
		{
			get
			{
				return this.ashearX;
			}
			set
			{
				this.ashearX = value;
			}
		}

		public float AShearY
		{
			get
			{
				return this.ashearY;
			}
			set
			{
				this.ashearY = value;
			}
		}

		public float A
		{
			get
			{
				return this.a;
			}
		}

		public float B
		{
			get
			{
				return this.b;
			}
		}

		public float C
		{
			get
			{
				return this.c;
			}
		}

		public float D
		{
			get
			{
				return this.d;
			}
		}

		public float WorldX
		{
			get
			{
				return this.worldX;
			}
		}

		public float WorldY
		{
			get
			{
				return this.worldY;
			}
		}

		public float WorldRotationX
		{
			get
			{
				return MathUtils.Atan2(this.c, this.a) * 57.2957764f;
			}
		}

		public float WorldRotationY
		{
			get
			{
				return MathUtils.Atan2(this.d, this.b) * 57.2957764f;
			}
		}

		public float WorldScaleX
		{
			get
			{
				return (float)Math.Sqrt((double)(this.a * this.a + this.c * this.c));
			}
		}

		public float WorldScaleY
		{
			get
			{
				return (float)Math.Sqrt((double)(this.b * this.b + this.d * this.d));
			}
		}

		public void Update()
		{
			this.UpdateWorldTransform(this.x, this.y, this.rotation, this.scaleX, this.scaleY, this.shearX, this.shearY);
		}

		public void UpdateWorldTransform()
		{
			this.UpdateWorldTransform(this.x, this.y, this.rotation, this.scaleX, this.scaleY, this.shearX, this.shearY);
		}

		public void UpdateWorldTransform(float x, float y, float rotation, float scaleX, float scaleY, float shearX, float shearY)
		{
			this.ax = x;
			this.ay = y;
			this.arotation = rotation;
			this.ascaleX = scaleX;
			this.ascaleY = scaleY;
			this.ashearX = shearX;
			this.ashearY = shearY;
			this.appliedValid = true;
			Skeleton skeleton = this.skeleton;
			Bone bone = this.parent;
			if (bone == null)
			{
				float degrees = rotation + 90f + shearY;
				float num = MathUtils.CosDeg(rotation + shearX) * scaleX;
				float num2 = MathUtils.CosDeg(degrees) * scaleY;
				float num3 = MathUtils.SinDeg(rotation + shearX) * scaleX;
				float num4 = MathUtils.SinDeg(degrees) * scaleY;
				if (skeleton.flipX)
				{
					x = -x;
					num = -num;
					num2 = -num2;
				}
				if (skeleton.flipY != Bone.yDown)
				{
					y = -y;
					num3 = -num3;
					num4 = -num4;
				}
				this.a = num;
				this.b = num2;
				this.c = num3;
				this.d = num4;
				this.worldX = x + skeleton.x;
				this.worldY = y + skeleton.y;
				return;
			}
			float num5 = bone.a;
			float num6 = bone.b;
			float num7 = bone.c;
			float num8 = bone.d;
			this.worldX = num5 * x + num6 * y + bone.worldX;
			this.worldY = num7 * x + num8 * y + bone.worldY;
			switch (this.data.transformMode)
			{
			case TransformMode.Normal:
			{
				float degrees2 = rotation + 90f + shearY;
				float num9 = MathUtils.CosDeg(rotation + shearX) * scaleX;
				float num10 = MathUtils.CosDeg(degrees2) * scaleY;
				float num11 = MathUtils.SinDeg(rotation + shearX) * scaleX;
				float num12 = MathUtils.SinDeg(degrees2) * scaleY;
				this.a = num5 * num9 + num6 * num11;
				this.b = num5 * num10 + num6 * num12;
				this.c = num7 * num9 + num8 * num11;
				this.d = num7 * num10 + num8 * num12;
				return;
			}
			case TransformMode.NoRotationOrReflection:
			{
				float num13 = num5 * num5 + num7 * num7;
				float num14;
				if (num13 > 0.0001f)
				{
					num13 = Math.Abs(num5 * num8 - num6 * num7) / num13;
					num6 = num7 * num13;
					num8 = num5 * num13;
					num14 = MathUtils.Atan2(num7, num5) * 57.2957764f;
				}
				else
				{
					num5 = 0f;
					num7 = 0f;
					num14 = 90f - MathUtils.Atan2(num8, num6) * 57.2957764f;
				}
				float degrees3 = rotation + shearX - num14;
				float degrees4 = rotation + shearY - num14 + 90f;
				float num15 = MathUtils.CosDeg(degrees3) * scaleX;
				float num16 = MathUtils.CosDeg(degrees4) * scaleY;
				float num17 = MathUtils.SinDeg(degrees3) * scaleX;
				float num18 = MathUtils.SinDeg(degrees4) * scaleY;
				this.a = num5 * num15 - num6 * num17;
				this.b = num5 * num16 - num6 * num18;
				this.c = num7 * num15 + num8 * num17;
				this.d = num7 * num16 + num8 * num18;
				break;
			}
			case TransformMode.NoScale:
			case TransformMode.NoScaleOrReflection:
			{
				float num19 = MathUtils.CosDeg(rotation);
				float num20 = MathUtils.SinDeg(rotation);
				float num21 = num5 * num19 + num6 * num20;
				float num22 = num7 * num19 + num8 * num20;
				float num23 = (float)Math.Sqrt((double)(num21 * num21 + num22 * num22));
				if (num23 > 1E-05f)
				{
					num23 = 1f / num23;
				}
				num21 *= num23;
				num22 *= num23;
				num23 = (float)Math.Sqrt((double)(num21 * num21 + num22 * num22));
				float radians = 1.57079637f + MathUtils.Atan2(num22, num21);
				float num24 = MathUtils.Cos(radians) * num23;
				float num25 = MathUtils.Sin(radians) * num23;
				float num26 = MathUtils.CosDeg(shearX) * scaleX;
				float num27 = MathUtils.CosDeg(90f + shearY) * scaleY;
				float num28 = MathUtils.SinDeg(shearX) * scaleX;
				float num29 = MathUtils.SinDeg(90f + shearY) * scaleY;
				if ((this.data.transformMode == TransformMode.NoScaleOrReflection) ? (skeleton.flipX != skeleton.flipY) : (num5 * num8 - num6 * num7 < 0f))
				{
					num24 = -num24;
					num25 = -num25;
				}
				this.a = num21 * num26 + num24 * num28;
				this.b = num21 * num27 + num24 * num29;
				this.c = num22 * num26 + num25 * num28;
				this.d = num22 * num27 + num25 * num29;
				return;
			}
			case TransformMode.OnlyTranslation:
			{
				float degrees5 = rotation + 90f + shearY;
				this.a = MathUtils.CosDeg(rotation + shearX) * scaleX;
				this.b = MathUtils.CosDeg(degrees5) * scaleY;
				this.c = MathUtils.SinDeg(rotation + shearX) * scaleX;
				this.d = MathUtils.SinDeg(degrees5) * scaleY;
				break;
			}
			}
			if (skeleton.flipX)
			{
				this.a = -this.a;
				this.b = -this.b;
			}
			if (skeleton.flipY != Bone.yDown)
			{
				this.c = -this.c;
				this.d = -this.d;
			}
		}

		public void SetToSetupPose()
		{
			BoneData boneData = this.data;
			this.x = boneData.x;
			this.y = boneData.y;
			this.rotation = boneData.rotation;
			this.scaleX = boneData.scaleX;
			this.scaleY = boneData.scaleY;
			this.shearX = boneData.shearX;
			this.shearY = boneData.shearY;
		}

		internal void UpdateAppliedTransform()
		{
			this.appliedValid = true;
			Bone bone = this.parent;
			if (bone == null)
			{
				this.ax = this.worldX;
				this.ay = this.worldY;
				this.arotation = MathUtils.Atan2(this.c, this.a) * 57.2957764f;
				this.ascaleX = (float)Math.Sqrt((double)(this.a * this.a + this.c * this.c));
				this.ascaleY = (float)Math.Sqrt((double)(this.b * this.b + this.d * this.d));
				this.ashearX = 0f;
				this.ashearY = MathUtils.Atan2(this.a * this.b + this.c * this.d, this.a * this.d - this.b * this.c) * 57.2957764f;
				return;
			}
			float num = bone.a;
			float num2 = bone.b;
			float num3 = bone.c;
			float num4 = bone.d;
			float num5 = 1f / (num * num4 - num2 * num3);
			float num6 = this.worldX - bone.worldX;
			float num7 = this.worldY - bone.worldY;
			this.ax = num6 * num4 * num5 - num7 * num2 * num5;
			this.ay = num7 * num * num5 - num6 * num3 * num5;
			float num8 = num5 * num4;
			float num9 = num5 * num;
			float num10 = num5 * num2;
			float num11 = num5 * num3;
			float num12 = num8 * this.a - num10 * this.c;
			float num13 = num8 * this.b - num10 * this.d;
			float num14 = num9 * this.c - num11 * this.a;
			float num15 = num9 * this.d - num11 * this.b;
			this.ashearX = 0f;
			this.ascaleX = (float)Math.Sqrt((double)(num12 * num12 + num14 * num14));
			if (this.ascaleX > 0.0001f)
			{
				float num16 = num12 * num15 - num13 * num14;
				this.ascaleY = num16 / this.ascaleX;
				this.ashearY = MathUtils.Atan2(num12 * num13 + num14 * num15, num16) * 57.2957764f;
				this.arotation = MathUtils.Atan2(num14, num12) * 57.2957764f;
			}
			else
			{
				this.ascaleX = 0f;
				this.ascaleY = (float)Math.Sqrt((double)(num13 * num13 + num15 * num15));
				this.ashearY = 0f;
				this.arotation = 90f - MathUtils.Atan2(num15, num13) * 57.2957764f;
			}
		}

		public void WorldToLocal(float worldX, float worldY, out float localX, out float localY)
		{
			float num = this.a;
			float num2 = this.b;
			float num3 = this.c;
			float num4 = this.d;
			float num5 = 1f / (num * num4 - num2 * num3);
			float num6 = worldX - this.worldX;
			float num7 = worldY - this.worldY;
			localX = num6 * num4 * num5 - num7 * num2 * num5;
			localY = num7 * num * num5 - num6 * num3 * num5;
		}

		public void LocalToWorld(float localX, float localY, out float worldX, out float worldY)
		{
			worldX = localX * this.a + localY * this.b + this.worldX;
			worldY = localX * this.c + localY * this.d + this.worldY;
		}

		public float WorldToLocalRotationX
		{
			get
			{
				Bone bone = this.parent;
				if (bone == null)
				{
					return this.arotation;
				}
				float num = bone.a;
				float num2 = bone.b;
				float num3 = bone.c;
				float num4 = bone.d;
				float num5 = this.a;
				float num6 = this.c;
				return MathUtils.Atan2(num * num6 - num3 * num5, num4 * num5 - num2 * num6) * 57.2957764f;
			}
		}

		public float WorldToLocalRotationY
		{
			get
			{
				Bone bone = this.parent;
				if (bone == null)
				{
					return this.arotation;
				}
				float num = bone.a;
				float num2 = bone.b;
				float num3 = bone.c;
				float num4 = bone.d;
				float num5 = this.b;
				float num6 = this.d;
				return MathUtils.Atan2(num * num6 - num3 * num5, num4 * num5 - num2 * num6) * 57.2957764f;
			}
		}

		public float WorldToLocalRotation(float worldRotation)
		{
			float num = MathUtils.SinDeg(worldRotation);
			float num2 = MathUtils.CosDeg(worldRotation);
			return MathUtils.Atan2(this.a * num - this.c * num2, this.d * num2 - this.b * num) * 57.2957764f;
		}

		public float LocalToWorldRotation(float localRotation)
		{
			float num = MathUtils.SinDeg(localRotation);
			float num2 = MathUtils.CosDeg(localRotation);
			return MathUtils.Atan2(num2 * this.c + num * this.d, num2 * this.a + num * this.b) * 57.2957764f;
		}

		public void RotateWorld(float degrees)
		{
			float num = this.a;
			float num2 = this.b;
			float num3 = this.c;
			float num4 = this.d;
			float num5 = MathUtils.CosDeg(degrees);
			float num6 = MathUtils.SinDeg(degrees);
			this.a = num5 * num - num6 * num3;
			this.b = num5 * num2 - num6 * num4;
			this.c = num6 * num + num5 * num3;
			this.d = num6 * num2 + num5 * num4;
			this.appliedValid = false;
		}

		public override string ToString()
		{
			return this.data.name;
		}

		public static bool yDown;

		internal BoneData data;

		internal Skeleton skeleton;

		internal Bone parent;

		internal ExposedList<Bone> children = new ExposedList<Bone>();

		internal float x;

		internal float y;

		internal float rotation;

		internal float scaleX;

		internal float scaleY;

		internal float shearX;

		internal float shearY;

		internal float ax;

		internal float ay;

		internal float arotation;

		internal float ascaleX;

		internal float ascaleY;

		internal float ashearX;

		internal float ashearY;

		internal bool appliedValid;

		internal float a;

		internal float b;

		internal float worldX;

		internal float c;

		internal float d;

		internal float worldY;

		internal bool sorted;
	}
}

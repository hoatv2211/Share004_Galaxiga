using System;

namespace Spine
{
	public class RegionAttachment : Attachment, IHasRendererObject
	{
		public RegionAttachment(string name) : base(name)
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

		public float Width
		{
			get
			{
				return this.width;
			}
			set
			{
				this.width = value;
			}
		}

		public float Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height = value;
			}
		}

		public float R
		{
			get
			{
				return this.r;
			}
			set
			{
				this.r = value;
			}
		}

		public float G
		{
			get
			{
				return this.g;
			}
			set
			{
				this.g = value;
			}
		}

		public float B
		{
			get
			{
				return this.b;
			}
			set
			{
				this.b = value;
			}
		}

		public float A
		{
			get
			{
				return this.a;
			}
			set
			{
				this.a = value;
			}
		}

		public string Path { get; set; }

		public object RendererObject { get; set; }

		public float RegionOffsetX
		{
			get
			{
				return this.regionOffsetX;
			}
			set
			{
				this.regionOffsetX = value;
			}
		}

		public float RegionOffsetY
		{
			get
			{
				return this.regionOffsetY;
			}
			set
			{
				this.regionOffsetY = value;
			}
		}

		public float RegionWidth
		{
			get
			{
				return this.regionWidth;
			}
			set
			{
				this.regionWidth = value;
			}
		}

		public float RegionHeight
		{
			get
			{
				return this.regionHeight;
			}
			set
			{
				this.regionHeight = value;
			}
		}

		public float RegionOriginalWidth
		{
			get
			{
				return this.regionOriginalWidth;
			}
			set
			{
				this.regionOriginalWidth = value;
			}
		}

		public float RegionOriginalHeight
		{
			get
			{
				return this.regionOriginalHeight;
			}
			set
			{
				this.regionOriginalHeight = value;
			}
		}

		public float[] Offset
		{
			get
			{
				return this.offset;
			}
		}

		public float[] UVs
		{
			get
			{
				return this.uvs;
			}
		}

		public void UpdateOffset()
		{
			float num = this.width;
			float num2 = this.height;
			float num3 = num * 0.5f;
			float num4 = num2 * 0.5f;
			float num5 = -num3;
			float num6 = -num4;
			if (this.regionOriginalWidth != 0f)
			{
				num5 += this.regionOffsetX / this.regionOriginalWidth * num;
				num6 += this.regionOffsetY / this.regionOriginalHeight * num2;
				num3 -= (this.regionOriginalWidth - this.regionOffsetX - this.regionWidth) / this.regionOriginalWidth * num;
				num4 -= (this.regionOriginalHeight - this.regionOffsetY - this.regionHeight) / this.regionOriginalHeight * num2;
			}
			float num7 = this.scaleX;
			float num8 = this.scaleY;
			num5 *= num7;
			num6 *= num8;
			num3 *= num7;
			num4 *= num8;
			float degrees = this.rotation;
			float num9 = MathUtils.CosDeg(degrees);
			float num10 = MathUtils.SinDeg(degrees);
			float num11 = this.x;
			float num12 = this.y;
			float num13 = num5 * num9 + num11;
			float num14 = num5 * num10;
			float num15 = num6 * num9 + num12;
			float num16 = num6 * num10;
			float num17 = num3 * num9 + num11;
			float num18 = num3 * num10;
			float num19 = num4 * num9 + num12;
			float num20 = num4 * num10;
			float[] array = this.offset;
			array[0] = num13 - num16;
			array[1] = num15 + num14;
			array[2] = num13 - num20;
			array[3] = num19 + num14;
			array[4] = num17 - num20;
			array[5] = num19 + num18;
			array[6] = num17 - num16;
			array[7] = num15 + num18;
		}

		public void SetUVs(float u, float v, float u2, float v2, bool rotate)
		{
			float[] array = this.uvs;
			if (rotate)
			{
				array[4] = u;
				array[5] = v2;
				array[6] = u;
				array[7] = v;
				array[0] = u2;
				array[1] = v;
				array[2] = u2;
				array[3] = v2;
			}
			else
			{
				array[2] = u;
				array[3] = v2;
				array[4] = u;
				array[5] = v;
				array[6] = u2;
				array[7] = v;
				array[0] = u2;
				array[1] = v2;
			}
		}

		public void ComputeWorldVertices(Bone bone, float[] worldVertices, int offset, int stride = 2)
		{
			float[] array = this.offset;
			float worldX = bone.worldX;
			float worldY = bone.worldY;
			float num = bone.a;
			float num2 = bone.b;
			float c = bone.c;
			float d = bone.d;
			float num3 = array[6];
			float num4 = array[7];
			worldVertices[offset] = num3 * num + num4 * num2 + worldX;
			worldVertices[offset + 1] = num3 * c + num4 * d + worldY;
			offset += stride;
			num3 = array[0];
			num4 = array[1];
			worldVertices[offset] = num3 * num + num4 * num2 + worldX;
			worldVertices[offset + 1] = num3 * c + num4 * d + worldY;
			offset += stride;
			num3 = array[2];
			num4 = array[3];
			worldVertices[offset] = num3 * num + num4 * num2 + worldX;
			worldVertices[offset + 1] = num3 * c + num4 * d + worldY;
			offset += stride;
			num3 = array[4];
			num4 = array[5];
			worldVertices[offset] = num3 * num + num4 * num2 + worldX;
			worldVertices[offset + 1] = num3 * c + num4 * d + worldY;
		}

		public const int BLX = 0;

		public const int BLY = 1;

		public const int ULX = 2;

		public const int ULY = 3;

		public const int URX = 4;

		public const int URY = 5;

		public const int BRX = 6;

		public const int BRY = 7;

		internal float x;

		internal float y;

		internal float rotation;

		internal float scaleX = 1f;

		internal float scaleY = 1f;

		internal float width;

		internal float height;

		internal float regionOffsetX;

		internal float regionOffsetY;

		internal float regionWidth;

		internal float regionHeight;

		internal float regionOriginalWidth;

		internal float regionOriginalHeight;

		internal float[] offset = new float[8];

		internal float[] uvs = new float[8];

		internal float r = 1f;

		internal float g = 1f;

		internal float b = 1f;

		internal float a = 1f;
	}
}

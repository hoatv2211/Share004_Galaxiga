using System;

namespace Spine
{
	public class SlotData
	{
		public SlotData(int index, string name, BoneData boneData)
		{
			if (index < 0)
			{
				throw new ArgumentException("index must be >= 0.", "index");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name", "name cannot be null.");
			}
			if (boneData == null)
			{
				throw new ArgumentNullException("boneData", "boneData cannot be null.");
			}
			this.index = index;
			this.name = name;
			this.boneData = boneData;
		}

		public int Index
		{
			get
			{
				return this.index;
			}
		}

		public string Name
		{
			get
			{
				return this.name;
			}
		}

		public BoneData BoneData
		{
			get
			{
				return this.boneData;
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

		public float R2
		{
			get
			{
				return this.r2;
			}
			set
			{
				this.r2 = value;
			}
		}

		public float G2
		{
			get
			{
				return this.g2;
			}
			set
			{
				this.g2 = value;
			}
		}

		public float B2
		{
			get
			{
				return this.b2;
			}
			set
			{
				this.b2 = value;
			}
		}

		public bool HasSecondColor
		{
			get
			{
				return this.hasSecondColor;
			}
			set
			{
				this.hasSecondColor = value;
			}
		}

		public string AttachmentName
		{
			get
			{
				return this.attachmentName;
			}
			set
			{
				this.attachmentName = value;
			}
		}

		public BlendMode BlendMode
		{
			get
			{
				return this.blendMode;
			}
			set
			{
				this.blendMode = value;
			}
		}

		public override string ToString()
		{
			return this.name;
		}

		internal int index;

		internal string name;

		internal BoneData boneData;

		internal float r = 1f;

		internal float g = 1f;

		internal float b = 1f;

		internal float a = 1f;

		internal float r2;

		internal float g2;

		internal float b2;

		internal bool hasSecondColor;

		internal string attachmentName;

		internal BlendMode blendMode;
	}
}

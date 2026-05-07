using System;

namespace Spine
{
	public class Slot
	{
		public Slot(SlotData data, Bone bone)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data", "data cannot be null.");
			}
			if (bone == null)
			{
				throw new ArgumentNullException("bone", "bone cannot be null.");
			}
			this.data = data;
			this.bone = bone;
			this.SetToSetupPose();
		}

		public SlotData Data
		{
			get
			{
				return this.data;
			}
		}

		public Bone Bone
		{
			get
			{
				return this.bone;
			}
		}

		public Skeleton Skeleton
		{
			get
			{
				return this.bone.skeleton;
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
				return this.data.hasSecondColor;
			}
			set
			{
				this.data.hasSecondColor = value;
			}
		}

		public Attachment Attachment
		{
			get
			{
				return this.attachment;
			}
			set
			{
				if (this.attachment == value)
				{
					return;
				}
				this.attachment = value;
				this.attachmentTime = this.bone.skeleton.time;
				this.attachmentVertices.Clear(false);
			}
		}

		public float AttachmentTime
		{
			get
			{
				return this.bone.skeleton.time - this.attachmentTime;
			}
			set
			{
				this.attachmentTime = this.bone.skeleton.time - value;
			}
		}

		public ExposedList<float> AttachmentVertices
		{
			get
			{
				return this.attachmentVertices;
			}
			set
			{
				this.attachmentVertices = value;
			}
		}

		public void SetToSetupPose()
		{
			this.r = this.data.r;
			this.g = this.data.g;
			this.b = this.data.b;
			this.a = this.data.a;
			if (this.data.attachmentName == null)
			{
				this.Attachment = null;
			}
			else
			{
				this.attachment = null;
				this.Attachment = this.bone.skeleton.GetAttachment(this.data.index, this.data.attachmentName);
			}
		}

		public override string ToString()
		{
			return this.data.name;
		}

		internal SlotData data;

		internal Bone bone;

		internal float r;

		internal float g;

		internal float b;

		internal float a;

		internal float r2;

		internal float g2;

		internal float b2;

		internal bool hasSecondColor;

		internal Attachment attachment;

		internal float attachmentTime;

		internal ExposedList<float> attachmentVertices = new ExposedList<float>();
	}
}

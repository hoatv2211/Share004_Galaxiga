using System;

namespace Spine
{
	public class ClippingAttachment : VertexAttachment
	{
		public ClippingAttachment(string name) : base(name)
		{
		}

		public SlotData EndSlot
		{
			get
			{
				return this.endSlot;
			}
			set
			{
				this.endSlot = value;
			}
		}

		internal SlotData endSlot;
	}
}

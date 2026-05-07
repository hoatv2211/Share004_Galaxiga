using System;
using UnityEngine;

namespace Spine.Unity
{
	public struct SubmeshInstruction
	{
		public int SlotCount
		{
			get
			{
				return this.endSlot - this.startSlot;
			}
		}

		public override string ToString()
		{
			return string.Format("[SubmeshInstruction: slots {0} to {1}. (Material){2}. preActiveClippingSlotSource:{3}]", new object[]
			{
				this.startSlot,
				this.endSlot - 1,
				(!(this.material == null)) ? this.material.name : "<none>",
				this.preActiveClippingSlotSource
			});
		}

		public Skeleton skeleton;

		public int startSlot;

		public int endSlot;

		public Material material;

		public bool forceSeparate;

		public int preActiveClippingSlotSource;

		public int rawTriangleCount;

		public int rawVertexCount;

		public int rawFirstVertexIndex;

		public bool hasClipping;
	}
}

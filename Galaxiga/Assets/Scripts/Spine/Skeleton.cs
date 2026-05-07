using System;
using System.Collections.Generic;

namespace Spine
{
	public class Skeleton
	{
		public Skeleton(SkeletonData data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data", "data cannot be null.");
			}
			this.data = data;
			this.bones = new ExposedList<Bone>(data.bones.Count);
			foreach (BoneData boneData in data.bones)
			{
				Bone item;
				if (boneData.parent == null)
				{
					item = new Bone(boneData, this, null);
				}
				else
				{
					Bone bone = this.bones.Items[boneData.parent.index];
					item = new Bone(boneData, this, bone);
					bone.children.Add(item);
				}
				this.bones.Add(item);
			}
			this.slots = new ExposedList<Slot>(data.slots.Count);
			this.drawOrder = new ExposedList<Slot>(data.slots.Count);
			foreach (SlotData slotData in data.slots)
			{
				Bone bone2 = this.bones.Items[slotData.boneData.index];
				Slot item2 = new Slot(slotData, bone2);
				this.slots.Add(item2);
				this.drawOrder.Add(item2);
			}
			this.ikConstraints = new ExposedList<IkConstraint>(data.ikConstraints.Count);
			foreach (IkConstraintData ikConstraintData in data.ikConstraints)
			{
				this.ikConstraints.Add(new IkConstraint(ikConstraintData, this));
			}
			this.transformConstraints = new ExposedList<TransformConstraint>(data.transformConstraints.Count);
			foreach (TransformConstraintData transformConstraintData in data.transformConstraints)
			{
				this.transformConstraints.Add(new TransformConstraint(transformConstraintData, this));
			}
			this.pathConstraints = new ExposedList<PathConstraint>(data.pathConstraints.Count);
			foreach (PathConstraintData pathConstraintData in data.pathConstraints)
			{
				this.pathConstraints.Add(new PathConstraint(pathConstraintData, this));
			}
			this.UpdateCache();
			this.UpdateWorldTransform();
		}

		public SkeletonData Data
		{
			get
			{
				return this.data;
			}
		}

		public ExposedList<Bone> Bones
		{
			get
			{
				return this.bones;
			}
		}

		public ExposedList<IUpdatable> UpdateCacheList
		{
			get
			{
				return this.updateCache;
			}
		}

		public ExposedList<Slot> Slots
		{
			get
			{
				return this.slots;
			}
		}

		public ExposedList<Slot> DrawOrder
		{
			get
			{
				return this.drawOrder;
			}
		}

		public ExposedList<IkConstraint> IkConstraints
		{
			get
			{
				return this.ikConstraints;
			}
		}

		public ExposedList<PathConstraint> PathConstraints
		{
			get
			{
				return this.pathConstraints;
			}
		}

		public ExposedList<TransformConstraint> TransformConstraints
		{
			get
			{
				return this.transformConstraints;
			}
		}

		public Skin Skin
		{
			get
			{
				return this.skin;
			}
			set
			{
				this.skin = value;
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

		public float Time
		{
			get
			{
				return this.time;
			}
			set
			{
				this.time = value;
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

		public bool FlipX
		{
			get
			{
				return this.flipX;
			}
			set
			{
				this.flipX = value;
			}
		}

		public bool FlipY
		{
			get
			{
				return this.flipY;
			}
			set
			{
				this.flipY = value;
			}
		}

		public Bone RootBone
		{
			get
			{
				return (this.bones.Count != 0) ? this.bones.Items[0] : null;
			}
		}

		public void UpdateCache()
		{
            ExposedList<IUpdatable> exposedList = updateCache;
            exposedList.Clear(true);
            updateCacheReset.Clear(true);
            ExposedList<Bone> exposedList2 = bones;
            int i = 0;
            for (int count = exposedList2.Count; i < count; i++)
            {
                exposedList2.Items[i].sorted = false;
            }
            ExposedList<IkConstraint> exposedList3 = ikConstraints;
            ExposedList<TransformConstraint> exposedList4 = transformConstraints;
            ExposedList<PathConstraint> exposedList5 = pathConstraints;
            int count2 = IkConstraints.Count;
            int count3 = exposedList4.Count;
            int count4 = exposedList5.Count;
            int num = count2 + count3 + count4;
            for (int j = 0; j < num; j++)
            {
                int num2 = 0;
                while (true)
                {
                    if (num2 < count2)
                    {
                        IkConstraint ikConstraint = exposedList3.Items[num2];
                        if (ikConstraint.data.order == j)
                        {
                            SortIkConstraint(ikConstraint);
                            break;
                        }
                        num2++;
                        continue;
                    }
                    int num3 = 0;
                    while (true)
                    {
                        if (num3 < count3)
                        {
                            TransformConstraint transformConstraint = exposedList4.Items[num3];
                            if (transformConstraint.data.order == j)
                            {
                                SortTransformConstraint(transformConstraint);
                                break;
                            }
                            num3++;
                            continue;
                        }
                        for (int k = 0; k < count4; k++)
                        {
                            PathConstraint pathConstraint = exposedList5.Items[k];
                            if (pathConstraint.data.order == j)
                            {
                                SortPathConstraint(pathConstraint);
                                break;
                            }
                        }
                        break;
                    }
                    break;
                }
            }
            int l = 0;
            for (int count5 = exposedList2.Count; l < count5; l++)
            {
                SortBone(exposedList2.Items[l]);
            }
        }

		private void SortIkConstraint(IkConstraint constraint)
		{
			Bone target = constraint.target;
			this.SortBone(target);
			ExposedList<Bone> exposedList = constraint.bones;
			Bone bone = exposedList.Items[0];
			this.SortBone(bone);
			if (exposedList.Count > 1)
			{
				Bone item = exposedList.Items[exposedList.Count - 1];
				if (!this.updateCache.Contains(item))
				{
					this.updateCacheReset.Add(item);
				}
			}
			this.updateCache.Add(constraint);
			Skeleton.SortReset(bone.children);
			exposedList.Items[exposedList.Count - 1].sorted = true;
		}

		private void SortPathConstraint(PathConstraint constraint)
		{
			Slot target = constraint.target;
			int index = target.data.index;
			Bone bone = target.bone;
			if (this.skin != null)
			{
				this.SortPathConstraintAttachment(this.skin, index, bone);
			}
			if (this.data.defaultSkin != null && this.data.defaultSkin != this.skin)
			{
				this.SortPathConstraintAttachment(this.data.defaultSkin, index, bone);
			}
			int i = 0;
			int count = this.data.skins.Count;
			while (i < count)
			{
				this.SortPathConstraintAttachment(this.data.skins.Items[i], index, bone);
				i++;
			}
			Attachment attachment = target.attachment;
			if (attachment is PathAttachment)
			{
				this.SortPathConstraintAttachment(attachment, bone);
			}
			ExposedList<Bone> exposedList = constraint.bones;
			int count2 = exposedList.Count;
			for (int j = 0; j < count2; j++)
			{
				this.SortBone(exposedList.Items[j]);
			}
			this.updateCache.Add(constraint);
			for (int k = 0; k < count2; k++)
			{
				Skeleton.SortReset(exposedList.Items[k].children);
			}
			for (int l = 0; l < count2; l++)
			{
				exposedList.Items[l].sorted = true;
			}
		}

		private void SortTransformConstraint(TransformConstraint constraint)
		{
			this.SortBone(constraint.target);
			ExposedList<Bone> exposedList = constraint.bones;
			int count = exposedList.Count;
			if (constraint.data.local)
			{
				for (int i = 0; i < count; i++)
				{
					Bone bone = exposedList.Items[i];
					this.SortBone(bone.parent);
					if (!this.updateCache.Contains(bone))
					{
						this.updateCacheReset.Add(bone);
					}
				}
			}
			else
			{
				for (int j = 0; j < count; j++)
				{
					this.SortBone(exposedList.Items[j]);
				}
			}
			this.updateCache.Add(constraint);
			for (int k = 0; k < count; k++)
			{
				Skeleton.SortReset(exposedList.Items[k].children);
			}
			for (int l = 0; l < count; l++)
			{
				exposedList.Items[l].sorted = true;
			}
		}

		private void SortPathConstraintAttachment(Skin skin, int slotIndex, Bone slotBone)
		{
			foreach (KeyValuePair<Skin.AttachmentKeyTuple, Attachment> keyValuePair in skin.Attachments)
			{
				if (keyValuePair.Key.slotIndex == slotIndex)
				{
					this.SortPathConstraintAttachment(keyValuePair.Value, slotBone);
				}
			}
		}

		private void SortPathConstraintAttachment(Attachment attachment, Bone slotBone)
		{
			if (!(attachment is PathAttachment))
			{
				return;
			}
			int[] array = ((PathAttachment)attachment).bones;
			if (array == null)
			{
				this.SortBone(slotBone);
			}
			else
			{
				ExposedList<Bone> exposedList = this.bones;
				int i = 0;
				int num = array.Length;
				while (i < num)
				{
					int num2 = array[i++];
					num2 += i;
					while (i < num2)
					{
						this.SortBone(exposedList.Items[array[i++]]);
					}
				}
			}
		}

		private void SortBone(Bone bone)
		{
			if (bone.sorted)
			{
				return;
			}
			Bone parent = bone.parent;
			if (parent != null)
			{
				this.SortBone(parent);
			}
			bone.sorted = true;
			this.updateCache.Add(bone);
		}

		private static void SortReset(ExposedList<Bone> bones)
		{
			Bone[] items = bones.Items;
			int i = 0;
			int count = bones.Count;
			while (i < count)
			{
				Bone bone = items[i];
				if (bone.sorted)
				{
					Skeleton.SortReset(bone.children);
				}
				bone.sorted = false;
				i++;
			}
		}

		public void UpdateWorldTransform()
		{
			ExposedList<Bone> exposedList = this.updateCacheReset;
			Bone[] items = exposedList.Items;
			int i = 0;
			int count = exposedList.Count;
			while (i < count)
			{
				Bone bone = items[i];
				bone.ax = bone.x;
				bone.ay = bone.y;
				bone.arotation = bone.rotation;
				bone.ascaleX = bone.scaleX;
				bone.ascaleY = bone.scaleY;
				bone.ashearX = bone.shearX;
				bone.ashearY = bone.shearY;
				bone.appliedValid = true;
				i++;
			}
			IUpdatable[] items2 = this.updateCache.Items;
			int j = 0;
			int count2 = this.updateCache.Count;
			while (j < count2)
			{
				items2[j].Update();
				j++;
			}
		}

		public void SetToSetupPose()
		{
			this.SetBonesToSetupPose();
			this.SetSlotsToSetupPose();
		}

		public void SetBonesToSetupPose()
		{
			Bone[] items = this.bones.Items;
			int i = 0;
			int count = this.bones.Count;
			while (i < count)
			{
				items[i].SetToSetupPose();
				i++;
			}
			IkConstraint[] items2 = this.ikConstraints.Items;
			int j = 0;
			int count2 = this.ikConstraints.Count;
			while (j < count2)
			{
				IkConstraint ikConstraint = items2[j];
				ikConstraint.bendDirection = ikConstraint.data.bendDirection;
				ikConstraint.mix = ikConstraint.data.mix;
				j++;
			}
			TransformConstraint[] items3 = this.transformConstraints.Items;
			int k = 0;
			int count3 = this.transformConstraints.Count;
			while (k < count3)
			{
				TransformConstraint transformConstraint = items3[k];
				TransformConstraintData transformConstraintData = transformConstraint.data;
				transformConstraint.rotateMix = transformConstraintData.rotateMix;
				transformConstraint.translateMix = transformConstraintData.translateMix;
				transformConstraint.scaleMix = transformConstraintData.scaleMix;
				transformConstraint.shearMix = transformConstraintData.shearMix;
				k++;
			}
			PathConstraint[] items4 = this.pathConstraints.Items;
			int l = 0;
			int count4 = this.pathConstraints.Count;
			while (l < count4)
			{
				PathConstraint pathConstraint = items4[l];
				PathConstraintData pathConstraintData = pathConstraint.data;
				pathConstraint.position = pathConstraintData.position;
				pathConstraint.spacing = pathConstraintData.spacing;
				pathConstraint.rotateMix = pathConstraintData.rotateMix;
				pathConstraint.translateMix = pathConstraintData.translateMix;
				l++;
			}
		}

		public void SetSlotsToSetupPose()
		{
			ExposedList<Slot> exposedList = this.slots;
			Slot[] items = exposedList.Items;
			this.drawOrder.Clear(true);
			int i = 0;
			int count = exposedList.Count;
			while (i < count)
			{
				this.drawOrder.Add(items[i]);
				i++;
			}
			int j = 0;
			int count2 = exposedList.Count;
			while (j < count2)
			{
				items[j].SetToSetupPose();
				j++;
			}
		}

		public Bone FindBone(string boneName)
		{
			if (boneName == null)
			{
				throw new ArgumentNullException("boneName", "boneName cannot be null.");
			}
			ExposedList<Bone> exposedList = this.bones;
			Bone[] items = exposedList.Items;
			int i = 0;
			int count = exposedList.Count;
			while (i < count)
			{
				Bone bone = items[i];
				if (bone.data.name == boneName)
				{
					return bone;
				}
				i++;
			}
			return null;
		}

		public int FindBoneIndex(string boneName)
		{
			if (boneName == null)
			{
				throw new ArgumentNullException("boneName", "boneName cannot be null.");
			}
			ExposedList<Bone> exposedList = this.bones;
			Bone[] items = exposedList.Items;
			int i = 0;
			int count = exposedList.Count;
			while (i < count)
			{
				if (items[i].data.name == boneName)
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		public Slot FindSlot(string slotName)
		{
			if (slotName == null)
			{
				throw new ArgumentNullException("slotName", "slotName cannot be null.");
			}
			ExposedList<Slot> exposedList = this.slots;
			Slot[] items = exposedList.Items;
			int i = 0;
			int count = exposedList.Count;
			while (i < count)
			{
				Slot slot = items[i];
				if (slot.data.name == slotName)
				{
					return slot;
				}
				i++;
			}
			return null;
		}

		public int FindSlotIndex(string slotName)
		{
			if (slotName == null)
			{
				throw new ArgumentNullException("slotName", "slotName cannot be null.");
			}
			ExposedList<Slot> exposedList = this.slots;
			Slot[] items = exposedList.Items;
			int i = 0;
			int count = exposedList.Count;
			while (i < count)
			{
				if (items[i].data.name.Equals(slotName))
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		public void SetSkin(string skinName)
		{
			Skin skin = this.data.FindSkin(skinName);
			if (skin == null)
			{
				throw new ArgumentException("Skin not found: " + skinName, "skinName");
			}
			this.SetSkin(skin);
		}

		public void SetSkin(Skin newSkin)
		{
			if (newSkin != null)
			{
				if (this.skin != null)
				{
					newSkin.AttachAll(this, this.skin);
				}
				else
				{
					ExposedList<Slot> exposedList = this.slots;
					int i = 0;
					int count = exposedList.Count;
					while (i < count)
					{
						Slot slot = exposedList.Items[i];
						string attachmentName = slot.data.attachmentName;
						if (attachmentName != null)
						{
							Attachment attachment = newSkin.GetAttachment(i, attachmentName);
							if (attachment != null)
							{
								slot.Attachment = attachment;
							}
						}
						i++;
					}
				}
			}
			this.skin = newSkin;
		}

		public Attachment GetAttachment(string slotName, string attachmentName)
		{
			return this.GetAttachment(this.data.FindSlotIndex(slotName), attachmentName);
		}

		public Attachment GetAttachment(int slotIndex, string attachmentName)
		{
			if (attachmentName == null)
			{
				throw new ArgumentNullException("attachmentName", "attachmentName cannot be null.");
			}
			if (this.skin != null)
			{
				Attachment attachment = this.skin.GetAttachment(slotIndex, attachmentName);
				if (attachment != null)
				{
					return attachment;
				}
			}
			return (this.data.defaultSkin == null) ? null : this.data.defaultSkin.GetAttachment(slotIndex, attachmentName);
		}

		public void SetAttachment(string slotName, string attachmentName)
		{
			if (slotName == null)
			{
				throw new ArgumentNullException("slotName", "slotName cannot be null.");
			}
			ExposedList<Slot> exposedList = this.slots;
			int i = 0;
			int count = exposedList.Count;
			while (i < count)
			{
				Slot slot = exposedList.Items[i];
				if (slot.data.name == slotName)
				{
					Attachment attachment = null;
					if (attachmentName != null)
					{
						attachment = this.GetAttachment(i, attachmentName);
						if (attachment == null)
						{
							throw new Exception("Attachment not found: " + attachmentName + ", for slot: " + slotName);
						}
					}
					slot.Attachment = attachment;
					return;
				}
				i++;
			}
			throw new Exception("Slot not found: " + slotName);
		}

		public IkConstraint FindIkConstraint(string constraintName)
		{
			if (constraintName == null)
			{
				throw new ArgumentNullException("constraintName", "constraintName cannot be null.");
			}
			ExposedList<IkConstraint> exposedList = this.ikConstraints;
			int i = 0;
			int count = exposedList.Count;
			while (i < count)
			{
				IkConstraint ikConstraint = exposedList.Items[i];
				if (ikConstraint.data.name == constraintName)
				{
					return ikConstraint;
				}
				i++;
			}
			return null;
		}

		public TransformConstraint FindTransformConstraint(string constraintName)
		{
			if (constraintName == null)
			{
				throw new ArgumentNullException("constraintName", "constraintName cannot be null.");
			}
			ExposedList<TransformConstraint> exposedList = this.transformConstraints;
			int i = 0;
			int count = exposedList.Count;
			while (i < count)
			{
				TransformConstraint transformConstraint = exposedList.Items[i];
				if (transformConstraint.data.name == constraintName)
				{
					return transformConstraint;
				}
				i++;
			}
			return null;
		}

		public PathConstraint FindPathConstraint(string constraintName)
		{
			if (constraintName == null)
			{
				throw new ArgumentNullException("constraintName", "constraintName cannot be null.");
			}
			ExposedList<PathConstraint> exposedList = this.pathConstraints;
			int i = 0;
			int count = exposedList.Count;
			while (i < count)
			{
				PathConstraint pathConstraint = exposedList.Items[i];
				if (pathConstraint.data.name.Equals(constraintName))
				{
					return pathConstraint;
				}
				i++;
			}
			return null;
		}

		public void Update(float delta)
		{
			this.time += delta;
		}

		public void GetBounds(out float x, out float y, out float width, out float height, ref float[] vertexBuffer)
		{
			float[] array = vertexBuffer;
			array = (array ?? new float[8]);
			Slot[] items = this.drawOrder.Items;
			float num = 2.14748365E+09f;
			float num2 = 2.14748365E+09f;
			float num3 = -2.14748365E+09f;
			float num4 = -2.14748365E+09f;
			int i = 0;
			int count = this.drawOrder.Count;
			while (i < count)
			{
				Slot slot = items[i];
				int num5 = 0;
				float[] array2 = null;
				Attachment attachment = slot.attachment;
				RegionAttachment regionAttachment = attachment as RegionAttachment;
				if (regionAttachment != null)
				{
					num5 = 8;
					array2 = array;
					if (array2.Length < 8)
					{
						array = (array2 = new float[8]);
					}
					regionAttachment.ComputeWorldVertices(slot.bone, array, 0, 2);
				}
				else
				{
					MeshAttachment meshAttachment = attachment as MeshAttachment;
					if (meshAttachment != null)
					{
						MeshAttachment meshAttachment2 = meshAttachment;
						num5 = meshAttachment2.WorldVerticesLength;
						array2 = array;
						if (array2.Length < num5)
						{
							array = (array2 = new float[num5]);
						}
						meshAttachment2.ComputeWorldVertices(slot, 0, num5, array, 0, 2);
					}
				}
				if (array2 != null)
				{
					for (int j = 0; j < num5; j += 2)
					{
						float val = array2[j];
						float val2 = array2[j + 1];
						num = Math.Min(num, val);
						num2 = Math.Min(num2, val2);
						num3 = Math.Max(num3, val);
						num4 = Math.Max(num4, val2);
					}
				}
				i++;
			}
			x = num;
			y = num2;
			width = num3 - num;
			height = num4 - num2;
			vertexBuffer = array;
		}

		internal SkeletonData data;

		internal ExposedList<Bone> bones;

		internal ExposedList<Slot> slots;

		internal ExposedList<Slot> drawOrder;

		internal ExposedList<IkConstraint> ikConstraints;

		internal ExposedList<TransformConstraint> transformConstraints;

		internal ExposedList<PathConstraint> pathConstraints;

		internal ExposedList<IUpdatable> updateCache = new ExposedList<IUpdatable>();

		internal ExposedList<Bone> updateCacheReset = new ExposedList<Bone>();

		internal Skin skin;

		internal float r = 1f;

		internal float g = 1f;

		internal float b = 1f;

		internal float a = 1f;

		internal float time;

		internal bool flipX;

		internal bool flipY;

		internal float x;

		internal float y;
	}
}

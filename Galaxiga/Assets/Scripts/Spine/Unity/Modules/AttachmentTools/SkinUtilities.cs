using System;
using System.Collections.Generic;

namespace Spine.Unity.Modules.AttachmentTools
{
	public static class SkinUtilities
	{
		public static Skin UnshareSkin(this Skeleton skeleton, bool includeDefaultSkin, bool unshareAttachments, AnimationState state = null)
		{
			Skin clonedSkin = skeleton.GetClonedSkin("cloned skin", includeDefaultSkin, unshareAttachments, true);
			skeleton.SetSkin(clonedSkin);
			if (state != null)
			{
				skeleton.SetToSetupPose();
				state.Apply(skeleton);
			}
			return clonedSkin;
		}

		public static Skin GetClonedSkin(this Skeleton skeleton, string newSkinName, bool includeDefaultSkin = false, bool cloneAttachments = false, bool cloneMeshesAsLinked = true)
		{
			Skin skin = new Skin(newSkinName);
			Skin defaultSkin = skeleton.data.DefaultSkin;
			Skin skin2 = skeleton.skin;
			if (includeDefaultSkin)
			{
				defaultSkin.CopyTo(skin, true, cloneAttachments, cloneMeshesAsLinked);
			}
			if (skin2 != null)
			{
				skin2.CopyTo(skin, true, cloneAttachments, cloneMeshesAsLinked);
			}
			return skin;
		}

		public static Skin GetClone(this Skin original)
		{
			Skin skin = new Skin(original.name + " clone");
			Dictionary<Skin.AttachmentKeyTuple, Attachment> attachments = skin.Attachments;
			foreach (KeyValuePair<Skin.AttachmentKeyTuple, Attachment> keyValuePair in original.Attachments)
			{
				attachments[keyValuePair.Key] = keyValuePair.Value;
			}
			return skin;
		}

		public static void SetAttachment(this Skin skin, string slotName, string keyName, Attachment attachment, Skeleton skeleton)
		{
			int num = skeleton.FindSlotIndex(slotName);
			if (skeleton == null)
			{
				throw new ArgumentNullException("skeleton", "skeleton cannot be null.");
			}
			if (num == -1)
			{
				throw new ArgumentException(string.Format("Slot '{0}' does not exist in skeleton.", slotName), "slotName");
			}
			skin.AddAttachment(num, keyName, attachment);
		}

		public static Attachment GetAttachment(this Skin skin, string slotName, string keyName, Skeleton skeleton)
		{
			int num = skeleton.FindSlotIndex(slotName);
			if (skeleton == null)
			{
				throw new ArgumentNullException("skeleton", "skeleton cannot be null.");
			}
			if (num == -1)
			{
				throw new ArgumentException(string.Format("Slot '{0}' does not exist in skeleton.", slotName), "slotName");
			}
			return skin.GetAttachment(num, keyName);
		}

		public static void SetAttachment(this Skin skin, int slotIndex, string keyName, Attachment attachment)
		{
			skin.AddAttachment(slotIndex, keyName, attachment);
		}

		public static bool RemoveAttachment(this Skin skin, string slotName, string keyName, Skeleton skeleton)
		{
			int num = skeleton.FindSlotIndex(slotName);
			if (skeleton == null)
			{
				throw new ArgumentNullException("skeleton", "skeleton cannot be null.");
			}
			if (num == -1)
			{
				throw new ArgumentException(string.Format("Slot '{0}' does not exist in skeleton.", slotName), "slotName");
			}
			return skin.RemoveAttachment(num, keyName);
		}

		public static bool RemoveAttachment(this Skin skin, int slotIndex, string keyName)
		{
			return skin.Attachments.Remove(new Skin.AttachmentKeyTuple(slotIndex, keyName));
		}

		public static void Clear(this Skin skin)
		{
			skin.Attachments.Clear();
		}

		public static void Append(this Skin destination, Skin source)
		{
			source.CopyTo(destination, true, false, true);
		}

		public static void CopyTo(this Skin source, Skin destination, bool overwrite, bool cloneAttachments, bool cloneMeshesAsLinked = true)
		{
			Dictionary<Skin.AttachmentKeyTuple, Attachment> attachments = source.Attachments;
			Dictionary<Skin.AttachmentKeyTuple, Attachment> attachments2 = destination.Attachments;
			if (cloneAttachments)
			{
				if (overwrite)
				{
					foreach (KeyValuePair<Skin.AttachmentKeyTuple, Attachment> keyValuePair in attachments)
					{
						attachments2[keyValuePair.Key] = keyValuePair.Value.GetClone(cloneMeshesAsLinked);
					}
				}
				else
				{
					foreach (KeyValuePair<Skin.AttachmentKeyTuple, Attachment> keyValuePair2 in attachments)
					{
						if (!attachments2.ContainsKey(keyValuePair2.Key))
						{
							attachments2.Add(keyValuePair2.Key, keyValuePair2.Value.GetClone(cloneMeshesAsLinked));
						}
					}
				}
			}
			else if (overwrite)
			{
				foreach (KeyValuePair<Skin.AttachmentKeyTuple, Attachment> keyValuePair3 in attachments)
				{
					attachments2[keyValuePair3.Key] = keyValuePair3.Value;
				}
			}
			else
			{
				foreach (KeyValuePair<Skin.AttachmentKeyTuple, Attachment> keyValuePair4 in attachments)
				{
					if (!attachments2.ContainsKey(keyValuePair4.Key))
					{
						attachments2.Add(keyValuePair4.Key, keyValuePair4.Value);
					}
				}
			}
		}
	}
}

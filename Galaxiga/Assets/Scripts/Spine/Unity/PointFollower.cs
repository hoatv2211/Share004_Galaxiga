using System;
using UnityEngine;

namespace Spine.Unity
{
	[ExecuteInEditMode]
	[AddComponentMenu("Spine/Point Follower")]
	public class PointFollower : MonoBehaviour, IHasSkeletonRenderer, IHasSkeletonComponent
	{
		public SkeletonRenderer SkeletonRenderer
		{
			get
			{
				return this.skeletonRenderer;
			}
		}

		public ISkeletonComponent SkeletonComponent
		{
			get
			{
				return this.skeletonRenderer;
			}
		}

		public bool IsValid
		{
			get
			{
				return this.valid;
			}
		}

		public void Initialize()
		{
			this.valid = (this.skeletonRenderer != null && this.skeletonRenderer.valid);
			if (!this.valid)
			{
				return;
			}
			this.UpdateReferences();
		}

		private void HandleRebuildRenderer(SkeletonRenderer skeletonRenderer)
		{
			this.Initialize();
		}

		private void UpdateReferences()
		{
			this.skeletonTransform = this.skeletonRenderer.transform;
			this.skeletonRenderer.OnRebuild -= this.HandleRebuildRenderer;
			this.skeletonRenderer.OnRebuild += this.HandleRebuildRenderer;
			this.skeletonTransformIsParent = object.ReferenceEquals(this.skeletonTransform, base.transform.parent);
			this.bone = null;
			this.point = null;
			if (!string.IsNullOrEmpty(this.pointAttachmentName))
			{
				Skeleton skeleton = this.skeletonRenderer.skeleton;
				int num = skeleton.FindSlotIndex(this.slotName);
				if (num >= 0)
				{
					Slot slot = skeleton.slots.Items[num];
					this.bone = slot.bone;
					this.point = (skeleton.GetAttachment(num, this.pointAttachmentName) as PointAttachment);
				}
			}
		}

		public void LateUpdate()
		{
			if (this.point == null)
			{
				if (string.IsNullOrEmpty(this.pointAttachmentName))
				{
					return;
				}
				this.UpdateReferences();
				if (this.point == null)
				{
					return;
				}
			}
			Vector2 vector;
			this.point.ComputeWorldPosition(this.bone, out vector.x, out vector.y);
			float num = this.point.ComputeWorldRotation(this.bone);
			Transform transform = base.transform;
			if (this.skeletonTransformIsParent)
			{
				transform.localPosition = new Vector3(vector.x, vector.y, (!this.followSkeletonZPosition) ? transform.localPosition.z : 0f);
				if (this.followRotation)
				{
					float f = num * 0.5f * 0.0174532924f;
					transform.localRotation = new Quaternion
					{
						z = Mathf.Sin(f),
						w = Mathf.Cos(f)
					};
				}
			}
			else
			{
				Vector3 position = this.skeletonTransform.TransformPoint(new Vector3(vector.x, vector.y, 0f));
				if (!this.followSkeletonZPosition)
				{
					position.z = transform.position.z;
				}
				Transform parent = transform.parent;
				if (parent != null)
				{
					Matrix4x4 localToWorldMatrix = parent.localToWorldMatrix;
					if (localToWorldMatrix.m00 * localToWorldMatrix.m11 - localToWorldMatrix.m01 * localToWorldMatrix.m10 < 0f)
					{
						num = -num;
					}
				}
				if (this.followRotation)
				{
					Vector3 eulerAngles = this.skeletonTransform.rotation.eulerAngles;
					transform.SetPositionAndRotation(position, Quaternion.Euler(eulerAngles.x, eulerAngles.y, eulerAngles.z + num));
				}
				else
				{
					transform.position = position;
				}
			}
			if (this.followSkeletonFlip)
			{
				Vector3 localScale = transform.localScale;
				localScale.y = Mathf.Abs(localScale.y) * ((!(this.bone.skeleton.flipX ^ this.bone.skeleton.flipY)) ? 1f : -1f);
				transform.localScale = localScale;
			}
		}

		[SerializeField]
		public SkeletonRenderer skeletonRenderer;

		[SpineSlot("", "skeletonRenderer", false, true, false)]
		public string slotName;

		[SpineAttachment(true, false, false, "slotName", "skeletonRenderer", "", true, true)]
		public string pointAttachmentName;

		public bool followRotation = true;

		public bool followSkeletonFlip = true;

		public bool followSkeletonZPosition;

		private Transform skeletonTransform;

		private bool skeletonTransformIsParent;

		private PointAttachment point;

		private Bone bone;

		private bool valid;
	}
}

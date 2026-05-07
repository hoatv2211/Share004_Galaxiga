using System;
using UnityEngine;

namespace Spine.Unity
{
	[ExecuteInEditMode]
	[DisallowMultipleComponent]
	[AddComponentMenu("Spine/UI/BoneFollowerGraphic")]
	public class BoneFollowerGraphic : MonoBehaviour
	{
		public SkeletonGraphic SkeletonGraphic
		{
			get
			{
				return this.skeletonGraphic;
			}
			set
			{
				this.skeletonGraphic = value;
				this.Initialize();
			}
		}

		public bool SetBone(string name)
		{
			this.bone = this.skeletonGraphic.Skeleton.FindBone(name);
			if (this.bone == null)
			{
				UnityEngine.Debug.LogError("Bone not found: " + name, this);
				return false;
			}
			this.boneName = name;
			return true;
		}

		public void Awake()
		{
			if (this.initializeOnAwake)
			{
				this.Initialize();
			}
		}

		public void Initialize()
		{
			this.bone = null;
			this.valid = (this.skeletonGraphic != null && this.skeletonGraphic.IsValid);
			if (!this.valid)
			{
				return;
			}
			this.skeletonTransform = this.skeletonGraphic.transform;
			this.skeletonTransformIsParent = object.ReferenceEquals(this.skeletonTransform, base.transform.parent);
			if (!string.IsNullOrEmpty(this.boneName))
			{
				this.bone = this.skeletonGraphic.Skeleton.FindBone(this.boneName);
			}
		}

		public void LateUpdate()
		{
			if (!this.valid)
			{
				this.Initialize();
				return;
			}
			if (this.bone == null)
			{
				if (string.IsNullOrEmpty(this.boneName))
				{
					return;
				}
				this.bone = this.skeletonGraphic.Skeleton.FindBone(this.boneName);
				if (!this.SetBone(this.boneName))
				{
					return;
				}
			}
			RectTransform rectTransform = base.transform as RectTransform;
			if (rectTransform == null)
			{
				return;
			}
			Canvas canvas = this.skeletonGraphic.canvas;
			if (canvas == null)
			{
				canvas = this.skeletonGraphic.GetComponentInParent<Canvas>();
			}
			float referencePixelsPerUnit = canvas.referencePixelsPerUnit;
			if (this.skeletonTransformIsParent)
			{
				rectTransform.localPosition = new Vector3(this.bone.worldX * referencePixelsPerUnit, this.bone.worldY * referencePixelsPerUnit, (!this.followZPosition) ? rectTransform.localPosition.z : 0f);
				if (this.followBoneRotation)
				{
					rectTransform.localRotation = this.bone.GetQuaternion();
				}
			}
			else
			{
				Vector3 position = this.skeletonTransform.TransformPoint(new Vector3(this.bone.worldX * referencePixelsPerUnit, this.bone.worldY * referencePixelsPerUnit, 0f));
				if (!this.followZPosition)
				{
					position.z = rectTransform.position.z;
				}
				float num = this.bone.WorldRotationX;
				Transform parent = rectTransform.parent;
				if (parent != null)
				{
					Matrix4x4 localToWorldMatrix = parent.localToWorldMatrix;
					if (localToWorldMatrix.m00 * localToWorldMatrix.m11 - localToWorldMatrix.m01 * localToWorldMatrix.m10 < 0f)
					{
						num = -num;
					}
				}
				if (this.followBoneRotation)
				{
					Vector3 eulerAngles = this.skeletonTransform.rotation.eulerAngles;
					rectTransform.SetPositionAndRotation(position, Quaternion.Euler(eulerAngles.x, eulerAngles.y, this.skeletonTransform.rotation.eulerAngles.z + num));
				}
				else
				{
					rectTransform.position = position;
				}
			}
			Vector3 localScale = (!this.followLocalScale) ? new Vector3(1f, 1f, 1f) : new Vector3(this.bone.scaleX, this.bone.scaleY, 1f);
			if (this.followSkeletonFlip)
			{
				localScale.y *= ((!(this.bone.skeleton.flipX ^ this.bone.skeleton.flipY)) ? 1f : -1f);
			}
			rectTransform.localScale = localScale;
		}

		public SkeletonGraphic skeletonGraphic;

		public bool initializeOnAwake = true;

		[SpineBone("", "skeletonGraphic", true, false)]
		[SerializeField]
		public string boneName;

		public bool followBoneRotation = true;

		[Tooltip("Follows the skeleton's flip state by controlling this Transform's local scale.")]
		public bool followSkeletonFlip = true;

		[Tooltip("Follows the target bone's local scale. BoneFollower cannot inherit world/skewed scale because of UnityEngine.Transform property limitations.")]
		public bool followLocalScale;

		public bool followZPosition = true;

		[NonSerialized]
		public Bone bone;

		private Transform skeletonTransform;

		private bool skeletonTransformIsParent;

		[NonSerialized]
		public bool valid;
	}
}

using System;
using UnityEngine;

namespace Spine.Unity
{
	[ExecuteInEditMode]
	[AddComponentMenu("Spine/SkeletonUtilityBone")]
	public class SkeletonUtilityBone : MonoBehaviour
	{
		public bool IncompatibleTransformMode
		{
			get
			{
				return this.incompatibleTransformMode;
			}
		}

		public void Reset()
		{
			this.bone = null;
			this.cachedTransform = base.transform;
			this.valid = (this.skeletonUtility != null && this.skeletonUtility.skeletonRenderer != null && this.skeletonUtility.skeletonRenderer.valid);
			if (!this.valid)
			{
				return;
			}
			this.skeletonTransform = this.skeletonUtility.transform;
			this.skeletonUtility.OnReset -= this.HandleOnReset;
			this.skeletonUtility.OnReset += this.HandleOnReset;
			this.DoUpdate(SkeletonUtilityBone.UpdatePhase.Local);
		}

		private void OnEnable()
		{
			this.skeletonUtility = base.transform.GetComponentInParent<SkeletonUtility>();
			if (this.skeletonUtility == null)
			{
				return;
			}
			this.skeletonUtility.RegisterBone(this);
			this.skeletonUtility.OnReset += this.HandleOnReset;
		}

		private void HandleOnReset()
		{
			this.Reset();
		}

		private void OnDisable()
		{
			if (this.skeletonUtility != null)
			{
				this.skeletonUtility.OnReset -= this.HandleOnReset;
				this.skeletonUtility.UnregisterBone(this);
			}
		}

		public void DoUpdate(SkeletonUtilityBone.UpdatePhase phase)
		{
			if (!this.valid)
			{
				this.Reset();
				return;
			}
			Skeleton skeleton = this.skeletonUtility.skeletonRenderer.skeleton;
			if (this.bone == null)
			{
				if (string.IsNullOrEmpty(this.boneName))
				{
					return;
				}
				this.bone = skeleton.FindBone(this.boneName);
				if (this.bone == null)
				{
					UnityEngine.Debug.LogError("Bone not found: " + this.boneName, this);
					return;
				}
			}
			Transform transform = this.cachedTransform;
			float num = (!(skeleton.flipX ^ skeleton.flipY)) ? 1f : -1f;
			if (this.mode == SkeletonUtilityBone.Mode.Follow)
			{
				if (phase != SkeletonUtilityBone.UpdatePhase.Local)
				{
					if (phase == SkeletonUtilityBone.UpdatePhase.World || phase == SkeletonUtilityBone.UpdatePhase.Complete)
					{
						if (!this.bone.appliedValid)
						{
							this.bone.UpdateAppliedTransform();
							if (this.position)
							{
								transform.localPosition = new Vector3(this.bone.ax, this.bone.ay, 0f);
							}
							if (this.rotation)
							{
								if (this.bone.data.transformMode.InheritsRotation())
								{
									transform.localRotation = Quaternion.Euler(0f, 0f, this.bone.AppliedRotation);
								}
								else
								{
									Vector3 eulerAngles = this.skeletonTransform.rotation.eulerAngles;
									transform.rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, eulerAngles.z + this.bone.WorldRotationX * num);
								}
							}
							if (this.scale)
							{
								transform.localScale = new Vector3(this.bone.ascaleX, this.bone.ascaleY, 1f);
								this.incompatibleTransformMode = SkeletonUtilityBone.BoneTransformModeIncompatible(this.bone);
							}
						}
					}
				}
				else
				{
					if (this.position)
					{
						transform.localPosition = new Vector3(this.bone.x, this.bone.y, 0f);
					}
					if (this.rotation)
					{
						if (this.bone.data.transformMode.InheritsRotation())
						{
							transform.localRotation = Quaternion.Euler(0f, 0f, this.bone.rotation);
						}
						else
						{
							Vector3 eulerAngles2 = this.skeletonTransform.rotation.eulerAngles;
							transform.rotation = Quaternion.Euler(eulerAngles2.x, eulerAngles2.y, eulerAngles2.z + this.bone.WorldRotationX * num);
						}
					}
					if (this.scale)
					{
						transform.localScale = new Vector3(this.bone.scaleX, this.bone.scaleY, 1f);
						this.incompatibleTransformMode = SkeletonUtilityBone.BoneTransformModeIncompatible(this.bone);
					}
				}
			}
			else if (this.mode == SkeletonUtilityBone.Mode.Override)
			{
				if (this.transformLerpComplete)
				{
					return;
				}
				if (this.parentReference == null)
				{
					if (this.position)
					{
						Vector3 localPosition = transform.localPosition;
						this.bone.x = Mathf.Lerp(this.bone.x, localPosition.x, this.overrideAlpha);
						this.bone.y = Mathf.Lerp(this.bone.y, localPosition.y, this.overrideAlpha);
					}
					if (this.rotation)
					{
						float appliedRotation = Mathf.LerpAngle(this.bone.Rotation, transform.localRotation.eulerAngles.z, this.overrideAlpha);
						this.bone.Rotation = appliedRotation;
						this.bone.AppliedRotation = appliedRotation;
					}
					if (this.scale)
					{
						Vector3 localScale = transform.localScale;
						this.bone.scaleX = Mathf.Lerp(this.bone.scaleX, localScale.x, this.overrideAlpha);
						this.bone.scaleY = Mathf.Lerp(this.bone.scaleY, localScale.y, this.overrideAlpha);
					}
				}
				else
				{
					if (this.transformLerpComplete)
					{
						return;
					}
					if (this.position)
					{
						Vector3 vector = this.parentReference.InverseTransformPoint(transform.position);
						this.bone.x = Mathf.Lerp(this.bone.x, vector.x, this.overrideAlpha);
						this.bone.y = Mathf.Lerp(this.bone.y, vector.y, this.overrideAlpha);
					}
					if (this.rotation)
					{
						float appliedRotation2 = Mathf.LerpAngle(this.bone.Rotation, Quaternion.LookRotation(Vector3.forward, this.parentReference.InverseTransformDirection(transform.up)).eulerAngles.z, this.overrideAlpha);
						this.bone.Rotation = appliedRotation2;
						this.bone.AppliedRotation = appliedRotation2;
					}
					if (this.scale)
					{
						Vector3 localScale2 = transform.localScale;
						this.bone.scaleX = Mathf.Lerp(this.bone.scaleX, localScale2.x, this.overrideAlpha);
						this.bone.scaleY = Mathf.Lerp(this.bone.scaleY, localScale2.y, this.overrideAlpha);
					}
					this.incompatibleTransformMode = SkeletonUtilityBone.BoneTransformModeIncompatible(this.bone);
				}
				this.transformLerpComplete = true;
			}
		}

		public static bool BoneTransformModeIncompatible(Bone bone)
		{
			return !bone.data.transformMode.InheritsScale();
		}

		public void AddBoundingBox(string skinName, string slotName, string attachmentName)
		{
			SkeletonUtility.AddBoundingBoxGameObject(this.bone.skeleton, skinName, slotName, attachmentName, base.transform, true);
		}

		public string boneName;

		public Transform parentReference;

		public SkeletonUtilityBone.Mode mode;

		public bool position;

		public bool rotation;

		public bool scale;

		public bool zPosition = true;

		[Range(0f, 1f)]
		public float overrideAlpha = 1f;

		[NonSerialized]
		public SkeletonUtility skeletonUtility;

		[NonSerialized]
		public Bone bone;

		[NonSerialized]
		public bool transformLerpComplete;

		[NonSerialized]
		public bool valid;

		private Transform cachedTransform;

		private Transform skeletonTransform;

		private bool incompatibleTransformMode;

		public enum Mode
		{
			Follow,
			Override
		}

		public enum UpdatePhase
		{
			Local,
			World,
			Complete
		}
	}
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Spine.Unity
{
	[RequireComponent(typeof(ISkeletonAnimation))]
	[ExecuteInEditMode]
	public class SkeletonUtility : MonoBehaviour
	{
		public static PolygonCollider2D AddBoundingBoxGameObject(Skeleton skeleton, string skinName, string slotName, string attachmentName, Transform parent, bool isTrigger = true)
		{
			Skin skin = (!string.IsNullOrEmpty(skinName)) ? skeleton.data.FindSkin(skinName) : skeleton.data.defaultSkin;
			if (skin == null)
			{
				UnityEngine.Debug.LogError("Skin " + skinName + " not found!");
				return null;
			}
			Attachment attachment = skin.GetAttachment(skeleton.FindSlotIndex(slotName), attachmentName);
			if (attachment == null)
			{
				UnityEngine.Debug.LogFormat("Attachment in slot '{0}' named '{1}' not found in skin '{2}'.", new object[]
				{
					slotName,
					attachmentName,
					skin.name
				});
				return null;
			}
			BoundingBoxAttachment boundingBoxAttachment = attachment as BoundingBoxAttachment;
			if (boundingBoxAttachment != null)
			{
				Slot slot = skeleton.FindSlot(slotName);
				return SkeletonUtility.AddBoundingBoxGameObject(boundingBoxAttachment.Name, boundingBoxAttachment, slot, parent, isTrigger);
			}
			UnityEngine.Debug.LogFormat("Attachment '{0}' was not a Bounding Box.", new object[]
			{
				attachmentName
			});
			return null;
		}

		public static PolygonCollider2D AddBoundingBoxGameObject(string name, BoundingBoxAttachment box, Slot slot, Transform parent, bool isTrigger = true)
		{
			GameObject gameObject = new GameObject("[BoundingBox]" + ((!string.IsNullOrEmpty(name)) ? name : box.Name));
			Transform transform = gameObject.transform;
			transform.parent = parent;
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = Vector3.one;
			return SkeletonUtility.AddBoundingBoxAsComponent(box, slot, gameObject, isTrigger, true, 0f);
		}

		public static PolygonCollider2D AddBoundingBoxAsComponent(BoundingBoxAttachment box, Slot slot, GameObject gameObject, bool isTrigger = true, bool isKinematic = true, float gravityScale = 0f)
		{
			if (box == null)
			{
				return null;
			}
			if (slot.bone != slot.Skeleton.RootBone)
			{
				Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
				if (rigidbody2D == null)
				{
					rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
					rigidbody2D.isKinematic = isKinematic;
					rigidbody2D.gravityScale = gravityScale;
				}
			}
			PolygonCollider2D polygonCollider2D = gameObject.AddComponent<PolygonCollider2D>();
			polygonCollider2D.isTrigger = isTrigger;
			SkeletonUtility.SetColliderPointsLocal(polygonCollider2D, slot, box);
			return polygonCollider2D;
		}

		public static void SetColliderPointsLocal(PolygonCollider2D collider, Slot slot, BoundingBoxAttachment box)
		{
			if (box == null)
			{
				return;
			}
			if (box.IsWeighted())
			{
				UnityEngine.Debug.LogWarning("UnityEngine.PolygonCollider2D does not support weighted or animated points. Collider points will not be animated and may have incorrect orientation. If you want to use it as a collider, please remove weights and animations from the bounding box in Spine editor.");
			}
			Vector2[] localVertices = box.GetLocalVertices(slot, null);
			collider.SetPath(0, localVertices);
		}

		public static Bounds GetBoundingBoxBounds(BoundingBoxAttachment boundingBox, float depth = 0f)
		{
			float[] vertices = boundingBox.Vertices;
			int num = vertices.Length;
			Bounds result = default(Bounds);
			result.center = new Vector3(vertices[0], vertices[1], 0f);
			for (int i = 2; i < num; i += 2)
			{
				result.Encapsulate(new Vector3(vertices[i], vertices[i + 1], 0f));
			}
			Vector3 size = result.size;
			size.z = depth;
			result.size = size;
			return result;
		}

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event SkeletonUtility.SkeletonUtilityDelegate OnReset;

		private void Update()
		{
			Skeleton skeleton = this.skeletonRenderer.skeleton;
			if (this.boneRoot != null && skeleton != null)
			{
				Vector3 one = Vector3.one;
				if (skeleton.FlipX)
				{
					one.x = -1f;
				}
				if (skeleton.FlipY)
				{
					one.y = -1f;
				}
				this.boneRoot.localScale = one;
			}
		}

		private void OnEnable()
		{
			if (this.skeletonRenderer == null)
			{
				this.skeletonRenderer = base.GetComponent<SkeletonRenderer>();
			}
			if (this.skeletonAnimation == null)
			{
				this.skeletonAnimation = base.GetComponent<SkeletonAnimation>();
				if (this.skeletonAnimation == null)
				{
					this.skeletonAnimation = base.GetComponent<SkeletonAnimator>();
				}
			}
			this.skeletonRenderer.OnRebuild -= this.HandleRendererReset;
			this.skeletonRenderer.OnRebuild += this.HandleRendererReset;
			if (this.skeletonAnimation != null)
			{
				this.skeletonAnimation.UpdateLocal -= this.UpdateLocal;
				this.skeletonAnimation.UpdateLocal += this.UpdateLocal;
			}
			this.CollectBones();
		}

		private void Start()
		{
			this.CollectBones();
		}

		private void OnDisable()
		{
			this.skeletonRenderer.OnRebuild -= this.HandleRendererReset;
			if (this.skeletonAnimation != null)
			{
				this.skeletonAnimation.UpdateLocal -= this.UpdateLocal;
				this.skeletonAnimation.UpdateWorld -= this.UpdateWorld;
				this.skeletonAnimation.UpdateComplete -= this.UpdateComplete;
			}
		}

		private void HandleRendererReset(SkeletonRenderer r)
		{
			if (this.OnReset != null)
			{
				this.OnReset();
			}
			this.CollectBones();
		}

		public void RegisterBone(SkeletonUtilityBone bone)
		{
			if (this.utilityBones.Contains(bone))
			{
				return;
			}
			this.utilityBones.Add(bone);
			this.needToReprocessBones = true;
		}

		public void UnregisterBone(SkeletonUtilityBone bone)
		{
			this.utilityBones.Remove(bone);
		}

		public void RegisterConstraint(SkeletonUtilityConstraint constraint)
		{
			if (this.utilityConstraints.Contains(constraint))
			{
				return;
			}
			this.utilityConstraints.Add(constraint);
			this.needToReprocessBones = true;
		}

		public void UnregisterConstraint(SkeletonUtilityConstraint constraint)
		{
			this.utilityConstraints.Remove(constraint);
		}

		public void CollectBones()
		{
			Skeleton skeleton = this.skeletonRenderer.skeleton;
			if (skeleton == null)
			{
				return;
			}
			if (this.boneRoot != null)
			{
				List<object> list = new List<object>();
				ExposedList<IkConstraint> ikConstraints = skeleton.IkConstraints;
				int i = 0;
				int count = ikConstraints.Count;
				while (i < count)
				{
					list.Add(ikConstraints.Items[i].target);
					i++;
				}
				ExposedList<TransformConstraint> transformConstraints = skeleton.TransformConstraints;
				int j = 0;
				int count2 = transformConstraints.Count;
				while (j < count2)
				{
					list.Add(transformConstraints.Items[j].target);
					j++;
				}
				List<SkeletonUtilityBone> list2 = this.utilityBones;
				int k = 0;
				int count3 = list2.Count;
				while (k < count3)
				{
					SkeletonUtilityBone skeletonUtilityBone = list2[k];
					if (skeletonUtilityBone.bone != null)
					{
						this.hasTransformBones |= (skeletonUtilityBone.mode == SkeletonUtilityBone.Mode.Override);
						this.hasUtilityConstraints |= list.Contains(skeletonUtilityBone.bone);
					}
					k++;
				}
				this.hasUtilityConstraints |= (this.utilityConstraints.Count > 0);
				if (this.skeletonAnimation != null)
				{
					this.skeletonAnimation.UpdateWorld -= this.UpdateWorld;
					this.skeletonAnimation.UpdateComplete -= this.UpdateComplete;
					if (this.hasTransformBones || this.hasUtilityConstraints)
					{
						this.skeletonAnimation.UpdateWorld += this.UpdateWorld;
					}
					if (this.hasUtilityConstraints)
					{
						this.skeletonAnimation.UpdateComplete += this.UpdateComplete;
					}
				}
				this.needToReprocessBones = false;
			}
			else
			{
				this.utilityBones.Clear();
				this.utilityConstraints.Clear();
			}
		}

		private void UpdateLocal(ISkeletonAnimation anim)
		{
			if (this.needToReprocessBones)
			{
				this.CollectBones();
			}
			List<SkeletonUtilityBone> list = this.utilityBones;
			if (list == null)
			{
				return;
			}
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				list[i].transformLerpComplete = false;
				i++;
			}
			this.UpdateAllBones(SkeletonUtilityBone.UpdatePhase.Local);
		}

		private void UpdateWorld(ISkeletonAnimation anim)
		{
			this.UpdateAllBones(SkeletonUtilityBone.UpdatePhase.World);
			int i = 0;
			int count = this.utilityConstraints.Count;
			while (i < count)
			{
				this.utilityConstraints[i].DoUpdate();
				i++;
			}
		}

		private void UpdateComplete(ISkeletonAnimation anim)
		{
			this.UpdateAllBones(SkeletonUtilityBone.UpdatePhase.Complete);
		}

		private void UpdateAllBones(SkeletonUtilityBone.UpdatePhase phase)
		{
			if (this.boneRoot == null)
			{
				this.CollectBones();
			}
			List<SkeletonUtilityBone> list = this.utilityBones;
			if (list == null)
			{
				return;
			}
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				list[i].DoUpdate(phase);
				i++;
			}
		}

		public Transform GetBoneRoot()
		{
			if (this.boneRoot != null)
			{
				return this.boneRoot;
			}
			this.boneRoot = new GameObject("SkeletonUtility-Root").transform;
			this.boneRoot.parent = base.transform;
			this.boneRoot.localPosition = Vector3.zero;
			this.boneRoot.localRotation = Quaternion.identity;
			this.boneRoot.localScale = Vector3.one;
			return this.boneRoot;
		}

		public GameObject SpawnRoot(SkeletonUtilityBone.Mode mode, bool pos, bool rot, bool sca)
		{
			this.GetBoneRoot();
			Skeleton skeleton = this.skeletonRenderer.skeleton;
			GameObject result = this.SpawnBone(skeleton.RootBone, this.boneRoot, mode, pos, rot, sca);
			this.CollectBones();
			return result;
		}

		public GameObject SpawnHierarchy(SkeletonUtilityBone.Mode mode, bool pos, bool rot, bool sca)
		{
			this.GetBoneRoot();
			Skeleton skeleton = this.skeletonRenderer.skeleton;
			GameObject result = this.SpawnBoneRecursively(skeleton.RootBone, this.boneRoot, mode, pos, rot, sca);
			this.CollectBones();
			return result;
		}

		public GameObject SpawnBoneRecursively(Bone bone, Transform parent, SkeletonUtilityBone.Mode mode, bool pos, bool rot, bool sca)
		{
			GameObject gameObject = this.SpawnBone(bone, parent, mode, pos, rot, sca);
			ExposedList<Bone> children = bone.Children;
			int i = 0;
			int count = children.Count;
			while (i < count)
			{
				Bone bone2 = children.Items[i];
				this.SpawnBoneRecursively(bone2, gameObject.transform, mode, pos, rot, sca);
				i++;
			}
			return gameObject;
		}

		public GameObject SpawnBone(Bone bone, Transform parent, SkeletonUtilityBone.Mode mode, bool pos, bool rot, bool sca)
		{
			GameObject gameObject = new GameObject(bone.Data.Name);
			gameObject.transform.parent = parent;
			SkeletonUtilityBone skeletonUtilityBone = gameObject.AddComponent<SkeletonUtilityBone>();
			skeletonUtilityBone.skeletonUtility = this;
			skeletonUtilityBone.position = pos;
			skeletonUtilityBone.rotation = rot;
			skeletonUtilityBone.scale = sca;
			skeletonUtilityBone.mode = mode;
			skeletonUtilityBone.zPosition = true;
			skeletonUtilityBone.Reset();
			skeletonUtilityBone.bone = bone;
			skeletonUtilityBone.boneName = bone.Data.Name;
			skeletonUtilityBone.valid = true;
			if (mode == SkeletonUtilityBone.Mode.Override)
			{
				if (rot)
				{
					gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, skeletonUtilityBone.bone.AppliedRotation);
				}
				if (pos)
				{
					gameObject.transform.localPosition = new Vector3(skeletonUtilityBone.bone.X, skeletonUtilityBone.bone.Y, 0f);
				}
				gameObject.transform.localScale = new Vector3(skeletonUtilityBone.bone.scaleX, skeletonUtilityBone.bone.scaleY, 0f);
			}
			return gameObject;
		}

		public Transform boneRoot;

		[HideInInspector]
		public SkeletonRenderer skeletonRenderer;

		[HideInInspector]
		public ISkeletonAnimation skeletonAnimation;

		[NonSerialized]
		public List<SkeletonUtilityBone> utilityBones = new List<SkeletonUtilityBone>();

		[NonSerialized]
		public List<SkeletonUtilityConstraint> utilityConstraints = new List<SkeletonUtilityConstraint>();

		protected bool hasTransformBones;

		protected bool hasUtilityConstraints;

		protected bool needToReprocessBones;

		public delegate void SkeletonUtilityDelegate();
	}
}

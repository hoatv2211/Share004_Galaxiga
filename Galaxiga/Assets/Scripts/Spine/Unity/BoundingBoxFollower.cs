using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spine.Unity
{
	[ExecuteInEditMode]
	public class BoundingBoxFollower : MonoBehaviour
	{
		public Slot Slot
		{
			get
			{
				return this.slot;
			}
		}

		public BoundingBoxAttachment CurrentAttachment
		{
			get
			{
				return this.currentAttachment;
			}
		}

		public string CurrentAttachmentName
		{
			get
			{
				return this.currentAttachmentName;
			}
		}

		public PolygonCollider2D CurrentCollider
		{
			get
			{
				return this.currentCollider;
			}
		}

		public bool IsTrigger
		{
			get
			{
				return this.isTrigger;
			}
		}

		private void Start()
		{
			this.Initialize(false);
		}

		private void OnEnable()
		{
			if (this.skeletonRenderer != null)
			{
				this.skeletonRenderer.OnRebuild -= this.HandleRebuild;
				this.skeletonRenderer.OnRebuild += this.HandleRebuild;
			}
			this.Initialize(false);
		}

		private void HandleRebuild(SkeletonRenderer sr)
		{
			this.Initialize(false);
		}

		public void Initialize(bool overwrite = false)
		{
			if (this.skeletonRenderer == null)
			{
				return;
			}
			this.skeletonRenderer.Initialize(false);
			if (string.IsNullOrEmpty(this.slotName))
			{
				return;
			}
			if (!overwrite && this.colliderTable.Count > 0 && this.slot != null && this.skeletonRenderer.skeleton == this.slot.Skeleton && this.slotName == this.slot.data.name)
			{
				return;
			}
			this.DisposeColliders();
			Skeleton skeleton = this.skeletonRenderer.skeleton;
			this.slot = skeleton.FindSlot(this.slotName);
			int slotIndex = skeleton.FindSlotIndex(this.slotName);
			if (this.slot == null)
			{
				if (BoundingBoxFollower.DebugMessages)
				{
					UnityEngine.Debug.LogWarning(string.Format("Slot '{0}' not found for BoundingBoxFollower on '{1}'. (Previous colliders were disposed.)", this.slotName, base.gameObject.name));
				}
				return;
			}
			if (base.gameObject.activeInHierarchy)
			{
				foreach (Skin skin in skeleton.Data.Skins)
				{
					this.AddSkin(skin, slotIndex);
				}
				if (skeleton.skin != null)
				{
					this.AddSkin(skeleton.skin, slotIndex);
				}
			}
			if (BoundingBoxFollower.DebugMessages && this.colliderTable.Count == 0)
			{
				if (base.gameObject.activeInHierarchy)
				{
					UnityEngine.Debug.LogWarning("Bounding Box Follower not valid! Slot [" + this.slotName + "] does not contain any Bounding Box Attachments!");
				}
				else
				{
					UnityEngine.Debug.LogWarning("Bounding Box Follower tried to rebuild as a prefab.");
				}
			}
		}

		private void AddSkin(Skin skin, int slotIndex)
		{
			if (skin == null)
			{
				return;
			}
			List<string> list = new List<string>();
			skin.FindNamesForSlot(slotIndex, list);
			foreach (string text in list)
			{
				Attachment attachment = skin.GetAttachment(slotIndex, text);
				BoundingBoxAttachment boundingBoxAttachment = attachment as BoundingBoxAttachment;
				if (BoundingBoxFollower.DebugMessages && attachment != null && boundingBoxAttachment == null)
				{
					UnityEngine.Debug.Log("BoundingBoxFollower tried to follow a slot that contains non-boundingbox attachments: " + this.slotName);
				}
				if (boundingBoxAttachment != null && !this.colliderTable.ContainsKey(boundingBoxAttachment))
				{
					PolygonCollider2D polygonCollider2D = SkeletonUtility.AddBoundingBoxAsComponent(boundingBoxAttachment, this.slot, base.gameObject, this.isTrigger, true, 0f);
					polygonCollider2D.enabled = false;
					polygonCollider2D.hideFlags = HideFlags.NotEditable;
					polygonCollider2D.isTrigger = this.IsTrigger;
					this.colliderTable.Add(boundingBoxAttachment, polygonCollider2D);
					this.nameTable.Add(boundingBoxAttachment, text);
				}
			}
		}

		private void OnDisable()
		{
			if (this.clearStateOnDisable)
			{
				this.ClearState();
			}
		}

		public void ClearState()
		{
			if (this.colliderTable != null)
			{
				foreach (PolygonCollider2D polygonCollider2D in this.colliderTable.Values)
				{
					polygonCollider2D.enabled = false;
				}
			}
			this.currentAttachment = null;
			this.currentAttachmentName = null;
			this.currentCollider = null;
		}

		private void DisposeColliders()
		{
			PolygonCollider2D[] components = base.GetComponents<PolygonCollider2D>();
			if (components.Length == 0)
			{
				return;
			}
			if (Application.isEditor)
			{
				if (Application.isPlaying)
				{
					foreach (PolygonCollider2D polygonCollider2D in components)
					{
						if (polygonCollider2D != null)
						{
							UnityEngine.Object.Destroy(polygonCollider2D);
						}
					}
				}
				else
				{
					foreach (PolygonCollider2D polygonCollider2D2 in components)
					{
						if (polygonCollider2D2 != null)
						{
							UnityEngine.Object.DestroyImmediate(polygonCollider2D2);
						}
					}
				}
			}
			else
			{
				foreach (PolygonCollider2D polygonCollider2D3 in components)
				{
					if (polygonCollider2D3 != null)
					{
						UnityEngine.Object.Destroy(polygonCollider2D3);
					}
				}
			}
			this.slot = null;
			this.currentAttachment = null;
			this.currentAttachmentName = null;
			this.currentCollider = null;
			this.colliderTable.Clear();
			this.nameTable.Clear();
		}

		private void LateUpdate()
		{
			if (this.slot != null && this.slot.Attachment != this.currentAttachment)
			{
				this.MatchAttachment(this.slot.Attachment);
			}
		}

		private void MatchAttachment(Attachment attachment)
		{
			BoundingBoxAttachment boundingBoxAttachment = attachment as BoundingBoxAttachment;
			if (BoundingBoxFollower.DebugMessages && attachment != null && boundingBoxAttachment == null)
			{
				UnityEngine.Debug.LogWarning("BoundingBoxFollower tried to match a non-boundingbox attachment. It will treat it as null.");
			}
			if (this.currentCollider != null)
			{
				this.currentCollider.enabled = false;
			}
			if (boundingBoxAttachment == null)
			{
				this.currentCollider = null;
				this.currentAttachment = null;
				this.currentAttachmentName = null;
			}
			else
			{
				PolygonCollider2D x;
				this.colliderTable.TryGetValue(boundingBoxAttachment, out x);
				if (x != null)
				{
					this.currentCollider = x;
					this.currentCollider.enabled = true;
					this.currentAttachment = boundingBoxAttachment;
					this.currentAttachmentName = this.nameTable[boundingBoxAttachment];
				}
				else
				{
					this.currentCollider = null;
					this.currentAttachment = boundingBoxAttachment;
					this.currentAttachmentName = null;
					if (BoundingBoxFollower.DebugMessages)
					{
						UnityEngine.Debug.LogFormat("Collider for BoundingBoxAttachment named '{0}' was not initialized. It is possibly from a new skin. currentAttachmentName will be null. You may need to call BoundingBoxFollower.Initialize(overwrite: true);", new object[]
						{
							boundingBoxAttachment.Name
						});
					}
				}
			}
		}

		internal static bool DebugMessages = true;

		public SkeletonRenderer skeletonRenderer;

		[SpineSlot("", "skeletonRenderer", true, true, false)]
		public string slotName;

		public bool isTrigger;

		public bool clearStateOnDisable = true;

		private Slot slot;

		private BoundingBoxAttachment currentAttachment;

		private string currentAttachmentName;

		private PolygonCollider2D currentCollider;

		public readonly Dictionary<BoundingBoxAttachment, PolygonCollider2D> colliderTable = new Dictionary<BoundingBoxAttachment, PolygonCollider2D>();

		public readonly Dictionary<BoundingBoxAttachment, string> nameTable = new Dictionary<BoundingBoxAttachment, string>();
	}
}

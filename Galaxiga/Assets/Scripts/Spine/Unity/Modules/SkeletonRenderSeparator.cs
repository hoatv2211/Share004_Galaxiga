using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Spine.Unity.Modules
{
	[ExecuteInEditMode]
	[HelpURL("https://github.com/pharan/spine-unity-docs/blob/master/SkeletonRenderSeparator.md")]
	public class SkeletonRenderSeparator : MonoBehaviour
	{
		public SkeletonRenderer SkeletonRenderer
		{
			get
			{
				return this.skeletonRenderer;
			}
			set
			{
				if (this.skeletonRenderer != null)
				{
					this.skeletonRenderer.GenerateMeshOverride -= this.HandleRender;
				}
				this.skeletonRenderer = value;
				base.enabled = false;
			}
		}

		public static SkeletonRenderSeparator AddToSkeletonRenderer(SkeletonRenderer skeletonRenderer, int sortingLayerID = 0, int extraPartsRenderers = 0, int sortingOrderIncrement = 5, int baseSortingOrder = 0, bool addMinimumPartsRenderers = true)
		{
			if (skeletonRenderer == null)
			{
				UnityEngine.Debug.Log("Tried to add SkeletonRenderSeparator to a null SkeletonRenderer reference.");
				return null;
			}
			SkeletonRenderSeparator skeletonRenderSeparator = skeletonRenderer.gameObject.AddComponent<SkeletonRenderSeparator>();
			skeletonRenderSeparator.skeletonRenderer = skeletonRenderer;
			skeletonRenderer.Initialize(false);
			int num = extraPartsRenderers;
			if (addMinimumPartsRenderers)
			{
				num = extraPartsRenderers + skeletonRenderer.separatorSlots.Count + 1;
			}
			Transform transform = skeletonRenderer.transform;
			List<SkeletonPartsRenderer> list = skeletonRenderSeparator.partsRenderers;
			for (int i = 0; i < num; i++)
			{
				SkeletonPartsRenderer skeletonPartsRenderer = SkeletonPartsRenderer.NewPartsRendererGameObject(transform, i.ToString());
				MeshRenderer meshRenderer = skeletonPartsRenderer.MeshRenderer;
				meshRenderer.sortingLayerID = sortingLayerID;
				meshRenderer.sortingOrder = baseSortingOrder + i * sortingOrderIncrement;
				list.Add(skeletonPartsRenderer);
			}
			return skeletonRenderSeparator;
		}

		public void AddPartsRenderer(int sortingOrderIncrement = 5)
		{
			int sortingLayerID = 0;
			int sortingOrder = 0;
			if (this.partsRenderers.Count > 0)
			{
				SkeletonPartsRenderer skeletonPartsRenderer = this.partsRenderers[this.partsRenderers.Count - 1];
				MeshRenderer meshRenderer = skeletonPartsRenderer.MeshRenderer;
				sortingLayerID = meshRenderer.sortingLayerID;
				sortingOrder = meshRenderer.sortingOrder + sortingOrderIncrement;
			}
			SkeletonPartsRenderer skeletonPartsRenderer2 = SkeletonPartsRenderer.NewPartsRendererGameObject(this.skeletonRenderer.transform, this.partsRenderers.Count.ToString());
			this.partsRenderers.Add(skeletonPartsRenderer2);
			MeshRenderer meshRenderer2 = skeletonPartsRenderer2.MeshRenderer;
			meshRenderer2.sortingLayerID = sortingLayerID;
			meshRenderer2.sortingOrder = sortingOrder;
		}

		private void OnEnable()
		{
			if (this.skeletonRenderer == null)
			{
				return;
			}
			if (this.copiedBlock == null)
			{
				this.copiedBlock = new MaterialPropertyBlock();
			}
			this.mainMeshRenderer = this.skeletonRenderer.GetComponent<MeshRenderer>();
			this.skeletonRenderer.GenerateMeshOverride -= this.HandleRender;
			this.skeletonRenderer.GenerateMeshOverride += this.HandleRender;
			if (this.copyMeshRendererFlags)
			{
				LightProbeUsage lightProbeUsage = this.mainMeshRenderer.lightProbeUsage;
				bool receiveShadows = this.mainMeshRenderer.receiveShadows;
				ReflectionProbeUsage reflectionProbeUsage = this.mainMeshRenderer.reflectionProbeUsage;
				ShadowCastingMode shadowCastingMode = this.mainMeshRenderer.shadowCastingMode;
				MotionVectorGenerationMode motionVectorGenerationMode = this.mainMeshRenderer.motionVectorGenerationMode;
				Transform probeAnchor = this.mainMeshRenderer.probeAnchor;
				for (int i = 0; i < this.partsRenderers.Count; i++)
				{
					SkeletonPartsRenderer skeletonPartsRenderer = this.partsRenderers[i];
					if (!(skeletonPartsRenderer == null))
					{
						MeshRenderer meshRenderer = skeletonPartsRenderer.MeshRenderer;
						meshRenderer.lightProbeUsage = lightProbeUsage;
						meshRenderer.receiveShadows = receiveShadows;
						meshRenderer.reflectionProbeUsage = reflectionProbeUsage;
						meshRenderer.shadowCastingMode = shadowCastingMode;
						meshRenderer.motionVectorGenerationMode = motionVectorGenerationMode;
						meshRenderer.probeAnchor = probeAnchor;
					}
				}
			}
		}

		private void OnDisable()
		{
			if (this.skeletonRenderer == null)
			{
				return;
			}
			this.skeletonRenderer.GenerateMeshOverride -= this.HandleRender;
			foreach (SkeletonPartsRenderer skeletonPartsRenderer in this.partsRenderers)
			{
				skeletonPartsRenderer.ClearMesh();
			}
		}

		private void HandleRender(SkeletonRendererInstruction instruction)
		{
			int count = this.partsRenderers.Count;
			if (count <= 0)
			{
				return;
			}
			if (this.copyPropertyBlock)
			{
				this.mainMeshRenderer.GetPropertyBlock(this.copiedBlock);
			}
			MeshGenerator.Settings settings = new MeshGenerator.Settings
			{
				addNormals = this.skeletonRenderer.addNormals,
				calculateTangents = this.skeletonRenderer.calculateTangents,
				immutableTriangles = false,
				pmaVertexColors = this.skeletonRenderer.pmaVertexColors,
				tintBlack = this.skeletonRenderer.tintBlack,
				useClipping = true,
				zSpacing = this.skeletonRenderer.zSpacing
			};
			ExposedList<SubmeshInstruction> submeshInstructions = instruction.submeshInstructions;
			SubmeshInstruction[] items = submeshInstructions.Items;
			int num = submeshInstructions.Count - 1;
			int i = 0;
			SkeletonPartsRenderer skeletonPartsRenderer = this.partsRenderers[i];
			int j = 0;
			int startSubmesh = 0;
			while (j <= num)
			{
				if (items[j].forceSeparate || j == num)
				{
					MeshGenerator meshGenerator = skeletonPartsRenderer.MeshGenerator;
					meshGenerator.settings = settings;
					if (this.copyPropertyBlock)
					{
						skeletonPartsRenderer.SetPropertyBlock(this.copiedBlock);
					}
					skeletonPartsRenderer.RenderParts(instruction.submeshInstructions, startSubmesh, j + 1);
					startSubmesh = j + 1;
					i++;
					if (i >= count)
					{
						break;
					}
					skeletonPartsRenderer = this.partsRenderers[i];
				}
				j++;
			}
			while (i < count)
			{
				this.partsRenderers[i].ClearMesh();
				i++;
			}
		}

		public const int DefaultSortingOrderIncrement = 5;

		[SerializeField]
		protected SkeletonRenderer skeletonRenderer;

		private MeshRenderer mainMeshRenderer;

		public bool copyPropertyBlock = true;

		[Tooltip("Copies MeshRenderer flags into each parts renderer")]
		public bool copyMeshRendererFlags = true;

		public List<SkeletonPartsRenderer> partsRenderers = new List<SkeletonPartsRenderer>();

		private MaterialPropertyBlock copiedBlock;
	}
}

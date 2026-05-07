using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Serialization;

namespace Spine.Unity
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
	[DisallowMultipleComponent]
	[HelpURL("http://esotericsoftware.com/spine-unity-documentation#Rendering")]
	public class SkeletonRenderer : MonoBehaviour, ISkeletonComponent, IHasSkeletonDataAsset
	{
		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event SkeletonRenderer.SkeletonRendererDelegate OnRebuild;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event MeshGeneratorDelegate OnPostProcessVertices;

		public SkeletonDataAsset SkeletonDataAsset
		{
			get
			{
				return this.skeletonDataAsset;
			}
		}

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private event SkeletonRenderer.InstructionDelegate generateMeshOverride;

		public event SkeletonRenderer.InstructionDelegate GenerateMeshOverride;

		public Dictionary<Material, Material> CustomMaterialOverride
		{
			get
			{
				return this.customMaterialOverride;
			}
		}

		public Dictionary<Slot, Material> CustomSlotMaterials
		{
			get
			{
				return this.customSlotMaterials;
			}
		}

		public Skeleton Skeleton
		{
			get
			{
				this.Initialize(false);
				return this.skeleton;
			}
		}

		public static T NewSpineGameObject<T>(SkeletonDataAsset skeletonDataAsset) where T : SkeletonRenderer
		{
			return SkeletonRenderer.AddSpineComponent<T>(new GameObject("New Spine GameObject"), skeletonDataAsset);
		}

		public static T AddSpineComponent<T>(GameObject gameObject, SkeletonDataAsset skeletonDataAsset) where T : SkeletonRenderer
		{
			T t = gameObject.AddComponent<T>();
			if (skeletonDataAsset != null)
			{
				t.skeletonDataAsset = skeletonDataAsset;
				t.Initialize(false);
			}
			return t;
		}

		public void SetMeshSettings(MeshGenerator.Settings settings)
		{
			this.calculateTangents = settings.calculateTangents;
			this.immutableTriangles = settings.immutableTriangles;
			this.pmaVertexColors = settings.pmaVertexColors;
			this.tintBlack = settings.tintBlack;
			this.useClipping = settings.useClipping;
			this.zSpacing = settings.zSpacing;
			this.meshGenerator.settings = settings;
		}

		public virtual void Awake()
		{
			this.Initialize(false);
		}

		private void OnDisable()
		{
			if (this.clearStateOnDisable && this.valid)
			{
				this.ClearState();
			}
		}

		private void OnDestroy()
		{
			this.rendererBuffers.Dispose();
			this.valid = false;
		}

		public virtual void ClearState()
		{
			this.meshFilter.sharedMesh = null;
			this.currentInstructions.Clear();
			if (this.skeleton != null)
			{
				this.skeleton.SetToSetupPose();
			}
		}

		public virtual void Initialize(bool overwrite)
		{
			if (this.valid && !overwrite)
			{
				return;
			}
			if (this.meshFilter != null)
			{
				this.meshFilter.sharedMesh = null;
			}
			this.meshRenderer = base.GetComponent<MeshRenderer>();
			if (this.meshRenderer != null)
			{
				this.meshRenderer.sharedMaterial = null;
			}
			this.currentInstructions.Clear();
			this.rendererBuffers.Clear();
			this.meshGenerator.Begin();
			this.skeleton = null;
			this.valid = false;
			if (!this.skeletonDataAsset)
			{
				if (this.logErrors)
				{
					UnityEngine.Debug.LogError("Missing SkeletonData asset.", this);
				}
				return;
			}
			SkeletonData skeletonData = this.skeletonDataAsset.GetSkeletonData(false);
			if (skeletonData == null)
			{
				return;
			}
			this.valid = true;
			this.meshFilter = base.GetComponent<MeshFilter>();
			this.meshRenderer = base.GetComponent<MeshRenderer>();
			this.rendererBuffers.Initialize();
			this.skeleton = new Skeleton(skeletonData)
			{
				flipX = this.initialFlipX,
				flipY = this.initialFlipY
			};
			if (!string.IsNullOrEmpty(this.initialSkinName) && !string.Equals(this.initialSkinName, "default", StringComparison.Ordinal))
			{
				this.skeleton.SetSkin(this.initialSkinName);
			}
			this.separatorSlots.Clear();
			for (int i = 0; i < this.separatorSlotNames.Length; i++)
			{
				this.separatorSlots.Add(this.skeleton.FindSlot(this.separatorSlotNames[i]));
			}
			this.LateUpdate();
			if (this.OnRebuild != null)
			{
				this.OnRebuild(this);
			}
		}

		public virtual void LateUpdate()
		{
			if (!this.valid)
			{
				return;
			}
			bool flag = this.generateMeshOverride != null;
			if (!this.meshRenderer.enabled && !flag)
			{
				return;
			}
			SkeletonRendererInstruction skeletonRendererInstruction = this.currentInstructions;
			ExposedList<SubmeshInstruction> submeshInstructions = skeletonRendererInstruction.submeshInstructions;
			MeshRendererBuffers.SmartMesh nextMesh = this.rendererBuffers.GetNextMesh();
			bool flag2;
			if (this.singleSubmesh)
			{
				MeshGenerator.GenerateSingleSubmeshInstruction(skeletonRendererInstruction, this.skeleton, this.skeletonDataAsset.atlasAssets[0].materials[0]);
				if (this.customMaterialOverride.Count > 0)
				{
					MeshGenerator.TryReplaceMaterials(submeshInstructions, this.customMaterialOverride);
				}
				this.meshGenerator.settings = new MeshGenerator.Settings
				{
					pmaVertexColors = this.pmaVertexColors,
					zSpacing = this.zSpacing,
					useClipping = this.useClipping,
					tintBlack = this.tintBlack,
					calculateTangents = this.calculateTangents,
					addNormals = this.addNormals
				};
				this.meshGenerator.Begin();
				flag2 = SkeletonRendererInstruction.GeometryNotEqual(skeletonRendererInstruction, nextMesh.instructionUsed);
				if (skeletonRendererInstruction.hasActiveClipping)
				{
					this.meshGenerator.AddSubmesh(submeshInstructions.Items[0], flag2);
				}
				else
				{
					this.meshGenerator.BuildMeshWithArrays(skeletonRendererInstruction, flag2);
				}
			}
			else
			{
				MeshGenerator.GenerateSkeletonRendererInstruction(skeletonRendererInstruction, this.skeleton, this.customSlotMaterials, this.separatorSlots, flag, this.immutableTriangles);
				if (this.customMaterialOverride.Count > 0)
				{
					MeshGenerator.TryReplaceMaterials(submeshInstructions, this.customMaterialOverride);
				}
				if (flag)
				{
					this.generateMeshOverride(skeletonRendererInstruction);
					if (this.disableRenderingOnOverride)
					{
						return;
					}
				}
				flag2 = SkeletonRendererInstruction.GeometryNotEqual(skeletonRendererInstruction, nextMesh.instructionUsed);
				this.meshGenerator.settings = new MeshGenerator.Settings
				{
					pmaVertexColors = this.pmaVertexColors,
					zSpacing = this.zSpacing,
					useClipping = this.useClipping,
					tintBlack = this.tintBlack,
					calculateTangents = this.calculateTangents,
					addNormals = this.addNormals
				};
				this.meshGenerator.Begin();
				if (skeletonRendererInstruction.hasActiveClipping)
				{
					this.meshGenerator.BuildMesh(skeletonRendererInstruction, flag2);
				}
				else
				{
					this.meshGenerator.BuildMeshWithArrays(skeletonRendererInstruction, flag2);
				}
			}
			if (this.OnPostProcessVertices != null)
			{
				this.OnPostProcessVertices(this.meshGenerator.Buffers);
			}
			Mesh mesh = nextMesh.mesh;
			this.meshGenerator.FillVertexData(mesh);
			this.rendererBuffers.UpdateSharedMaterials(submeshInstructions);
			if (flag2)
			{
				this.meshGenerator.FillTriangles(mesh);
				this.meshRenderer.sharedMaterials = this.rendererBuffers.GetUpdatedSharedMaterialsArray();
			}
			else if (this.rendererBuffers.MaterialsChangedInLastUpdate())
			{
				this.meshRenderer.sharedMaterials = this.rendererBuffers.GetUpdatedSharedMaterialsArray();
			}
			this.meshGenerator.FillLateVertexData(mesh);
			this.meshFilter.sharedMesh = mesh;
			nextMesh.instructionUsed.Set(skeletonRendererInstruction);
		}

		public SkeletonDataAsset skeletonDataAsset;

		public string initialSkinName;

		public bool initialFlipX;

		public bool initialFlipY;

		[FormerlySerializedAs("submeshSeparators")]
		[SpineSlot("", "", false, true, false)]
		public string[] separatorSlotNames = new string[0];

		[NonSerialized]
		public readonly List<Slot> separatorSlots = new List<Slot>();

		[Range(-0.1f, 0f)]
		public float zSpacing;

		public bool useClipping = true;

		public bool immutableTriangles;

		public bool pmaVertexColors = true;

		public bool clearStateOnDisable;

		public bool tintBlack;

		public bool singleSubmesh;

		[FormerlySerializedAs("calculateNormals")]
		public bool addNormals;

		public bool calculateTangents;

		public bool logErrors;

		public bool disableRenderingOnOverride = true;

		[NonSerialized]
		private readonly Dictionary<Material, Material> customMaterialOverride = new Dictionary<Material, Material>();

		[NonSerialized]
		private readonly Dictionary<Slot, Material> customSlotMaterials = new Dictionary<Slot, Material>();

		private MeshRenderer meshRenderer;

		private MeshFilter meshFilter;

		[NonSerialized]
		public bool valid;

		[NonSerialized]
		public Skeleton skeleton;

		[NonSerialized]
		private readonly SkeletonRendererInstruction currentInstructions = new SkeletonRendererInstruction();

		private readonly MeshGenerator meshGenerator = new MeshGenerator();

		[NonSerialized]
		private readonly MeshRendererBuffers rendererBuffers = new MeshRendererBuffers();

		public delegate void SkeletonRendererDelegate(SkeletonRenderer skeletonRenderer);

		public delegate void InstructionDelegate(SkeletonRendererInstruction instruction);
	}
}

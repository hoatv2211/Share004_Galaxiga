using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace Spine.Unity
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(CanvasRenderer), typeof(RectTransform))]
	[DisallowMultipleComponent]
	[AddComponentMenu("Spine/SkeletonGraphic (Unity UI Canvas)")]
	public class SkeletonGraphic : MaskableGraphic, ISkeletonComponent, IAnimationStateComponent, ISkeletonAnimation, IHasSkeletonDataAsset
	{
		public SkeletonDataAsset SkeletonDataAsset
		{
			get
			{
				return this.skeletonDataAsset;
			}
		}

		public static SkeletonGraphic NewSkeletonGraphicGameObject(SkeletonDataAsset skeletonDataAsset, Transform parent)
		{
			SkeletonGraphic skeletonGraphic = SkeletonGraphic.AddSkeletonGraphicComponent(new GameObject("New Spine GameObject"), skeletonDataAsset);
			if (parent != null)
			{
				skeletonGraphic.transform.SetParent(parent, false);
			}
			return skeletonGraphic;
		}

		public static SkeletonGraphic AddSkeletonGraphicComponent(GameObject gameObject, SkeletonDataAsset skeletonDataAsset)
		{
			SkeletonGraphic skeletonGraphic = gameObject.AddComponent<SkeletonGraphic>();
			if (skeletonDataAsset != null)
			{
				skeletonGraphic.skeletonDataAsset = skeletonDataAsset;
				skeletonGraphic.Initialize(false);
			}
			return skeletonGraphic;
		}

		public Texture OverrideTexture
		{
			get
			{
				return this.overrideTexture;
			}
			set
			{
				this.overrideTexture = value;
				base.canvasRenderer.SetTexture(this.mainTexture);
			}
		}

		public override Texture mainTexture
		{
			get
			{
				if (this.overrideTexture != null)
				{
					return this.overrideTexture;
				}
				return (!(this.skeletonDataAsset == null)) ? this.skeletonDataAsset.atlasAssets[0].materials[0].mainTexture : null;
			}
		}

		protected override void Awake()
		{
			base.Awake();
			if (!this.IsValid)
			{
				this.Initialize(false);
				this.Rebuild(CanvasUpdate.PreRender);
			}
		}

		public override void Rebuild(CanvasUpdate update)
		{
			base.Rebuild(update);
			if (base.canvasRenderer.cull)
			{
				return;
			}
			if (update == CanvasUpdate.PreRender)
			{
				this.UpdateMesh();
			}
		}

		public virtual void Update()
		{
			if (this.freeze)
			{
				return;
			}
			this.Update((!this.unscaledTime) ? Time.deltaTime : Time.unscaledDeltaTime);
		}

		public virtual void Update(float deltaTime)
		{
			if (!this.IsValid)
			{
				return;
			}
			deltaTime *= this.timeScale;
			this.skeleton.Update(deltaTime);
			this.state.Update(deltaTime);
			this.state.Apply(this.skeleton);
			if (this.UpdateLocal != null)
			{
				this.UpdateLocal(this);
			}
			this.skeleton.UpdateWorldTransform();
			if (this.UpdateWorld != null)
			{
				this.UpdateWorld(this);
				this.skeleton.UpdateWorldTransform();
			}
			if (this.UpdateComplete != null)
			{
				this.UpdateComplete(this);
			}
		}

		public void LateUpdate()
		{
			if (this.freeze)
			{
				return;
			}
			this.UpdateMesh();
		}

		public Skeleton Skeleton
		{
			get
			{
				return this.skeleton;
			}
			internal set
			{
				this.skeleton = value;
			}
		}

		public SkeletonData SkeletonData
		{
			get
			{
				return (this.skeleton != null) ? this.skeleton.data : null;
			}
		}

		public bool IsValid
		{
			get
			{
				return this.skeleton != null;
			}
		}

		public AnimationState AnimationState
		{
			get
			{
				return this.state;
			}
		}

		public MeshGenerator MeshGenerator
		{
			get
			{
				return this.meshGenerator;
			}
		}

		public Mesh GetLastMesh()
		{
			return this.meshBuffers.GetCurrent().mesh;
		}

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event UpdateBonesDelegate UpdateLocal;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event UpdateBonesDelegate UpdateWorld;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event UpdateBonesDelegate UpdateComplete;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event MeshGeneratorDelegate OnPostProcessVertices;

		public void Clear()
		{
			this.skeleton = null;
			base.canvasRenderer.Clear();
		}

		public void Initialize(bool overwrite)
		{
			if (this.IsValid && !overwrite)
			{
				return;
			}
			if (this.skeletonDataAsset == null)
			{
				return;
			}
			SkeletonData skeletonData = this.skeletonDataAsset.GetSkeletonData(false);
			if (skeletonData == null)
			{
				return;
			}
			if (this.skeletonDataAsset.atlasAssets.Length <= 0 || this.skeletonDataAsset.atlasAssets[0].materials.Length <= 0)
			{
				return;
			}
			this.state = new AnimationState(this.skeletonDataAsset.GetAnimationStateData());
			if (this.state == null)
			{
				this.Clear();
				return;
			}
			this.skeleton = new Skeleton(skeletonData)
			{
				flipX = this.initialFlipX,
				flipY = this.initialFlipY
			};
			this.meshBuffers = new DoubleBuffered<MeshRendererBuffers.SmartMesh>();
			base.canvasRenderer.SetTexture(this.mainTexture);
			if (!string.IsNullOrEmpty(this.initialSkinName))
			{
				this.skeleton.SetSkin(this.initialSkinName);
			}
			if (!string.IsNullOrEmpty(this.startingAnimation))
			{
				this.state.SetAnimation(0, this.startingAnimation, this.startingLoop);
				this.Update(0f);
			}
		}

		public void UpdateMesh()
		{
			if (!this.IsValid)
			{
				return;
			}
			this.skeleton.SetColor(this.color);
			MeshRendererBuffers.SmartMesh next = this.meshBuffers.GetNext();
			SkeletonRendererInstruction skeletonRendererInstruction = this.currentInstructions;
			MeshGenerator.GenerateSingleSubmeshInstruction(skeletonRendererInstruction, this.skeleton, this.material);
			bool flag = SkeletonRendererInstruction.GeometryNotEqual(skeletonRendererInstruction, next.instructionUsed);
			this.meshGenerator.Begin();
			if (skeletonRendererInstruction.hasActiveClipping)
			{
				this.meshGenerator.AddSubmesh(skeletonRendererInstruction.submeshInstructions.Items[0], flag);
			}
			else
			{
				this.meshGenerator.BuildMeshWithArrays(skeletonRendererInstruction, flag);
			}
			if (base.canvas != null)
			{
				this.meshGenerator.ScaleVertexData(base.canvas.referencePixelsPerUnit);
			}
			if (this.OnPostProcessVertices != null)
			{
				this.OnPostProcessVertices(this.meshGenerator.Buffers);
			}
			Mesh mesh = next.mesh;
			this.meshGenerator.FillVertexData(mesh);
			if (flag)
			{
				this.meshGenerator.FillTrianglesSingle(mesh);
			}
			this.meshGenerator.FillLateVertexData(mesh);
			base.canvasRenderer.SetMesh(mesh);
			next.instructionUsed.Set(skeletonRendererInstruction);
		}

		public SkeletonDataAsset skeletonDataAsset;

		[SpineSkin("", "skeletonDataAsset", true, false)]
		public string initialSkinName = "default";

		public bool initialFlipX;

		public bool initialFlipY;

		[SpineAnimation("", "skeletonDataAsset", true, false)]
		public string startingAnimation;

		public bool startingLoop;

		public float timeScale = 1f;

		public bool freeze;

		public bool unscaledTime;

		private Texture overrideTexture;

		protected Skeleton skeleton;

		protected AnimationState state;

		[SerializeField]
		protected MeshGenerator meshGenerator = new MeshGenerator();

		private DoubleBuffered<MeshRendererBuffers.SmartMesh> meshBuffers;

		private SkeletonRendererInstruction currentInstructions = new SkeletonRendererInstruction();
	}
}

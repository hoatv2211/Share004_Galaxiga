using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Coffee.UIExtensions
{
	[ExecuteInEditMode]
	public class UIParticle : MaskableGraphic
	{
		public override Texture mainTexture
		{
			get
			{
				Texture texture = null;
				if (!this.m_IsTrail)
				{
					ParticleSystem.TextureSheetAnimationModule textureSheetAnimation = this.m_ParticleSystem.textureSheetAnimation;
					if (textureSheetAnimation.enabled && textureSheetAnimation.mode == ParticleSystemAnimationMode.Sprites && 0 < textureSheetAnimation.spriteCount)
					{
						texture = textureSheetAnimation.GetSprite(0).texture;
					}
				}
				if (!texture && this._renderer)
				{
					Material material = (!this.m_IsTrail) ? ((!Application.isPlaying) ? this._renderer.sharedMaterial : this._renderer.material) : this._renderer.trailMaterial;
					if (material && material.HasProperty(UIParticle.s_IdMainTex))
					{
						texture = material.mainTexture;
					}
				}
				return texture ?? Graphic.s_WhiteTexture;
			}
		}

		public override Material GetModifiedMaterial(Material baseMaterial)
		{
			return base.GetModifiedMaterial((!this._renderer) ? baseMaterial : this._renderer.sharedMaterial);
		}

		protected override void OnEnable()
		{
			this.m_ParticleSystem = ((!this.m_ParticleSystem) ? base.GetComponent<ParticleSystem>() : this.m_ParticleSystem);
			this._renderer = ((!this.m_ParticleSystem) ? null : this.m_ParticleSystem.GetComponent<ParticleSystemRenderer>());
			this._mesh = new Mesh();
			this._mesh.MarkDynamic();
			this.CheckTrail();
			base.OnEnable();
			Canvas.willRenderCanvases += this.UpdateMesh;
		}

		protected override void OnDisable()
		{
			Canvas.willRenderCanvases -= this.UpdateMesh;
			UnityEngine.Object.DestroyImmediate(this._mesh);
			this._mesh = null;
			this.CheckTrail();
			base.OnDisable();
		}

		protected override void UpdateGeometry()
		{
		}

		private void UpdateMesh()
		{
			try
			{
				this.CheckTrail();
				if (this.m_ParticleSystem)
				{
					if (Application.isPlaying)
					{
						this._renderer.enabled = false;
					}
					Camera camera = base.canvas.worldCamera ?? Camera.main;
					bool useTransform = false;
					Matrix4x4 matrix4x = default(Matrix4x4);
					ParticleSystemSimulationSpace simulationSpace = this.m_ParticleSystem.main.simulationSpace;
					if (simulationSpace != ParticleSystemSimulationSpace.Local)
					{
						if (simulationSpace != ParticleSystemSimulationSpace.World)
						{
							if (simulationSpace != ParticleSystemSimulationSpace.Custom)
							{
							}
						}
						else
						{
							matrix4x = this.m_ParticleSystem.transform.worldToLocalMatrix;
						}
					}
					else
					{
						matrix4x = Matrix4x4.Rotate(this.m_ParticleSystem.transform.rotation).inverse * Matrix4x4.Scale(this.m_ParticleSystem.transform.lossyScale).inverse;
						useTransform = true;
					}
					this._mesh.Clear();
					if (0 < this.m_ParticleSystem.particleCount)
					{
						if (this.m_IsTrail)
						{
							this._renderer.BakeTrailsMesh(this._mesh, camera, useTransform);
						}
						else
						{
							this._renderer.BakeMesh(this._mesh, camera, useTransform);
						}
						this._mesh.GetVertices(UIParticle.s_Vertices);
						int count = UIParticle.s_Vertices.Count;
						for (int i = 0; i < count; i++)
						{
							UIParticle.s_Vertices[i] = matrix4x.MultiplyPoint3x4(UIParticle.s_Vertices[i]);
						}
						this._mesh.SetVertices(UIParticle.s_Vertices);
						UIParticle.s_Vertices.Clear();
					}
					base.canvasRenderer.SetMesh(this._mesh);
					base.canvasRenderer.SetTexture(this.mainTexture);
				}
			}
			catch (Exception exception)
			{
				UnityEngine.Debug.LogException(exception);
			}
		}

		private void CheckTrail()
		{
			if (base.isActiveAndEnabled && !this.m_IsTrail && this.m_ParticleSystem && this.m_ParticleSystem.trails.enabled)
			{
				if (!this.m_TrailParticle)
				{
					this.m_TrailParticle = new GameObject("[UIParticle] Trail").AddComponent<UIParticle>();
					Transform transform = this.m_TrailParticle.transform;
					transform.SetParent(base.transform);
					transform.localPosition = Vector3.zero;
					transform.localRotation = Quaternion.identity;
					transform.localScale = Vector3.one;
					this.m_TrailParticle._renderer = base.GetComponent<ParticleSystemRenderer>();
					this.m_TrailParticle.m_ParticleSystem = base.GetComponent<ParticleSystem>();
					this.m_TrailParticle.m_IsTrail = true;
				}
				this.m_TrailParticle.enabled = true;
			}
			else if (this.m_TrailParticle)
			{
				this.m_TrailParticle.enabled = false;
			}
		}

		private static readonly int s_IdMainTex = Shader.PropertyToID("_MainTex");

		private static readonly List<Vector3> s_Vertices = new List<Vector3>();

		[Tooltip("The ParticleSystem rendered by CanvasRenderer")]
		[SerializeField]
		private ParticleSystem m_ParticleSystem;

		[Tooltip("The UIParticle to render trail effect")]
		[SerializeField]
		private UIParticle m_TrailParticle;

		[HideInInspector]
		[SerializeField]
		private bool m_IsTrail;

		private Mesh _mesh;

		private ParticleSystemRenderer _renderer;
	}
}

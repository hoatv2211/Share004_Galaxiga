using System;
using UnityEngine;

namespace Spine.Unity.Modules
{
	[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
	public class SkeletonPartsRenderer : MonoBehaviour
	{
		public MeshGenerator MeshGenerator
		{
			get
			{
				this.LazyIntialize();
				return this.meshGenerator;
			}
		}

		public MeshRenderer MeshRenderer
		{
			get
			{
				this.LazyIntialize();
				return this.meshRenderer;
			}
		}

		public MeshFilter MeshFilter
		{
			get
			{
				this.LazyIntialize();
				return this.meshFilter;
			}
		}

		private void LazyIntialize()
		{
			if (this.buffers == null)
			{
				this.buffers = new MeshRendererBuffers();
				this.buffers.Initialize();
				if (this.meshGenerator != null)
				{
					return;
				}
				this.meshGenerator = new MeshGenerator();
				this.meshFilter = base.GetComponent<MeshFilter>();
				this.meshRenderer = base.GetComponent<MeshRenderer>();
				this.currentInstructions.Clear();
			}
		}

		public void ClearMesh()
		{
			this.LazyIntialize();
			this.meshFilter.sharedMesh = null;
		}

		public void RenderParts(ExposedList<SubmeshInstruction> instructions, int startSubmesh, int endSubmesh)
		{
			this.LazyIntialize();
			MeshRendererBuffers.SmartMesh nextMesh = this.buffers.GetNextMesh();
			this.currentInstructions.SetWithSubset(instructions, startSubmesh, endSubmesh);
			bool flag = SkeletonRendererInstruction.GeometryNotEqual(this.currentInstructions, nextMesh.instructionUsed);
			SubmeshInstruction[] items = this.currentInstructions.submeshInstructions.Items;
			this.meshGenerator.Begin();
			if (this.currentInstructions.hasActiveClipping)
			{
				for (int i = 0; i < this.currentInstructions.submeshInstructions.Count; i++)
				{
					this.meshGenerator.AddSubmesh(items[i], flag);
				}
			}
			else
			{
				this.meshGenerator.BuildMeshWithArrays(this.currentInstructions, flag);
			}
			this.buffers.UpdateSharedMaterials(this.currentInstructions.submeshInstructions);
			Mesh mesh = nextMesh.mesh;
			if (this.meshGenerator.VertexCount <= 0)
			{
				mesh.Clear();
			}
			else
			{
				this.meshGenerator.FillVertexData(mesh);
				if (flag)
				{
					this.meshGenerator.FillTriangles(mesh);
					this.meshRenderer.sharedMaterials = this.buffers.GetUpdatedSharedMaterialsArray();
				}
				else if (this.buffers.MaterialsChangedInLastUpdate())
				{
					this.meshRenderer.sharedMaterials = this.buffers.GetUpdatedSharedMaterialsArray();
				}
			}
			this.meshGenerator.FillLateVertexData(mesh);
			this.meshFilter.sharedMesh = mesh;
			nextMesh.instructionUsed.Set(this.currentInstructions);
		}

		public void SetPropertyBlock(MaterialPropertyBlock block)
		{
			this.LazyIntialize();
			this.meshRenderer.SetPropertyBlock(block);
		}

		public static SkeletonPartsRenderer NewPartsRendererGameObject(Transform parent, string name)
		{
			GameObject gameObject = new GameObject(name, new Type[]
			{
				typeof(MeshFilter),
				typeof(MeshRenderer)
			});
			gameObject.transform.SetParent(parent, false);
			return gameObject.AddComponent<SkeletonPartsRenderer>();
		}

		private MeshGenerator meshGenerator;

		private MeshRenderer meshRenderer;

		private MeshFilter meshFilter;

		private MeshRendererBuffers buffers;

		private SkeletonRendererInstruction currentInstructions = new SkeletonRendererInstruction();
	}
}

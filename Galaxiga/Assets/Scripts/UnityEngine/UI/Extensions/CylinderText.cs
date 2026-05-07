using System;

namespace UnityEngine.UI.Extensions
{
	[RequireComponent(typeof(Text), typeof(RectTransform))]
	[AddComponentMenu("UI/Effects/Extensions/Cylinder Text")]
	public class CylinderText : BaseMeshEffect
	{
		protected override void Awake()
		{
			base.Awake();
			this.rectTrans = base.GetComponent<RectTransform>();
			this.OnRectTransformDimensionsChange();
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			this.rectTrans = base.GetComponent<RectTransform>();
			this.OnRectTransformDimensionsChange();
		}

		public override void ModifyMesh(VertexHelper vh)
		{
			if (!this.IsActive())
			{
				return;
			}
			int currentVertCount = vh.currentVertCount;
			if (!this.IsActive() || currentVertCount == 0)
			{
				return;
			}
			for (int i = 0; i < vh.currentVertCount; i++)
			{
				UIVertex vertex = default(UIVertex);
				vh.PopulateUIVertex(ref vertex, i);
				float x = vertex.position.x;
				vertex.position.z = -this.radius * Mathf.Cos(x / this.radius);
				vertex.position.x = this.radius * Mathf.Sin(x / this.radius);
				vh.SetUIVertex(vertex, i);
			}
		}

		public float radius;

		private RectTransform rectTrans;
	}
}

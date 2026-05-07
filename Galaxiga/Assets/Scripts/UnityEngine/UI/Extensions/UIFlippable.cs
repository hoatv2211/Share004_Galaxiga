using System;

namespace UnityEngine.UI.Extensions
{
	[RequireComponent(typeof(RectTransform), typeof(Graphic))]
	[DisallowMultipleComponent]
	[AddComponentMenu("UI/Effects/Extensions/Flippable")]
	public class UIFlippable : BaseMeshEffect
	{
		public bool horizontal
		{
			get
			{
				return this.m_Horizontal;
			}
			set
			{
				this.m_Horizontal = value;
			}
		}

		public bool vertical
		{
			get
			{
				return this.m_Veritical;
			}
			set
			{
				this.m_Veritical = value;
			}
		}

		public override void ModifyMesh(VertexHelper verts)
		{
			RectTransform rectTransform = base.transform as RectTransform;
			for (int i = 0; i < verts.currentVertCount; i++)
			{
				UIVertex vertex = default(UIVertex);
				verts.PopulateUIVertex(ref vertex, i);
				vertex.position = new Vector3((!this.m_Horizontal) ? vertex.position.x : (vertex.position.x + (rectTransform.rect.center.x - vertex.position.x) * 2f), (!this.m_Veritical) ? vertex.position.y : (vertex.position.y + (rectTransform.rect.center.y - vertex.position.y) * 2f), vertex.position.z);
				verts.SetUIVertex(vertex, i);
			}
		}

		[SerializeField]
		private bool m_Horizontal;

		[SerializeField]
		private bool m_Veritical;
	}
}

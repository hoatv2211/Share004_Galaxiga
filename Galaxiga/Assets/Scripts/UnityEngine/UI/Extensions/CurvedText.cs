using System;

namespace UnityEngine.UI.Extensions
{
	[RequireComponent(typeof(Text), typeof(RectTransform))]
	[AddComponentMenu("UI/Effects/Extensions/Curved Text")]
	public class CurvedText : BaseMeshEffect
	{
		public AnimationCurve CurveForText
		{
			get
			{
				return this._curveForText;
			}
			set
			{
				this._curveForText = value;
				base.graphic.SetVerticesDirty();
			}
		}

		public float CurveMultiplier
		{
			get
			{
				return this._curveMultiplier;
			}
			set
			{
				this._curveMultiplier = value;
				base.graphic.SetVerticesDirty();
			}
		}

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
			int currentVertCount = vh.currentVertCount;
			if (!this.IsActive() || currentVertCount == 0)
			{
				return;
			}
			for (int i = 0; i < vh.currentVertCount; i++)
			{
				UIVertex vertex = default(UIVertex);
				vh.PopulateUIVertex(ref vertex, i);
				vertex.position.y = vertex.position.y + this._curveForText.Evaluate(this.rectTrans.rect.width * this.rectTrans.pivot.x + vertex.position.x) * this._curveMultiplier;
				vh.SetUIVertex(vertex, i);
			}
		}

		protected override void OnRectTransformDimensionsChange()
		{
			if (this.rectTrans)
			{
				Keyframe key = this._curveForText[this._curveForText.length - 1];
				key.time = this.rectTrans.rect.width;
				this._curveForText.MoveKey(this._curveForText.length - 1, key);
			}
		}

		[SerializeField]
		private AnimationCurve _curveForText = AnimationCurve.Linear(0f, 0f, 1f, 10f);

		[SerializeField]
		private float _curveMultiplier = 1f;

		private RectTransform rectTrans;
	}
}

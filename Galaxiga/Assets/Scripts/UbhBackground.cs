using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class UbhBackground : UbhMonoBehaviour
{
	private void Start()
	{
		UbhGameManager ubhGameManager = UnityEngine.Object.FindObjectOfType<UbhGameManager>();
		if (ubhGameManager != null && ubhGameManager.m_scaleToFit)
		{
			Vector2 a = Camera.main.ViewportToWorldPoint(UbhUtil.VECTOR2_ONE);
			Vector2 v = a * 2f;
			base.transform.localScale = v;
		}
		if (this.isImageUI)
		{
			this.img = base.gameObject.GetComponent<Image>();
		}
		if (this.isLineRenderer)
		{
			this.lineRenderer = base.GetComponent<UILineRenderer>();
		}
	}

	private void Update()
	{
		if (GameContext.isPause)
		{
			return;
		}
		float num = Mathf.Repeat(Time.time * this.m_speed, 1f);
		if (!this.isMoveAxisX)
		{
			this.m_offset.x = 0f;
			this.m_offset.y = num;
		}
		else
		{
			this.m_offset.x = num;
			this.m_offset.y = 0f;
		}
		if (this.isImageUI)
		{
			this.img.materialForRendering.SetTextureOffset("_MainTex", this.m_offset);
		}
		else if (this.isLineRenderer)
		{
			this.lineRenderer.materialForRendering.SetTextureOffset("_MainTex", this.m_offset);
		}
		else
		{
			base.renderer.sharedMaterial.SetTextureOffset("_MainTex", this.m_offset);
		}
	}

	private const string TEX_OFFSET_PROPERTY = "_MainTex";

	[SerializeField]
	[FormerlySerializedAs("_Speed")]
	private float m_speed = 0.1f;

	private Vector2 m_offset = UbhUtil.VECTOR2_ZERO;

	private Image img;

	public bool isImageUI;

	public bool isLineRenderer;

	public bool isMoveAxisX;

	private UILineRenderer lineRenderer;
}

using System;
using System.Collections.Generic;

namespace UnityEngine.UI.Extensions
{
	public class UIPrimitiveBase : MaskableGraphic, ILayoutElement, ICanvasRaycastFilter
	{
		protected UIPrimitiveBase()
		{
			base.useLegacyMeshGeneration = false;
		}

		public Sprite sprite
		{
			get
			{
				return this.m_Sprite;
			}
			set
			{
				if (SetPropertyUtility.SetClass<Sprite>(ref this.m_Sprite, value))
				{
					this.GeneratedUVs();
				}
				this.SetAllDirty();
			}
		}

		public Sprite overrideSprite
		{
			get
			{
				return this.activeSprite;
			}
			set
			{
				if (SetPropertyUtility.SetClass<Sprite>(ref this.m_OverrideSprite, value))
				{
					this.GeneratedUVs();
				}
				this.SetAllDirty();
			}
		}

		protected Sprite activeSprite
		{
			get
			{
				return (!(this.m_OverrideSprite != null)) ? this.sprite : this.m_OverrideSprite;
			}
		}

		public float eventAlphaThreshold
		{
			get
			{
				return this.m_EventAlphaThreshold;
			}
			set
			{
				this.m_EventAlphaThreshold = value;
			}
		}

		public ResolutionMode ImproveResolution
		{
			get
			{
				return this.m_improveResolution;
			}
			set
			{
				this.m_improveResolution = value;
				this.SetAllDirty();
			}
		}

		public float Resoloution
		{
			get
			{
				return this.m_Resolution;
			}
			set
			{
				this.m_Resolution = value;
				this.SetAllDirty();
			}
		}

		public bool UseNativeSize
		{
			get
			{
				return this.m_useNativeSize;
			}
			set
			{
				this.m_useNativeSize = value;
				this.SetAllDirty();
			}
		}

		public static Material defaultETC1GraphicMaterial
		{
			get
			{
				if (UIPrimitiveBase.s_ETC1DefaultUI == null)
				{
					UIPrimitiveBase.s_ETC1DefaultUI = Canvas.GetETC1SupportedCanvasMaterial();
				}
				return UIPrimitiveBase.s_ETC1DefaultUI;
			}
		}

		public override Texture mainTexture
		{
			get
			{
				if (!(this.activeSprite == null))
				{
					return this.activeSprite.texture;
				}
				if (this.material != null && this.material.mainTexture != null)
				{
					return this.material.mainTexture;
				}
				return Graphic.s_WhiteTexture;
			}
		}

		public bool hasBorder
		{
			get
			{
				return this.activeSprite != null && this.activeSprite.border.sqrMagnitude > 0f;
			}
		}

		public float pixelsPerUnit
		{
			get
			{
				float num = 100f;
				if (this.activeSprite)
				{
					num = this.activeSprite.pixelsPerUnit;
				}
				float num2 = 100f;
				if (base.canvas)
				{
					num2 = base.canvas.referencePixelsPerUnit;
				}
				return num / num2;
			}
		}

		public override Material material
		{
			get
			{
				if (this.m_Material != null)
				{
					return this.m_Material;
				}
				if (this.activeSprite && this.activeSprite.associatedAlphaSplitTexture != null)
				{
					return UIPrimitiveBase.defaultETC1GraphicMaterial;
				}
				return this.defaultMaterial;
			}
			set
			{
				base.material = value;
			}
		}

		protected UIVertex[] SetVbo(Vector2[] vertices, Vector2[] uvs)
		{
			UIVertex[] array = new UIVertex[4];
			for (int i = 0; i < vertices.Length; i++)
			{
				UIVertex simpleVert = UIVertex.simpleVert;
				simpleVert.color = this.color;
				simpleVert.position = vertices[i];
				simpleVert.uv0 = uvs[i];
				array[i] = simpleVert;
			}
			return array;
		}

		protected Vector2[] IncreaseResolution(Vector2[] input)
		{
			List<Vector2> list = new List<Vector2>();
			ResolutionMode improveResolution = this.ImproveResolution;
			if (improveResolution != ResolutionMode.PerLine)
			{
				if (improveResolution == ResolutionMode.PerSegment)
				{
					for (int i = 0; i < input.Length - 1; i++)
					{
						Vector2 vector = input[i];
						list.Add(vector);
						Vector2 vector2 = input[i + 1];
						this.ResolutionToNativeSize(Vector2.Distance(vector, vector2));
						float num = 1f / this.m_Resolution;
						for (float num2 = 1f; num2 < this.m_Resolution; num2 += 1f)
						{
							list.Add(Vector2.Lerp(vector, vector2, num * num2));
						}
						list.Add(vector2);
					}
				}
			}
			else
			{
				float num3 = 0f;
				for (int j = 0; j < input.Length - 1; j++)
				{
					num3 += Vector2.Distance(input[j], input[j + 1]);
				}
				this.ResolutionToNativeSize(num3);
				float num = num3 / this.m_Resolution;
				int num4 = 0;
				for (int k = 0; k < input.Length - 1; k++)
				{
					Vector2 vector3 = input[k];
					list.Add(vector3);
					Vector2 vector4 = input[k + 1];
					float num5 = Vector2.Distance(vector3, vector4) / num;
					float num6 = 1f / num5;
					int num7 = 0;
					while ((float)num7 < num5)
					{
						list.Add(Vector2.Lerp(vector3, vector4, (float)num7 * num6));
						num4++;
						num7++;
					}
					list.Add(vector4);
				}
			}
			return list.ToArray();
		}

		protected virtual void GeneratedUVs()
		{
		}

		protected virtual void ResolutionToNativeSize(float distance)
		{
		}

		public virtual void CalculateLayoutInputHorizontal()
		{
		}

		public virtual void CalculateLayoutInputVertical()
		{
		}

		public virtual float minWidth
		{
			get
			{
				return 0f;
			}
		}

		public virtual float preferredWidth
		{
			get
			{
				if (this.overrideSprite == null)
				{
					return 0f;
				}
				return this.overrideSprite.rect.size.x / this.pixelsPerUnit;
			}
		}

		public virtual float flexibleWidth
		{
			get
			{
				return -1f;
			}
		}

		public virtual float minHeight
		{
			get
			{
				return 0f;
			}
		}

		public virtual float preferredHeight
		{
			get
			{
				if (this.overrideSprite == null)
				{
					return 0f;
				}
				return this.overrideSprite.rect.size.y / this.pixelsPerUnit;
			}
		}

		public virtual float flexibleHeight
		{
			get
			{
				return -1f;
			}
		}

		public virtual int layoutPriority
		{
			get
			{
				return 0;
			}
		}

		public virtual bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
		{
			if (this.m_EventAlphaThreshold >= 1f)
			{
				return true;
			}
			Sprite overrideSprite = this.overrideSprite;
			if (overrideSprite == null)
			{
				return true;
			}
			Vector2 local;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(base.rectTransform, screenPoint, eventCamera, out local);
			Rect pixelAdjustedRect = base.GetPixelAdjustedRect();
			local.x += base.rectTransform.pivot.x * pixelAdjustedRect.width;
			local.y += base.rectTransform.pivot.y * pixelAdjustedRect.height;
			local = this.MapCoordinate(local, pixelAdjustedRect);
			Rect textureRect = overrideSprite.textureRect;
			Vector2 vector = new Vector2(local.x / textureRect.width, local.y / textureRect.height);
			float x = Mathf.Lerp(textureRect.x, textureRect.xMax, vector.x) / (float)overrideSprite.texture.width;
			float y = Mathf.Lerp(textureRect.y, textureRect.yMax, vector.y) / (float)overrideSprite.texture.height;
			bool result;
			try
			{
				result = (overrideSprite.texture.GetPixelBilinear(x, y).a >= this.m_EventAlphaThreshold);
			}
			catch (UnityException ex)
			{
				UnityEngine.Debug.LogError("Using clickAlphaThreshold lower than 1 on Image whose sprite texture cannot be read. " + ex.Message + " Also make sure to disable sprite packing for this sprite.", this);
				result = true;
			}
			return result;
		}

		private Vector2 MapCoordinate(Vector2 local, Rect rect)
		{
			Rect rect2 = this.sprite.rect;
			return new Vector2(local.x * rect.width, local.y * rect.height);
		}

		private Vector4 GetAdjustedBorders(Vector4 border, Rect rect)
		{
			for (int i = 0; i <= 1; i++)
			{
				float num = border[i] + border[i + 2];
				if (rect.size[i] < num && num != 0f)
				{
					float num2 = rect.size[i] / num;
					Vector4 ptr = border;
					int index;
					border[index = i] = ptr[index] * num2;
					ptr = border;
					int index2;
					border[index2 = i + 2] = ptr[index2] * num2;
				}
			}
			return border;
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			this.SetAllDirty();
		}

		protected static Material s_ETC1DefaultUI;

		[SerializeField]
		private Sprite m_Sprite;

		[NonSerialized]
		private Sprite m_OverrideSprite;

		internal float m_EventAlphaThreshold = 1f;

		[SerializeField]
		private ResolutionMode m_improveResolution;

		[SerializeField]
		protected float m_Resolution;

		[SerializeField]
		private bool m_useNativeSize;
	}
}

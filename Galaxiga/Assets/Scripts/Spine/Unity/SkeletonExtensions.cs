using System;
using UnityEngine;

namespace Spine.Unity
{
	public static class SkeletonExtensions
	{
		public static Color GetColor(this Skeleton s)
		{
			return new Color(s.r, s.g, s.b, s.a);
		}

		public static Color GetColor(this RegionAttachment a)
		{
			return new Color(a.r, a.g, a.b, a.a);
		}

		public static Color GetColor(this MeshAttachment a)
		{
			return new Color(a.r, a.g, a.b, a.a);
		}

		public static Color GetColor(this Slot s)
		{
			return new Color(s.r, s.g, s.b, s.a);
		}

		public static Color GetColorTintBlack(this Slot s)
		{
			return new Color(s.r2, s.g2, s.b2, 1f);
		}

		public static void SetColor(this Skeleton skeleton, Color color)
		{
			skeleton.A = color.a;
			skeleton.R = color.r;
			skeleton.G = color.g;
			skeleton.B = color.b;
		}

		public static void SetColor(this Skeleton skeleton, Color32 color)
		{
			skeleton.A = (float)color.a * 0.003921569f;
			skeleton.R = (float)color.r * 0.003921569f;
			skeleton.G = (float)color.g * 0.003921569f;
			skeleton.B = (float)color.b * 0.003921569f;
		}

		public static void SetColor(this Slot slot, Color color)
		{
			slot.A = color.a;
			slot.R = color.r;
			slot.G = color.g;
			slot.B = color.b;
		}

		public static void SetColor(this Slot slot, Color32 color)
		{
			slot.A = (float)color.a * 0.003921569f;
			slot.R = (float)color.r * 0.003921569f;
			slot.G = (float)color.g * 0.003921569f;
			slot.B = (float)color.b * 0.003921569f;
		}

		public static void SetColor(this RegionAttachment attachment, Color color)
		{
			attachment.A = color.a;
			attachment.R = color.r;
			attachment.G = color.g;
			attachment.B = color.b;
		}

		public static void SetColor(this RegionAttachment attachment, Color32 color)
		{
			attachment.A = (float)color.a * 0.003921569f;
			attachment.R = (float)color.r * 0.003921569f;
			attachment.G = (float)color.g * 0.003921569f;
			attachment.B = (float)color.b * 0.003921569f;
		}

		public static void SetColor(this MeshAttachment attachment, Color color)
		{
			attachment.A = color.a;
			attachment.R = color.r;
			attachment.G = color.g;
			attachment.B = color.b;
		}

		public static void SetColor(this MeshAttachment attachment, Color32 color)
		{
			attachment.A = (float)color.a * 0.003921569f;
			attachment.R = (float)color.r * 0.003921569f;
			attachment.G = (float)color.g * 0.003921569f;
			attachment.B = (float)color.b * 0.003921569f;
		}

		public static void SetPosition(this Bone bone, Vector2 position)
		{
			bone.X = position.x;
			bone.Y = position.y;
		}

		public static void SetPosition(this Bone bone, Vector3 position)
		{
			bone.X = position.x;
			bone.Y = position.y;
		}

		public static Vector2 GetLocalPosition(this Bone bone)
		{
			return new Vector2(bone.x, bone.y);
		}

		public static Vector2 GetSkeletonSpacePosition(this Bone bone)
		{
			return new Vector2(bone.worldX, bone.worldY);
		}

		public static Vector2 GetSkeletonSpacePosition(this Bone bone, Vector2 boneLocal)
		{
			Vector2 result;
			bone.LocalToWorld(boneLocal.x, boneLocal.y, out result.x, out result.y);
			return result;
		}

		public static Vector3 GetWorldPosition(this Bone bone, Transform spineGameObjectTransform)
		{
			return spineGameObjectTransform.TransformPoint(new Vector3(bone.worldX, bone.worldY));
		}

		public static Vector3 GetWorldPosition(this Bone bone, Transform spineGameObjectTransform, float positionScale)
		{
			return spineGameObjectTransform.TransformPoint(new Vector3(bone.worldX * positionScale, bone.worldY * positionScale));
		}

		public static Quaternion GetQuaternion(this Bone bone)
		{
			float f = Mathf.Atan2(bone.c, bone.a) * 0.5f;
			return new Quaternion(0f, 0f, Mathf.Sin(f), Mathf.Cos(f));
		}

		public static Quaternion GetLocalQuaternion(this Bone bone)
		{
			float f = bone.rotation * 0.0174532924f * 0.5f;
			return new Quaternion(0f, 0f, Mathf.Sin(f), Mathf.Cos(f));
		}

		public static Matrix4x4 GetMatrix4x4(this Bone bone)
		{
			return new Matrix4x4
			{
				m00 = bone.a,
				m01 = bone.b,
				m03 = bone.worldX,
				m10 = bone.c,
				m11 = bone.d,
				m13 = bone.worldY,
				m33 = 1f
			};
		}

		public static void GetWorldToLocalMatrix(this Bone bone, out float ia, out float ib, out float ic, out float id)
		{
			float a = bone.a;
			float b = bone.b;
			float c = bone.c;
			float d = bone.d;
			float num = 1f / (a * d - b * c);
			ia = num * d;
			ib = num * -b;
			ic = num * -c;
			id = num * a;
		}

		public static Vector2 WorldToLocal(this Bone bone, Vector2 worldPosition)
		{
			Vector2 result;
			bone.WorldToLocal(worldPosition.x, worldPosition.y, out result.x, out result.y);
			return result;
		}

		public static Vector2 SetPositionSkeletonSpace(this Bone bone, Vector2 skeletonSpacePosition)
		{
			if (bone.parent == null)
			{
				bone.SetPosition(skeletonSpacePosition);
				return skeletonSpacePosition;
			}
			Bone parent = bone.parent;
			Vector2 vector = parent.WorldToLocal(skeletonSpacePosition);
			bone.SetPosition(vector);
			return vector;
		}

		public static Material GetMaterial(this Attachment a)
		{
			object obj = null;
			IHasRendererObject hasRendererObject = a as IHasRendererObject;
			if (hasRendererObject != null)
			{
				obj = hasRendererObject.RendererObject;
			}
			if (obj == null)
			{
				return null;
			}
			return (Material)((AtlasRegion)obj).page.rendererObject;
		}

		public static Vector2[] GetLocalVertices(this VertexAttachment va, Slot slot, Vector2[] buffer)
		{
			int worldVerticesLength = va.worldVerticesLength;
			int num = worldVerticesLength >> 1;
			buffer = (buffer ?? new Vector2[num]);
			if (buffer.Length < num)
			{
				throw new ArgumentException(string.Format("Vector2 buffer too small. {0} requires an array of size {1}. Use the attachment's .WorldVerticesLength to get the correct size.", va.Name, worldVerticesLength), "buffer");
			}
			if (va.bones == null)
			{
				float[] vertices = va.vertices;
				for (int i = 0; i < num; i++)
				{
					int num2 = i * 2;
					buffer[i] = new Vector2(vertices[num2], vertices[num2 + 1]);
				}
			}
			else
			{
				float[] array = new float[worldVerticesLength];
				va.ComputeWorldVertices(slot, array);
				Bone bone = slot.bone;
				float worldX = bone.worldX;
				float worldY = bone.worldY;
				float num3;
				float num4;
				float num5;
				float num6;
				bone.GetWorldToLocalMatrix(out num3, out num4, out num5, out num6);
				for (int j = 0; j < num; j++)
				{
					int num7 = j * 2;
					float num8 = array[num7] - worldX;
					float num9 = array[num7 + 1] - worldY;
					buffer[j] = new Vector2(num8 * num3 + num9 * num4, num8 * num5 + num9 * num6);
				}
			}
			return buffer;
		}

		public static Vector2[] GetWorldVertices(this VertexAttachment a, Slot slot, Vector2[] buffer)
		{
			int worldVerticesLength = a.worldVerticesLength;
			int num = worldVerticesLength >> 1;
			buffer = (buffer ?? new Vector2[num]);
			if (buffer.Length < num)
			{
				throw new ArgumentException(string.Format("Vector2 buffer too small. {0} requires an array of size {1}. Use the attachment's .WorldVerticesLength to get the correct size.", a.Name, worldVerticesLength), "buffer");
			}
			float[] array = new float[worldVerticesLength];
			a.ComputeWorldVertices(slot, array);
			int i = 0;
			int num2 = worldVerticesLength >> 1;
			while (i < num2)
			{
				int num3 = i * 2;
				buffer[i] = new Vector2(array[num3], array[num3 + 1]);
				i++;
			}
			return buffer;
		}

		public static Vector3 GetWorldPosition(this PointAttachment attachment, Slot slot, Transform spineGameObjectTransform)
		{
			Vector3 position;
			position.z = 0f;
			attachment.ComputeWorldPosition(slot.bone, out position.x, out position.y);
			return spineGameObjectTransform.TransformPoint(position);
		}

		public static Vector3 GetWorldPosition(this PointAttachment attachment, Bone bone, Transform spineGameObjectTransform)
		{
			Vector3 position;
			position.z = 0f;
			attachment.ComputeWorldPosition(bone, out position.x, out position.y);
			return spineGameObjectTransform.TransformPoint(position);
		}

		private const float ByteToFloat = 0.003921569f;
	}
}

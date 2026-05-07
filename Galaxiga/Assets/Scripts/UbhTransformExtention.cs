using System;
using UnityEngine;

public static class UbhTransformExtention
{
	public static void ResetTransform(this Transform self, bool worldSpace = false)
	{
		self.ResetPosition(worldSpace);
		self.ResetRotation(worldSpace);
		self.ResetLocalScale();
	}

	public static void ResetPosition(this Transform self, bool worldSpace = false)
	{
		if (worldSpace)
		{
			self.position = UbhUtil.VECTOR3_ZERO;
		}
		else
		{
			self.localPosition = UbhUtil.VECTOR3_ZERO;
		}
	}

	public static void ResetRotation(this Transform self, bool worldSpace = false)
	{
		if (worldSpace)
		{
			self.rotation = Quaternion.identity;
		}
		else
		{
			self.localRotation = Quaternion.identity;
		}
	}

	public static void ResetLocalScale(this Transform self)
	{
		self.localScale = UbhUtil.VECTOR3_ONE;
	}

	public static Vector2 GetVector2PositionXY(this Transform self)
	{
		return new Vector2(self.position.x, self.position.y);
	}

	public static Vector2 GetVector2LocalPositionXY(this Transform self)
	{
		return new Vector2(self.localPosition.x, self.localPosition.y);
	}

	public static Vector2 GetVector2PositionXZ(this Transform self)
	{
		return new Vector2(self.position.x, self.position.z);
	}

	public static Vector2 GetVector2LocalPositionXZ(this Transform self)
	{
		return new Vector2(self.localPosition.x, self.localPosition.z);
	}

	public static void SetPosition(this Transform self, float x, float y, float z)
	{
		UbhTransformExtention.m_tmpVector3.Set(x, y, z);
		self.position = UbhTransformExtention.m_tmpVector3;
	}

	public static void SetPosition(this Transform self, float x, float y)
	{
		self.SetPosition(x, y, self.position.z);
	}

	public static void SetPositionX(this Transform self, float x)
	{
		self.SetPosition(x, self.position.y, self.position.z);
	}

	public static void SetPositionY(this Transform self, float y)
	{
		self.SetPosition(self.position.x, y, self.position.z);
	}

	public static void SetPositionZ(this Transform self, float z)
	{
		self.SetPosition(self.position.x, self.position.y, z);
	}

	public static void SetLocalPosition(this Transform self, float x, float y, float z)
	{
		UbhTransformExtention.m_tmpVector3.Set(x, y, z);
		self.localPosition = UbhTransformExtention.m_tmpVector3;
	}

	public static void SetLocalPosition(this Transform self, float x, float y)
	{
		self.SetLocalPosition(x, y, self.localPosition.z);
	}

	public static void SetLocalPositionX(this Transform self, float x)
	{
		self.SetLocalPosition(x, self.localPosition.y, self.localPosition.z);
	}

	public static void SetLocalPositionY(this Transform self, float y)
	{
		self.SetLocalPosition(self.localPosition.x, y, self.localPosition.z);
	}

	public static void SetLocalPositionZ(this Transform self, float z)
	{
		self.SetLocalPosition(self.localPosition.x, self.localPosition.y, z);
	}

	public static void SetEulerAngles(this Transform self, float x, float y, float z)
	{
		self.rotation = Quaternion.Euler(x, y, z);
	}

	public static void SetEulerAnglesX(this Transform self, float x)
	{
		Vector3 eulerAngles = self.eulerAngles;
		self.rotation = Quaternion.Euler(x, eulerAngles.y, eulerAngles.z);
	}

	public static void SetEulerAnglesY(this Transform self, float y)
	{
		Vector3 eulerAngles = self.eulerAngles;
		self.rotation = Quaternion.Euler(eulerAngles.x, y, eulerAngles.z);
	}

	public static void SetEulerAnglesZ(this Transform self, float z)
	{
		Vector3 eulerAngles = self.eulerAngles;
		self.rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, z);
	}

	public static void SetLocalEulerAngles(this Transform self, float x, float y, float z)
	{
		self.localRotation = Quaternion.Euler(x, y, z);
	}

	public static void SetLocalEulerAnglesX(this Transform self, float x)
	{
		Vector3 localEulerAngles = self.localEulerAngles;
		self.localRotation = Quaternion.Euler(x, localEulerAngles.y, localEulerAngles.z);
	}

	public static void SetLocalEulerAnglesY(this Transform self, float y)
	{
		Vector3 localEulerAngles = self.localEulerAngles;
		self.localRotation = Quaternion.Euler(localEulerAngles.x, y, localEulerAngles.z);
	}

	public static void SetLocalEulerAnglesZ(this Transform self, float z)
	{
		Vector3 localEulerAngles = self.localEulerAngles;
		self.localRotation = Quaternion.Euler(localEulerAngles.x, localEulerAngles.y, z);
	}

	public static void SetLocalScale(this Transform self, float x, float y, float z)
	{
		UbhTransformExtention.m_tmpVector3.Set(x, y, z);
		self.localScale = UbhTransformExtention.m_tmpVector3;
	}

	public static void SetLocalScaleX(this Transform self, float x)
	{
		self.SetLocalScale(x, self.localScale.y, self.localScale.z);
	}

	public static void SetLocalScaleY(this Transform self, float y)
	{
		self.SetLocalScale(self.localScale.x, y, self.localScale.z);
	}

	public static void SetLocalScaleZ(this Transform self, float z)
	{
		self.SetLocalScale(self.localScale.x, self.localScale.y, z);
	}

	public static void AddPosition(this Transform self, float x, float y, float z)
	{
		self.SetPosition(self.position.x + x, self.position.y + y, self.position.z + z);
	}

	public static void AddPositionX(this Transform self, float x)
	{
		self.SetPositionX(self.position.x + x);
	}

	public static void AddPositionY(this Transform self, float y)
	{
		self.SetPositionY(self.position.y + y);
	}

	public static void AddPositionZ(this Transform self, float z)
	{
		self.SetPositionZ(self.position.z + z);
	}

	public static void AddLocalPosition(this Transform self, float x, float y, float z)
	{
		self.SetLocalPosition(self.localPosition.x + x, self.localPosition.y + y, self.localPosition.z + z);
	}

	public static void AddLocalPositionX(this Transform self, float x)
	{
		self.SetLocalPositionX(self.localPosition.x + x);
	}

	public static void AddLocalPositionY(this Transform self, float y)
	{
		self.SetLocalPositionY(self.localPosition.y + y);
	}

	public static void AddLocalPositionZ(this Transform self, float z)
	{
		self.SetLocalPositionZ(self.localPosition.z + z);
	}

	public static void AddEulerAngles(this Transform self, float x, float y, float z)
	{
		Vector3 eulerAngles = self.eulerAngles;
		self.rotation = Quaternion.Euler(eulerAngles.x + x, eulerAngles.y + y, eulerAngles.z + z);
	}

	public static void AddEulerAnglesX(this Transform self, float x)
	{
		Vector3 eulerAngles = self.eulerAngles;
		self.rotation = Quaternion.Euler(eulerAngles.x + x, eulerAngles.y, eulerAngles.z);
	}

	public static void AddEulerAnglesY(this Transform self, float y)
	{
		Vector3 eulerAngles = self.eulerAngles;
		self.rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y + y, eulerAngles.z);
	}

	public static void AddEulerAnglesZ(this Transform self, float z)
	{
		Vector3 eulerAngles = self.eulerAngles;
		self.rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, eulerAngles.z + z);
	}

	public static void AddLocalEulerAngles(this Transform self, float x, float y, float z)
	{
		Vector3 localEulerAngles = self.localEulerAngles;
		self.localRotation = Quaternion.Euler(localEulerAngles.x + x, localEulerAngles.y + y, localEulerAngles.z + z);
	}

	public static void AddLocalEulerAnglesX(this Transform self, float x)
	{
		Vector3 localEulerAngles = self.localEulerAngles;
		self.localRotation = Quaternion.Euler(localEulerAngles.x + x, localEulerAngles.y, localEulerAngles.z);
	}

	public static void AddLocalEulerAnglesY(this Transform self, float y)
	{
		Vector3 localEulerAngles = self.localEulerAngles;
		self.localRotation = Quaternion.Euler(localEulerAngles.x, localEulerAngles.y + y, localEulerAngles.z);
	}

	public static void AddLocalEulerAnglesZ(this Transform self, float z)
	{
		Vector3 localEulerAngles = self.localEulerAngles;
		self.localRotation = Quaternion.Euler(localEulerAngles.x, localEulerAngles.y, localEulerAngles.z + z);
	}

	public static void AddLocalScale(this Transform self, float x, float y, float z)
	{
		self.SetLocalScale(self.localScale.x + x, self.localScale.y + y, self.localScale.z + z);
	}

	public static void AddLocalScaleX(this Transform self, float x)
	{
		self.SetLocalScaleX(self.localScale.x + x);
	}

	public static void AddLocalScaleY(this Transform self, float y)
	{
		self.SetLocalScaleY(self.localScale.y + y);
	}

	public static void AddLocalScaleZ(this Transform self, float z)
	{
		self.SetLocalScaleZ(self.localScale.z + z);
	}

	public static void ClampPosition(this Transform self, Vector3 min, Vector3 max)
	{
		float x = Mathf.Clamp(self.position.x, min.x, max.x);
		float y = Mathf.Clamp(self.position.y, min.y, max.y);
		float z = Mathf.Clamp(self.position.z, min.z, max.z);
		self.SetPosition(x, y, z);
	}

	public static void ClampPosition(this Transform self, Vector2 min, Vector2 max)
	{
		float x = Mathf.Clamp(self.position.x, min.x, max.x);
		float y = Mathf.Clamp(self.position.y, min.y, max.y);
		self.SetPosition(x, y);
	}

	public static void ClampPositionX(this Transform self, float min, float max)
	{
		self.SetPositionX(Mathf.Clamp(self.position.x, min, max));
	}

	public static void ClampPositionY(this Transform self, float min, float max)
	{
		self.SetPositionY(Mathf.Clamp(self.position.y, min, max));
	}

	public static void ClampPositionZ(this Transform self, float min, float max)
	{
		self.SetPositionZ(Mathf.Clamp(self.position.z, min, max));
	}

	public static void ClampLocalPosition(this Transform self, Vector3 min, Vector3 max)
	{
		float x = Mathf.Clamp(self.localPosition.x, min.x, max.x);
		float y = Mathf.Clamp(self.localPosition.y, min.y, max.y);
		float z = Mathf.Clamp(self.localPosition.z, min.z, max.z);
		self.SetLocalPosition(x, y, z);
	}

	public static void ClampLocalPosition(this Transform self, Vector2 min, Vector2 max)
	{
		float x = Mathf.Clamp(self.localPosition.x, min.x, max.x);
		float y = Mathf.Clamp(self.localPosition.y, min.y, max.y);
		self.SetLocalPosition(x, y);
	}

	public static void ClampLocalPositionX(this Transform self, float min, float max)
	{
		self.SetLocalPositionX(Mathf.Clamp(self.localPosition.x, min, max));
	}

	public static void ClampLocalPositionY(this Transform self, float min, float max)
	{
		self.SetLocalPositionY(Mathf.Clamp(self.localPosition.y, min, max));
	}

	public static void ClampLocalPositionZ(this Transform self, float min, float max)
	{
		self.SetLocalPositionZ(Mathf.Clamp(self.localPosition.z, min, max));
	}

	public static void ClampEulerAngles(this Transform self, Vector3 min, Vector3 max)
	{
		float x = Mathf.Clamp(self.eulerAngles.x, min.x, max.x);
		float y = Mathf.Clamp(self.eulerAngles.y, min.y, max.y);
		float z = Mathf.Clamp(self.eulerAngles.z, min.z, max.z);
		self.SetEulerAngles(x, y, z);
	}

	public static void ClampEulerAnglesX(this Transform self, float min, float max)
	{
		self.SetEulerAnglesX(Mathf.Clamp(self.eulerAngles.x, min, max));
	}

	public static void ClampEulerAnglesY(this Transform self, float min, float max)
	{
		self.SetEulerAnglesY(Mathf.Clamp(self.eulerAngles.y, min, max));
	}

	public static void ClampEulerAnglesZ(this Transform self, float min, float max)
	{
		self.SetEulerAnglesZ(Mathf.Clamp(self.eulerAngles.z, min, max));
	}

	public static void ClampLocalEulerAngles(this Transform self, Vector3 min, Vector3 max)
	{
		float x = Mathf.Clamp(self.localEulerAngles.x, min.x, max.x);
		float y = Mathf.Clamp(self.localEulerAngles.y, min.y, max.y);
		float z = Mathf.Clamp(self.localEulerAngles.z, min.z, max.z);
		self.SetLocalEulerAngles(x, y, z);
	}

	public static void ClampLocalEulerAnglesX(this Transform self, float min, float max)
	{
		self.SetLocalEulerAnglesX(Mathf.Clamp(self.localEulerAngles.x, min, max));
	}

	public static void ClampLocalEulerAnglesY(this Transform self, float min, float max)
	{
		self.SetLocalEulerAnglesY(Mathf.Clamp(self.localEulerAngles.y, min, max));
	}

	public static void ClampLocalEulerAnglesZ(this Transform self, float min, float max)
	{
		self.SetLocalEulerAnglesZ(Mathf.Clamp(self.localEulerAngles.z, min, max));
	}

	public static void ClampLocalScale(this Transform self, Vector3 min, Vector3 max)
	{
		float x = Mathf.Clamp(self.localScale.x, min.x, max.x);
		float y = Mathf.Clamp(self.localScale.y, min.y, max.y);
		float z = Mathf.Clamp(self.localScale.z, min.z, max.z);
		self.SetLocalScale(x, y, z);
	}

	public static void ClampLocalScaleX(this Transform self, float min, float max)
	{
		self.SetLocalScaleX(Mathf.Clamp(self.localScale.x, min, max));
	}

	public static void ClampLocalScaleY(this Transform self, float min, float max)
	{
		self.SetLocalScaleY(Mathf.Clamp(self.localScale.y, min, max));
	}

	public static void ClampLocalScaleZ(this Transform self, float min, float max)
	{
		self.SetLocalScaleZ(Mathf.Clamp(self.localScale.z, min, max));
	}

	public static void HasChanged(this Transform self, Action changed)
	{
		if (self.hasChanged)
		{
			changed();
			self.hasChanged = false;
		}
	}

	private static void HasChanged(this Transform self, Action<Transform> changed)
	{
		if (self.hasChanged)
		{
			changed(self);
			self.hasChanged = false;
		}
	}

	public static void HasChangedInChildren(this Transform self, Action<Transform> changed)
	{
		Transform[] componentsInChildren = self.GetComponentsInChildren<Transform>();
		if (componentsInChildren == null)
		{
			return;
		}
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].HasChanged(changed);
		}
	}

	public static void HasChangedInParent(this Transform self, Action<Transform> changed)
	{
		Transform[] componentsInParent = self.GetComponentsInParent<Transform>();
		if (componentsInParent == null)
		{
			return;
		}
		for (int i = 0; i < componentsInParent.Length; i++)
		{
			componentsInParent[i].HasChanged(changed);
		}
	}

	public static void LookAt2D(this Transform self, Transform target)
	{
		self.LookAt2D(target.position, Vector3.forward, 0f);
	}

	public static void LookAt2D(this Transform self, Vector2 target)
	{
		self.LookAt2D(target, Vector3.forward, 0f);
	}

	public static void LookAt2D(this Transform self, Transform target, float angle)
	{
		self.LookAt2D(target.position, Vector3.forward, angle);
	}

	public static void LookAt2D(this Transform self, Vector2 target, float angle)
	{
		self.LookAt2D(target, Vector3.forward, angle);
	}

	public static void LookAt2D(this Transform self, Transform target, Vector3 axis)
	{
		self.LookAt2D(target.position, axis, 0f);
	}

	public static void LookAt2D(this Transform self, Vector2 target, Vector3 axis)
	{
		self.LookAt2D(target, axis, 0f);
	}

	public static void LookAt2D(this Transform self, Transform target, Vector3 axis, float angle)
	{
		self.LookAt2D(target.position, axis, angle);
	}

	public static void LookAt2D(this Transform self, Vector2 target, Vector3 axis, float angle)
	{
		UbhTransformExtention.m_tmpVector2.Set(target.x - self.position.x, target.y - self.position.y);
		angle += Mathf.Atan2(UbhTransformExtention.m_tmpVector2.y, UbhTransformExtention.m_tmpVector2.x) * 57.29578f;
		self.rotation = Quaternion.AngleAxis(angle, axis);
	}

	public static Transform FindInChildren(this Transform self, string name)
	{
		int childCount = self.childCount;
		for (int i = 0; i < childCount; i++)
		{
			Transform child = self.GetChild(i);
			if (child.name == name)
			{
				return child;
			}
			Transform transform = child.FindInChildren(name);
			if (transform != null)
			{
				return transform;
			}
		}
		return null;
	}

	public static float Distance(this Transform self, Transform other, bool localPosition = false)
	{
		if (localPosition)
		{
			return Vector3.Distance(self.localPosition, other.localPosition);
		}
		return Vector3.Distance(self.position, other.position);
	}

	public static float DistanceXY(this Transform self, Transform other, bool localPosition = false)
	{
		if (localPosition)
		{
			return Vector2.Distance(self.GetVector2LocalPositionXY(), other.GetVector2LocalPositionXY());
		}
		return Vector2.Distance(self.GetVector2PositionXY(), other.GetVector2PositionXY());
	}

	public static float DistanceXZ(this Transform self, Transform other, bool localPosition = false)
	{
		if (localPosition)
		{
			return Vector2.Distance(self.GetVector2LocalPositionXZ(), other.GetVector2LocalPositionXZ());
		}
		return Vector2.Distance(self.GetVector2PositionXZ(), other.GetVector2PositionXZ());
	}

	public static bool IsFrontAngle(this Transform self, Vector3 target, float checkAngle)
	{
		return Vector3.Angle(target - self.position, self.forward) <= checkAngle;
	}

	public static bool IsBackAngle(this Transform self, Vector3 target, float checkAngle)
	{
		return Vector3.Angle(target - self.position, -self.forward) <= checkAngle;
	}

	public static bool IsRightAngle(this Transform self, Vector3 target, float checkAngle)
	{
		return Vector3.Angle(target - self.position, self.right) <= checkAngle;
	}

	public static bool IsLeftAngle(this Transform self, Vector3 target, float checkAngle)
	{
		return Vector3.Angle(target - self.position, -self.right) <= checkAngle;
	}

	public static bool IsUpAngle(this Transform self, Vector3 target, float checkAngle)
	{
		return Vector3.Angle(target - self.position, self.up) <= checkAngle;
	}

	public static bool IsDownAngle(this Transform self, Vector3 target, float checkAngle)
	{
		return Vector3.Angle(target - self.position, -self.up) <= checkAngle;
	}

	private static Vector3 m_tmpVector3 = UbhUtil.VECTOR3_ZERO;

	private static Vector2 m_tmpVector2 = UbhUtil.VECTOR2_ZERO;
}

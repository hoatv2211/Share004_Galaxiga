using System;
using System.Collections;
using UnityEngine;

public static class UbhUtil
{
	public static bool IsMobilePlatform()
	{
		return true;
	}

	public static IEnumerator WaitForSeconds(float waitTime)
	{
		float elapsedTime = 0f;
		while (elapsedTime < waitTime)
		{
			elapsedTime += UbhSingletonMonoBehavior<UbhTimer>.instance.deltaTime;
			yield return null;
		}
		yield break;
	}

	public static Transform GetTransformFromTagName(string tagName, bool randomSelect)
	{
		if (string.IsNullOrEmpty(tagName))
		{
			return null;
		}
		GameObject gameObject = null;
		if (randomSelect)
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag(tagName);
			if (array != null && array.Length > 0)
			{
				gameObject = array[UnityEngine.Random.Range(0, array.Length)];
			}
		}
		else
		{
			gameObject = GameObject.FindWithTag(tagName);
		}
		if (gameObject == null)
		{
			return null;
		}
		return gameObject.transform;
	}

	public static float GetShiftedAngle(int wayIndex, float baseAngle, float betweenAngle)
	{
		return (wayIndex % 2 != 0) ? (baseAngle + betweenAngle * Mathf.Ceil((float)wayIndex / 2f)) : (baseAngle - betweenAngle * (float)wayIndex / 2f);
	}

	public static float GetNormalizedAngle(float angle)
	{
		while (angle < 0f)
		{
			angle += 360f;
		}
		while (360f < angle)
		{
			angle -= 360f;
		}
		return angle;
	}

	public static float GetAngleFromTwoPosition(Transform fromTrans, Transform toTrans, UbhUtil.AXIS axisMove)
	{
		if (axisMove == UbhUtil.AXIS.X_AND_Y)
		{
			return UbhUtil.GetZangleFromTwoPosition(fromTrans, toTrans);
		}
		if (axisMove != UbhUtil.AXIS.X_AND_Z)
		{
			return 0f;
		}
		return UbhUtil.GetYangleFromTwoPosition(fromTrans, toTrans);
	}

	private static float GetZangleFromTwoPosition(Transform fromTrans, Transform toTrans)
	{
		if (fromTrans == null || toTrans == null)
		{
			return 0f;
		}
		float x = toTrans.position.x - fromTrans.position.x;
		float y = toTrans.position.y - fromTrans.position.y;
		float angle = Mathf.Atan2(y, x) * 57.29578f - 90f;
		return UbhUtil.GetNormalizedAngle(angle);
	}

	private static float GetYangleFromTwoPosition(Transform fromTrans, Transform toTrans)
	{
		if (fromTrans == null || toTrans == null)
		{
			return 0f;
		}
		float x = toTrans.position.x - fromTrans.position.x;
		float y = toTrans.position.z - fromTrans.position.z;
		float angle = Mathf.Atan2(y, x) * 57.29578f - 90f;
		return UbhUtil.GetNormalizedAngle(angle);
	}

	public static readonly Vector3 VECTOR3_ZERO = Vector3.zero;

	public static readonly Vector3 VECTOR3_ONE = Vector3.one;

	public static readonly Vector3 VECTOR3_HALF = new Vector3(0.5f, 0.5f, 0.5f);

	public static readonly Vector2 VECTOR2_ZERO = Vector2.zero;

	public static readonly Vector2 VECTOR2_ONE = Vector2.one;

	public static readonly Vector2 VECTOR2_HALF = new Vector2(0.5f, 0.5f);

	public static readonly Quaternion QUATERNION_IDENTITY = Quaternion.identity;

	public enum AXIS
	{
		X_AND_Y,
		X_AND_Z
	}

	public enum TIME
	{
		DELTA_TIME,
		UNSCALED_DELTA_TIME,
		FIXED_DELTA_TIME
	}
}

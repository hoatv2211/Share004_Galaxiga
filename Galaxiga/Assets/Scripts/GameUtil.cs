using System;
using System.Collections;
using DG.Tweening;
using PathologicalGames;
using UnityEngine;

public class GameUtil
{
	public static GameObject ObjectPoolSpawnGameObject(string poolName, string nameObj, Vector3 _positon, Quaternion _rotation)
	{
		return PoolManager.Pools[poolName].Spawn(nameObj, _positon, _rotation).gameObject;
	}

	public static void ObjectPoolSpawn(string poolName, string nameObj, Vector3 _positon, Quaternion _rotation)
	{
		PoolManager.Pools[poolName].Spawn(nameObj, _positon, _rotation);
	}

	public static void ObjectPoolDespawn(string poolName, GameObject obj)
	{
		PoolManager.Pools[poolName].Despawn(obj.transform);
	}

	public static void ShowPopup(GameObject popup, string IDDoTween)
	{
		popup.SetActive(true);
		DOTween.Restart(IDDoTween, true, -1f);
		DOTween.Play(IDDoTween);
	}

	public static void HidePopup(GameObject popup, string IDDoTween)
	{
		popup.SetActive(false);
	}

	private static IEnumerator DelayHidePopup(GameObject popup)
	{
		yield return new WaitForSeconds(0.25f);
		popup.SetActive(false);
		yield break;
	}

	public static string FormatNumber(int number)
	{
		return number.ToString("N0").Replace(',', ' ');
	}

	public static float AngleSigned(Vector3 v1, Vector3 v2, Vector3 n)
	{
		return Mathf.Atan2(Vector3.Dot(n, Vector3.Cross(v1, v2)), Vector3.Dot(v1, v2)) * 57.29578f;
	}

	public static DateTime UnixTimestampToDateTimeLocal(long unixTime)
	{
		return DateTimeOffset.FromUnixTimeMilliseconds(unixTime).LocalDateTime;
	}

	public static long DateTimeToUnixTimestamp(DateTime dateTime)
	{
        return ((DateTimeOffset)dateTime).ToUnixTimeMilliseconds();
    }

	public static long DateTimeToUnixTimestamp(string dateTimeBinary)
	{
        DateTime dateTime = DateTime.FromBinary(Convert.ToInt64(dateTimeBinary));
        return ((DateTimeOffset)dateTime).ToUnixTimeMilliseconds();
    }

	public static string[] SplitString(string myString)
	{
		return myString.Split(new char[]
		{
			','
		});
	}

	public static int ConverStringToInt(string myString)
	{
		return int.Parse(myString);
	}

	public static string GetFormatTimeFrom(float seconds)
	{
		GameUtil.timeSpan = TimeSpan.FromSeconds((double)seconds);
		return GameUtil.timeSpan.ToString("mm\\:ss");
	}

	public static string GetFormatTimeFrom(int seconds)
	{
		GameUtil.timeSpan = TimeSpan.FromSeconds((double)seconds);
		return GameUtil.timeSpan.ToString("mm\\:ss");
	}

	private static TimeSpan timeSpan;
}

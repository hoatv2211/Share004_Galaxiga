using System;
using UnityEngine;

public class LTUtility
{
	public static Vector3[] reverse(Vector3[] arr)
	{
		int num = arr.Length;
		int i = 0;
		int num2 = num - 1;
		while (i < num2)
		{
			Vector3 vector = arr[i];
			arr[i] = arr[num2];
			arr[num2] = vector;
			i++;
			num2--;
		}
		return arr;
	}
}

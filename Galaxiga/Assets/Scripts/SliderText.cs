using System;
using UnityEngine;
using UnityEngine.UI;

public class SliderText : MonoBehaviour
{
	public void SetText(float value)
	{
		base.GetComponent<Text>().text = value.ToString("f2");
	}
}

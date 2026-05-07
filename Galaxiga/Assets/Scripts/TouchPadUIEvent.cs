using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TouchPadUIEvent : MonoBehaviour
{
	public void TouchDown()
	{
		this.touchDownText.text = "YES";
		base.StartCoroutine(this.ClearText(this.touchDownText));
	}

	public void TouchEvt(Vector2 value)
	{
		this.touchText.text = value.ToString();
	}

	public void TouchUp()
	{
		this.touchUpText.text = "YES";
		base.StartCoroutine(this.ClearText(this.touchUpText));
		base.StartCoroutine(this.ClearText(this.touchText));
	}

	private IEnumerator ClearText(Text textToCLead)
	{
		yield return new WaitForSeconds(0.3f);
		textToCLead.text = string.Empty;
		yield break;
	}

	public Text touchDownText;

	public Text touchText;

	public Text touchUpText;
}

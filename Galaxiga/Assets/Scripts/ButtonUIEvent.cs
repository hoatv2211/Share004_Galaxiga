using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUIEvent : MonoBehaviour
{
	public void Down()
	{
		this.downText.text = "YES";
		base.StartCoroutine(this.ClearText(this.downText));
	}

	public void Up()
	{
		this.upText.text = "YES";
		base.StartCoroutine(this.ClearText(this.upText));
		base.StartCoroutine(this.ClearText(this.pressText));
		base.StartCoroutine(this.ClearText(this.pressValueText));
	}

	public void Press()
	{
		this.pressText.text = "YES";
	}

	public void PressValue(float value)
	{
		this.pressValueText.text = value.ToString();
	}

	private IEnumerator ClearText(Text textToCLead)
	{
		yield return new WaitForSeconds(0.3f);
		textToCLead.text = string.Empty;
		yield break;
	}

	public Text downText;

	public Text pressText;

	public Text pressValueText;

	public Text upText;
}

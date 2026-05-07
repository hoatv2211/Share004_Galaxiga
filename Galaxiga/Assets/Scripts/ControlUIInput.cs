using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ControlUIInput : MonoBehaviour
{
	private void Update()
	{
		this.getAxisText.text = ETCInput.GetAxis("Horizontal").ToString("f2");
		this.getAxisSpeedText.text = ETCInput.GetAxisSpeed("Horizontal").ToString("f2");
		this.getAxisYText.text = ETCInput.GetAxis("Vertical").ToString("f2");
		this.getAxisYSpeedText.text = ETCInput.GetAxisSpeed("Vertical").ToString("f2");
		if (ETCInput.GetAxisDownRight("Horizontal"))
		{
			this.downRightText.text = "YES";
			base.StartCoroutine(this.ClearText(this.downRightText));
		}
		if (ETCInput.GetAxisDownDown("Vertical"))
		{
			this.downDownText.text = "YES";
			base.StartCoroutine(this.ClearText(this.downDownText));
		}
		if (ETCInput.GetAxisDownLeft("Horizontal"))
		{
			this.downLeftText.text = "YES";
			base.StartCoroutine(this.ClearText(this.downLeftText));
		}
		if (ETCInput.GetAxisDownUp("Vertical"))
		{
			this.downUpText.text = "YES";
			base.StartCoroutine(this.ClearText(this.downUpText));
		}
		if (ETCInput.GetAxisPressedRight("Horizontal"))
		{
			this.rightText.text = "YES";
		}
		else
		{
			this.rightText.text = string.Empty;
		}
		if (ETCInput.GetAxisPressedDown("Vertical"))
		{
			this.downText.text = "YES";
		}
		else
		{
			this.downText.text = string.Empty;
		}
		if (ETCInput.GetAxisPressedLeft("Horizontal"))
		{
			this.leftText.text = "Yes";
		}
		else
		{
			this.leftText.text = string.Empty;
		}
		if (ETCInput.GetAxisPressedUp("Vertical"))
		{
			this.upText.text = "YES";
		}
		else
		{
			this.upText.text = string.Empty;
		}
	}

	private IEnumerator ClearText(Text textToCLead)
	{
		yield return new WaitForSeconds(0.3f);
		textToCLead.text = string.Empty;
		yield break;
	}

	public Text getAxisText;

	public Text getAxisSpeedText;

	public Text getAxisYText;

	public Text getAxisYSpeedText;

	public Text downRightText;

	public Text downDownText;

	public Text downLeftText;

	public Text downUpText;

	public Text rightText;

	public Text downText;

	public Text leftText;

	public Text upText;
}

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInputUI : MonoBehaviour
{
	private void Update()
	{
		if (ETCInput.GetButton("Button"))
		{
			this.getButtonText.text = "YES";
			this.getButtonTimeText.text = ETCInput.GetButtonValue("Button").ToString();
		}
		else
		{
			this.getButtonText.text = string.Empty;
			this.getButtonTimeText.text = string.Empty;
		}
		if (ETCInput.GetButtonDown("Button"))
		{
			this.getButtonDownText.text = "YES";
			base.StartCoroutine(this.ClearText(this.getButtonDownText));
		}
		if (ETCInput.GetButtonUp("Button"))
		{
			this.getButtonUpText.text = "YES";
			base.StartCoroutine(this.ClearText(this.getButtonUpText));
		}
	}

	private IEnumerator ClearText(Text textToCLead)
	{
		yield return new WaitForSeconds(0.3f);
		textToCLead.text = string.Empty;
		yield break;
	}

	public void SetSwipeIn(bool value)
	{
		ETCInput.SetControlSwipeIn("Button", value);
	}

	public void SetSwipeOut(bool value)
	{
		ETCInput.SetControlSwipeOut("Button", value);
	}

	public void setTimePush(bool value)
	{
		ETCInput.SetAxisOverTime("Button", value);
	}

	public Text getButtonDownText;

	public Text getButtonText;

	public Text getButtonTimeText;

	public Text getButtonUpText;
}

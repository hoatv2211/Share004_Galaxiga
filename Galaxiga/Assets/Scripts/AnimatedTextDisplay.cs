using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedTextDisplay : MonoBehaviour
{
	private void Awake()
	{
		AnimatedTextDisplay.current = this;
	}

	public void DisplayText(TextMeshProUGUI textMeshProUGUI, string _string)
	{
		textMeshProUGUI.text = string.Empty;
		this.characterIndex = 0;
		base.StartCoroutine(this.DisplayTimer(textMeshProUGUI, _string));
	}

	public void DisplayText(Text textUGUI, string _string)
	{
		textUGUI.text = string.Empty;
		this.characterIndex = 0;
		base.StartCoroutine(this.DisplayTimer(textUGUI, _string));
	}

	private IEnumerator DisplayTimer(TextMeshProUGUI textMeshProUGUI, string _text)
	{
		for (;;)
		{
			yield return new WaitForSeconds(this.speedDisplay);
			if (this.characterIndex <= _text.Length)
			{
				textMeshProUGUI.text = _text.Substring(0, this.characterIndex);
				this.characterIndex++;
			}
		}
		yield break;
	}

	private IEnumerator DisplayTimer(Text textUGUI, string _text)
	{
		for (;;)
		{
			yield return new WaitForSeconds(this.speedDisplay);
			if (this.characterIndex <= _text.Length)
			{
				textUGUI.text = _text.Substring(0, this.characterIndex);
				this.characterIndex++;
			}
		}
		yield break;
	}

	public static AnimatedTextDisplay current;

	public float speedDisplay = 0.001f;

	private int characterIndex;
}

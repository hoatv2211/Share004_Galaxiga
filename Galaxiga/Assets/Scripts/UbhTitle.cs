using System;
using UnityEngine;
using UnityEngine.UI;

public class UbhTitle : UbhMonoBehaviour
{
	private void Start()
	{
		this.m_startText.text = ((!UbhUtil.IsMobilePlatform()) ? "Press X" : "Tap To Start");
	}

	private const string TITLE_PC = "Press X";

	private const string TITLE_MOBILE = "Tap To Start";

	[SerializeField]
	private Text m_startText;
}

using System;
using UnityEngine;

namespace SuperScrollView
{
	public class FPSDisplay : MonoBehaviour
	{
		private void Awake()
		{
			this.mStyle = new GUIStyle();
			this.mStyle.alignment = TextAnchor.UpperLeft;
			this.mStyle.normal.background = null;
			this.mStyle.fontSize = 25;
			this.mStyle.normal.textColor = new Color(0f, 1f, 0f, 1f);
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		private void Update()
		{
			this.deltaTime += (Time.deltaTime - this.deltaTime) * 0.1f;
		}

		private void OnGUI()
		{
			int width = Screen.width;
			int height = Screen.height;
			Rect position = new Rect(0f, 0f, (float)width, (float)(height * 2 / 100));
			float num = 1f / this.deltaTime;
			string text = string.Format("   {0:0.} FPS", num);
			GUI.Label(position, text, this.mStyle);
		}

		private float deltaTime;

		private GUIStyle mStyle;
	}
}

using System;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
	private void Awake()
	{
		TimeManager.current = this;
	}

	public void DoSlowmotion()
	{
		Time.timeScale = this.slowDownFactor;
		Time.fixedDeltaTime = Time.timeScale * 0.5f;
	}

	public void StopSlowmotion()
	{
		Time.timeScale = 1f / this.slowDownLength * Time.unscaledDeltaTime;
		Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
	}

	public void SetMinusTime()
	{
		if (this.totalTime >= 0f)
		{
			this.totalTime -= Time.deltaTime;
			this.minute = (int)this.totalTime / 60;
			this.second = (int)this.totalTime % 60;
			string str = (this.minute >= 10) ? (string.Empty + this.minute) : ("0" + this.minute);
			string str2 = (this.second >= 10) ? (string.Empty + this.second) : ("0" + this.second);
			this.lblTime.text = str + ":" + str2;
		}
	}

	public void SetTime()
	{
		this.timer += Time.deltaTime;
		int num = Mathf.FloorToInt(this.timer / 60f);
		int num2 = Mathf.FloorToInt(this.timer - (float)(num * 60));
		string text = string.Format("{0:0}:{1:00}", num, num2);
		this.lblTime.text = text;
	}

	private void ShowTime()
	{
		this.timer += Time.deltaTime;
		int num = (int)(this.timer % 60f);
		int num2 = (int)(this.timer / 60f) % 60;
		int num3 = (int)(this.timer / 3600f) % 24;
		string text = string.Format("{0:0}:{1:00}:{2:00}", num3, num2, num);
		this.lblTime.text = text;
	}

	public static TimeManager current;

	public Text lblTime;

	private float timer;

	private float totalTime = 500f;

	private int minute;

	private int second;

	private float slowDownFactor = 0.5f;

	private float slowDownLength = 2f;
}

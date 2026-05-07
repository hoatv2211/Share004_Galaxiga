using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ControlUIEvent : MonoBehaviour
{
	private void Update()
	{
		if (this.isDown)
		{
			this.downText.text = "YES";
			this.isDown = false;
		}
		else
		{
			this.downText.text = string.Empty;
		}
		if (this.isLeft)
		{
			this.leftText.text = "YES";
			this.isLeft = false;
		}
		else
		{
			this.leftText.text = string.Empty;
		}
		if (this.isUp)
		{
			this.upText.text = "YES";
			this.isUp = false;
		}
		else
		{
			this.upText.text = string.Empty;
		}
		if (this.isRight)
		{
			this.rightText.text = "YES";
			this.isRight = false;
		}
		else
		{
			this.rightText.text = string.Empty;
		}
	}

	public void MoveStart()
	{
		this.moveStartText.text = "YES";
		base.StartCoroutine(this.ClearText(this.moveStartText));
	}

	public void Move(Vector2 move)
	{
		this.moveText.text = move.ToString();
	}

	public void MoveSpeed(Vector2 move)
	{
		this.moveSpeedText.text = move.ToString();
	}

	public void MoveEnd()
	{
		if (this.moveEndText.enabled)
		{
			this.moveEndText.text = "YES";
			base.StartCoroutine(this.ClearText(this.moveEndText));
			base.StartCoroutine(this.ClearText(this.touchUpText));
			base.StartCoroutine(this.ClearText(this.moveText));
			base.StartCoroutine(this.ClearText(this.moveSpeedText));
		}
	}

	public void TouchStart()
	{
		this.touchStartText.text = "YES";
		base.StartCoroutine(this.ClearText(this.touchStartText));
	}

	public void TouchUp()
	{
		this.touchUpText.text = "YES";
		base.StartCoroutine(this.ClearText(this.touchUpText));
		base.StartCoroutine(this.ClearText(this.moveText));
		base.StartCoroutine(this.ClearText(this.moveSpeedText));
	}

	public void DownRight()
	{
		this.downRightText.text = "YES";
		base.StartCoroutine(this.ClearText(this.downRightText));
	}

	public void DownDown()
	{
		this.downDownText.text = "YES";
		base.StartCoroutine(this.ClearText(this.downDownText));
	}

	public void DownLeft()
	{
		this.downLeftText.text = "YES";
		base.StartCoroutine(this.ClearText(this.downLeftText));
	}

	public void DownUp()
	{
		this.downUpText.text = "YES";
		base.StartCoroutine(this.ClearText(this.downUpText));
	}

	public void Right()
	{
		this.isRight = true;
	}

	public void Down()
	{
		this.isDown = true;
	}

	public void Left()
	{
		this.isLeft = true;
	}

	public void Up()
	{
		this.isUp = true;
	}

	private IEnumerator ClearText(Text textToCLead)
	{
		yield return new WaitForSeconds(0.3f);
		textToCLead.text = string.Empty;
		yield break;
	}

	public Text moveStartText;

	public Text moveText;

	public Text moveSpeedText;

	public Text moveEndText;

	public Text touchStartText;

	public Text touchUpText;

	public Text downRightText;

	public Text downDownText;

	public Text downLeftText;

	public Text downUpText;

	public Text rightText;

	public Text downText;

	public Text leftText;

	public Text upText;

	private bool isDown;

	private bool isLeft;

	private bool isUp;

	private bool isRight;
}

using System;
using UnityEngine;

public class FXQ_MoveParticle : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (this.m_MoveMethod != this.m_MoveMethodOld || this.m_Range != this.m_RangeOld)
		{
			this.m_MoveMethodOld = this.m_MoveMethod;
			this.ResetPosition();
		}
		switch (this.m_MoveMethod)
		{
		case eMoveMethod.LeftRight:
			this.UpdateLeftRight();
			break;
		case eMoveMethod.UpDown:
			this.UpdateUpDown();
			break;
		case eMoveMethod.CircularXY_Clockwise:
			this.UpdateCircularXY_Clockwise();
			break;
		case eMoveMethod.CircularXY_CounterClockwise:
			this.UpdateCircularXY_CounterClockwise();
			break;
		case eMoveMethod.CircularXZ_Clockwise:
			this.UpdateCircularXZ_Clockwise();
			break;
		case eMoveMethod.CircularXZ_CounterClockwise:
			this.UpdateCircularXZ_CounterClockwise();
			break;
		case eMoveMethod.CircularYZ_Clockwise:
			this.UpdateCircularYZ_Clockwise();
			break;
		case eMoveMethod.CircularYZ_CounterClockwise:
			this.UpdateCircularYZ_CounterClockwise();
			break;
		}
	}

	private void ResetPosition()
	{
		switch (this.m_MoveMethod)
		{
		case eMoveMethod.LeftRight:
		case eMoveMethod.UpDown:
			base.transform.localPosition = new Vector3(0f, 0f, 0f);
			break;
		case eMoveMethod.CircularXY_Clockwise:
		case eMoveMethod.CircularXY_CounterClockwise:
		case eMoveMethod.CircularXZ_Clockwise:
		case eMoveMethod.CircularXZ_CounterClockwise:
			base.transform.localPosition = new Vector3(this.m_Range, 0f, 0f);
			break;
		case eMoveMethod.CircularYZ_Clockwise:
		case eMoveMethod.CircularYZ_CounterClockwise:
			base.transform.localPosition = new Vector3(0f, 0f, this.m_Range);
			break;
		}
		this.m_RangeOld = this.m_Range;
	}

	private void UpdateLeftRight()
	{
		if (!this.m_ToggleDirectionFlag)
		{
			base.transform.localPosition = new Vector3(base.transform.localPosition.x - this.m_Speed * Time.deltaTime, 0f, 0f);
			if (base.transform.localPosition.x <= -this.m_Range)
			{
				this.m_ToggleDirectionFlag = true;
			}
		}
		else
		{
			base.transform.localPosition = new Vector3(base.transform.localPosition.x + this.m_Speed * Time.deltaTime, 0f, 0f);
			if (base.transform.localPosition.x >= this.m_Range)
			{
				this.m_ToggleDirectionFlag = false;
			}
		}
	}

	private void UpdateUpDown()
	{
		if (!this.m_ToggleDirectionFlag)
		{
			base.transform.localPosition = new Vector3(0f, base.transform.localPosition.y + this.m_Speed * Time.deltaTime, 0f);
			if (base.transform.localPosition.y >= this.m_Range)
			{
				this.m_ToggleDirectionFlag = true;
			}
		}
		else
		{
			base.transform.localPosition = new Vector3(0f, base.transform.localPosition.y - this.m_Speed * Time.deltaTime, 0f);
			if (base.transform.localPosition.y <= -this.m_Range)
			{
				this.m_ToggleDirectionFlag = false;
			}
		}
	}

	private void UpdateCircularXY_Clockwise()
	{
		float num = 0f;
		float num2 = 0f;
		float x = base.transform.localPosition.x;
		float y = base.transform.localPosition.y;
		float x2 = num + ((x - num) * Mathf.Cos(this.m_Speed / 360f) - (y - num2) * Mathf.Sin(this.m_Speed / 360f));
		float y2 = num2 + ((x - num) * Mathf.Sin(this.m_Speed / 360f) + (y - num2) * Mathf.Cos(this.m_Speed / 360f));
		base.transform.localPosition = new Vector3(x2, y2, 0f);
	}

	private void UpdateCircularXY_CounterClockwise()
	{
		float num = 0f;
		float num2 = 0f;
		float x = base.transform.localPosition.x;
		float y = base.transform.localPosition.y;
		float x2 = num + ((x - num) * Mathf.Cos(-this.m_Speed / 360f) - (y - num2) * Mathf.Sin(-this.m_Speed / 360f));
		float y2 = num2 + ((x - num) * Mathf.Sin(-this.m_Speed / 360f) + (y - num2) * Mathf.Cos(-this.m_Speed / 360f));
		base.transform.localPosition = new Vector3(x2, y2, 0f);
	}

	private void UpdateCircularXZ_Clockwise()
	{
		float num = 0f;
		float num2 = 0f;
		float x = base.transform.localPosition.x;
		float z = base.transform.localPosition.z;
		float x2 = num + ((x - num) * Mathf.Cos(this.m_Speed / 360f) - (z - num2) * Mathf.Sin(this.m_Speed / 360f));
		float z2 = num2 + ((x - num) * Mathf.Sin(this.m_Speed / 360f) + (z - num2) * Mathf.Cos(this.m_Speed / 360f));
		base.transform.localPosition = new Vector3(x2, 0f, z2);
	}

	private void UpdateCircularXZ_CounterClockwise()
	{
		float num = 0f;
		float num2 = 0f;
		float x = base.transform.localPosition.x;
		float z = base.transform.localPosition.z;
		float x2 = num + ((x - num) * Mathf.Cos(-this.m_Speed / 360f) - (z - num2) * Mathf.Sin(-this.m_Speed / 360f));
		float z2 = num2 + ((x - num) * Mathf.Sin(-this.m_Speed / 360f) + (z - num2) * Mathf.Cos(-this.m_Speed / 360f));
		base.transform.localPosition = new Vector3(x2, 0f, z2);
	}

	private void UpdateCircularYZ_Clockwise()
	{
		float num = 0f;
		float num2 = 0f;
		float y = base.transform.localPosition.y;
		float z = base.transform.localPosition.z;
		float y2 = num + ((y - num) * Mathf.Cos(this.m_Speed / 360f) - (z - num2) * Mathf.Sin(this.m_Speed / 360f));
		float z2 = num2 + ((y - num) * Mathf.Sin(this.m_Speed / 360f) + (z - num2) * Mathf.Cos(this.m_Speed / 360f));
		base.transform.localPosition = new Vector3(0f, y2, z2);
	}

	private void UpdateCircularYZ_CounterClockwise()
	{
		float num = 0f;
		float num2 = 0f;
		float y = base.transform.localPosition.y;
		float z = base.transform.localPosition.z;
		float y2 = num + ((y - num) * Mathf.Cos(-this.m_Speed / 360f) - (z - num2) * Mathf.Sin(-this.m_Speed / 360f));
		float z2 = num2 + ((y - num) * Mathf.Sin(-this.m_Speed / 360f) + (z - num2) * Mathf.Cos(-this.m_Speed / 360f));
		base.transform.localPosition = new Vector3(0f, y2, z2);
	}

	public eMoveMethod m_MoveMethod;

	private eMoveMethod m_MoveMethodOld;

	public float m_Range = 5f;

	private float m_RangeOld = 5f;

	public float m_Speed = 2f;

	private bool m_ToggleDirectionFlag;
}

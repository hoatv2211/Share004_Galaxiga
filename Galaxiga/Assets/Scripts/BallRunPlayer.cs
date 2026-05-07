using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

public class BallRunPlayer : MonoBehaviour
{
	private void OnEnable()
	{
		EasyTouch.On_SwipeEnd += this.On_SwipeEnd;
	}

	private void OnDestroy()
	{
		EasyTouch.On_SwipeEnd -= this.On_SwipeEnd;
	}

	private void Start()
	{
		this.characterController = base.GetComponent<CharacterController>();
		this.startPosition = base.transform.position;
	}

	private void Update()
	{
		if (this.start)
		{
			this.moveDirection = base.transform.TransformDirection(Vector3.forward) * 10f * Time.deltaTime;
			this.moveDirection.y = this.moveDirection.y - 9.81f * Time.deltaTime;
			if (this.isJump)
			{
				this.moveDirection.y = 8f;
				this.isJump = false;
			}
			this.characterController.Move(this.moveDirection);
			this.ballModel.Rotate(Vector3.right * 400f * Time.deltaTime);
		}
		if ((double)base.transform.position.y < 0.5)
		{
			this.start = false;
			base.transform.position = this.startPosition;
		}
	}

	private void OnCollision()
	{
		UnityEngine.Debug.Log("ok");
	}

	private void On_SwipeEnd(Gesture gesture)
	{
		if (this.start)
		{
			switch (gesture.swipe)
			{
			case EasyTouch.SwipeDirection.Left:
			case EasyTouch.SwipeDirection.UpLeft:
			case EasyTouch.SwipeDirection.DownLeft:
				base.transform.Rotate(Vector3.up * -90f);
				break;
			case EasyTouch.SwipeDirection.Right:
			case EasyTouch.SwipeDirection.UpRight:
			case EasyTouch.SwipeDirection.DownRight:
				base.transform.Rotate(Vector3.up * 90f);
				break;
			case EasyTouch.SwipeDirection.Up:
				if (this.characterController.isGrounded)
				{
					this.isJump = true;
				}
				break;
			}
		}
	}

	public void StartGame()
	{
		this.start = true;
	}

	public Transform ballModel;

	private bool start;

	private Vector3 moveDirection;

	private CharacterController characterController;

	private Vector3 startPosition;

	private bool isJump;
}

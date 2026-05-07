using System;
using UnityEngine;

namespace SkyGameKit
{
	public class PlaneState : StateMachineBehaviour
	{
		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			this.oldPosX = animator.transform.position.x;
		}

		public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			if (PlaneState.ActivePlaneState)
			{
				float num = animator.transform.position.x - this.oldPosX;
				float num2 = num / Time.deltaTime;
				float num3 = num2 * this.inclinationPerSpeed;
				this.oldPosX = animator.transform.position.x;
				if (Mathf.Abs(num) > 0.001f)
				{
					if (animator.GetFloat("PlaneState") * num < 0f)
					{
						num3 *= 5f;
					}
					animator.SetFloat("PlaneState", animator.GetFloat("PlaneState") + num3);
				}
				else
				{
					animator.SetFloat("PlaneState", Fu.GoTo0(animator.GetFloat("PlaneState"), this.inclinationPerSpeed * 15f));
				}
				animator.SetFloat("PlaneState", Mathf.Clamp(animator.GetFloat("PlaneState"), -1f, 1f));
			}
		}

		public float inclinationPerSpeed = 0.008f;

		private float oldPosX;

		public static bool ActivePlaneState = true;
	}
}

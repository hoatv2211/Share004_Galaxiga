using System;
using UnityEngine;

namespace SkyGameKit
{
	public class EnemyPlaneState : StateMachineBehaviour
	{
		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			this.oldPos = animator.transform.position;
			this.oldRot = animator.transform.rotation.eulerAngles.z;
		}

		public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			this.deltaPos = Vector3.Distance(this.oldPos, animator.transform.position);
			this.deltaRot = Mathf.DeltaAngle(animator.transform.rotation.eulerAngles.z, this.oldRot);
			if (this.deltaPos <= 0f && this.deltaPos >= 1f)
			{
				return;
			}
			float num = this.inclinationPerDelta * (this.deltaRot / this.deltaPos);
			if (Mathf.Abs(this.deltaRot) > 0.1f)
			{
				if (animator.GetFloat("PlaneState") * this.deltaRot < 0f)
				{
					num *= 3f;
				}
				animator.SetFloat("PlaneState", animator.GetFloat("PlaneState") + num);
			}
			else
			{
				animator.SetFloat("PlaneState", Fu.GoTo0(animator.GetFloat("PlaneState"), this.inclinationPerDelta * (0.5f / this.deltaPos)));
			}
			animator.SetFloat("PlaneState", Mathf.Clamp(animator.GetFloat("PlaneState"), -1f, 1f));
			this.oldRot = animator.transform.rotation.eulerAngles.z;
			this.oldPos = animator.transform.position;
		}

		public float inclinationPerDelta = 0.05f;

		private Vector3 oldPos;

		private float oldRot;

		private float deltaPos;

		private float deltaRot;
	}
}

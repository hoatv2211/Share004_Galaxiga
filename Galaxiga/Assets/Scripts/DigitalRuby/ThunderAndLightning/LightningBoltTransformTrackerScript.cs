using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DigitalRuby.ThunderAndLightning
{
	public class LightningBoltTransformTrackerScript : MonoBehaviour
	{
		private void Start()
		{
			if (this.LightningScript != null)
			{
				this.LightningScript.CustomTransformHandler.RemoveAllListeners();
				this.LightningScript.CustomTransformHandler.AddListener(new UnityAction<LightningCustomTransformStateInfo>(this.CustomTransformHandler));
			}
		}

		private static float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
		{
			Vector2 normalized = (vec2 - vec1).normalized;
			return Vector2.Angle(Vector2.right, normalized) * Mathf.Sign(vec2.y - vec1.y);
		}

		private static void UpdateTransform(LightningCustomTransformStateInfo state, LightningBoltPrefabScript script, RangeOfFloats scaleLimit)
		{
			if (state.Transform == null || state.StartTransform == null)
			{
				return;
			}
			if (state.EndTransform == null)
			{
				state.Transform.position = state.StartTransform.position - state.BoltStartPosition;
				return;
			}
			Quaternion rotation;
			if ((script.CameraMode == CameraMode.Auto && script.Camera.orthographic) || script.CameraMode == CameraMode.OrthographicXY)
			{
				float num = LightningBoltTransformTrackerScript.AngleBetweenVector2(state.BoltStartPosition, state.BoltEndPosition);
				float num2 = LightningBoltTransformTrackerScript.AngleBetweenVector2(state.StartTransform.position, state.EndTransform.position);
				rotation = Quaternion.AngleAxis(num2 - num, Vector3.forward);
			}
			if (script.CameraMode == CameraMode.OrthographicXZ)
			{
				float num3 = LightningBoltTransformTrackerScript.AngleBetweenVector2(new Vector2(state.BoltStartPosition.x, state.BoltStartPosition.z), new Vector2(state.BoltEndPosition.x, state.BoltEndPosition.z));
				float num4 = LightningBoltTransformTrackerScript.AngleBetweenVector2(new Vector2(state.StartTransform.position.x, state.StartTransform.position.z), new Vector2(state.EndTransform.position.x, state.EndTransform.position.z));
				rotation = Quaternion.AngleAxis(num4 - num3, Vector3.up);
			}
			else
			{
				Quaternion rotation2 = Quaternion.LookRotation((state.BoltEndPosition - state.BoltStartPosition).normalized);
				Quaternion lhs = Quaternion.LookRotation((state.EndTransform.position - state.StartTransform.position).normalized);
				rotation = lhs * Quaternion.Inverse(rotation2);
			}
			state.Transform.rotation = rotation;
			float num5 = Vector3.Distance(state.BoltStartPosition, state.BoltEndPosition);
			float num6 = Vector3.Distance(state.EndTransform.position, state.StartTransform.position);
			float num7 = Mathf.Clamp((num5 >= Mathf.Epsilon) ? (num6 / num5) : 1f, scaleLimit.Minimum, scaleLimit.Maximum);
			state.Transform.localScale = new Vector3(num7, num7, num7);
			Vector3 b = rotation * (num7 * state.BoltStartPosition);
			state.Transform.position = state.StartTransform.position - b;
		}

		public virtual void TakeDamage()
		{
		}

		public void CustomTransformHandler(LightningCustomTransformStateInfo state)
		{
			if (!base.enabled)
			{
				return;
			}
			if (this.LightningScript == null)
			{
				UnityEngine.Debug.LogError("LightningScript property must be set to non-null.");
				return;
			}
			if (state.State == LightningCustomTransformState.Executing)
			{
				LightningBoltTransformTrackerScript.UpdateTransform(state, this.LightningScript, this.ScaleLimit);
			}
			else if (state.State == LightningCustomTransformState.Started)
			{
				state.StartTransform = this.StartTarget;
				state.EndTransform = this.EndTarget;
				this.transformStartPositions[base.transform] = state;
				this.TakeDamage();
			}
			else
			{
				this.transformStartPositions.Remove(base.transform);
			}
		}

		[Tooltip("The lightning script to track.")]
		public LightningBoltPrefabScript LightningScript;

		[Tooltip("The transform to track which will be where the bolts are emitted from.")]
		public Transform StartTarget;

		[Tooltip("(Optional) The transform to track which will be where the bolts are emitted to. If no end target is specified, lightning will simply move to stay on top of the start target.")]
		public Transform EndTarget;

		[SingleLine("Scaling limits.")]
		public RangeOfFloats ScaleLimit = new RangeOfFloats
		{
			Minimum = 0.1f,
			Maximum = 10f
		};

		private readonly Dictionary<Transform, LightningCustomTransformStateInfo> transformStartPositions = new Dictionary<Transform, LightningCustomTransformStateInfo>();
	}
}

using System;

namespace UnityEngine.UI.Extensions
{
	public class CUIBezierCurve : MonoBehaviour
	{
		public Vector3[] ControlPoints
		{
			get
			{
				return this.controlPoints;
			}
		}

		public void Refresh()
		{
			if (this.OnRefresh != null)
			{
				this.OnRefresh();
			}
		}

		public Vector3 GetPoint(float _time)
		{
			float num = 1f - _time;
			return num * num * num * this.controlPoints[0] + 3f * num * num * _time * this.controlPoints[1] + 3f * num * _time * _time * this.controlPoints[2] + _time * _time * _time * this.controlPoints[3];
		}

		public Vector3 GetTangent(float _time)
		{
			float num = 1f - _time;
			return 3f * num * num * (this.controlPoints[1] - this.controlPoints[0]) + 6f * num * _time * (this.controlPoints[2] - this.controlPoints[1]) + 3f * _time * _time * (this.controlPoints[3] - this.controlPoints[2]);
		}

		public void ReportSet()
		{
			if (this.controlPoints == null)
			{
				this.controlPoints = new Vector3[CUIBezierCurve.CubicBezierCurvePtNum];
				this.controlPoints[0] = new Vector3(0f, 0f, 0f);
				this.controlPoints[1] = new Vector3(0f, 1f, 0f);
				this.controlPoints[2] = new Vector3(1f, 1f, 0f);
				this.controlPoints[3] = new Vector3(1f, 0f, 0f);
			}
			bool flag = true;
			flag &= (this.controlPoints.Length == CUIBezierCurve.CubicBezierCurvePtNum);
		}

		public static readonly int CubicBezierCurvePtNum = 4;

		[SerializeField]
		protected Vector3[] controlPoints;

		public Action OnRefresh;
	}
}

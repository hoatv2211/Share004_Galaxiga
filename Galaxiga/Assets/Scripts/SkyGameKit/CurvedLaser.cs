using System;
using System.Collections.Generic;
using UnityEngine;

namespace SkyGameKit
{
	[RequireComponent(typeof(LineRenderer))]
	public class CurvedLaser : MonoBehaviour
	{
		private void Start()
		{
			this.line = base.GetComponent<LineRenderer>();
			this.points = new Vector3[this.pointNumber];
			for (int i = 0; i < this.pointNumber; i++)
			{
				this.points[i] = base.transform.position + Vector3.up * (float)i * 0.5f;
			}
			this.line.positionCount = this.pointNumber;
			for (int j = 0; j < this.pointNumber * this.followPlayerSpeed; j++)
			{
				this.playerXPos.Add(base.transform.position.x);
			}
		}

		private void Update()
		{
			this.points[0] = base.transform.position;
			for (int i = 1; i < this.pointNumber; i++)
			{
				Vector2 b;
				b.x = this.playerXPos[this.playerXPos.Count - i * this.followPlayerSpeed];
				b.y = base.transform.position.y + (float)i * 0.5f;
				this.points[i] = Vector2.Lerp(this.points[i], b, 0.5f);
				if (i < this.listBox.Length - 1)
				{
					this.listBox[i].transform.position = this.points[i];
				}
			}
			this.line.SetPositions(this.points);
		}

		private void FixedUpdate()
		{
			this.playerXPos.Add(base.transform.position.x);
			if (this.playerXPos.Count > this.pointNumber * this.followPlayerSpeed)
			{
				this.playerXPos.RemoveAt(0);
			}
		}

		public int pointNumber = 40;

		public int followPlayerSpeed = 1;

		private LineRenderer line;

		private Vector3[] points;

		[SerializeField]
		private GameObject[] listBox;

		private List<float> playerXPos = new List<float>();
	}
}

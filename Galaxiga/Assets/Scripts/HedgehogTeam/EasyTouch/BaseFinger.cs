using System;
using UnityEngine;

namespace HedgehogTeam.EasyTouch
{
	public class BaseFinger
	{
		public Gesture GetGesture()
		{
			return new Gesture
			{
				fingerIndex = this.fingerIndex,
				touchCount = this.touchCount,
				startPosition = this.startPosition,
				position = this.position,
				deltaPosition = this.deltaPosition,
				actionTime = this.actionTime,
				deltaTime = this.deltaTime,
				isOverGui = this.isOverGui,
				pickedCamera = this.pickedCamera,
				pickedObject = this.pickedObject,
				isGuiCamera = this.isGuiCamera,
				pickedUIElement = this.pickedUIElement,
				altitudeAngle = this.altitudeAngle,
				azimuthAngle = this.azimuthAngle,
				maximumPossiblePressure = this.maximumPossiblePressure,
				pressure = this.pressure,
				radius = this.radius,
				radiusVariance = this.radiusVariance,
				touchType = this.touchType
			};
		}

		public int fingerIndex;

		public int touchCount;

		public Vector2 startPosition;

		public Vector2 position;

		public Vector2 deltaPosition;

		public float actionTime;

		public float deltaTime;

		public Camera pickedCamera;

		public GameObject pickedObject;

		public bool isGuiCamera;

		public bool isOverGui;

		public GameObject pickedUIElement;

		public float altitudeAngle;

		public float azimuthAngle;

		public float maximumPossiblePressure;

		public float pressure;

		public float radius;

		public float radiusVariance;

		public TouchType touchType;
	}
}

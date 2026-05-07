using System;
using UnityEngine;

namespace SkyGameKit
{
	public class SgkCamera : MonoBehaviour
	{
		private void Awake()
		{
			SgkCamera.FixCamera();
		}

		public static Vector2 bottomLeft { get; private set; } = new Vector2(-4.5f, -8f);

		public static Vector2 topRight { get; private set; } = new Vector2(4.5f, 8f);

		private static void FixCamera()
		{
			Camera.main.orthographicSize = 4.5f / Camera.main.aspect;
			SgkCamera.CalculateCameraProperty();
		}

		private static void CalculateCameraProperty()
		{
			SgkCamera.bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, Camera.main.nearClipPlane));
			SgkCamera.topRight = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, Camera.main.nearClipPlane));
		}

		public static bool Outside(Vector2 point)
		{
			return Fu.Outside(point, SgkCamera.bottomLeft, SgkCamera.topRight);
		}

		public static bool Inside(Vector2 point)
		{
			return !SgkCamera.Outside(point);
		}

		public const float WIDTH = 9f;

		public const float DEFAULT_HEIGHT = 16f;
	}
}

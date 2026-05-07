using System;
using UnityEngine;

namespace SkyGameKit.QuickAccess
{
	public class Player
	{
		public static GameObject gameObject
		{
			get
			{
				if (Player._playerGameObject == null)
				{
					Player._playerGameObject = GameObject.FindWithTag("Player");
				}
				return Player._playerGameObject;
			}
		}

		public static Transform transform
		{
			get
			{
				if (Player.gameObject == null)
				{
					return null;
				}
				return Player.gameObject.transform;
			}
		}

		public static APlayerAttack Attack
		{
			get
			{
				if (Player._attack == null)
				{
					Player._attack = Player.GetPlayerComponent<APlayerAttack>();
				}
				return Player._attack;
			}
		}

		public static APlayerMove Move
		{
			get
			{
				if (Player._move == null)
				{
					Player._move = Player.GetPlayerComponent<APlayerMove>();
				}
				return Player._move;
			}
		}

		public static APlayerState State
		{
			get
			{
				if (Player._state == null)
				{
					Player._state = Player.GetPlayerComponent<APlayerState>();
				}
				return Player._state;
			}
		}

		public static APlayerHealth Health
		{
			get
			{
				if (Player._health == null)
				{
					Player._health = Player.GetPlayerComponent<APlayerHealth>();
				}
				return Player._health;
			}
		}

		public static T GetPlayerComponent<T>()
		{
			if (Player.gameObject == null)
			{
				return default(T);
			}
			T t = Player.gameObject.GetComponent<T>();
			if (t == null)
			{
				t = Player.gameObject.GetComponentInChildren<T>();
			}
			return t;
		}

		public const string playerTag = "Player";

		private static GameObject _playerGameObject;

		private static APlayerAttack _attack;

		private static APlayerMove _move;

		private static APlayerState _state;

		private static APlayerHealth _health;
	}
}

using System;
using PathologicalGames;
using UnityEngine;

namespace SkyGameKit
{
	public static class Const
	{
		public static Transform EnemyPoolTransform
		{
			get
			{
				if (Const._enemyPool == null)
				{
					Const._enemyPool = PoolManager.Pools["EnemyPool"].transform;
				}
				return Const._enemyPool;
			}
		}

		public static Transform ExplosionPoolTransform
		{
			get
			{
				if (Const._explosionPool == null)
				{
					Const._explosionPool = PoolManager.Pools["ExplosionPool"].transform;
				}
				return Const._explosionPool;
			}
		}

		public static int GetSequenceWaveID(int index)
		{
			index++;
			return 1000000 * index;
		}

		public static int GetPointWaveID(int index)
		{
			index++;
			return 1000000 * (500 + index);
		}

		public static int GetTurnID(int index, int waveID)
		{
			index++;
			return waveID + 1000 * index;
		}

		public static int GetEnemyID(int index, int TurnID)
		{
			index++;
			return TurnID + index;
		}

		public static int GetPlayerID(int index)
		{
			index++;
			return 1000000000 + index;
		}

		public const string ENEMY_POOL = "EnemyPool";

		public const string EXPLOSION_POOL = "ExplosionPool";

		public const string EFFECT_POOL = "EffectPool";

		public const string ITEM_POOL = "ItemPool";

		public const string PATH_POOL = "PathPool";

		public static readonly string[] allPoolName = new string[]
		{
			"EnemyPool",
			"PathPool",
			"ItemPool",
			"ExplosionPool",
			"EffectPool"
		};

		public const string DEAD_ZONE = "DeadZone";

		public const float offsetCamera = 1f;

		public static Transform _enemyPool;

		public static Transform _explosionPool;

		public const int POINT_WAVE_ID_OFFSET = 500;

		public const int WAVE_ID_BASE = 1000000;

		public const int TURN_ID_BASE = 1000;

		public const int ENEMY_ID_BASE = 1;

		public const int PLAYER_ID_OFFSET = 1000000000;
	}
}

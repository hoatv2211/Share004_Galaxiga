using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BayatGames.SaveGamePro.Extensions
{
	public static class TilemapExtensions
	{
		public static T[] GetTiles<T>(this Tilemap tilemap) where T : TileBase
		{
			List<T> list = new List<T>();
			for (int i = tilemap.origin.y; i < tilemap.origin.y + tilemap.size.y; i++)
			{
				for (int j = tilemap.origin.x; j < tilemap.origin.x + tilemap.size.x; j++)
				{
					Vector3Int position = new Vector3Int(j, i, 0);
					T tile = tilemap.GetTile<T>(position);
					if (tile != null)
					{
						list.Add(tile);
					}
				}
			}
			return list.ToArray();
		}

		public static T[] GetTiles<T>(this Tilemap tilemap, out Vector3Int[] positions) where T : TileBase
		{
			List<T> list = new List<T>();
			List<Vector3Int> list2 = new List<Vector3Int>();
			for (int i = tilemap.origin.y; i < tilemap.origin.y + tilemap.size.y; i++)
			{
				for (int j = tilemap.origin.x; j < tilemap.origin.x + tilemap.size.x; j++)
				{
					Vector3Int vector3Int = new Vector3Int(j, i, 0);
					T tile = tilemap.GetTile<T>(vector3Int);
					if (tile != null)
					{
						list2.Add(vector3Int);
						list.Add(tile);
					}
				}
			}
			positions = list2.ToArray();
			return list.ToArray();
		}
	}
}

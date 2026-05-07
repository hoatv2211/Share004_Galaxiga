using System;
using System.Collections.Generic;
using UnityEngine;

namespace SuperScrollView
{
	public class ResManager : MonoBehaviour
	{
		public static ResManager Get
		{
			get
			{
				if (ResManager.instance == null)
				{
					ResManager.instance = UnityEngine.Object.FindObjectOfType<ResManager>();
				}
				return ResManager.instance;
			}
		}

		private void InitData()
		{
			this.spriteObjDict.Clear();
			foreach (Sprite sprite in this.spriteObjArray)
			{
				this.spriteObjDict[sprite.name] = sprite;
			}
		}

		private void Awake()
		{
			ResManager.instance = null;
			this.InitData();
		}

		public Sprite GetSpriteByName(string spriteName)
		{
			Sprite result = null;
			if (this.spriteObjDict.TryGetValue(spriteName, out result))
			{
				return result;
			}
			return null;
		}

		public string GetRandomSpriteName()
		{
			int max = this.spriteObjArray.Length;
			int num = UnityEngine.Random.Range(0, max);
			return this.spriteObjArray[num].name;
		}

		public int SpriteCount
		{
			get
			{
				return this.spriteObjArray.Length;
			}
		}

		public Sprite GetSpriteByIndex(int index)
		{
			if (index < 0 || index >= this.spriteObjArray.Length)
			{
				return null;
			}
			return this.spriteObjArray[index];
		}

		public string GetSpriteNameByIndex(int index)
		{
			if (index < 0 || index >= this.spriteObjArray.Length)
			{
				return string.Empty;
			}
			return this.spriteObjArray[index].name;
		}

		public Sprite[] spriteObjArray;

		private static ResManager instance;

		private string[] mWordList;

		private Dictionary<string, Sprite> spriteObjDict = new Dictionary<string, Sprite>();
	}
}

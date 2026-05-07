using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
	public static ResourceManager Get
	{
		get
		{
			if (ResourceManager.instance == null)
			{
				ResourceManager.instance = UnityEngine.Object.FindObjectOfType<ResourceManager>();
			}
			return ResourceManager.instance;
		}
	}

	private void Awake()
	{
		ResourceManager.instance = null;
		this.InitData();
	}

	private void Start()
	{
	}

	private void InitData()
	{
		this.spriteFlagObjDict.Clear();
		foreach (Sprite sprite in this.spriteFlagArray)
		{
			this.spriteFlagObjDict[sprite.name] = sprite;
		}
		this.spriteAvatarObjDict.Clear();
		foreach (Sprite sprite2 in this.spriteAvatarArray)
		{
			this.spriteAvatarObjDict[sprite2.name] = sprite2;
		}
	}

	public Sprite GetSpriteAvatarByName(string spriteName)
	{
		Sprite result = null;
		if (this.spriteAvatarObjDict.TryGetValue(spriteName, out result))
		{
			return result;
		}
		return null;
	}

	public Sprite GetSpriteFlagByName(string spriteName)
	{
		Sprite result = null;
		if (this.spriteFlagObjDict.TryGetValue(spriteName, out result))
		{
			return result;
		}
		return null;
	}

	public Sprite GetSpriteFlagByIndex(int index)
	{
		if (index < 0 || index >= this.spriteFlagArray.Length)
		{
			return null;
		}
		return this.spriteFlagArray[index];
	}

	public Sprite GetSpriteAvatarByIndex(int index)
	{
		if (index < 0 || index >= this.spriteAvatarArray.Length)
		{
			return null;
		}
		return this.spriteAvatarArray[index];
	}

	public Sprite[] spriteFlagArray;

	public Sprite[] spriteAvatarArray;

	private Dictionary<string, Sprite> spriteFlagObjDict = new Dictionary<string, Sprite>();

	private Dictionary<string, Sprite> spriteAvatarObjDict = new Dictionary<string, Sprite>();

	private static ResourceManager instance;
}

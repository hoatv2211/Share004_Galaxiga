using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BayatGames.SaveGamePro.Examples
{
	public class SaveCollections : MonoBehaviour
	{
		private void Awake()
		{
			SaveGame.OnSaved += this.SaveGame_OnSaved;
			SaveGame.OnLoaded += this.SaveGame_OnLoaded;
		}

		private void SaveGame_OnSaved(string identifier, object value, SaveGameSettings settings)
		{
			UnityEngine.Debug.LogFormat("{0} Saved Successfully", new object[]
			{
				identifier
			});
		}

		private void SaveGame_OnLoaded(string identifier, object result, Type type, object defaultValue, SaveGameSettings settings)
		{
			UnityEngine.Debug.LogFormat("{0} Loaded Successfully", new object[]
			{
				identifier
			});
		}

		public void Save()
		{
			SaveGame.Save<Dictionary<string, string>>("dictionary.txt", SaveCollections.dictionary);
			SaveGame.Save<Dictionary<string, Dictionary<string, string>>>("nestedDictionary.txt", SaveCollections.nestedDictionary);
			SaveGame.Save<List<string>>("list.txt", SaveCollections.list);
			SaveGame.Save<List<List<string>>>("nestedList.txt", SaveCollections.nestedList);
			SaveGame.Save<string[]>("array.txt", SaveCollections.array);
			SaveGame.Save<string[][]>("jaggedArray.txt", SaveCollections.jaggedArray);
			SaveGame.Save<string[,]>("multiDimensionalArray.txt", SaveCollections.multiDimensionalArray);
			SaveGame.Save<Hashtable>("hashtable.txt", SaveCollections.hashtable);
			SaveGame.Save<Hashtable>("nestedHashtable.txt", SaveCollections.nestedHashtable);
		}

		public void Load()
		{
			SaveCollections.dictionary = SaveGame.Load<Dictionary<string, string>>("dictionary.txt");
			SaveCollections.nestedDictionary = SaveGame.Load<Dictionary<string, Dictionary<string, string>>>("nestedDictionary.txt");
			SaveCollections.list = SaveGame.Load<List<string>>("list.txt");
			SaveCollections.nestedList = SaveGame.Load<List<List<string>>>("nestedList.txt");
			SaveCollections.array = SaveGame.Load<string[]>("array.txt");
			SaveCollections.jaggedArray = SaveGame.Load<string[][]>("jaggedArray.txt");
			SaveCollections.multiDimensionalArray = SaveGame.Load<string[,]>("multiDimensionalArray.txt");
			SaveCollections.hashtable = SaveGame.Load<Hashtable>("hashtable.txt");
			SaveCollections.nestedHashtable = SaveGame.Load<Hashtable>("nestedHashtable.txt");
		}

		// Note: this type is marked as 'beforefieldinit'.
		static SaveCollections()
		{
			string[,] array = new string[1, 2];
			array[0, 0] = "Hello";
			array[0, 1] = "World";
			SaveCollections.multiDimensionalArray = array;
			SaveCollections.hashtable = new Hashtable
			{
				{
					"Hello",
					"World"
				}
			};
			SaveCollections.nestedHashtable = new Hashtable
			{
				{
					"Hello",
					new Hashtable
					{
						{
							"Hello",
							"World"
						}
					}
				}
			};
		}

		public static Dictionary<string, string> dictionary = new Dictionary<string, string>
		{
			{
				"Hello",
				"World"
			}
		};

		public static Dictionary<string, Dictionary<string, string>> nestedDictionary = new Dictionary<string, Dictionary<string, string>>
		{
			{
				"Hello",
				new Dictionary<string, string>
				{
					{
						"Hello",
						"World"
					}
				}
			}
		};

		public static List<string> list = new List<string>
		{
			"Hello",
			"World"
		};

		public static List<List<string>> nestedList = new List<List<string>>
		{
			new List<string>
			{
				"Hello",
				"World"
			}
		};

		public static string[] array = new string[]
		{
			"Hello",
			"World"
		};

		public static string[][] jaggedArray = new string[][]
		{
			new string[]
			{
				"Hello",
				"World"
			}
		};

		public static string[,] multiDimensionalArray;

		public static Hashtable hashtable;

		public static Hashtable nestedHashtable;
	}
}

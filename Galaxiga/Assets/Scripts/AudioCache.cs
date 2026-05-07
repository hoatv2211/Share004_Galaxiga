using System;
using UnityEngine;

public class AudioCache : MonoBehaviour
{
	public static AudioCache Instance
	{
		get
		{
			if (AudioCache._instance == null)
			{
				AudioCache._instance = UnityEngine.Object.FindObjectOfType<AudioCache>();
				if (AudioCache._instance == null)
				{
					UnityEngine.Debug.LogError("Không có AudioCache");
				}
			}
			return AudioCache._instance;
		}
	}

	protected virtual void Awake()
	{
		AudioCache._instance = this;
		UnityEngine.Object.DontDestroyOnLoad(base.transform.gameObject);
	}

	public static AudioCache.MusicContainerStruct Music
	{
		get
		{
			return AudioCache.Instance.music;
		}
	}

	public static AudioCache.SoundContainerStruct Sound
	{
		get
		{
			return AudioCache.Instance.sound;
		}
	}

	public static AudioCache.UISoundContainerStruct UISound
	{
		get
		{
			return AudioCache.Instance.uiSound;
		}
	}

	private static AudioCache _instance;

	public AudioCache.MusicContainerStruct music;

	public AudioCache.SoundContainerStruct sound;

	public AudioCache.UISoundContainerStruct uiSound;

	[Serializable]
	public struct MusicContainerStruct
	{
		public AudioClip boss_bg;

		public AudioClip hard_bg1;

		public AudioClip hard_bg2;

		public AudioClip hard_bg3;

		public AudioClip main_menu;

		public AudioClip normal_bg1;

		public AudioClip normal_bg2;

		public AudioClip normal_bg3;
	}

	[Serializable]
	public struct SoundContainerStruct
	{
		public AudioClip active_skill;

		public AudioClip bata_shot;

		public AudioClip bossdie;

		public AudioClip break_shield;

		public AudioClip bullet_upgrade;

		public AudioClip enemydie;

		public AudioClip enemy_die1;

		public AudioClip enemy_die2;

		public AudioClip enemy_die3;

		public AudioClip fury_shot;

		public AudioClip gameover;

		public AudioClip get_coin;

		public AudioClip get_item;

		public AudioClip get_item_2;

		public AudioClip greataxe_shot;

		public AudioClip Item_get2;

		public AudioClip player_die;

		public AudioClip ShockLoop4;

		public AudioClip shooting_1;

		public AudioClip shooting_2;

		public AudioClip shotting_3;

		public AudioClip shotting_4;

		public AudioClip skywraith_shot;

		public AudioClip switch_airship;

		public AudioClip twilight_laser;

		public AudioClip twilight_shot;

		public AudioClip warning_boss;

		public AudioClip win;
	}

	[Serializable]
	public struct UISoundContainerStruct
	{
		public AudioClip evolve_1;

		public AudioClip evolve_2;

		public AudioClip evolve_3;

		public AudioClip message;

		public AudioClip spawn_star;

		public AudioClip tap;

		public AudioClip tap2;

		public AudioClip tap3;

		public AudioClip tik_tok;

		public AudioClip upgrade_craft;

		public AudioClip upgrade_plane_drone;
	}
}

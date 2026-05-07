using System;
using SkyGameKit.QuickAccess;
using UnityEngine;
using UnityEngine.UI;

namespace SkyGameKit
{
	public class HUDManager : MonoBehaviour
	{
		private void Start()
		{
			LevelManager instance = SgkSingleton<LevelManager>.Instance;
			instance.OnScoreChange = (Action<int>)Delegate.Combine(instance.OnScoreChange, new Action<int>(delegate(int x)
			{
				this.scoreText.text = "Score: " + SgkSingleton<LevelManager>.Instance.Score;
			}));
			LevelManager instance2 = SgkSingleton<LevelManager>.Instance;
			instance2.OnStateChange = (Action)Delegate.Combine(instance2.OnStateChange, new Action(delegate()
			{
				if (SgkSingleton<LevelManager>.Instance.State == LevelState.Victory || SgkSingleton<LevelManager>.Instance.State == LevelState.Defeat)
				{
					MonoBehaviour.print(SgkSingleton<LevelManager>.Instance.State);
					this.endGameNotification.gameObject.SetActive(true);
					this.endGameNotification.text = SgkSingleton<LevelManager>.Instance.State.ToString();
				}
			}));
			APlayerState state = Player.State;
			state.onAddStar = (Action<int>)Delegate.Combine(state.onAddStar, new Action<int>(delegate(int x)
			{
				this.starText.text = "Star: " + Player.State.Star;
			}));
			APlayerHealth health = Player.Health;
			health.onCurrentHPChange = (Action<int>)Delegate.Combine(health.onCurrentHPChange, new Action<int>(delegate(int x)
			{
				this.liveText.text = "Live: " + Player.Health.CurrentHP;
			}));
			APlayerAttack attack = Player.Attack;
			attack.onBulletLevelChange = (Action<int>)Delegate.Combine(attack.onBulletLevelChange, new Action<int>(delegate(int x)
			{
				this.bulletLevelText.text = "Bullet: " + Player.Attack.BulletLevel;
			}));
		}

		public void ChangeColor(Image image)
		{
			image.color = UnityEngine.Random.ColorHSV();
		}

		public void ChangeMoveMode(Text text)
		{
			Player.Move.moveMode = MoveMode.Relative - (int)Player.Move.moveMode;
			text.text = Player.Move.moveMode.ToString();
		}

		public Text scoreText;

		public Text liveText;

		public Text bulletLevelText;

		public Text starText;

		public Text endGameNotification;
	}
}

using System;
using System.Diagnostics;
using UnityEngine;

namespace Spine.Unity
{
	[ExecuteInEditMode]
	[AddComponentMenu("Spine/SkeletonAnimation")]
	[HelpURL("http://esotericsoftware.com/spine-unity-documentation#Controlling-Animation")]
	public class SkeletonAnimation : SkeletonRenderer, ISkeletonAnimation, IAnimationStateComponent
	{
		public AnimationState AnimationState
		{
			get
			{
				return this.state;
			}
		}

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		protected event UpdateBonesDelegate _UpdateLocal;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		protected event UpdateBonesDelegate _UpdateWorld;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		protected event UpdateBonesDelegate _UpdateComplete;

		public event UpdateBonesDelegate UpdateLocal;

		public event UpdateBonesDelegate UpdateWorld;

		public event UpdateBonesDelegate UpdateComplete;

		public string AnimationName
		{
			get
			{
				if (!this.valid)
				{
					return this._animationName;
				}
				TrackEntry current = this.state.GetCurrent(0);
				return (current != null) ? current.Animation.Name : null;
			}
			set
			{
				if (this._animationName == value)
				{
					return;
				}
				this._animationName = value;
				if (string.IsNullOrEmpty(value))
				{
					this.state.ClearTrack(0);
				}
				else
				{
					this.TrySetAnimation(value, this.loop);
				}
			}
		}

		private TrackEntry TrySetAnimation(string animationName, bool animationLoop)
		{
			Animation animation = this.skeletonDataAsset.GetSkeletonData(false).FindAnimation(animationName);
			if (animation != null)
			{
				return this.state.SetAnimation(0, animation, animationLoop);
			}
			return null;
		}

		public static SkeletonAnimation AddToGameObject(GameObject gameObject, SkeletonDataAsset skeletonDataAsset)
		{
			return SkeletonRenderer.AddSpineComponent<SkeletonAnimation>(gameObject, skeletonDataAsset);
		}

		public static SkeletonAnimation NewSkeletonAnimationGameObject(SkeletonDataAsset skeletonDataAsset)
		{
			return SkeletonRenderer.NewSpineGameObject<SkeletonAnimation>(skeletonDataAsset);
		}

		public override void ClearState()
		{
			base.ClearState();
			if (this.state != null)
			{
				this.state.ClearTracks();
			}
		}

		public override void Initialize(bool overwrite)
		{
			if (this.valid && !overwrite)
			{
				return;
			}
			base.Initialize(overwrite);
			if (!this.valid)
			{
				return;
			}
			this.state = new AnimationState(this.skeletonDataAsset.GetAnimationStateData());
			if (!string.IsNullOrEmpty(this._animationName))
			{
				TrackEntry trackEntry = this.TrySetAnimation(this._animationName, this.loop);
				if (trackEntry != null)
				{
					this.Update(0f);
				}
			}
		}

		private void Update()
		{
			this.Update(Time.deltaTime);
		}

		public void Update(float deltaTime)
		{
			if (!this.valid)
			{
				return;
			}
			deltaTime *= this.timeScale;
			this.skeleton.Update(deltaTime);
			this.state.Update(deltaTime);
			this.state.Apply(this.skeleton);
			if (this._UpdateLocal != null)
			{
				this._UpdateLocal(this);
			}
			this.skeleton.UpdateWorldTransform();
			if (this._UpdateWorld != null)
			{
				this._UpdateWorld(this);
				this.skeleton.UpdateWorldTransform();
			}
			if (this._UpdateComplete != null)
			{
				this._UpdateComplete(this);
			}
		}

		public AnimationState state;

		[SerializeField]
		[SpineAnimation("", "", true, false)]
		private string _animationName;

		public bool loop;

		public float timeScale = 1f;
	}
}

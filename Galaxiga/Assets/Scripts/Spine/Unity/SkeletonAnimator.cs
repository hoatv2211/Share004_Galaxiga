using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Spine.Unity
{
	[RequireComponent(typeof(Animator))]
	public class SkeletonAnimator : SkeletonRenderer, ISkeletonAnimation
	{
		public SkeletonAnimator.MecanimTranslator Translator
		{
			get
			{
				return this.translator;
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
			if (this.translator == null)
			{
				this.translator = new SkeletonAnimator.MecanimTranslator();
			}
			this.translator.Initialize(base.GetComponent<Animator>(), this.skeletonDataAsset);
		}

		public void Update()
		{
			if (!this.valid)
			{
				return;
			}
			this.translator.Apply(this.skeleton);
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

		[SerializeField]
		protected SkeletonAnimator.MecanimTranslator translator;

		[Serializable]
		public class MecanimTranslator
		{
			public Animator Animator
			{
				get
				{
					return this.animator;
				}
			}

			public void Initialize(Animator animator, SkeletonDataAsset skeletonDataAsset)
			{
				this.animator = animator;
				this.previousAnimations.Clear();
				this.animationTable.Clear();
				SkeletonData skeletonData = skeletonDataAsset.GetSkeletonData(true);
				foreach (Animation animation in skeletonData.Animations)
				{
					this.animationTable.Add(animation.Name.GetHashCode(), animation);
				}
				this.clipNameHashCodeTable.Clear();
				this.clipInfoCache.Clear();
				this.nextClipInfoCache.Clear();
			}

			public void Apply(Skeleton skeleton)
			{
				if (this.layerMixModes.Length < this.animator.layerCount)
				{
					Array.Resize<SkeletonAnimator.MecanimTranslator.MixMode>(ref this.layerMixModes, this.animator.layerCount);
				}
				if (this.autoReset)
				{
					List<Animation> list = this.previousAnimations;
					int i = 0;
					int count = list.Count;
					while (i < count)
					{
						list[i].SetKeyedItemsToSetupPose(skeleton);
						i++;
					}
					list.Clear();
					int j = 0;
					int layerCount = this.animator.layerCount;
					while (j < layerCount)
					{
						float num = (j != 0) ? this.animator.GetLayerWeight(j) : 1f;
						if (num > 0f)
						{
							bool flag = this.animator.GetNextAnimatorStateInfo(j).fullPathHash != 0;
							int num2;
							int num3;
							IList<AnimatorClipInfo> list2;
							IList<AnimatorClipInfo> list3;
							this.GetAnimatorClipInfos(j, out num2, out num3, out list2, out list3);
							for (int k = 0; k < num2; k++)
							{
								AnimatorClipInfo animatorClipInfo = list2[k];
								float num4 = animatorClipInfo.weight * num;
								if (num4 != 0f)
								{
									list.Add(this.GetAnimation(animatorClipInfo.clip));
								}
							}
							if (flag)
							{
								for (int l = 0; l < num3; l++)
								{
									AnimatorClipInfo animatorClipInfo2 = list3[l];
									float num5 = animatorClipInfo2.weight * num;
									if (num5 != 0f)
									{
										list.Add(this.GetAnimation(animatorClipInfo2.clip));
									}
								}
							}
						}
						j++;
					}
				}
				int m = 0;
				int layerCount2 = this.animator.layerCount;
				while (m < layerCount2)
				{
					float num6 = (m != 0) ? this.animator.GetLayerWeight(m) : 1f;
					AnimatorStateInfo currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(m);
					AnimatorStateInfo nextAnimatorStateInfo = this.animator.GetNextAnimatorStateInfo(m);
					bool flag2 = nextAnimatorStateInfo.fullPathHash != 0;
					int num7;
					int num8;
					IList<AnimatorClipInfo> list4;
					IList<AnimatorClipInfo> list5;
					this.GetAnimatorClipInfos(m, out num7, out num8, out list4, out list5);
					SkeletonAnimator.MecanimTranslator.MixMode mixMode = this.layerMixModes[m];
					if (mixMode == SkeletonAnimator.MecanimTranslator.MixMode.AlwaysMix)
					{
						for (int n = 0; n < num7; n++)
						{
							AnimatorClipInfo animatorClipInfo3 = list4[n];
							float num9 = animatorClipInfo3.weight * num6;
							if (num9 != 0f)
							{
								this.GetAnimation(animatorClipInfo3.clip).Apply(skeleton, 0f, SkeletonAnimator.MecanimTranslator.AnimationTime(currentAnimatorStateInfo.normalizedTime, animatorClipInfo3.clip.length, currentAnimatorStateInfo.loop, currentAnimatorStateInfo.speed < 0f), currentAnimatorStateInfo.loop, null, num9, MixPose.Current, MixDirection.In);
							}
						}
						if (flag2)
						{
							for (int num10 = 0; num10 < num8; num10++)
							{
								AnimatorClipInfo animatorClipInfo4 = list5[num10];
								float num11 = animatorClipInfo4.weight * num6;
								if (num11 != 0f)
								{
									this.GetAnimation(animatorClipInfo4.clip).Apply(skeleton, 0f, SkeletonAnimator.MecanimTranslator.AnimationTime(nextAnimatorStateInfo.normalizedTime, animatorClipInfo4.clip.length, nextAnimatorStateInfo.speed < 0f), nextAnimatorStateInfo.loop, null, num11, MixPose.Current, MixDirection.In);
								}
							}
						}
					}
					else
					{
						int num12;
						for (num12 = 0; num12 < num7; num12++)
						{
							AnimatorClipInfo animatorClipInfo5 = list4[num12];
							float num13 = animatorClipInfo5.weight * num6;
							if (num13 != 0f)
							{
								this.GetAnimation(animatorClipInfo5.clip).Apply(skeleton, 0f, SkeletonAnimator.MecanimTranslator.AnimationTime(currentAnimatorStateInfo.normalizedTime, animatorClipInfo5.clip.length, currentAnimatorStateInfo.loop, currentAnimatorStateInfo.speed < 0f), currentAnimatorStateInfo.loop, null, 1f, MixPose.Current, MixDirection.In);
								break;
							}
						}
						while (num12 < num7)
						{
							AnimatorClipInfo animatorClipInfo6 = list4[num12];
							float num14 = animatorClipInfo6.weight * num6;
							if (num14 != 0f)
							{
								this.GetAnimation(animatorClipInfo6.clip).Apply(skeleton, 0f, SkeletonAnimator.MecanimTranslator.AnimationTime(currentAnimatorStateInfo.normalizedTime, animatorClipInfo6.clip.length, currentAnimatorStateInfo.loop, currentAnimatorStateInfo.speed < 0f), currentAnimatorStateInfo.loop, null, num14, MixPose.Current, MixDirection.In);
							}
							num12++;
						}
						num12 = 0;
						if (flag2)
						{
							if (mixMode == SkeletonAnimator.MecanimTranslator.MixMode.SpineStyle)
							{
								while (num12 < num8)
								{
									AnimatorClipInfo animatorClipInfo7 = list5[num12];
									float num15 = animatorClipInfo7.weight * num6;
									if (num15 != 0f)
									{
										this.GetAnimation(animatorClipInfo7.clip).Apply(skeleton, 0f, SkeletonAnimator.MecanimTranslator.AnimationTime(nextAnimatorStateInfo.normalizedTime, animatorClipInfo7.clip.length, nextAnimatorStateInfo.speed < 0f), nextAnimatorStateInfo.loop, null, 1f, MixPose.Current, MixDirection.In);
										break;
									}
									num12++;
								}
							}
							while (num12 < num8)
							{
								AnimatorClipInfo animatorClipInfo8 = list5[num12];
								float num16 = animatorClipInfo8.weight * num6;
								if (num16 != 0f)
								{
									this.GetAnimation(animatorClipInfo8.clip).Apply(skeleton, 0f, SkeletonAnimator.MecanimTranslator.AnimationTime(nextAnimatorStateInfo.normalizedTime, animatorClipInfo8.clip.length, nextAnimatorStateInfo.speed < 0f), nextAnimatorStateInfo.loop, null, num16, MixPose.Current, MixDirection.In);
								}
								num12++;
							}
						}
					}
					m++;
				}
			}

			private static float AnimationTime(float normalizedTime, float clipLength, bool loop, bool reversed)
			{
				if (reversed)
				{
					normalizedTime = 1f - normalizedTime + (float)((int)normalizedTime) + (float)((int)normalizedTime);
				}
				float num = normalizedTime * clipLength;
				if (loop)
				{
					return num;
				}
				return (clipLength - num >= 0.0333333351f) ? num : clipLength;
			}

			private static float AnimationTime(float normalizedTime, float clipLength, bool reversed)
			{
				if (reversed)
				{
					normalizedTime = 1f - normalizedTime + (float)((int)normalizedTime) + (float)((int)normalizedTime);
				}
				return normalizedTime * clipLength;
			}

			private void GetAnimatorClipInfos(int layer, out int clipInfoCount, out int nextClipInfoCount, out IList<AnimatorClipInfo> clipInfo, out IList<AnimatorClipInfo> nextClipInfo)
			{
				clipInfoCount = this.animator.GetCurrentAnimatorClipInfoCount(layer);
				nextClipInfoCount = this.animator.GetNextAnimatorClipInfoCount(layer);
				if (this.clipInfoCache.Capacity < clipInfoCount)
				{
					this.clipInfoCache.Capacity = clipInfoCount;
				}
				if (this.nextClipInfoCache.Capacity < nextClipInfoCount)
				{
					this.nextClipInfoCache.Capacity = nextClipInfoCount;
				}
				this.animator.GetCurrentAnimatorClipInfo(layer, this.clipInfoCache);
				this.animator.GetNextAnimatorClipInfo(layer, this.nextClipInfoCache);
				clipInfo = this.clipInfoCache;
				nextClipInfo = this.nextClipInfoCache;
			}

			private Animation GetAnimation(AnimationClip clip)
			{
				int hashCode;
				if (!this.clipNameHashCodeTable.TryGetValue(clip, out hashCode))
				{
					hashCode = clip.name.GetHashCode();
					this.clipNameHashCodeTable.Add(clip, hashCode);
				}
				Animation result;
				this.animationTable.TryGetValue(hashCode, out result);
				return result;
			}

			public bool autoReset = true;

			public SkeletonAnimator.MecanimTranslator.MixMode[] layerMixModes = new SkeletonAnimator.MecanimTranslator.MixMode[0];

			private readonly Dictionary<int, Animation> animationTable = new Dictionary<int, Animation>(SkeletonAnimator.MecanimTranslator.IntEqualityComparer.Instance);

			private readonly Dictionary<AnimationClip, int> clipNameHashCodeTable = new Dictionary<AnimationClip, int>(SkeletonAnimator.MecanimTranslator.AnimationClipEqualityComparer.Instance);

			private readonly List<Animation> previousAnimations = new List<Animation>();

			private readonly List<AnimatorClipInfo> clipInfoCache = new List<AnimatorClipInfo>();

			private readonly List<AnimatorClipInfo> nextClipInfoCache = new List<AnimatorClipInfo>();

			private Animator animator;

			public enum MixMode
			{
				AlwaysMix,
				MixNext,
				SpineStyle
			}

			private class AnimationClipEqualityComparer : IEqualityComparer<AnimationClip>
			{
				public bool Equals(AnimationClip x, AnimationClip y)
				{
					return x.GetInstanceID() == y.GetInstanceID();
				}

				public int GetHashCode(AnimationClip o)
				{
					return o.GetInstanceID();
				}

				internal static readonly IEqualityComparer<AnimationClip> Instance = new SkeletonAnimator.MecanimTranslator.AnimationClipEqualityComparer();
			}

			private class IntEqualityComparer : IEqualityComparer<int>
			{
				public bool Equals(int x, int y)
				{
					return x == y;
				}

				public int GetHashCode(int o)
				{
					return o;
				}

				internal static readonly IEqualityComparer<int> Instance = new SkeletonAnimator.MecanimTranslator.IntEqualityComparer();
			}
		}
	}
}

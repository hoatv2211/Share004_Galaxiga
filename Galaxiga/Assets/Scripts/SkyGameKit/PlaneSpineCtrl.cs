using System;
using System.Collections.Generic;
using System.Linq;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace SkyGameKit
{
	public class PlaneSpineCtrl : MonoBehaviour
	{
		public int MinRank { get; private set; }

		public int MaxRank { get; private set; }

		public int Rank
		{
			get
			{
				return this.rank;
			}
			set
			{
				if (this.isLoaded)
				{
					if (value < this.MinRank || value > this.MaxRank)
					{
						SgkLog.LogError("Máy bay hiện tại không có rank " + value);
					}
					this.rank = Mathf.Clamp(value, this.MinRank, this.MaxRank);
					this.skeleton.SetSkin(this.skins[this.rank - this.MinRank]);
				}
				else
				{
					this.rank = value;
				}
			}
		}

		private void Start()
		{
			this.skeletonAnimation = base.GetComponent<SkeletonAnimation>();
			this.spineAnimationState = this.skeletonAnimation.AnimationState;
			this.skeletonData = this.skeletonAnimation.SkeletonDataAsset.GetSkeletonData(false);
			this.skeleton = this.skeletonAnimation.Skeleton;
			this.LoadAnimation();
			this.LoadSkin();
			this.isLoaded = true;
			this.Rank = ((this.rank <= 0) ? this.defaultRank : this.rank);
		}

		private void OnEnable()
		{
			this.oldPosX = base.transform.position.x;
			this.oldIndex = 0;
		}

		private void LoadAnimation()
		{
			foreach (Spine.Animation animation in this.skeletonData.Animations)
			{
				if (animation.Name == "idle")
				{
					this.idle = animation;
				}
				else if (animation.Name.StartsWith("right", StringComparison.OrdinalIgnoreCase))
				{
					this.right.Add(animation);
				}
			}
			this.right = (from o in this.right
			orderby o.Name
			select o).ToList<Spine.Animation>();
			this.right.Insert(0, this.idle);
		}

		private void LoadSkin()
		{
			this.skins = (from s in this.skeletonData.Skins
			where s.Name.StartsWith("E", StringComparison.OrdinalIgnoreCase)
			orderby s.Name
			select s).ToList<Skin>();
			if (this.skins.Count > 0)
			{
				this.MinRank = int.Parse(this.skins[0].Name.Substring(1));
				this.MaxRank = this.MinRank + this.skins.Count - 1;
			}
			else
			{
				int num = 0;
				this.MaxRank = num;
				this.MinRank = num;
			}
		}

		private void Update()
		{
			float num = base.transform.position.x - this.oldPosX;
			float num2 = num / Time.deltaTime;
			float num3 = num2 * this.inclinationPerSpeed * 0.01f;
			this.oldPosX = base.transform.position.x;
			if (Mathf.Abs(num) > 0.01f)
			{
				if (this.inclination * num < 0f)
				{
					num3 *= 5f;
				}
				this.inclination += num3;
			}
			else
			{
				this.inclination = Fu.GoTo0(this.inclination, this.inclinationPerSpeed * 0.15f);
			}
			int num4 = Mathf.RoundToInt(Mathf.Clamp(Mathf.Abs(this.inclination), 0f, (float)(this.right.Count - 1)));
			if (this.oldIndex != num4)
			{
				this.oldIndex = num4;
				this.skeleton.FlipX = (this.inclination < 0f);
				this.spineAnimationState.SetAnimation(0, this.right[num4], true);
			}
		}

		[Tooltip("Tỉ lệ độ nghiêng chia tốc độ")]
		public float inclinationPerSpeed = 1f;

		public int defaultRank = 1;

		private float oldPosX;

		private int oldIndex;

		private SkeletonAnimation skeletonAnimation;

		private Spine.AnimationState spineAnimationState;

		private SkeletonData skeletonData;

		private Skeleton skeleton;

		private List<Spine.Animation> right = new List<Spine.Animation>();

		private List<Skin> skins = new List<Skin>();

		private float inclination;

		private bool isLoaded;

		private int rank = -1;

		private Spine.Animation idle;
	}
}

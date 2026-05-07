using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Spine.Unity.Playables
{
	[Serializable]
	public class SpineAnimationStateClip : PlayableAsset, ITimelineClipAsset
	{
		public ClipCaps clipCaps
		{
			get
			{
				return ClipCaps.None;
			}
		}

		public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
		{
			ScriptPlayable<SpineAnimationStateBehaviour> playable = ScriptPlayable<SpineAnimationStateBehaviour>.Create(graph, this.template, 0);
			playable.GetBehaviour();
			return playable;
		}

		public SpineAnimationStateBehaviour template = new SpineAnimationStateBehaviour();
	}
}

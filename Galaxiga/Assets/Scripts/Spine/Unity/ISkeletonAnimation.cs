using System;

namespace Spine.Unity
{
	public interface ISkeletonAnimation
	{
		event UpdateBonesDelegate UpdateLocal;

		event UpdateBonesDelegate UpdateWorld;

		event UpdateBonesDelegate UpdateComplete;

		Skeleton Skeleton { get; }
	}
}

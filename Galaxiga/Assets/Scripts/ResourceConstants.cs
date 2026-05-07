using System;

public class ResourceConstants
{
	public const string FLOWTYPE_SOURCE = "source";

	public const string FLOWTYPE_SINK = "sink";

	public const string CURRENCY_GEM = "gem";

	public const string CURRENCY_GOLD = "gold";

	public enum SOURCE
	{
		inapp,
		play_level,
		watch_video
	}

	public enum SINK
	{
		upgrade_plane,
		upgrade_wingman,
		buy
	}
}

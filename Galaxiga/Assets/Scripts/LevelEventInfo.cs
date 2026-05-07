using System;
using SkyGameKit;

public class LevelEventInfo : SgkSingleton<LevelEventInfo>
{
	public LevelEventInfo.modeLevel mode;

	public int numberItemEvent;

	public enum modeLevel
	{
		Campain,
		EndLess,
		Boss
	}
}

using System;

namespace OSNet
{
	public class CSLevelLog : CSMessage
	{
		public CSLevelLog()
		{
		}

		public CSLevelLog(int level, int maxPassedLevel, string difficulty, int duration, string status, string where)
		{
			this.level = level;
			this.maxPassedLevel = maxPassedLevel;
			this.difficulty = difficulty;
			this.duration = duration;
			this.status = status;
			this.where = where;
		}

		public override string GetEvent()
		{
			return "cs_level_log";
		}

		public int level;

		public int maxPassedLevel;

		public string difficulty;

		public int duration;

		public string status;

		public string where;

		public const string DIFFUCULTY_EASY = "easy";

		public const string DIFFUCULTY_NORMAL = "normal";

		public const string DIFFUCULTY_HARD = "hard";

		public const string STATUS_EXIT = "exit";

		public const string STATUS_FAIL = "fail";

		public const string STATUS_PASS = "pass";
	}
}

using System;
using System.Collections.Generic;

[Serializable]
public class EndessModeData
{
	public int maxWave;

	public int numberBlueStar;

	public int numberTicketUniverse2;

	public int numberTicketUniverse3;

	public int numberTicketUniverse4;

	public int numberTicketUniverse5;

	public bool isPurchasePackOneTime;

	public List<EndessModeData.UniverseData> universeDatas;

	public List<bool> arrBoolStateRewardOneTime;

	[Serializable]
	public class UniverseData
	{
		public UniverseData(int _id, int _bestScore, int _wave, int _time)
		{
			this.idUniverse = _id;
			this.bestScore = _bestScore;
			this.wave = _wave;
			this.time = _time;
		}

		public int idUniverse;

		public int bestScore;

		public int wave;

		public int time;
	}
}

using System;
using BayatGames.SaveGamePro.Serialization;

namespace BayatGames.SaveGamePro
{
	public interface ISavable
	{
		void OnWrite(ISaveGameWriter writer);

		void OnRead(ISaveGameReader reader);
	}
}

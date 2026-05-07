using System;

public interface ITurnCanIntegrateEndless
{
	int NumberOfEnemySelected { get; set; }

	float TimeToNextAction { get; set; }
}

using System;

[Serializable]
public class RewardLevel
{
	public RewardLevel(string _unlockPlane = "None", string _cardPlane = "None", string _cardDrone = "None", int _numberCardPlane = 0, int _numberCardDrone = 0, int _numberCoin = 0, int _numberGem = 0, int _numberBox = 0, int _numberUltraStarshipCard = 0, int _numberUltraDroneCard = 0)
	{
		this.unlockPlane = _unlockPlane;
		this.cardPlane = _cardPlane;
		this.cardDrone = _cardDrone;
		this.numberCardPlane = _numberCardPlane;
		this.numberCardDrone = _numberCardDrone;
		this.numberCoins = _numberCoin;
		this.numberGem = _numberGem;
		this.numberBox = _numberBox;
		this.numberUltraStarshipCard = _numberUltraStarshipCard;
		this.numberUltraDroneCard = _numberUltraDroneCard;
	}

	public string unlockPlane;

	public string cardPlane;

	public string cardDrone;

	public int numberCardPlane;

	public int numberCardDrone;

	public int numberCoins;

	public int numberGem;

	public int numberBox;

	public int numberUltraStarshipCard;

	public int numberUltraDroneCard;
}

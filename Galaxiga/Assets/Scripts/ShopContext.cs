using System;

public class ShopContext
{
	public static int PRICE_ITEM_POWERUP_SHIELD
	{
		get
		{
			if (GameContext.currentVIP > 0)
			{
				return 4;
			}
			return 5;
		}
	}

	public static int PRICE_ITEM_POWERUP_BULLET_5
	{
		get
		{
			if (GameContext.currentVIP > 0)
			{
				return 4;
			}
			return 5;
		}
	}

	public static int PRICE_ITEM_POWERUP_BULLET_10
	{
		get
		{
			if (GameContext.currentVIP > 0 && GameContext.currentVIP < 5)
			{
				return 9;
			}
			if (GameContext.currentVIP >= 5)
			{
				return 8;
			}
			return 10;
		}
	}

	public static int PRICE_ITEM_POWERUP_HEART
	{
		get
		{
			if (GameContext.currentVIP > 0 && GameContext.currentVIP < 5)
			{
				return 9;
			}
			if (GameContext.currentVIP >= 5)
			{
				return 8;
			}
			return 10;
		}
	}

	public const int PRICE_AIR_CRAFT_BATA_FD_01 = 100000;

	public const int PRICE_AIR_CRAFT_FURY_OF_ARES = 20000;

	public const int PRICE_AIR_CRAFT_SKY_WRAITH = 30000;

	public const int PRICE_AIR_CRAFT_TWILIGHT_X = 40000;

	public const int PRICE_DRONES_GATLING_GUN = 10000;

	public const int PRICE_DRONES_AUTO_GATLING_GUN = 10000;

	public const int PRICE_DRONES_LAZER = 10000;

	public const int PRICE_EVOLVE_PLANE_RANK_B = 100;

	public const int PRICE_EVOLVE_PLANE_RANK_A = 200;

	public const int PRICE_EVOLVE_PLANE_RANK_S = 300;

	public const int PRICE_EVOLVE_PLANE_RANK_SS = 400;

	public const int PRICE_EVOLVE_PLANE_RANK_SSS = 500;

	public static int PRICE_ITEM_POWERUP_SKILL_PLANE = 500;

	public const int PRICE_COIN_PACK_1 = 10;

	public const int PRICE_COIN_PACK_2 = 200;

	public const int PRICE_COIN_PACK_3 = 1000;

	public const int PRICE_RESET_SHOP_1 = 20;

	public const int PRICE_RESET_SHOP_2 = 50;

	public const int PRICE_RESET_SHOP_3 = 100;

	public const int PRICE_RESET_SHOP_4 = 150;

	public static int currentCoin;

	public static int currentGem;
}

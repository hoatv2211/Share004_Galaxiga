using System;
using SkyGameKit.QuickAccess;

namespace SkyGameKit
{
	public class SgkPlayerAttack : APlayerAttack
	{
		public override bool LockShot
		{
			get
			{
				return this.shotCtrl.shooting;
			}
			set
			{
				if (value)
				{
					this.shotCtrl.StopShotRoutine();
				}
				else
				{
					this.shotCtrl.StartShotRoutine();
				}
			}
		}

		public UbhShotCtrl shotCtrl;
	}
}

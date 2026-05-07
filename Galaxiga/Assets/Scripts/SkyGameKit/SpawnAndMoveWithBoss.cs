using System;
using UnityEngine;

namespace SkyGameKit
{
	public class SpawnAndMoveWithBoss : SpawnByBoss
	{
		protected virtual void Update()
		{
            if (lerp)
            {
                base.transform.position = Vector3.Lerp(base.transform.position, boss.position + (Vector3)offset, 0.5f);
            }
            else
            {
                base.transform.position = boss.position + (Vector3)offset;
            }
        }

		public bool lerp = true;
	}
}

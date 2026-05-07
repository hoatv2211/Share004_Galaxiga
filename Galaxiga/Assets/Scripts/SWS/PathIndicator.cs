using System;
using System.Collections;
using UnityEngine;

namespace SWS
{
	public class PathIndicator : MonoBehaviour
	{
		private void Start()
		{
			this.pSys = base.GetComponentInChildren<ParticleSystem>();
			base.StartCoroutine("EmitParticles");
		}

		private IEnumerator EmitParticles()
		{
            yield return new WaitForEndOfFrame();
            while (true)
            {
                float rot = (base.transform.eulerAngles.y + modRotation) * ((float)Math.PI / 180f);
                ParticleSystem.MainModule pMain = pSys.main;
                pMain.startRotation = rot;
                pSys.Emit(1);
                yield return new WaitForSeconds(0.2f);
            }
        }

		public float modRotation;

		private ParticleSystem pSys;
	}
}

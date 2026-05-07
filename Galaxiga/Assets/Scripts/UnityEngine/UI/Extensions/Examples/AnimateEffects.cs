using System;

namespace UnityEngine.UI.Extensions.Examples
{
	public class AnimateEffects : MonoBehaviour
	{
		private void Start()
		{
			this.cylinderTextRT = this.cylinderText.GetComponent<Transform>();
		}

		private void Update()
		{
			this.letterSpacing.spacing += this.letterSpacingModifier;
			if (this.letterSpacing.spacing > this.letterSpacingMax || this.letterSpacing.spacing < this.letterSpacingMin)
			{
				this.letterSpacingModifier = -this.letterSpacingModifier;
			}
			this.curvedText.CurveMultiplier += this.curvedTextModifier;
			if (this.curvedText.CurveMultiplier > this.curvedTextMax || this.curvedText.CurveMultiplier < this.curvedTextMin)
			{
				this.curvedTextModifier = -this.curvedTextModifier;
			}
			this.gradient2.Offset += this.gradient2Modifier;
			if (this.gradient2.Offset > this.gradient2Max || this.gradient2.Offset < this.gradient2Min)
			{
				this.gradient2Modifier = -this.gradient2Modifier;
			}
			this.cylinderTextRT.Rotate(this.cylinderRotation);
			this.SAUIM.CutOff += this.SAUIMModifier;
			if (this.SAUIM.CutOff > this.SAUIMMax || this.SAUIM.CutOff < this.SAUIMMin)
			{
				this.SAUIMModifier = -this.SAUIMModifier;
			}
		}

		public LetterSpacing letterSpacing;

		private float letterSpacingMax = 10f;

		private float letterSpacingMin = -10f;

		private float letterSpacingModifier = 0.1f;

		public CurvedText curvedText;

		private float curvedTextMax = 0.05f;

		private float curvedTextMin = -0.05f;

		private float curvedTextModifier = 0.001f;

		public Gradient2 gradient2;

		private float gradient2Max = 1f;

		private float gradient2Min = -1f;

		private float gradient2Modifier = 0.01f;

		public CylinderText cylinderText;

		private Transform cylinderTextRT;

		private Vector3 cylinderRotation = new Vector3(0f, 1f, 0f);

		public SoftMaskScript SAUIM;

		private float SAUIMMax = 1f;

		private float SAUIMMin;

		private float SAUIMModifier = 0.01f;
	}
}

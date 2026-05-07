using System;
using System.Collections;
using SkyGameKit;

public class ShapeTurnLoop : ShapeTurn
{
	protected override IEnumerator SpawnAndMove()
	{
		yield return base.SpawnAndMove();
		yield return this.MoveEnemy(this.loopTransformations, this.loop, this.loopType);
		yield return this.MoveEnemy(this.outTransformations, 1, TransformationLoopType.Restart);
		yield break;
	}

	protected override ShapeManager GetFirstShape()
	{
		if (!Fu.IsNullOrEmpty(this.transformations))
		{
			return this.transformations[0].shape;
		}
		if (!Fu.IsNullOrEmpty(this.loopTransformations))
		{
			return this.loopTransformations[0].shape;
		}
		return this.outTransformations[0].shape;
	}

	protected override int GetNumberOfTransformations()
	{
		return base.GetNumberOfTransformations() + this.loopTransformations.Length * this.loop + this.outTransformations.Length;
	}

	public ShapeTransformation[] loopTransformations;

	public int loop = 1;

	public TransformationLoopType loopType;

	public ShapeTransformation[] outTransformations;
}

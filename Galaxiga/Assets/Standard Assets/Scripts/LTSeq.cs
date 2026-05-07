using System;
using UnityEngine;

public class LTSeq
{
	public int id
	{
		get
		{
			return (int)(this._id | this.counter << 16);
		}
	}

	public void reset()
	{
		this.previous = null;
		this.tween = null;
		this.totalDelay = 0f;
	}

	public void init(uint id, uint global_counter)
	{
		this.reset();
		this._id = id;
		this.counter = global_counter;
		this.current = this;
	}

	private LTSeq addOn()
	{
		this.current.toggle = true;
		LTSeq ltseq = this.current;
		this.current = LeanTween.sequence(true);
		UnityEngine.Debug.Log(string.Concat(new object[]
		{
			"this.current:",
			this.current.id,
			" lastCurrent:",
			ltseq.id
		}));
		this.current.previous = ltseq;
		ltseq.toggle = false;
		this.current.totalDelay = ltseq.totalDelay;
		this.current.debugIter = ltseq.debugIter + 1;
		return this.current;
	}

	private float addPreviousDelays()
	{
		LTSeq ltseq = this.current.previous;
		if (ltseq != null && ltseq.tween != null)
		{
			return this.current.totalDelay + ltseq.tween.time;
		}
		return this.current.totalDelay;
	}

	public LTSeq append(float delay)
	{
		this.current.totalDelay += delay;
		return this.current;
	}

	public LTSeq append(Action callback)
	{
		LTDescr ltdescr = LeanTween.delayedCall(0f, callback);
		this.append(ltdescr);
		return this.addOn();
	}

	public LTSeq append(Action<object> callback, object obj)
	{
		this.append(LeanTween.delayedCall(0f, callback).setOnCompleteParam(obj));
		return this.addOn();
	}

	public LTSeq append(GameObject gameObject, Action callback)
	{
		this.append(LeanTween.delayedCall(gameObject, 0f, callback));
		return this.addOn();
	}

	public LTSeq append(GameObject gameObject, Action<object> callback, object obj)
	{
		this.append(LeanTween.delayedCall(gameObject, 0f, callback).setOnCompleteParam(obj));
		return this.addOn();
	}

	public LTSeq append(LTDescr tween)
	{
		this.current.tween = tween;
		this.current.totalDelay = this.addPreviousDelays();
		tween.setDelay(this.current.totalDelay);
		return this.addOn();
	}

	public LTSeq insert(LTDescr tween)
	{
		this.current.tween = tween;
		tween.setDelay(this.addPreviousDelays());
		return this.addOn();
	}

	public LTSeq setScale(float timeScale)
	{
		this.setScaleRecursive(this.current, timeScale, 500);
		return this.addOn();
	}

	private void setScaleRecursive(LTSeq seq, float timeScale, int count)
	{
		if (count > 0)
		{
			this.timeScale = timeScale;
			seq.totalDelay *= timeScale;
			if (seq.tween != null)
			{
				if (seq.tween.time != 0f)
				{
					seq.tween.setTime(seq.tween.time * timeScale);
				}
				seq.tween.setDelay(seq.tween.delay * timeScale);
			}
			if (seq.previous != null)
			{
				this.setScaleRecursive(seq.previous, timeScale, count - 1);
			}
		}
	}

	public LTSeq reverse()
	{
		return this.addOn();
	}

	public LTSeq previous;

	public LTSeq current;

	public LTDescr tween;

	public float totalDelay;

	public float timeScale;

	private int debugIter;

	public uint counter;

	public bool toggle;

	private uint _id;
}

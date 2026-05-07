using System;
using UnityEngine;

public class PathSplineTrack : MonoBehaviour
{
	private void Start()
	{
		this.track = new LTSpline(new Vector3[]
		{
			this.trackOnePoints[0].position,
			this.trackOnePoints[1].position,
			this.trackOnePoints[2].position,
			this.trackOnePoints[3].position,
			this.trackOnePoints[4].position,
			this.trackOnePoints[5].position,
			this.trackOnePoints[6].position
		});
		LeanTween.moveSpline(this.trackTrailRenderers, this.track, 2f).setOrientToPath(true).setRepeat(-1);
	}

	private void Update()
	{
		float axis = UnityEngine.Input.GetAxis("Horizontal");
		if (Input.anyKeyDown)
		{
			if (axis < 0f && this.trackIter > 0)
			{
				this.trackIter--;
				this.playSwish();
			}
			else if (axis > 0f && this.trackIter < 2)
			{
				this.trackIter++;
				this.playSwish();
			}
			LeanTween.moveLocalX(this.carInternal, (float)(this.trackIter - 1) * 6f, 0.3f).setEase(LeanTweenType.easeOutBack);
		}
		this.track.place(this.car.transform, this.trackPosition);
		this.trackPosition += Time.deltaTime * 0.03f;
		if (this.trackPosition < 0f)
		{
			this.trackPosition = 1f;
		}
		else if (this.trackPosition > 1f)
		{
			this.trackPosition = 0f;
		}
	}

	private void OnDrawGizmos()
	{
		LTSpline.drawGizmo(this.trackOnePoints, Color.red);
	}

	private void playSwish()
	{
		AnimationCurve volume = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0.005464481f, 1.83897f, 0f),
			new Keyframe(0.1114856f, 2.281785f, 0f, 0f),
			new Keyframe(0.2482903f, 2.271654f, 0f, 0f),
			new Keyframe(0.3f, 0.01670286f, 0f, 0f)
		});
		AnimationCurve frequency = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0.00136725f, 0f, 0f),
			new Keyframe(0.1482391f, 0.005405405f, 0f, 0f),
			new Keyframe(0.2650336f, 0.002480127f, 0f, 0f)
		});
		AudioClip audio = LeanAudio.createAudio(volume, frequency, LeanAudio.options().setVibrato(new Vector3[]
		{
			new Vector3(0.2f, 0.5f, 0f)
		}).setWaveNoise().setWaveNoiseScale(1000f));
		LeanAudio.play(audio);
	}

	public GameObject car;

	public GameObject carInternal;

	public GameObject trackTrailRenderers;

	public Transform[] trackOnePoints;

	private LTSpline track;

	private int trackIter = 1;

	private float trackPosition;
}

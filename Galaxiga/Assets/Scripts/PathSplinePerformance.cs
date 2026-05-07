using System;
using System.Collections.Generic;
using UnityEngine;

public class PathSplinePerformance : MonoBehaviour
{
	private void Start()
	{
		Application.targetFrameRate = 240;
		List<Vector3> list = new List<Vector3>();
		float num = 0f;
		int num2 = this.trackNodes + 1;
		for (int i = 0; i < num2; i++)
		{
			float x = Mathf.Cos(num * 0.0174532924f) * this.circleLength + UnityEngine.Random.Range(0f, this.randomRange);
			float z = Mathf.Sin(num * 0.0174532924f) * this.circleLength + UnityEngine.Random.Range(0f, this.randomRange);
			list.Add(new Vector3(x, 1f, z));
			num += 360f / (float)this.trackNodes;
		}
		list[0] = list[list.Count - 1];
		list.Add(list[1]);
		list.Add(list[2]);
		this.track = new LTSpline(list.ToArray());
		this.carAdd = this.carSpeed / this.track.distance;
		this.tracerSpeed = this.track.distance / (this.carSpeed * 1.2f);
		LeanTween.moveSpline(this.trackTrailRenderers, this.track, this.tracerSpeed).setOrientToPath(true).setRepeat(-1);
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
		this.trackPosition += Time.deltaTime * this.carAdd;
		if (this.trackPosition > 1f)
		{
			this.trackPosition = 0f;
		}
	}

	private void OnDrawGizmos()
	{
		if (this.track != null)
		{
			this.track.drawGizmo(Color.red);
		}
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

	public GameObject trackTrailRenderers;

	public GameObject car;

	public GameObject carInternal;

	public float circleLength = 10f;

	public float randomRange = 1f;

	public int trackNodes = 30;

	public float carSpeed = 30f;

	public float tracerSpeed = 2f;

	private LTSpline track;

	private int trackIter = 1;

	private float carAdd;

	private float trackPosition;
}

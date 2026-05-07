using System;
using System.Collections;
using UnityEngine;

namespace DigitalRuby.ThunderAndLightning
{
	public class ThunderAndLightningScript : MonoBehaviour
	{
		private void Start()
		{
			this.EnableLightning = true;
			if (this.Camera == null)
			{
				this.Camera = Camera.main;
			}
			if (RenderSettings.skybox != null)
			{
				Material skybox = new Material(RenderSettings.skybox);
				RenderSettings.skybox = skybox;
				this.skyboxMaterial = skybox;
			}
			this.skyboxExposureOriginal = (this.skyboxExposureStorm = ((!(this.skyboxMaterial == null) && this.skyboxMaterial.HasProperty("_Exposure")) ? this.skyboxMaterial.GetFloat("_Exposure") : 1f));
			this.audioSourceThunder = base.gameObject.AddComponent<AudioSource>();
			this.lightningBoltHandler = new ThunderAndLightningScript.LightningBoltHandler(this);
			this.lightningBoltHandler.VolumeMultiplier = this.VolumeMultiplier;
		}

		private void Update()
		{
			if (this.lightningBoltHandler != null && this.EnableLightning)
			{
				this.lightningBoltHandler.VolumeMultiplier = this.VolumeMultiplier;
				this.lightningBoltHandler.Update();
			}
		}

		public void CallNormalLightning()
		{
			this.CallNormalLightning(null, null);
		}

		public void CallNormalLightning(Vector3? start, Vector3? end)
		{
			base.StartCoroutine(this.lightningBoltHandler.ProcessLightning(start, end, false, true));
		}

		public void CallIntenseLightning()
		{
			this.CallIntenseLightning(null, null);
		}

		public void CallIntenseLightning(Vector3? start, Vector3? end)
		{
			base.StartCoroutine(this.lightningBoltHandler.ProcessLightning(start, end, true, true));
		}

		public float SkyboxExposureOriginal
		{
			get
			{
				return this.skyboxExposureOriginal;
			}
		}

		public bool EnableLightning { get; set; }

		[Tooltip("Lightning bolt script - optional, leave null if you don't want lightning bolts")]
		public LightningBoltPrefabScript LightningBoltScript;

		[Tooltip("Camera where the lightning should be centered over. Defaults to main camera.")]
		public Camera Camera;

		[SingleLine("Random interval between strikes.")]
		public RangeOfFloats LightningIntervalTimeRange = new RangeOfFloats
		{
			Minimum = 10f,
			Maximum = 25f
		};

		[Tooltip("Probability (0-1) of an intense lightning bolt that hits really close. Intense lightning has increased brightness and louder thunder compared to normal lightning, and the thunder sounds plays a lot sooner.")]
		[Range(0f, 1f)]
		public float LightningIntenseProbability = 0.2f;

		[Tooltip("Sounds to play for normal thunder. One will be chosen at random for each lightning strike. Depending on intensity, some normal lightning may not play a thunder sound.")]
		public AudioClip[] ThunderSoundsNormal;

		[Tooltip("Sounds to play for intense thunder. One will be chosen at random for each lightning strike.")]
		public AudioClip[] ThunderSoundsIntense;

		[Tooltip("Whether lightning strikes should always try to be in the camera view")]
		public bool LightningAlwaysVisible = true;

		[Tooltip("The chance lightning will simply be in the clouds with no visible bolt")]
		[Range(0f, 1f)]
		public float CloudLightningChance = 0.5f;

		[Tooltip("Whether to modify the skybox exposure when lightning is created")]
		public bool ModifySkyboxExposure;

		[Tooltip("Base point light range for lightning bolts. Increases as intensity increases.")]
		[Range(1f, 10000f)]
		public float BaseLightRange = 2000f;

		[Tooltip("Starting y value for the lightning strikes")]
		[Range(0f, 100000f)]
		public float LightningYStart = 500f;

		[Tooltip("Volume multiplier")]
		[Range(0f, 1f)]
		public float VolumeMultiplier = 1f;

		private float skyboxExposureOriginal;

		private float skyboxExposureStorm;

		private float nextLightningTime;

		private bool lightningInProgress;

		private AudioSource audioSourceThunder;

		private ThunderAndLightningScript.LightningBoltHandler lightningBoltHandler;

		private Material skyboxMaterial;

		private AudioClip lastThunderSound;

		private class LightningBoltHandler
		{
			public LightningBoltHandler(ThunderAndLightningScript script)
			{
				this.script = script;
				this.CalculateNextLightningTime();
			}

			public float VolumeMultiplier { get; set; }

			private void UpdateLighting()
			{
				if (this.script.lightningInProgress)
				{
					return;
				}
				if (this.script.ModifySkyboxExposure)
				{
					this.script.skyboxExposureStorm = 0.35f;
					if (this.script.skyboxMaterial != null && this.script.skyboxMaterial.HasProperty("_Exposure"))
					{
						this.script.skyboxMaterial.SetFloat("_Exposure", this.script.skyboxExposureStorm);
					}
				}
				this.CheckForLightning();
			}

			private void CalculateNextLightningTime()
			{
				this.script.nextLightningTime = Time.time + this.script.LightningIntervalTimeRange.Random(this.random);
				this.script.lightningInProgress = false;
				if (this.script.ModifySkyboxExposure && this.script.skyboxMaterial.HasProperty("_Exposure"))
				{
					this.script.skyboxMaterial.SetFloat("_Exposure", this.script.skyboxExposureStorm);
				}
			}

			public IEnumerator ProcessLightning(Vector3? _start, Vector3? _end, bool intense, bool visible)
			{
				this.script.lightningInProgress = true;
				float intensity;
				float sleepTime;
				AudioClip[] sounds;
				if (intense)
				{
					float t = UnityEngine.Random.Range(0f, 1f);
					intensity = Mathf.Lerp(2f, 8f, t);
					sleepTime = 5f / intensity;
					sounds = this.script.ThunderSoundsIntense;
				}
				else
				{
					float t2 = UnityEngine.Random.Range(0f, 1f);
					intensity = Mathf.Lerp(0f, 2f, t2);
					sleepTime = 30f / intensity;
					sounds = this.script.ThunderSoundsNormal;
				}
				if (this.script.skyboxMaterial != null && this.script.ModifySkyboxExposure)
				{
					this.script.skyboxMaterial.SetFloat("_Exposure", Mathf.Max(intensity * 0.5f, this.script.skyboxExposureStorm));
				}
				this.Strike(_start, _end, intense, intensity, this.script.Camera, (!visible) ? null : this.script.Camera);
				this.CalculateNextLightningTime();
				bool playThunder = intensity >= 1f;
				if (playThunder && sounds != null && sounds.Length != 0)
				{
					yield return new WaitForSeconds(sleepTime);
					AudioClip clip = null;
					do
					{
						clip = sounds[UnityEngine.Random.Range(0, sounds.Length - 1)];
					}
					while (sounds.Length > 1 && clip == this.script.lastThunderSound);
					this.script.lastThunderSound = clip;
					this.script.audioSourceThunder.PlayOneShot(clip, intensity * 0.5f * this.VolumeMultiplier);
				}
				yield break;
			}

			private void Strike(Vector3? _start, Vector3? _end, bool intense, float intensity, Camera camera, Camera visibleInCamera)
			{
				float min = (!intense) ? -5000f : -1000f;
				float max = (!intense) ? 5000f : 1000f;
				float num = (!intense) ? 2500f : 500f;
				float num2 = (UnityEngine.Random.Range(0, 2) != 0) ? UnityEngine.Random.Range(num, max) : UnityEngine.Random.Range(min, -num);
				float y = this.script.LightningYStart;
				float num3 = (UnityEngine.Random.Range(0, 2) != 0) ? UnityEngine.Random.Range(num, max) : UnityEngine.Random.Range(min, -num);
				Vector3 vector = this.script.Camera.transform.position;
				vector.x += num2;
				vector.y = y;
				vector.z += num3;
				if (visibleInCamera != null)
				{
					Quaternion rotation = visibleInCamera.transform.rotation;
					visibleInCamera.transform.rotation = Quaternion.Euler(0f, rotation.eulerAngles.y, 0f);
					float x = UnityEngine.Random.Range((float)visibleInCamera.pixelWidth * 0.1f, (float)visibleInCamera.pixelWidth * 0.9f);
					float z = UnityEngine.Random.Range(visibleInCamera.nearClipPlane + num + num, max);
					Vector3 vector2 = visibleInCamera.ScreenToWorldPoint(new Vector3(x, 0f, z));
					vector = vector2;
					vector.y = y;
					visibleInCamera.transform.rotation = rotation;
				}
				Vector3 vector3 = vector;
				num2 = UnityEngine.Random.Range(-100f, 100f);
				y = ((UnityEngine.Random.Range(0, 4) != 0) ? -1f : UnityEngine.Random.Range(-1f, 600f));
				num3 += UnityEngine.Random.Range(-100f, 100f);
				vector3.x += num2;
				vector3.y = y;
				vector3.z += num3;
				vector3.x += num * camera.transform.forward.x;
				vector3.z += num * camera.transform.forward.z;
				while ((vector - vector3).magnitude < 500f)
				{
					vector3.x += num * camera.transform.forward.x;
					vector3.z += num * camera.transform.forward.z;
				}
				vector = ((_start == null) ? vector : _start.Value);
				vector3 = ((_end == null) ? vector3 : _end.Value);
				RaycastHit raycastHit;
				if (Physics.Raycast(vector, (vector - vector3).normalized, out raycastHit, 3.40282347E+38f))
				{
					vector3 = raycastHit.point;
				}
				int generations = this.script.LightningBoltScript.Generations;
				RangeOfFloats trunkWidthRange = this.script.LightningBoltScript.TrunkWidthRange;
				if (UnityEngine.Random.value < this.script.CloudLightningChance)
				{
					this.script.LightningBoltScript.TrunkWidthRange = default(RangeOfFloats);
					this.script.LightningBoltScript.Generations = 1;
				}
				this.script.LightningBoltScript.LightParameters.LightIntensity = intensity * 0.5f;
				this.script.LightningBoltScript.Trigger(new Vector3?(vector), new Vector3?(vector3));
				this.script.LightningBoltScript.TrunkWidthRange = trunkWidthRange;
				this.script.LightningBoltScript.Generations = generations;
			}

			private void CheckForLightning()
			{
				if (Time.time >= this.script.nextLightningTime)
				{
					bool intense = UnityEngine.Random.value < this.script.LightningIntenseProbability;
					this.script.StartCoroutine(this.ProcessLightning(null, null, intense, this.script.LightningAlwaysVisible));
				}
			}

			public void Update()
			{
				this.UpdateLighting();
			}

			private ThunderAndLightningScript script;

			private readonly System.Random random = new System.Random();
		}
	}
}

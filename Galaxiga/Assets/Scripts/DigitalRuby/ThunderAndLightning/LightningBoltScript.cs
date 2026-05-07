using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace DigitalRuby.ThunderAndLightning
{
	public class LightningBoltScript : MonoBehaviour
	{
		public Action<LightningBoltParameters, Vector3, Vector3> LightningStartedCallback { get; set; }

		public Action<LightningBoltParameters, Vector3, Vector3> LightningEndedCallback { get; set; }

		public Action<Light> LightAddedCallback { get; set; }

		public Action<Light> LightRemovedCallback { get; set; }

		public bool HasActiveBolts
		{
			get
			{
				return this.activeBolts.Count != 0;
			}
		}

		public static Vector4 LastTime { get; private set; }

		public virtual void CreateLightningBolt(LightningBoltParameters p)
		{
			if (p != null)
			{
				this.UpdateTexture();
				this.oneParameterArray[0] = p;
				LightningBolt orCreateLightningBolt = this.GetOrCreateLightningBolt();
				LightningBoltDependencies dependencies = this.CreateLightningBoltDependencies(this.oneParameterArray);
				orCreateLightningBolt.SetupLightningBolt(dependencies);
			}
		}

		public void CreateLightningBolts(ICollection<LightningBoltParameters> parameters)
		{
			if (parameters != null && parameters.Count != 0)
			{
				this.UpdateTexture();
				LightningBolt orCreateLightningBolt = this.GetOrCreateLightningBolt();
				LightningBoltDependencies dependencies = this.CreateLightningBoltDependencies(parameters);
				orCreateLightningBolt.SetupLightningBolt(dependencies);
			}
		}

		protected virtual void Awake()
		{
			this.UpdateShaderIds();
		}

		protected virtual void Start()
		{
			this.UpdateCamera();
			this.UpdateMaterialsForLastTexture();
			this.UpdateShaderParameters();
			this.CheckCompensateForParentTransform();
			SceneManager.sceneLoaded += this.OnSceneLoaded;
			if (this.MultiThreaded)
			{
				this.threadState = new LightningThreadState();
				base.InvokeRepeating("UpdateMainThreadActions", 0f, 0.004166667f);
			}
		}

		protected virtual void Update()
		{
			if (this.HasActiveBolts)
			{
				this.UpdateCamera();
				this.UpdateShaderParameters();
				this.CheckCompensateForParentTransform();
				this.UpdateActiveBolts();
				float timeSinceLevelLoad = Time.timeSinceLevelLoad;
				int nameID = LightningBoltScript.shaderId_LightningTime;
				Vector4 vector = new Vector4(timeSinceLevelLoad * 0.05f, timeSinceLevelLoad, timeSinceLevelLoad * 2f, timeSinceLevelLoad * 3f);
				LightningBoltScript.LastTime = vector;
				Shader.SetGlobalVector(nameID, vector);
			}
		}

		protected virtual LightningBoltParameters OnCreateParameters()
		{
			return LightningBoltParameters.GetOrCreateParameters();
		}

		protected LightningBoltParameters CreateParameters()
		{
			LightningBoltParameters lightningBoltParameters = this.OnCreateParameters();
			lightningBoltParameters.quality = this.QualitySetting;
			this.PopulateParameters(lightningBoltParameters);
			return lightningBoltParameters;
		}

		protected virtual void PopulateParameters(LightningBoltParameters p)
		{
		}

		internal Material lightningMaterialMeshInternal { get; private set; }

		internal Material lightningMaterialMeshNoGlowInternal { get; private set; }

		private Coroutine StartCoroutineWrapper(IEnumerator routine)
		{
			if (base.isActiveAndEnabled)
			{
				return base.StartCoroutine(routine);
			}
			return null;
		}

		private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
		{
			LightningBolt.ClearCache();
		}

		private LightningBoltDependencies CreateLightningBoltDependencies(ICollection<LightningBoltParameters> parameters)
		{
			LightningBoltDependencies lightningBoltDependencies;
			if (this.dependenciesCache.Count == 0)
			{
				lightningBoltDependencies = new LightningBoltDependencies();
				lightningBoltDependencies.AddActiveBolt = new Action<LightningBolt>(this.AddActiveBolt);
				lightningBoltDependencies.LightAdded = new Action<Light>(this.OnLightAdded);
				lightningBoltDependencies.LightRemoved = new Action<Light>(this.OnLightRemoved);
				lightningBoltDependencies.ReturnToCache = new Action<LightningBoltDependencies>(this.ReturnLightningDependenciesToCache);
				lightningBoltDependencies.StartCoroutine = new Func<IEnumerator, Coroutine>(this.StartCoroutineWrapper);
				lightningBoltDependencies.Parent = base.gameObject;
			}
			else
			{
				int index = this.dependenciesCache.Count - 1;
				lightningBoltDependencies = this.dependenciesCache[index];
				this.dependenciesCache.RemoveAt(index);
			}
			lightningBoltDependencies.CameraPos = this.Camera.transform.position;
			lightningBoltDependencies.CameraIsOrthographic = this.Camera.orthographic;
			lightningBoltDependencies.CameraMode = this.calculatedCameraMode;
			lightningBoltDependencies.DestParticleSystem = this.LightningDestinationParticleSystem;
			lightningBoltDependencies.LightningMaterialMesh = this.lightningMaterialMeshInternal;
			lightningBoltDependencies.LightningMaterialMeshNoGlow = this.lightningMaterialMeshNoGlowInternal;
			lightningBoltDependencies.OriginParticleSystem = this.LightningOriginParticleSystem;
			lightningBoltDependencies.SortLayerName = this.SortLayerName;
			lightningBoltDependencies.SortOrderInLayer = this.SortOrderInLayer;
			lightningBoltDependencies.UseWorldSpace = this.UseWorldSpace;
			lightningBoltDependencies.ThreadState = this.threadState;
			if (this.threadState != null)
			{
				lightningBoltDependencies.Parameters = new List<LightningBoltParameters>(parameters);
			}
			else
			{
				lightningBoltDependencies.Parameters = parameters;
			}
			lightningBoltDependencies.LightningBoltStarted = this.LightningStartedCallback;
			lightningBoltDependencies.LightningBoltEnded = this.LightningEndedCallback;
			return lightningBoltDependencies;
		}

		private void ReturnLightningDependenciesToCache(LightningBoltDependencies d)
		{
			d.Parameters = null;
			d.OriginParticleSystem = null;
			d.DestParticleSystem = null;
			d.LightningMaterialMesh = null;
			d.LightningMaterialMeshNoGlow = null;
			this.dependenciesCache.Add(d);
		}

		internal void OnLightAdded(Light l)
		{
			if (this.LightAddedCallback != null)
			{
				this.LightAddedCallback(l);
			}
		}

		internal void OnLightRemoved(Light l)
		{
			if (this.LightRemovedCallback != null)
			{
				this.LightRemovedCallback(l);
			}
		}

		internal void AddActiveBolt(LightningBolt bolt)
		{
			this.activeBolts.Add(bolt);
		}

		private void UpdateMainThreadActions()
		{
			this.threadState.UpdateMainThreadActions();
		}

		private void UpdateShaderIds()
		{
			if (LightningBoltScript.shaderId_MainTex != -2147483648)
			{
				return;
			}
			LightningBoltScript.shaderId_MainTex = Shader.PropertyToID("_MainTex");
			LightningBoltScript.shaderId_GlowTex = Shader.PropertyToID("_GlowTex");
			LightningBoltScript.shaderId_TintColor = Shader.PropertyToID("_TintColor");
			LightningBoltScript.shaderId_GlowTintColor = Shader.PropertyToID("_GlowTintColor");
			LightningBoltScript.shaderId_JitterMultiplier = Shader.PropertyToID("_JitterMultiplier");
			LightningBoltScript.shaderId_Turbulence = Shader.PropertyToID("_Turbulence");
			LightningBoltScript.shaderId_TurbulenceVelocity = Shader.PropertyToID("_TurbulenceVelocity");
			LightningBoltScript.shaderId_SrcBlendMode = Shader.PropertyToID("_SrcBlendMode");
			LightningBoltScript.shaderId_DstBlendMode = Shader.PropertyToID("_DstBlendMode");
			LightningBoltScript.shaderId_InvFade = Shader.PropertyToID("_InvFade");
			LightningBoltScript.shaderId_LightningTime = Shader.PropertyToID("_LightningTime");
			LightningBoltScript.shaderId_IntensityFlicker = Shader.PropertyToID("_IntensityFlicker");
			LightningBoltScript.shaderId_IntensityFlickerTexture = Shader.PropertyToID("_IntensityFlickerTexture");
		}

		private void UpdateMaterialsForLastTexture()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			this.calculatedCameraMode = CameraMode.Unknown;
			this.lightningMaterialMeshInternal = new Material(this.LightningMaterialMesh);
			this.lightningMaterialMeshNoGlowInternal = new Material(this.LightningMaterialMeshNoGlow);
			if (this.LightningTexture != null)
			{
				this.lightningMaterialMeshInternal.SetTexture(LightningBoltScript.shaderId_MainTex, this.LightningTexture);
				this.lightningMaterialMeshNoGlowInternal.SetTexture(LightningBoltScript.shaderId_MainTex, this.LightningTexture);
			}
			if (this.LightningGlowTexture != null)
			{
				this.lightningMaterialMeshInternal.SetTexture(LightningBoltScript.shaderId_GlowTex, this.LightningGlowTexture);
			}
			this.SetupMaterialCamera();
		}

		private void UpdateTexture()
		{
			if (this.LightningTexture != null && this.LightningTexture != this.lastLightningTexture)
			{
				this.lastLightningTexture = this.LightningTexture;
				this.UpdateMaterialsForLastTexture();
			}
			if (this.LightningGlowTexture != null && this.LightningGlowTexture != this.lastLightningGlowTexture)
			{
				this.lastLightningGlowTexture = this.LightningGlowTexture;
				this.UpdateMaterialsForLastTexture();
			}
		}

		private void SetMaterialPerspective()
		{
			if (this.calculatedCameraMode != CameraMode.Perspective)
			{
				this.calculatedCameraMode = CameraMode.Perspective;
				this.lightningMaterialMeshInternal.EnableKeyword("PERSPECTIVE");
				this.lightningMaterialMeshNoGlowInternal.EnableKeyword("PERSPECTIVE");
				this.lightningMaterialMeshInternal.DisableKeyword("ORTHOGRAPHIC_XY");
				this.lightningMaterialMeshNoGlowInternal.DisableKeyword("ORTHOGRAPHIC_XY");
				this.lightningMaterialMeshInternal.DisableKeyword("ORTHOGRAPHIC_XZ");
				this.lightningMaterialMeshNoGlowInternal.DisableKeyword("ORTHOGRAPHIC_XZ");
			}
		}

		private void SetMaterialOrthographicXY()
		{
			if (this.calculatedCameraMode != CameraMode.OrthographicXY)
			{
				this.calculatedCameraMode = CameraMode.OrthographicXY;
				this.lightningMaterialMeshInternal.EnableKeyword("ORTHOGRAPHIC_XY");
				this.lightningMaterialMeshNoGlowInternal.EnableKeyword("ORTHOGRAPHIC_XY");
				this.lightningMaterialMeshInternal.DisableKeyword("ORTHOGRAPHIC_XZ");
				this.lightningMaterialMeshNoGlowInternal.DisableKeyword("ORTHOGRAPHIC_XZ");
				this.lightningMaterialMeshInternal.DisableKeyword("PERSPECTIVE");
				this.lightningMaterialMeshNoGlowInternal.DisableKeyword("PERSPECTIVE");
			}
		}

		private void SetMaterialOrthographicXZ()
		{
			if (this.calculatedCameraMode != CameraMode.OrthographicXZ)
			{
				this.calculatedCameraMode = CameraMode.OrthographicXZ;
				this.lightningMaterialMeshInternal.EnableKeyword("ORTHOGRAPHIC_XZ");
				this.lightningMaterialMeshNoGlowInternal.EnableKeyword("ORTHOGRAPHIC_XZ");
				this.lightningMaterialMeshInternal.DisableKeyword("ORTHOGRAPHIC_XY");
				this.lightningMaterialMeshNoGlowInternal.DisableKeyword("ORTHOGRAPHIC_XY");
				this.lightningMaterialMeshInternal.DisableKeyword("PERSPECTIVE");
				this.lightningMaterialMeshNoGlowInternal.DisableKeyword("PERSPECTIVE");
			}
		}

		private void SetupMaterialCamera()
		{
			if (this.Camera == null && this.CameraMode == CameraMode.Auto)
			{
				this.SetMaterialPerspective();
				return;
			}
			if (this.CameraMode == CameraMode.Auto)
			{
				if (this.Camera.orthographic)
				{
					this.SetMaterialOrthographicXY();
				}
				else
				{
					this.SetMaterialPerspective();
				}
			}
			else if (this.CameraMode == CameraMode.Perspective)
			{
				this.SetMaterialPerspective();
			}
			else if (this.CameraMode == CameraMode.OrthographicXY)
			{
				this.SetMaterialOrthographicXY();
			}
			else
			{
				this.SetMaterialOrthographicXZ();
			}
		}

		private void EnableKeyword(string keyword, bool enable, Material m)
		{
			if (enable)
			{
				m.EnableKeyword(keyword);
			}
			else
			{
				m.DisableKeyword(keyword);
			}
		}

		private void UpdateShaderParameters()
		{
			this.lightningMaterialMeshInternal.SetColor(LightningBoltScript.shaderId_TintColor, this.LightningTintColor);
			this.lightningMaterialMeshInternal.SetColor(LightningBoltScript.shaderId_GlowTintColor, this.GlowTintColor);
			this.lightningMaterialMeshInternal.SetFloat(LightningBoltScript.shaderId_JitterMultiplier, this.JitterMultiplier);
			this.lightningMaterialMeshInternal.SetFloat(LightningBoltScript.shaderId_Turbulence, this.Turbulence * LightningBoltParameters.Scale);
			this.lightningMaterialMeshInternal.SetVector(LightningBoltScript.shaderId_TurbulenceVelocity, this.TurbulenceVelocity * LightningBoltParameters.Scale);
			this.lightningMaterialMeshInternal.SetInt(LightningBoltScript.shaderId_SrcBlendMode, (int)this.SourceBlendMode);
			this.lightningMaterialMeshInternal.SetInt(LightningBoltScript.shaderId_DstBlendMode, (int)this.DestinationBlendMode);
			this.lightningMaterialMeshInternal.renderQueue = this.RenderQueue;
			this.lightningMaterialMeshInternal.SetFloat(LightningBoltScript.shaderId_InvFade, this.SoftParticlesFactor);
			this.lightningMaterialMeshNoGlowInternal.SetColor(LightningBoltScript.shaderId_TintColor, this.LightningTintColor);
			this.lightningMaterialMeshNoGlowInternal.SetFloat(LightningBoltScript.shaderId_JitterMultiplier, this.JitterMultiplier);
			this.lightningMaterialMeshNoGlowInternal.SetFloat(LightningBoltScript.shaderId_Turbulence, this.Turbulence * LightningBoltParameters.Scale);
			this.lightningMaterialMeshNoGlowInternal.SetVector(LightningBoltScript.shaderId_TurbulenceVelocity, this.TurbulenceVelocity * LightningBoltParameters.Scale);
			this.lightningMaterialMeshNoGlowInternal.SetInt(LightningBoltScript.shaderId_SrcBlendMode, (int)this.SourceBlendMode);
			this.lightningMaterialMeshNoGlowInternal.SetInt(LightningBoltScript.shaderId_DstBlendMode, (int)this.DestinationBlendMode);
			this.lightningMaterialMeshNoGlowInternal.renderQueue = this.RenderQueue;
			this.lightningMaterialMeshNoGlowInternal.SetFloat(LightningBoltScript.shaderId_InvFade, this.SoftParticlesFactor);
			if (this.IntensityFlicker != LightningBoltScript.intensityFlickerDefault && this.IntensityFlickerTexture != null)
			{
				this.lightningMaterialMeshInternal.SetVector(LightningBoltScript.shaderId_IntensityFlicker, this.IntensityFlicker);
				this.lightningMaterialMeshInternal.SetTexture(LightningBoltScript.shaderId_IntensityFlickerTexture, this.IntensityFlickerTexture);
				this.lightningMaterialMeshNoGlowInternal.SetVector(LightningBoltScript.shaderId_IntensityFlicker, this.IntensityFlicker);
				this.lightningMaterialMeshNoGlowInternal.SetTexture(LightningBoltScript.shaderId_IntensityFlickerTexture, this.IntensityFlickerTexture);
				this.lightningMaterialMeshInternal.EnableKeyword("INTENSITY_FLICKER");
				this.lightningMaterialMeshNoGlowInternal.EnableKeyword("INTENSITY_FLICKER");
			}
			else
			{
				this.lightningMaterialMeshInternal.DisableKeyword("INTENSITY_FLICKER");
				this.lightningMaterialMeshNoGlowInternal.DisableKeyword("INTENSITY_FLICKER");
			}
			this.SetupMaterialCamera();
		}

		private void CheckCompensateForParentTransform()
		{
			if (this.CompensateForParentTransform)
			{
				Transform parent = base.transform.parent;
				if (parent != null)
				{
					base.transform.position = parent.position;
					base.transform.localScale = new Vector3(1f / parent.localScale.x, 1f / parent.localScale.y, 1f / parent.localScale.z);
					base.transform.rotation = parent.rotation;
				}
			}
		}

		private void UpdateCamera()
		{
			this.Camera = ((!(this.Camera == null)) ? this.Camera : ((!(Camera.current == null)) ? Camera.current : Camera.main));
		}

		private LightningBolt GetOrCreateLightningBolt()
		{
			if (this.lightningBoltCache.Count == 0)
			{
				return new LightningBolt();
			}
			LightningBolt result = this.lightningBoltCache[this.lightningBoltCache.Count - 1];
			this.lightningBoltCache.RemoveAt(this.lightningBoltCache.Count - 1);
			return result;
		}

		private void UpdateActiveBolts()
		{
			for (int i = this.activeBolts.Count - 1; i >= 0; i--)
			{
				LightningBolt lightningBolt = this.activeBolts[i];
				if (!lightningBolt.Update())
				{
					this.activeBolts.RemoveAt(i);
					lightningBolt.Cleanup();
					this.lightningBoltCache.Add(lightningBolt);
				}
			}
		}

		private void OnApplicationQuit()
		{
			if (this.threadState != null)
			{
				this.threadState.Running = false;
			}
		}

		private void Cleanup()
		{
			foreach (LightningBolt lightningBolt in this.activeBolts)
			{
				lightningBolt.Cleanup();
			}
			this.activeBolts.Clear();
		}

		private void OnDestroy()
		{
			if (this.threadState != null)
			{
				this.threadState.TerminateAndWaitForEnd();
			}
			if (this.lightningMaterialMeshInternal != null)
			{
				UnityEngine.Object.Destroy(this.lightningMaterialMeshInternal);
			}
			if (this.lightningMaterialMeshNoGlowInternal != null)
			{
				UnityEngine.Object.Destroy(this.lightningMaterialMeshNoGlowInternal);
			}
			this.Cleanup();
		}

		private void OnDisable()
		{
			this.Cleanup();
		}

		[Header("Lightning General Properties")]
		[Tooltip("The camera the lightning should be shown in. Defaults to the current camera, or the main camera if current camera is null. If you are using a different camera, you may want to put the lightning in it's own layer and cull that layer out of any other cameras.")]
		public Camera Camera;

		[Tooltip("Type of camera mode. Auto detects the camera and creates appropriate lightning. Can be overriden to do something more specific regardless of camera.")]
		public CameraMode CameraMode;

		internal CameraMode calculatedCameraMode = CameraMode.Unknown;

		[Tooltip("True if you are using world space coordinates for the lightning bolt, false if you are using coordinates relative to the parent game object.")]
		public bool UseWorldSpace = true;

		[Tooltip("Whether to compensate for the parent transform. Default is false. If true, rotation, scale and position are altered by the parent transform. Use this to fix scaling, rotation and other offset problems with the lightning.")]
		public bool CompensateForParentTransform;

		[Tooltip("Lightning quality setting. This allows setting limits on generations, lights and shadow casting lights based on the global quality setting.")]
		public LightningBoltQualitySetting QualitySetting;

		[Tooltip("Whether to use multi-threaded generation of lightning. Lightning will be delayed by about 1 frame if this is turned on, but this can significantly improve performance.")]
		public bool MultiThreaded;

		[Header("Lightning 2D Settings")]
		[Tooltip("Sort layer name")]
		public string SortLayerName;

		[Tooltip("Order in sort layer")]
		public int SortOrderInLayer;

		[Header("Lightning Rendering Properties")]
		[Tooltip("Soft particles factor. 0.01 to 3.0 are typical, 100.0 to disable.")]
		[Range(0.01f, 100f)]
		public float SoftParticlesFactor = 3f;

		[Tooltip("The render queue for the lightning. -1 for default.")]
		public int RenderQueue = -1;

		[Tooltip("Lightning material for mesh renderer")]
		public Material LightningMaterialMesh;

		[Tooltip("Lightning material for mesh renderer, without glow")]
		public Material LightningMaterialMeshNoGlow;

		[Tooltip("The texture to use for the lightning bolts, or null for the material default texture.")]
		public Texture2D LightningTexture;

		[Tooltip("The texture to use for the lightning glow, or null for the material default texture.")]
		public Texture2D LightningGlowTexture;

		[Tooltip("Particle system to play at the point of emission (start). 'Emission rate' particles will be emitted all at once.")]
		public ParticleSystem LightningOriginParticleSystem;

		[Tooltip("Particle system to play at the point of impact (end). 'Emission rate' particles will be emitted all at once.")]
		public ParticleSystem LightningDestinationParticleSystem;

		[Tooltip("Tint color for the lightning")]
		public Color LightningTintColor = Color.white;

		[Tooltip("Tint color for the lightning glow")]
		public Color GlowTintColor = new Color(0.1f, 0.2f, 1f, 1f);

		[Tooltip("Source blend mode. Default is SrcAlpha.")]
		public BlendMode SourceBlendMode = BlendMode.SrcAlpha;

		[Tooltip("Destination blend mode. Default is One. For additive blend use One. For alpha blend use OneMinusSrcAlpha.")]
		public BlendMode DestinationBlendMode = BlendMode.One;

		[Header("Lightning Movement Properties")]
		[Tooltip("Jitter multiplier to randomize lightning size. Jitter depends on trunk width and will make the lightning move rapidly and jaggedly, giving a more lively and sometimes cartoony feel. Jitter may be shared with other bolts depending on materials. If you need different jitters for the same material, create a second script object.")]
		public float JitterMultiplier;

		[Tooltip("Built in turbulance based on the direction of each segment. Small values usually work better, like 0.2.")]
		public float Turbulence;

		[Tooltip("Global turbulence velocity for this script")]
		public Vector3 TurbulenceVelocity = Vector3.zero;

		[Tooltip("Cause lightning to flicker, x = min, y = max, z = time multiplier, w = add to intensity")]
		public Vector4 IntensityFlicker = LightningBoltScript.intensityFlickerDefault;

		private static readonly Vector4 intensityFlickerDefault = new Vector4(1f, 1f, 1f, 0f);

		[Tooltip("Lightning intensity flicker lookup texture")]
		public Texture2D IntensityFlickerTexture;

		private Texture2D lastLightningTexture;

		private Texture2D lastLightningGlowTexture;

		private readonly List<LightningBolt> activeBolts = new List<LightningBolt>();

		private readonly LightningBoltParameters[] oneParameterArray = new LightningBoltParameters[1];

		private readonly List<LightningBolt> lightningBoltCache = new List<LightningBolt>();

		private readonly List<LightningBoltDependencies> dependenciesCache = new List<LightningBoltDependencies>();

		private LightningThreadState threadState;

		private static int shaderId_MainTex = int.MinValue;

		private static int shaderId_GlowTex;

		private static int shaderId_TintColor;

		private static int shaderId_GlowTintColor;

		private static int shaderId_JitterMultiplier;

		private static int shaderId_Turbulence;

		private static int shaderId_TurbulenceVelocity;

		private static int shaderId_SrcBlendMode;

		private static int shaderId_DstBlendMode;

		private static int shaderId_InvFade;

		private static int shaderId_LightningTime;

		private static int shaderId_IntensityFlicker;

		private static int shaderId_IntensityFlickerTexture;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace DigitalRuby.ThunderAndLightning
{
	public class LightningBolt
	{
		public float MinimumDelay { get; private set; }

		public bool HasGlow { get; private set; }

		public bool IsActive
		{
			get
			{
				return this.elapsedTime < this.lifeTime;
			}
		}

		public CameraMode CameraMode { get; private set; }

		public void SetupLightningBolt(LightningBoltDependencies dependencies)
		{
			if (dependencies == null || dependencies.Parameters.Count == 0)
			{
				UnityEngine.Debug.LogError("Lightning bolt dependencies must not be null");
				return;
			}
			if (this.dependencies != null)
			{
				UnityEngine.Debug.LogError("This lightning bolt is already in use!");
				return;
			}
			this.dependencies = dependencies;
			this.CameraMode = dependencies.CameraMode;
			this.timeSinceLevelLoad = Time.timeSinceLevelLoad;
			this.CheckForGlow(dependencies.Parameters);
			this.MinimumDelay = float.MaxValue;
			if (dependencies.ThreadState != null)
			{
				this.startTimeOffset = DateTime.UtcNow;
				dependencies.ThreadState.AddActionForBackgroundThread(new Action(this.ProcessAllLightningParameters));
			}
			else
			{
				this.ProcessAllLightningParameters();
			}
		}

		public bool Update()
		{
			this.elapsedTime += Time.deltaTime;
			if (this.elapsedTime > this.maxLifeTime)
			{
				return false;
			}
			if (this.hasLight)
			{
				this.UpdateLights();
			}
			return true;
		}

		public void Cleanup()
		{
			foreach (LightningBoltSegmentGroup lightningBoltSegmentGroup in this.segmentGroupsWithLight)
			{
				foreach (Light l in lightningBoltSegmentGroup.Lights)
				{
					this.CleanupLight(l);
				}
				lightningBoltSegmentGroup.Lights.Clear();
			}
			object obj = LightningBolt.groupCache;
			lock (obj)
			{
				foreach (LightningBoltSegmentGroup item in this.segmentGroups)
				{
					LightningBolt.groupCache.Add(item);
				}
			}
			this.hasLight = false;
			this.elapsedTime = 0f;
			this.lifeTime = 0f;
			this.maxLifeTime = 0f;
			if (this.dependencies != null)
			{
				this.dependencies.ReturnToCache(this.dependencies);
				this.dependencies = null;
			}
			foreach (LightningBolt.LineRendererMesh lineRendererMesh in this.activeLineRenderers)
			{
				if (lineRendererMesh != null)
				{
					lineRendererMesh.Reset();
					LightningBolt.lineRendererCache.Add(lineRendererMesh);
				}
			}
			this.segmentGroups.Clear();
			this.segmentGroupsWithLight.Clear();
			this.activeLineRenderers.Clear();
		}

		public LightningBoltSegmentGroup AddGroup()
		{
			object obj = LightningBolt.groupCache;
			LightningBoltSegmentGroup lightningBoltSegmentGroup;
			lock (obj)
			{
				if (LightningBolt.groupCache.Count == 0)
				{
					lightningBoltSegmentGroup = new LightningBoltSegmentGroup();
				}
				else
				{
					int index = LightningBolt.groupCache.Count - 1;
					lightningBoltSegmentGroup = LightningBolt.groupCache[index];
					lightningBoltSegmentGroup.Reset();
					LightningBolt.groupCache.RemoveAt(index);
				}
			}
			this.segmentGroups.Add(lightningBoltSegmentGroup);
			return lightningBoltSegmentGroup;
		}

		public static void ClearCache()
		{
			foreach (LightningBolt.LineRendererMesh lineRendererMesh in LightningBolt.lineRendererCache)
			{
				if (lineRendererMesh != null)
				{
					UnityEngine.Object.Destroy(lineRendererMesh.GameObject);
				}
			}
			foreach (Light light in LightningBolt.lightCache)
			{
				if (light != null)
				{
					UnityEngine.Object.Destroy(light.gameObject);
				}
			}
			LightningBolt.lineRendererCache.Clear();
			LightningBolt.lightCache.Clear();
			object obj = LightningBolt.groupCache;
			lock (obj)
			{
				LightningBolt.groupCache.Clear();
			}
		}

		private void CleanupLight(Light l)
		{
			if (l != null)
			{
				this.dependencies.LightRemoved(l);
				LightningBolt.lightCache.Add(l);
				l.gameObject.SetActive(false);
				LightningBolt.lightCount--;
			}
		}

		private void EnableLineRenderer(LightningBolt.LineRendererMesh lineRenderer, int tag)
		{
			bool flag = lineRenderer != null && lineRenderer.GameObject != null && lineRenderer.Tag == tag && this.IsActive;
			if (flag)
			{
				lineRenderer.PopulateMesh();
			}
		}

		private IEnumerator EnableLastRendererCoRoutine()
		{
			LightningBolt.LineRendererMesh lineRenderer = this.activeLineRenderers[this.activeLineRenderers.Count - 1];
			int tag = ++lineRenderer.Tag;
			yield return new WaitForSeconds(this.MinimumDelay);
			this.EnableLineRenderer(lineRenderer, tag);
			yield break;
		}

		private LightningBolt.LineRendererMesh GetOrCreateLineRenderer()
		{
            LineRendererMesh lineRendererMesh;
            do
            {
                if (lineRendererCache.Count == 0)
                {
                    lineRendererMesh = new LineRendererMesh();
                    break;
                }
                int index = lineRendererCache.Count - 1;
                lineRendererMesh = lineRendererCache[index];
                lineRendererCache.RemoveAt(index);
            }
            while (lineRendererMesh == null || lineRendererMesh.Transform == null);
            lineRendererMesh.Transform.parent = null;
            lineRendererMesh.Transform.rotation = Quaternion.identity;
            lineRendererMesh.Transform.localScale = Vector3.one;
            lineRendererMesh.Transform.parent = dependencies.Parent.transform;
            lineRendererMesh.GameObject.layer = dependencies.Parent.layer;
            if (dependencies.UseWorldSpace)
            {
                lineRendererMesh.GameObject.transform.position = Vector3.zero;
            }
            else
            {
                lineRendererMesh.GameObject.transform.localPosition = Vector3.zero;
            }
            lineRendererMesh.Material = ((!HasGlow) ? dependencies.LightningMaterialMeshNoGlow : dependencies.LightningMaterialMesh);
            if (!string.IsNullOrEmpty(dependencies.SortLayerName))
            {
                lineRendererMesh.MeshRenderer.sortingLayerName = dependencies.SortLayerName;
                lineRendererMesh.MeshRenderer.sortingOrder = dependencies.SortOrderInLayer;
            }
            else
            {
                lineRendererMesh.MeshRenderer.sortingLayerName = null;
                lineRendererMesh.MeshRenderer.sortingOrder = 0;
            }
            activeLineRenderers.Add(lineRendererMesh);
            return lineRendererMesh;
        }

		private void RenderGroup(LightningBoltSegmentGroup group, LightningBoltParameters p)
		{
			if (group.SegmentCount == 0)
			{
				return;
			}
			float num = (this.dependencies.ThreadState != null) ? ((float)(DateTime.UtcNow - this.startTimeOffset).TotalSeconds) : 0f;
			float num2 = this.timeSinceLevelLoad + group.Delay + num;
			Vector4 fadeLifeTime = new Vector4(num2, num2 + group.PeakStart, num2 + group.PeakEnd, num2 + group.LifeTime);
			float num3 = group.LineWidth * 0.5f * LightningBoltParameters.Scale;
			int num4 = group.Segments.Count - group.StartIndex;
			float num5 = (num3 - num3 * group.EndWidthMultiplier) / (float)num4;
			float num6;
			if (p.GrowthMultiplier > 0f)
			{
				num6 = group.LifeTime / (float)num4 * p.GrowthMultiplier;
				num = 0f;
			}
			else
			{
				num6 = 0f;
				num = 0f;
			}
			LightningBolt.LineRendererMesh currentLineRenderer = (this.activeLineRenderers.Count != 0) ? this.activeLineRenderers[this.activeLineRenderers.Count - 1] : this.GetOrCreateLineRenderer();
			if (!currentLineRenderer.PrepareForLines(num4))
			{
				if (currentLineRenderer.CustomTransform != null)
				{
					return;
				}
				if (this.dependencies.ThreadState != null)
				{
					this.dependencies.ThreadState.AddActionForMainThread(delegate
					{
						this.EnableCurrentLineRenderer();
						currentLineRenderer = this.GetOrCreateLineRenderer();
					}, true);
				}
				else
				{
					this.EnableCurrentLineRenderer();
					currentLineRenderer = this.GetOrCreateLineRenderer();
				}
			}
			currentLineRenderer.BeginLine(group.Segments[group.StartIndex].Start, group.Segments[group.StartIndex].End, num3, group.Color, p.Intensity, fadeLifeTime, p.GlowWidthMultiplier, p.GlowIntensity);
			for (int i = group.StartIndex + 1; i < group.Segments.Count; i++)
			{
				num3 -= num5;
				if (p.GrowthMultiplier < 1f)
				{
					num += num6;
					fadeLifeTime = new Vector4(num2 + num, num2 + group.PeakStart + num, num2 + group.PeakEnd, num2 + group.LifeTime);
				}
				currentLineRenderer.AppendLine(group.Segments[i].Start, group.Segments[i].End, num3, group.Color, p.Intensity, fadeLifeTime, p.GlowWidthMultiplier, p.GlowIntensity);
			}
		}

		private static IEnumerator NotifyBolt(LightningBoltDependencies dependencies, LightningBoltParameters p, Transform transform, Vector3 start, Vector3 end)
		{
			float delay = p.delaySeconds;
			float lifeTime = p.LifeTime;
			yield return new WaitForSeconds(delay);
			if (dependencies.LightningBoltStarted != null)
			{
				dependencies.LightningBoltStarted(p, start, end);
			}
			LightningCustomTransformStateInfo state = (p.CustomTransform != null) ? LightningCustomTransformStateInfo.GetOrCreateStateInfo() : null;
			if (state != null)
			{
				state.BoltStartPosition = start;
				state.BoltEndPosition = end;
				state.State = LightningCustomTransformState.Started;
				state.Transform = transform;
				p.CustomTransform(state);
				state.State = LightningCustomTransformState.Executing;
			}
			if (p.CustomTransform == null)
			{
				yield return new WaitForSeconds(lifeTime);
			}
			else
			{
				while (lifeTime > 0f)
				{
					p.CustomTransform(state);
					lifeTime -= Time.deltaTime;
					yield return null;
				}
			}
			if (p.CustomTransform != null)
			{
				state.State = LightningCustomTransformState.Ended;
				p.CustomTransform(state);
				LightningCustomTransformStateInfo.ReturnStateInfoToCache(state);
			}
			if (dependencies.LightningBoltEnded != null)
			{
				dependencies.LightningBoltEnded(p, start, end);
			}
			LightningBoltParameters.ReturnParametersToCache(p);
			yield break;
		}

		private void ProcessParameters(LightningBoltParameters p, RangeOfFloats delay)
		{
			this.MinimumDelay = Mathf.Min(delay.Minimum, this.MinimumDelay);
			p.delaySeconds = delay.Random(p.Random);
			p.generationWhereForksStop = p.Generations - p.GenerationWhereForksStopSubtractor;
			this.lifeTime = Mathf.Max(p.LifeTime + p.delaySeconds, this.lifeTime);
			this.maxLifeTime = Mathf.Max(this.lifeTime, this.maxLifeTime);
			p.forkednessCalculated = (int)Mathf.Ceil(p.Forkedness * (float)p.Generations);
			if (p.Generations > 0)
			{
				p.Generator = (p.Generator ?? LightningGenerator.GeneratorInstance);
				Vector3 start;
				Vector3 end;
				p.Generator.GenerateLightningBolt(this, p, out start, out end);
				p.Start = start;
				p.End = end;
			}
		}

		private void ProcessAllLightningParameters()
		{
            int maxLights = MaximumLightsPerBatch / dependencies.Parameters.Count;
            RangeOfFloats delay = default(RangeOfFloats);
            List<int> list = new List<int>(dependencies.Parameters.Count + 1);
            int num = 0;
            foreach (LightningBoltParameters parameter in dependencies.Parameters)
            {
                delay.Minimum = parameter.DelayRange.Minimum + parameter.Delay;
                delay.Maximum = parameter.DelayRange.Maximum + parameter.Delay;
                parameter.maxLights = maxLights;
                list.Add(segmentGroups.Count);
                ProcessParameters(parameter, delay);
            }
            list.Add(segmentGroups.Count);
            LightningBoltDependencies dependenciesRef = dependencies;
            foreach (LightningBoltParameters parameters in dependenciesRef.Parameters)
            {
                Transform transform = RenderLightningBolt(parameters.quality, parameters.Generations, list[num], list[++num], parameters);
                if (dependenciesRef.ThreadState != null)
                {
                    dependenciesRef.ThreadState.AddActionForMainThread(delegate
                    {
                        dependenciesRef.StartCoroutine(NotifyBolt(dependenciesRef, parameters, transform, parameters.Start, parameters.End));
                    });
                }
                else
                {
                    dependenciesRef.StartCoroutine(NotifyBolt(dependenciesRef, parameters, transform, parameters.Start, parameters.End));
                }
            }
            if (dependencies.ThreadState != null)
            {
                dependencies.ThreadState.AddActionForMainThread(EnableCurrentLineRendererFromThread);
                return;
            }
            EnableCurrentLineRenderer();
            dependencies.AddActiveBolt(this);
        }

		private void EnableCurrentLineRendererFromThread()
		{
			this.EnableCurrentLineRenderer();
			this.dependencies.ThreadState = null;
			this.dependencies.AddActiveBolt(this);
		}

		private void EnableCurrentLineRenderer()
		{
			if (this.activeLineRenderers.Count == 0)
			{
				return;
			}
			if (this.MinimumDelay <= 0f)
			{
				this.EnableLineRenderer(this.activeLineRenderers[this.activeLineRenderers.Count - 1], this.activeLineRenderers[this.activeLineRenderers.Count - 1].Tag);
			}
			else
			{
				this.dependencies.StartCoroutine(this.EnableLastRendererCoRoutine());
			}
		}

		private void RenderParticleSystems(Vector3 start, Vector3 end, float trunkWidth, float lifeTime, float delaySeconds)
		{
			if (trunkWidth > 0f)
			{
				if (this.dependencies.OriginParticleSystem != null)
				{
					this.dependencies.StartCoroutine(this.GenerateParticleCoRoutine(this.dependencies.OriginParticleSystem, start, delaySeconds));
				}
				if (this.dependencies.DestParticleSystem != null)
				{
					this.dependencies.StartCoroutine(this.GenerateParticleCoRoutine(this.dependencies.DestParticleSystem, end, delaySeconds + lifeTime * 0.8f));
				}
			}
		}

		private Transform RenderLightningBolt(LightningBoltQualitySetting quality, int generations, int startGroupIndex, int endGroupIndex, LightningBoltParameters parameters)
		{
			if (this.segmentGroups.Count == 0 || startGroupIndex >= this.segmentGroups.Count || endGroupIndex > this.segmentGroups.Count)
			{
				return null;
			}
			Transform result = null;
			LightningLightParameters lp = parameters.LightParameters;
			if (lp != null)
			{
				if (this.hasLight |= lp.HasLight)
				{
					lp.LightPercent = Mathf.Clamp(lp.LightPercent, Mathf.Epsilon, 1f);
					lp.LightShadowPercent = Mathf.Clamp(lp.LightShadowPercent, 0f, 1f);
				}
				else
				{
					lp = null;
				}
			}
			LightningBoltSegmentGroup lightningBoltSegmentGroup = this.segmentGroups[startGroupIndex];
			Vector3 start = lightningBoltSegmentGroup.Segments[lightningBoltSegmentGroup.StartIndex].Start;
			Vector3 end = lightningBoltSegmentGroup.Segments[lightningBoltSegmentGroup.StartIndex + lightningBoltSegmentGroup.SegmentCount - 1].End;
			parameters.FadePercent = Mathf.Clamp(parameters.FadePercent, 0f, 0.5f);
			if (parameters.CustomTransform != null)
			{
				LightningBolt.LineRendererMesh currentLineRenderer = (this.activeLineRenderers.Count != 0 && this.activeLineRenderers[this.activeLineRenderers.Count - 1].Empty) ? this.activeLineRenderers[this.activeLineRenderers.Count - 1] : null;
				if (currentLineRenderer == null)
				{
					if (this.dependencies.ThreadState != null)
					{
						this.dependencies.ThreadState.AddActionForMainThread(delegate
						{
							this.EnableCurrentLineRenderer();
							currentLineRenderer = this.GetOrCreateLineRenderer();
						}, true);
					}
					else
					{
						this.EnableCurrentLineRenderer();
						currentLineRenderer = this.GetOrCreateLineRenderer();
					}
				}
				if (currentLineRenderer == null)
				{
					return null;
				}
				currentLineRenderer.CustomTransform = parameters.CustomTransform;
				result = currentLineRenderer.Transform;
			}
			for (int i = startGroupIndex; i < endGroupIndex; i++)
			{
				LightningBoltSegmentGroup lightningBoltSegmentGroup2 = this.segmentGroups[i];
				lightningBoltSegmentGroup2.Delay = parameters.delaySeconds;
				lightningBoltSegmentGroup2.LifeTime = parameters.LifeTime;
				lightningBoltSegmentGroup2.PeakStart = lightningBoltSegmentGroup2.LifeTime * parameters.FadePercent;
				lightningBoltSegmentGroup2.PeakEnd = lightningBoltSegmentGroup2.LifeTime - lightningBoltSegmentGroup2.PeakStart;
				float num = lightningBoltSegmentGroup2.PeakEnd - lightningBoltSegmentGroup2.PeakStart;
				float num2 = lightningBoltSegmentGroup2.LifeTime - lightningBoltSegmentGroup2.PeakEnd;
				lightningBoltSegmentGroup2.PeakStart *= parameters.FadeInMultiplier;
				lightningBoltSegmentGroup2.PeakEnd = lightningBoltSegmentGroup2.PeakStart + num * parameters.FadeFullyLitMultiplier;
				lightningBoltSegmentGroup2.LifeTime = lightningBoltSegmentGroup2.PeakEnd + num2 * parameters.FadeOutMultiplier;
				lightningBoltSegmentGroup2.LightParameters = lp;
				this.RenderGroup(lightningBoltSegmentGroup2, parameters);
			}
			if (this.dependencies.ThreadState != null)
			{
				this.dependencies.ThreadState.AddActionForMainThread(delegate
				{
					this.RenderParticleSystems(start, end, parameters.TrunkWidth, parameters.LifeTime, parameters.delaySeconds);
					if (lp != null)
					{
						this.CreateLightsForGroup(this.segmentGroups[startGroupIndex], lp, quality, parameters.maxLights);
					}
				}, false);
			}
			else
			{
				this.RenderParticleSystems(start, end, parameters.TrunkWidth, parameters.LifeTime, parameters.delaySeconds);
				if (lp != null)
				{
					this.CreateLightsForGroup(this.segmentGroups[startGroupIndex], lp, quality, parameters.maxLights);
				}
			}
			return result;
		}

		private void CreateLightsForGroup(LightningBoltSegmentGroup group, LightningLightParameters lp, LightningBoltQualitySetting quality, int maxLights)
		{
			if (LightningBolt.lightCount == LightningBolt.MaximumLightCount || maxLights <= 0)
			{
				return;
			}
			float num = (this.lifeTime - group.PeakEnd) * lp.FadeOutMultiplier;
			float num2 = (group.PeakEnd - group.PeakStart) * lp.FadeFullyLitMultiplier;
			float num3 = group.PeakStart * lp.FadeInMultiplier;
			float num4 = num3 + num2;
			float num5 = num4 + num;
			this.maxLifeTime = Mathf.Max(this.maxLifeTime, group.Delay + num5);
			this.segmentGroupsWithLight.Add(group);
			int segmentCount = group.SegmentCount;
			float num6;
			float num7;
			if (quality == LightningBoltQualitySetting.LimitToQualitySetting)
			{
				int qualityLevel = QualitySettings.GetQualityLevel();
				LightningQualityMaximum lightningQualityMaximum;
				if (LightningBoltParameters.QualityMaximums.TryGetValue(qualityLevel, out lightningQualityMaximum))
				{
					num6 = Mathf.Min(lp.LightPercent, lightningQualityMaximum.MaximumLightPercent);
					num7 = Mathf.Min(lp.LightShadowPercent, lightningQualityMaximum.MaximumShadowPercent);
				}
				else
				{
					UnityEngine.Debug.LogError("Unable to read lightning quality for level " + qualityLevel.ToString());
					num6 = lp.LightPercent;
					num7 = lp.LightShadowPercent;
				}
			}
			else
			{
				num6 = lp.LightPercent;
				num7 = lp.LightShadowPercent;
			}
			maxLights = Mathf.Max(1, Mathf.Min(maxLights, (int)((float)segmentCount * num6)));
			int num8 = Mathf.Max(1, segmentCount / maxLights);
			int num9 = maxLights - (int)((float)maxLights * num7);
			int num10 = num9;
			for (int i = group.StartIndex + (int)((float)num8 * 0.5f); i < group.Segments.Count; i += num8)
			{
				if (this.AddLightToGroup(group, lp, i, num8, num9, ref maxLights, ref num10))
				{
					return;
				}
			}
		}

		private bool AddLightToGroup(LightningBoltSegmentGroup group, LightningLightParameters lp, int segmentIndex, int nthLight, int nthShadows, ref int maxLights, ref int nthShadowCounter)
		{
			Light orCreateLight = this.GetOrCreateLight(lp);
			group.Lights.Add(orCreateLight);
			Vector3 vector = (group.Segments[segmentIndex].Start + group.Segments[segmentIndex].End) * 0.5f;
			if (this.dependencies.CameraIsOrthographic)
			{
				if (this.dependencies.CameraMode == CameraMode.OrthographicXZ)
				{
					vector.y = this.dependencies.CameraPos.y + lp.OrthographicOffset;
				}
				else
				{
					vector.z = this.dependencies.CameraPos.z + lp.OrthographicOffset;
				}
			}
			if (this.dependencies.UseWorldSpace)
			{
				orCreateLight.gameObject.transform.position = vector;
			}
			else
			{
				orCreateLight.gameObject.transform.localPosition = vector;
			}
			if (lp.LightShadowPercent == 0f || ++nthShadowCounter < nthShadows)
			{
				orCreateLight.shadows = LightShadows.None;
			}
			else
			{
				orCreateLight.shadows = LightShadows.Soft;
				nthShadowCounter = 0;
			}
			return ++LightningBolt.lightCount == LightningBolt.MaximumLightCount || --maxLights == 0;
		}

		private Light GetOrCreateLight(LightningLightParameters lp)
		{
            Light light;
            do
            {
                if (lightCache.Count == 0)
                {
                    GameObject gameObject = new GameObject("LightningBoltLight");
                    light = gameObject.AddComponent<Light>();
                    light.type = LightType.Point;
                    break;
                }
                light = lightCache[lightCache.Count - 1];
                lightCache.RemoveAt(lightCache.Count - 1);
            }
            while (light == null);
            light.bounceIntensity = lp.BounceIntensity;
            light.shadowNormalBias = lp.ShadowNormalBias;
            light.color = lp.LightColor;
            light.renderMode = lp.RenderMode;
            light.range = lp.LightRange;
            light.shadowStrength = lp.ShadowStrength;
            light.shadowBias = lp.ShadowBias;
            light.intensity = 0f;
            light.gameObject.transform.parent = dependencies.Parent.transform;
            light.gameObject.SetActive(true);
            dependencies.LightAdded(light);
            return light;
        }

		private void UpdateLight(LightningLightParameters lp, IEnumerable<Light> lights, float delay, float peakStart, float peakEnd, float lifeTime)
		{
			if (this.elapsedTime < delay)
			{
				return;
			}
			float num = (lifeTime - peakEnd) * lp.FadeOutMultiplier;
			float num2 = (peakEnd - peakStart) * lp.FadeFullyLitMultiplier;
			peakStart *= lp.FadeInMultiplier;
			peakEnd = peakStart + num2;
			lifeTime = peakEnd + num;
			float num3 = this.elapsedTime - delay;
			if (num3 >= peakStart)
			{
				if (num3 <= peakEnd)
				{
					foreach (Light light in lights)
					{
						light.intensity = lp.LightIntensity;
					}
				}
				else
				{
					float t = (num3 - peakEnd) / (lifeTime - peakEnd);
					foreach (Light light2 in lights)
					{
						light2.intensity = Mathf.Lerp(lp.LightIntensity, 0f, t);
					}
				}
			}
			else
			{
				float t2 = num3 / peakStart;
				foreach (Light light3 in lights)
				{
					light3.intensity = Mathf.Lerp(0f, lp.LightIntensity, t2);
				}
			}
		}

		private void UpdateLights()
		{
			foreach (LightningBoltSegmentGroup lightningBoltSegmentGroup in this.segmentGroupsWithLight)
			{
				this.UpdateLight(lightningBoltSegmentGroup.LightParameters, lightningBoltSegmentGroup.Lights, lightningBoltSegmentGroup.Delay, lightningBoltSegmentGroup.PeakStart, lightningBoltSegmentGroup.PeakEnd, lightningBoltSegmentGroup.LifeTime);
			}
		}

		private IEnumerator GenerateParticleCoRoutine(ParticleSystem p, Vector3 pos, float delay)
		{
			yield return new WaitForSeconds(delay);
			p.transform.position = pos;
			if (p.emission.burstCount > 0)
			{
				ParticleSystem.Burst[] array = new ParticleSystem.Burst[p.emission.burstCount];
				p.emission.GetBursts(array);
				int count = UnityEngine.Random.Range((int)array[0].minCount, (int)(array[0].maxCount + 1));
				p.Emit(count);
			}
			else
			{
				ParticleSystem.MinMaxCurve rateOverTime = p.emission.rateOverTime;
				int count = (int)((rateOverTime.constantMax - rateOverTime.constantMin) * 0.5f);
				count = UnityEngine.Random.Range(count, count * 2);
				p.Emit(count);
			}
			yield break;
		}

		private void CheckForGlow(IEnumerable<LightningBoltParameters> parameters)
		{
			foreach (LightningBoltParameters lightningBoltParameters in parameters)
			{
				this.HasGlow = (lightningBoltParameters.GlowIntensity >= Mathf.Epsilon && lightningBoltParameters.GlowWidthMultiplier >= Mathf.Epsilon);
				if (this.HasGlow)
				{
					break;
				}
			}
		}

		public static int MaximumLightCount = 128;

		public static int MaximumLightsPerBatch = 8;

		private DateTime startTimeOffset;

		private LightningBoltDependencies dependencies;

		private float elapsedTime;

		private float lifeTime;

		private float maxLifeTime;

		private bool hasLight;

		private float timeSinceLevelLoad;

		private readonly List<LightningBoltSegmentGroup> segmentGroups = new List<LightningBoltSegmentGroup>();

		private readonly List<LightningBoltSegmentGroup> segmentGroupsWithLight = new List<LightningBoltSegmentGroup>();

		private readonly List<LightningBolt.LineRendererMesh> activeLineRenderers = new List<LightningBolt.LineRendererMesh>();

		private static int lightCount;

		private static readonly List<LightningBolt.LineRendererMesh> lineRendererCache = new List<LightningBolt.LineRendererMesh>();

		private static readonly List<LightningBoltSegmentGroup> groupCache = new List<LightningBoltSegmentGroup>();

		private static readonly List<Light> lightCache = new List<Light>();

		public class LineRendererMesh
		{
			public LineRendererMesh()
			{
				this.GameObject = new GameObject("LightningBoltMeshRenderer");
				this.GameObject.SetActive(false);
				this.mesh = new Mesh
				{
					name = "ProceduralLightningMesh"
				};
				this.mesh.MarkDynamic();
				this.meshFilter = this.GameObject.AddComponent<MeshFilter>();
				this.meshFilter.sharedMesh = this.mesh;
				this.meshRenderer = this.GameObject.AddComponent<MeshRenderer>();
				this.meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
				this.meshRenderer.reflectionProbeUsage = ReflectionProbeUsage.Off;
				this.meshRenderer.lightProbeUsage = LightProbeUsage.Off;
				this.meshRenderer.receiveShadows = false;
				this.Transform = this.GameObject.GetComponent<Transform>();
			}

			public GameObject GameObject { get; private set; }

			public Material Material
			{
				get
				{
					return this.meshRenderer.sharedMaterial;
				}
				set
				{
					this.meshRenderer.sharedMaterial = value;
				}
			}

			public MeshRenderer MeshRenderer
			{
				get
				{
					return this.meshRenderer;
				}
			}

			public int Tag { get; set; }

			public Action<LightningCustomTransformStateInfo> CustomTransform { get; set; }

			public Transform Transform { get; private set; }

			public bool Empty
			{
				get
				{
					return this.vertices.Count == 0;
				}
			}

			public void PopulateMesh()
			{
				if (this.vertices.Count == 0)
				{
					this.mesh.Clear();
				}
				else
				{
					this.PopulateMeshInternal();
				}
			}

			public bool PrepareForLines(int lineCount)
			{
				int num = lineCount * 4;
				return this.vertices.Count + num <= 64999;
			}

			public void BeginLine(Vector3 start, Vector3 end, float radius, Color32 color, float colorIntensity, Vector4 fadeLifeTime, float glowWidthModifier, float glowIntensity)
			{
				Vector4 vector = end - start;
				vector.w = radius;
				this.AppendLineInternal(ref start, ref end, ref vector, ref vector, ref vector, color, colorIntensity, ref fadeLifeTime, glowWidthModifier, glowIntensity);
			}

			public void AppendLine(Vector3 start, Vector3 end, float radius, Color32 color, float colorIntensity, Vector4 fadeLifeTime, float glowWidthModifier, float glowIntensity)
			{
				Vector4 vector = end - start;
				vector.w = radius;
				Vector4 vector2 = this.lineDirs[this.lineDirs.Count - 3];
				Vector4 vector3 = this.lineDirs[this.lineDirs.Count - 1];
				this.AppendLineInternal(ref start, ref end, ref vector, ref vector2, ref vector3, color, colorIntensity, ref fadeLifeTime, glowWidthModifier, glowIntensity);
			}

			public void Reset()
			{
				this.CustomTransform = null;
				this.Tag++;
				this.GameObject.SetActive(false);
				this.mesh.Clear();
				this.indices.Clear();
				this.vertices.Clear();
				this.colors.Clear();
				this.lineDirs.Clear();
				this.ends.Clear();
				this.texCoordsAndGlowModifiers.Clear();
				this.fadeLifetimes.Clear();
				this.currentBoundsMaxX = (this.currentBoundsMaxY = (this.currentBoundsMaxZ = -1147483648));
				this.currentBoundsMinX = (this.currentBoundsMinY = (this.currentBoundsMinZ = 1147483647));
			}

			private void PopulateMeshInternal()
			{
				this.GameObject.SetActive(true);
				this.mesh.SetVertices(this.vertices);
				this.mesh.SetTangents(this.lineDirs);
				this.mesh.SetColors(this.colors);
				this.mesh.SetUVs(0, this.texCoordsAndGlowModifiers);
				this.mesh.SetUVs(1, this.fadeLifetimes);
				this.mesh.SetNormals(this.ends);
				this.mesh.SetTriangles(this.indices, 0);
				Bounds bounds = default(Bounds);
				Vector3 b = new Vector3((float)(this.currentBoundsMinX - 2), (float)(this.currentBoundsMinY - 2), (float)(this.currentBoundsMinZ - 2));
				Vector3 a = new Vector3((float)(this.currentBoundsMaxX + 2), (float)(this.currentBoundsMaxY + 2), (float)(this.currentBoundsMaxZ + 2));
				bounds.center = (a + b) * 0.5f;
				bounds.size = (a - b) * 1.2f;
				this.mesh.bounds = bounds;
			}

			private void UpdateBounds(ref Vector3 point1, ref Vector3 point2)
			{
				int num = (int)point1.x - (int)point2.x;
				num &= num >> 31;
				int num2 = (int)point2.x + num;
				int num3 = (int)point1.x - num;
				num = this.currentBoundsMinX - num2;
				num &= num >> 31;
				this.currentBoundsMinX = num2 + num;
				num = this.currentBoundsMaxX - num3;
				num &= num >> 31;
				this.currentBoundsMaxX -= num;
				int num4 = (int)point1.y - (int)point2.y;
				num4 &= num4 >> 31;
				int num5 = (int)point2.y + num4;
				int num6 = (int)point1.y - num4;
				num4 = this.currentBoundsMinY - num5;
				num4 &= num4 >> 31;
				this.currentBoundsMinY = num5 + num4;
				num4 = this.currentBoundsMaxY - num6;
				num4 &= num4 >> 31;
				this.currentBoundsMaxY -= num4;
				int num7 = (int)point1.z - (int)point2.z;
				num7 &= num7 >> 31;
				int num8 = (int)point2.z + num7;
				int num9 = (int)point1.z - num7;
				num7 = this.currentBoundsMinZ - num8;
				num7 &= num7 >> 31;
				this.currentBoundsMinZ = num8 + num7;
				num7 = this.currentBoundsMaxZ - num9;
				num7 &= num7 >> 31;
				this.currentBoundsMaxZ -= num7;
			}

			private void AddIndices()
			{
				int count = this.vertices.Count;
				this.indices.Add(count++);
				this.indices.Add(count++);
				this.indices.Add(count);
				this.indices.Add(count--);
				this.indices.Add(count);
				this.indices.Add(count + 2);
			}

			private void AppendLineInternal(ref Vector3 start, ref Vector3 end, ref Vector4 dir, ref Vector4 dirPrev1, ref Vector4 dirPrev2, Color32 color, float colorIntensity, ref Vector4 fadeLifeTime, float glowWidthModifier, float glowIntensity)
			{
				this.AddIndices();
				color.a = (byte)Mathf.Lerp(0f, 255f, colorIntensity * 0.1f);
				Vector4 item = new Vector4(LightningBolt.LineRendererMesh.uv1.x, LightningBolt.LineRendererMesh.uv1.y, glowWidthModifier, glowIntensity);
				this.vertices.Add(start);
				this.lineDirs.Add(dirPrev1);
				this.colors.Add(color);
				this.ends.Add(dir);
				this.vertices.Add(end);
				this.lineDirs.Add(dir);
				this.colors.Add(color);
				this.ends.Add(dir);
				dir.w = -dir.w;
				this.vertices.Add(start);
				this.lineDirs.Add(dirPrev2);
				this.colors.Add(color);
				this.ends.Add(dir);
				this.vertices.Add(end);
				this.lineDirs.Add(dir);
				this.colors.Add(color);
				this.ends.Add(dir);
				this.texCoordsAndGlowModifiers.Add(item);
				item.x = LightningBolt.LineRendererMesh.uv2.x;
				item.y = LightningBolt.LineRendererMesh.uv2.y;
				this.texCoordsAndGlowModifiers.Add(item);
				item.x = LightningBolt.LineRendererMesh.uv3.x;
				item.y = LightningBolt.LineRendererMesh.uv3.y;
				this.texCoordsAndGlowModifiers.Add(item);
				item.x = LightningBolt.LineRendererMesh.uv4.x;
				item.y = LightningBolt.LineRendererMesh.uv4.y;
				this.texCoordsAndGlowModifiers.Add(item);
				this.fadeLifetimes.Add(fadeLifeTime);
				this.fadeLifetimes.Add(fadeLifeTime);
				this.fadeLifetimes.Add(fadeLifeTime);
				this.fadeLifetimes.Add(fadeLifeTime);
				this.UpdateBounds(ref start, ref end);
			}

			private static readonly Vector2 uv1 = new Vector2(0f, 0f);

			private static readonly Vector2 uv2 = new Vector2(1f, 0f);

			private static readonly Vector2 uv3 = new Vector2(0f, 1f);

			private static readonly Vector2 uv4 = new Vector2(1f, 1f);

			private readonly List<int> indices = new List<int>();

			private readonly List<Vector3> vertices = new List<Vector3>();

			private readonly List<Vector4> lineDirs = new List<Vector4>();

			private readonly List<Color32> colors = new List<Color32>();

			private readonly List<Vector3> ends = new List<Vector3>();

			private readonly List<Vector4> texCoordsAndGlowModifiers = new List<Vector4>();

			private readonly List<Vector4> fadeLifetimes = new List<Vector4>();

			private const int boundsPadder = 1000000000;

			private int currentBoundsMinX = 1147483647;

			private int currentBoundsMinY = 1147483647;

			private int currentBoundsMinZ = 1147483647;

			private int currentBoundsMaxX = -1147483648;

			private int currentBoundsMaxY = -1147483648;

			private int currentBoundsMaxZ = -1147483648;

			private Mesh mesh;

			private MeshFilter meshFilter;

			private MeshRenderer meshRenderer;
		}
	}
}

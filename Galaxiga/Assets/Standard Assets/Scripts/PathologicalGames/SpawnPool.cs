using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathologicalGames
{
	[AddComponentMenu("Path-o-logical/PoolManager/SpawnPool")]
	public sealed class SpawnPool : MonoBehaviour, IList<Transform>, ICollection<Transform>, IEnumerable<Transform>, IEnumerable
	{
		public bool dontDestroyOnLoad
		{
			get
			{
				return this._dontDestroyOnLoad;
			}
			set
			{
				this._dontDestroyOnLoad = value;
				if (this.group != null)
				{
					UnityEngine.Object.DontDestroyOnLoad(this.group.gameObject);
				}
			}
		}

		public Transform group { get; private set; }

		public Dictionary<string, PrefabPool> prefabPools
		{
			get
			{
				Dictionary<string, PrefabPool> dictionary = new Dictionary<string, PrefabPool>();
				for (int i = 0; i < this._prefabPools.Count; i++)
				{
					dictionary[this._prefabPools[i].prefabGO.name] = this._prefabPools[i];
				}
				return dictionary;
			}
		}

		private void Awake()
		{
			if (this._dontDestroyOnLoad)
			{
				UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			}
			this.group = base.transform;
			if (this.poolName == string.Empty)
			{
				this.poolName = this.group.name.Replace("Pool", string.Empty);
				this.poolName = this.poolName.Replace("(Clone)", string.Empty);
			}
			if (this.logMessages)
			{
				UnityEngine.Debug.Log(string.Format("SpawnPool {0}: Initializing..", this.poolName));
			}
			for (int i = 0; i < this._perPrefabPoolOptions.Count; i++)
			{
				if (this._perPrefabPoolOptions[i].prefab == null)
				{
					UnityEngine.Debug.LogWarning(string.Format("Initialization Warning: Pool '{0}' contains a PrefabPool with no prefab reference. Skipping.", this.poolName));
				}
				else
				{
					this._perPrefabPoolOptions[i].inspectorInstanceConstructor();
					this.CreatePrefabPool(this._perPrefabPoolOptions[i]);
				}
			}
			PoolManager.Pools.Add(this);
		}

		internal GameObject InstantiatePrefab(GameObject prefab, Vector3 pos, Quaternion rot)
		{
			if (this.instantiateDelegates != null)
			{
				return this.instantiateDelegates(prefab, pos, rot);
			}
			return InstanceHandler.InstantiatePrefab(prefab, pos, rot);
		}

		internal void DestroyInstance(GameObject instance)
		{
			if (this.destroyDelegates != null)
			{
				this.destroyDelegates(instance);
			}
			else
			{
				InstanceHandler.DestroyInstance(instance);
			}
		}

		private void OnDestroy()
		{
			if (this.logMessages)
			{
				UnityEngine.Debug.Log(string.Format("SpawnPool {0}: Destroying...", this.poolName));
			}
			if (PoolManager.Pools.ContainsValue(this))
			{
				PoolManager.Pools.Remove(this);
			}
			base.StopAllCoroutines();
			this._spawned.Clear();
			foreach (PrefabPool prefabPool in this._prefabPools)
			{
				prefabPool.SelfDestruct();
			}
			this._prefabPools.Clear();
			this.prefabs._Clear();
		}

		public void CreatePrefabPool(PrefabPool prefabPool)
		{
			bool prefabPool2 = this.GetPrefabPool(prefabPool.prefab) != null;
			if (prefabPool2)
			{
				throw new Exception(string.Format("Prefab '{0}' is already in  SpawnPool '{1}'. Prefabs can be in more than 1 SpawnPool but cannot be in the same SpawnPool twice.", prefabPool.prefab, this.poolName));
			}
			prefabPool.spawnPool = this;
			this._prefabPools.Add(prefabPool);
			this.prefabs._Add(prefabPool.prefab.name, prefabPool.prefab);
			if (!prefabPool.preloaded)
			{
				if (this.logMessages)
				{
					UnityEngine.Debug.Log(string.Format("SpawnPool {0}: Preloading {1} {2}", this.poolName, prefabPool.preloadAmount, prefabPool.prefab.name));
				}
				prefabPool.PreloadInstances();
			}
		}

		public void Add(Transform instance, string prefabName, bool despawn, bool parent)
		{
			for (int i = 0; i < this._prefabPools.Count; i++)
			{
				if (this._prefabPools[i].prefabGO == null)
				{
					UnityEngine.Debug.LogError("Unexpected Error: PrefabPool.prefabGO is null");
					return;
				}
				if (this._prefabPools[i].prefabGO.name == prefabName)
				{
					this._prefabPools[i].AddUnpooled(instance, despawn);
					if (this.logMessages)
					{
						UnityEngine.Debug.Log(string.Format("SpawnPool {0}: Adding previously unpooled instance {1}", this.poolName, instance.name));
					}
					if (parent)
					{
						bool worldPositionStays = !(instance is RectTransform);
						instance.SetParent(this.group, worldPositionStays);
					}
					if (!despawn)
					{
						this._spawned.Add(instance);
					}
					return;
				}
			}
			UnityEngine.Debug.LogError(string.Format("SpawnPool {0}: PrefabPool {1} not found.", this.poolName, prefabName));
		}

		public void Add(Transform item)
		{
			string message = "Use SpawnPool.Spawn() to properly add items to the pool.";
			throw new NotImplementedException(message);
		}

		public void Remove(Transform item)
		{
			string message = "Use Despawn() to properly manage items that should remain in the pool but be deactivated.";
			throw new NotImplementedException(message);
		}

		public Transform Spawn(Transform prefab, Vector3 pos, Quaternion rot, Transform parent)
		{
			int i = 0;
			Transform transform;
			bool worldPositionStays;
			while (i < this._prefabPools.Count)
			{
				if (this._prefabPools[i].prefabGO == prefab.gameObject)
				{
					transform = this._prefabPools[i].SpawnInstance(pos, rot);
					if (transform == null)
					{
						return null;
					}
					worldPositionStays = !(transform is RectTransform);
					if (parent != null)
					{
						transform.SetParent(parent, worldPositionStays);
					}
					else if (!this.dontReparent && transform.parent != this.group)
					{
						transform.SetParent(this.group, worldPositionStays);
					}
					this._spawned.Add(transform);
					transform.gameObject.BroadcastMessage("OnSpawned", this, SendMessageOptions.DontRequireReceiver);
					return transform;
				}
				else
				{
					i++;
				}
			}
			PrefabPool prefabPool = new PrefabPool(prefab);
			this.CreatePrefabPool(prefabPool);
			transform = prefabPool.SpawnInstance(pos, rot);
			worldPositionStays = !(transform is RectTransform);
			if (parent != null)
			{
				transform.SetParent(parent, worldPositionStays);
			}
			else if (!this.dontReparent && transform.parent != this.group)
			{
				transform.SetParent(this.group, worldPositionStays);
			}
			this._spawned.Add(transform);
			transform.gameObject.BroadcastMessage("OnSpawned", this, SendMessageOptions.DontRequireReceiver);
			return transform;
		}

		public Transform Spawn(Transform prefab, Vector3 pos, Quaternion rot)
		{
			Transform transform = this.Spawn(prefab, pos, rot, null);
			if (transform == null)
			{
				return null;
			}
			return transform;
		}

		public Transform Spawn(Transform prefab)
		{
			return this.Spawn(prefab, Vector3.zero, Quaternion.identity);
		}

		public Transform Spawn(Transform prefab, Transform parent)
		{
			return this.Spawn(prefab, Vector3.zero, Quaternion.identity, parent);
		}

		public Transform Spawn(GameObject prefab, Vector3 pos, Quaternion rot, Transform parent)
		{
			return this.Spawn(prefab.transform, pos, rot, parent);
		}

		public Transform Spawn(GameObject prefab, Vector3 pos, Quaternion rot)
		{
			return this.Spawn(prefab.transform, pos, rot);
		}

		public Transform Spawn(GameObject prefab)
		{
			return this.Spawn(prefab.transform);
		}

		public Transform Spawn(GameObject prefab, Transform parent)
		{
			return this.Spawn(prefab.transform, parent);
		}

		public Transform Spawn(string prefabName)
		{
			Transform prefab = this.prefabs[prefabName];
			return this.Spawn(prefab);
		}

		public Transform Spawn(string prefabName, Transform parent)
		{
			Transform prefab = this.prefabs[prefabName];
			return this.Spawn(prefab, parent);
		}

		public Transform Spawn(string prefabName, Vector3 pos, Quaternion rot)
		{
			Transform prefab = this.prefabs[prefabName];
			return this.Spawn(prefab, pos, rot);
		}

		public Transform Spawn(string prefabName, Vector3 pos, Quaternion rot, Transform parent)
		{
			Transform prefab = this.prefabs[prefabName];
			return this.Spawn(prefab, pos, rot, parent);
		}

		public AudioSource Spawn(AudioSource prefab, Vector3 pos, Quaternion rot)
		{
			return this.Spawn(prefab, pos, rot, null);
		}

		public AudioSource Spawn(AudioSource prefab)
		{
			return this.Spawn(prefab, Vector3.zero, Quaternion.identity, null);
		}

		public AudioSource Spawn(AudioSource prefab, Transform parent)
		{
			return this.Spawn(prefab, Vector3.zero, Quaternion.identity, parent);
		}

		public AudioSource Spawn(AudioSource prefab, Vector3 pos, Quaternion rot, Transform parent)
		{
			Transform transform = this.Spawn(prefab.transform, pos, rot, parent);
			if (transform == null)
			{
				return null;
			}
			AudioSource component = transform.GetComponent<AudioSource>();
			component.Play();
			base.StartCoroutine(this.ListForAudioStop(component));
			return component;
		}

		public ParticleSystem Spawn(ParticleSystem prefab, Vector3 pos, Quaternion rot)
		{
			return this.Spawn(prefab, pos, rot, null);
		}

		public ParticleSystem Spawn(ParticleSystem prefab, Vector3 pos, Quaternion rot, Transform parent)
		{
			Transform transform = this.Spawn(prefab.transform, pos, rot, parent);
			if (transform == null)
			{
				return null;
			}
			ParticleSystem component = transform.GetComponent<ParticleSystem>();
			base.StartCoroutine(this.ListenForEmitDespawn(component));
			return component;
		}

		public void Despawn(Transform instance)
		{
			bool flag = false;
			for (int i = 0; i < this._prefabPools.Count; i++)
			{
				if (this._prefabPools[i]._spawned.Contains(instance))
				{
					flag = this._prefabPools[i].DespawnInstance(instance);
					break;
				}
				if (this._prefabPools[i]._despawned.Contains(instance))
				{
					UnityEngine.Debug.LogError(string.Format("SpawnPool {0}: {1} has already been despawned. You cannot despawn something more than once!", this.poolName, instance.name));
					return;
				}
			}
			if (!flag)
			{
				UnityEngine.Debug.LogError(string.Format("SpawnPool {0}: {1} not found in SpawnPool", this.poolName, instance.name));
				return;
			}
			this._spawned.Remove(instance);
		}

		public void Despawn(Transform instance, Transform parent)
		{
			bool worldPositionStays = !(instance is RectTransform);
			instance.SetParent(parent, worldPositionStays);
			this.Despawn(instance);
		}

		public void Despawn(Transform instance, float seconds)
		{
			base.StartCoroutine(this.DoDespawnAfterSeconds(instance, seconds, false, null));
		}

		public void Despawn(Transform instance, float seconds, Transform parent)
		{
			base.StartCoroutine(this.DoDespawnAfterSeconds(instance, seconds, true, parent));
		}

		private IEnumerator DoDespawnAfterSeconds(Transform instance, float seconds, bool useParent, Transform parent)
		{
			GameObject go = instance.gameObject;
			while (seconds > 0f)
			{
				yield return null;
				if (!go.activeInHierarchy)
				{
					yield break;
				}
				seconds -= Time.deltaTime;
			}
			if (useParent)
			{
				this.Despawn(instance, parent);
			}
			else
			{
				this.Despawn(instance);
			}
			yield break;
		}

		public void DespawnAll()
		{
			List<Transform> list = new List<Transform>(this._spawned);
			for (int i = 0; i < list.Count; i++)
			{
				this.Despawn(list[i]);
			}
		}

		public bool IsSpawned(Transform instance)
		{
			return this._spawned.Contains(instance);
		}

		public PrefabPool GetPrefabPool(Transform prefab)
		{
			for (int i = 0; i < this._prefabPools.Count; i++)
			{
				if (this._prefabPools[i].prefabGO == null)
				{
					UnityEngine.Debug.LogError(string.Format("SpawnPool {0}: PrefabPool.prefabGO is null", this.poolName));
				}
				if (this._prefabPools[i].prefabGO == prefab.gameObject)
				{
					return this._prefabPools[i];
				}
			}
			return null;
		}

		public PrefabPool GetPrefabPool(GameObject prefab)
		{
			for (int i = 0; i < this._prefabPools.Count; i++)
			{
				if (this._prefabPools[i].prefabGO == null)
				{
					UnityEngine.Debug.LogError(string.Format("SpawnPool {0}: PrefabPool.prefabGO is null", this.poolName));
				}
				if (this._prefabPools[i].prefabGO == prefab)
				{
					return this._prefabPools[i];
				}
			}
			return null;
		}

		public Transform GetPrefab(Transform instance)
		{
			for (int i = 0; i < this._prefabPools.Count; i++)
			{
				if (this._prefabPools[i].Contains(instance))
				{
					return this._prefabPools[i].prefab;
				}
			}
			return null;
		}

		public GameObject GetPrefab(GameObject instance)
		{
			for (int i = 0; i < this._prefabPools.Count; i++)
			{
				if (this._prefabPools[i].Contains(instance.transform))
				{
					return this._prefabPools[i].prefabGO;
				}
			}
			return null;
		}

		private IEnumerator ListForAudioStop(AudioSource src)
		{
			yield return null;
			GameObject srcGameObject = src.gameObject;
			while (src.isPlaying)
			{
				yield return null;
			}
			if (!srcGameObject.activeInHierarchy)
			{
				src.Stop();
				yield break;
			}
			this.Despawn(src.transform);
			yield break;
		}

		private IEnumerator ListenForEmitDespawn(ParticleSystem emitter)
		{
			yield return new WaitForSeconds(emitter.main.startDelay.constantMax + 0.25f);
			float safetimer = 0f;
			GameObject emitterGO = emitter.gameObject;
			while (emitter.IsAlive(true) && emitterGO.activeInHierarchy)
			{
				safetimer += Time.deltaTime;
				if (safetimer > this.maxParticleDespawnTime)
				{
					UnityEngine.Debug.LogWarning(string.Format("SpawnPool {0}: Timed out while listening for all particles to die. Waited for {1}sec.", this.poolName, this.maxParticleDespawnTime));
				}
				yield return null;
			}
			if (emitterGO.activeInHierarchy)
			{
				this.Despawn(emitter.transform);
				emitter.Clear(true);
			}
			yield break;
		}

		public override string ToString()
		{
			List<string> list = new List<string>();
			foreach (Transform transform in this._spawned)
			{
				list.Add(transform.name);
			}
			return string.Join(", ", list.ToArray());
		}

		public Transform this[int index]
		{
			get
			{
				return this._spawned[index];
			}
			set
			{
				throw new NotImplementedException("Read-only.");
			}
		}

		public bool Contains(Transform item)
		{
			string message = "Use IsSpawned(Transform instance) instead.";
			throw new NotImplementedException(message);
		}

		public void CopyTo(Transform[] array, int arrayIndex)
		{
			this._spawned.CopyTo(array, arrayIndex);
		}

		public int Count
		{
			get
			{
				return this._spawned.Count;
			}
		}

		public IEnumerator<Transform> GetEnumerator()
		{
			for (int i = 0; i < this._spawned.Count; i++)
			{
				yield return this._spawned[i];
			}
			yield break;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			for (int i = 0; i < this._spawned.Count; i++)
			{
				yield return this._spawned[i];
			}
			yield break;
		}

		public int IndexOf(Transform item)
		{
			throw new NotImplementedException();
		}

		public void Insert(int index, Transform item)
		{
			throw new NotImplementedException();
		}

		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		public void Clear()
		{
			throw new NotImplementedException();
		}

		public bool IsReadOnly
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		bool ICollection<Transform>.Remove(Transform item)
		{
			throw new NotImplementedException();
		}

		public string poolName = string.Empty;

		public bool matchPoolScale;

		public bool matchPoolLayer;

		public bool dontReparent;

		public bool _dontDestroyOnLoad;

		public bool logMessages;

		public List<PrefabPool> _perPrefabPoolOptions = new List<PrefabPool>();

		public Dictionary<object, bool> prefabsFoldOutStates = new Dictionary<object, bool>();

		public float maxParticleDespawnTime = 300f;

		public PrefabsDict prefabs = new PrefabsDict();

		public Dictionary<object, bool> _editorListItemStates = new Dictionary<object, bool>();

		private List<PrefabPool> _prefabPools = new List<PrefabPool>();

		internal List<Transform> _spawned = new List<Transform>();

		public SpawnPool.InstantiateDelegate instantiateDelegates;

		public SpawnPool.DestroyDelegate destroyDelegates;

		public delegate GameObject InstantiateDelegate(GameObject prefab, Vector3 pos, Quaternion rot);

		public delegate void DestroyDelegate(GameObject instance);
	}
}

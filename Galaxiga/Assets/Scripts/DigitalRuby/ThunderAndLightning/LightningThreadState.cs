using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace DigitalRuby.ThunderAndLightning
{
	public class LightningThreadState
	{
		public LightningThreadState()
		{
			this.lightningThread = new Thread(new ThreadStart(this.BackgroundThreadMethod))
			{
				IsBackground = true,
				Name = "LightningBoltScriptThread"
			};
			this.lightningThread.Start();
		}

		private bool UpdateMainThreadActionsOnce()
		{
			object obj = this.actionsForMainThread;
			KeyValuePair<Action, ManualResetEvent> keyValuePair;
			lock (obj)
			{
				if (this.actionsForMainThread.Count == 0)
				{
					return false;
				}
				keyValuePair = this.actionsForMainThread.Dequeue();
			}
			keyValuePair.Key();
			if (keyValuePair.Value != null)
			{
				keyValuePair.Value.Set();
			}
			return true;
		}

		private void BackgroundThreadMethod()
		{
			Action action = null;
			while (this.Running)
			{
				try
				{
					if (this.lightningThreadEvent.WaitOne(500))
					{
						for (;;)
						{
							object obj = this.actionsForBackgroundThread;
							lock (obj)
							{
								if (this.actionsForBackgroundThread.Count == 0)
								{
									break;
								}
								action = this.actionsForBackgroundThread.Dequeue();
							}
							action();
						}
					}
				}
				catch (ThreadAbortException)
				{
				}
				catch (Exception ex)
				{
					UnityEngine.Debug.LogErrorFormat("Lightning thread exception: {0}", new object[]
					{
						ex
					});
				}
			}
		}

		public void TerminateAndWaitForEnd()
		{
			this.isTerminating = true;
			for (;;)
			{
				if (!this.UpdateMainThreadActionsOnce())
				{
					object obj = this.actionsForBackgroundThread;
					lock (obj)
					{
						if (this.actionsForBackgroundThread.Count == 0)
						{
							break;
						}
					}
				}
			}
		}

		public void UpdateMainThreadActions()
		{
			while (this.UpdateMainThreadActionsOnce())
			{
			}
		}

		public bool AddActionForMainThread(Action action, bool waitForAction = false)
		{
			if (this.isTerminating)
			{
				return false;
			}
			ManualResetEvent manualResetEvent = (!waitForAction) ? null : new ManualResetEvent(false);
			object obj = this.actionsForMainThread;
			lock (obj)
			{
				this.actionsForMainThread.Enqueue(new KeyValuePair<Action, ManualResetEvent>(action, manualResetEvent));
			}
			if (manualResetEvent != null)
			{
				manualResetEvent.WaitOne(10000);
			}
			return true;
		}

		public bool AddActionForBackgroundThread(Action action)
		{
			if (this.isTerminating)
			{
				return false;
			}
			object obj = this.actionsForBackgroundThread;
			lock (obj)
			{
				this.actionsForBackgroundThread.Enqueue(action);
			}
			this.lightningThreadEvent.Set();
			return true;
		}

		private Thread lightningThread;

		private AutoResetEvent lightningThreadEvent = new AutoResetEvent(false);

		private readonly Queue<Action> actionsForBackgroundThread = new Queue<Action>();

		private readonly Queue<KeyValuePair<Action, ManualResetEvent>> actionsForMainThread = new Queue<KeyValuePair<Action, ManualResetEvent>>();

		public bool Running = true;

		private bool isTerminating;
	}
}

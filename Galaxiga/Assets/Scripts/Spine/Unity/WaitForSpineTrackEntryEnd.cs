using System;
using System.Collections;
using UnityEngine;

namespace Spine.Unity
{
	public class WaitForSpineTrackEntryEnd : IEnumerator
	{
		public WaitForSpineTrackEntryEnd(TrackEntry trackEntry)
		{
			this.SafeSubscribe(trackEntry);
		}

		private void HandleEnd(TrackEntry trackEntry)
		{
			this.m_WasFired = true;
		}

		private void SafeSubscribe(TrackEntry trackEntry)
		{
			if (trackEntry == null)
			{
				UnityEngine.Debug.LogWarning("TrackEntry was null. Coroutine will continue immediately.");
				this.m_WasFired = true;
			}
			else
			{
				trackEntry.End += this.HandleEnd;
			}
		}

		public WaitForSpineTrackEntryEnd NowWaitFor(TrackEntry trackEntry)
		{
			this.SafeSubscribe(trackEntry);
			return this;
		}

		bool IEnumerator.MoveNext()
		{
			if (this.m_WasFired)
			{
				((IEnumerator)this).Reset();
				return false;
			}
			return true;
		}

		void IEnumerator.Reset()
		{
			this.m_WasFired = false;
		}

		object IEnumerator.Current
		{
			get
			{
				return null;
			}
		}

		private bool m_WasFired;
	}
}

using System;
using System.Collections.Generic;
using System.Threading;
using Moments.Encoder;

namespace Moments
{
	internal sealed class Worker
	{
		internal Worker(int taskId, ThreadPriority priority, List<GifFrame> frames, GifEncoder encoder, string filepath, Action<int, float> onFileSaveProgress, Action<int, string> onFileSaved)
		{
			this.m_Id = taskId;
			this.m_Thread = new Thread(new ThreadStart(this.Run));
			this.m_Thread.Priority = priority;
			this.m_Frames = frames;
			this.m_Encoder = encoder;
			this.m_FilePath = filepath;
			this.m_OnFileSaved = onFileSaved;
			this.m_OnFileSaveProgress = onFileSaveProgress;
		}

		internal void Start()
		{
			this.m_Thread.Start();
		}

		private void Run()
		{
			this.m_Encoder.Start(this.m_FilePath);
			for (int i = 0; i < this.m_Frames.Count; i++)
			{
				GifFrame frame = this.m_Frames[i];
				this.m_Encoder.AddFrame(frame);
				if (this.m_OnFileSaveProgress != null)
				{
					float arg = (float)i / (float)this.m_Frames.Count;
					this.m_OnFileSaveProgress(this.m_Id, arg);
				}
			}
			this.m_Encoder.Finish();
			if (this.m_OnFileSaved != null)
			{
				this.m_OnFileSaved(this.m_Id, this.m_FilePath);
			}
		}

		private Thread m_Thread;

		private int m_Id;

		internal List<GifFrame> m_Frames;

		internal GifEncoder m_Encoder;

		internal string m_FilePath;

		internal Action<int, string> m_OnFileSaved;

		internal Action<int, float> m_OnFileSaveProgress;
	}
}

using System;
using System.IO;
using UnityEngine;

namespace Moments.Encoder
{
	public class GifEncoder
	{
		public GifEncoder() : this(-1, 10)
		{
		}

		public GifEncoder(int repeat, int quality)
		{
			if (repeat >= 0)
			{
				this.m_Repeat = repeat;
			}
			this.m_SampleInterval = Mathf.Clamp(quality, 1, 100);
		}

		public void SetDelay(int ms)
		{
			this.m_FrameDelay = Mathf.RoundToInt((float)ms / 10f);
		}

		public void SetFrameRate(float fps)
		{
			if (fps > 0f)
			{
				this.m_FrameDelay = Mathf.RoundToInt(100f / fps);
			}
		}

		public void AddFrame(GifFrame frame)
		{
			if (frame == null)
			{
				throw new ArgumentNullException("Can't add a null frame to the gif.");
			}
			if (!this.m_HasStarted)
			{
				throw new InvalidOperationException("Call Start() before adding frames to the gif.");
			}
			if (!this.m_IsSizeSet)
			{
				this.SetSize(frame.Width, frame.Height);
			}
			this.m_CurrentFrame = frame;
			this.GetImagePixels();
			this.AnalyzePixels();
			if (this.m_IsFirstFrame)
			{
				this.WriteLSD();
				this.WritePalette();
				if (this.m_Repeat >= 0)
				{
					this.WriteNetscapeExt();
				}
			}
			this.WriteGraphicCtrlExt();
			this.WriteImageDesc();
			if (!this.m_IsFirstFrame)
			{
				this.WritePalette();
			}
			this.WritePixels();
			this.m_IsFirstFrame = false;
		}

		public void Start(FileStream os)
		{
			if (os == null)
			{
				throw new ArgumentNullException("Stream is null.");
			}
			this.m_ShouldCloseStream = false;
			this.m_FileStream = os;
			try
			{
				this.WriteString("GIF89a");
			}
			catch (IOException ex)
			{
				throw ex;
			}
			this.m_HasStarted = true;
		}

		public void Start(string file)
		{
			try
			{
				this.m_FileStream = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
				this.Start(this.m_FileStream);
				this.m_ShouldCloseStream = true;
			}
			catch (IOException ex)
			{
				throw ex;
			}
		}

		public void Finish()
		{
			if (!this.m_HasStarted)
			{
				throw new InvalidOperationException("Can't finish a non-started gif.");
			}
			this.m_HasStarted = false;
			try
			{
				this.m_FileStream.WriteByte(59);
				this.m_FileStream.Flush();
				if (this.m_ShouldCloseStream)
				{
					this.m_FileStream.Close();
				}
			}
			catch (IOException ex)
			{
				throw ex;
			}
			this.m_FileStream = null;
			this.m_CurrentFrame = null;
			this.m_Pixels = null;
			this.m_IndexedPixels = null;
			this.m_ColorTab = null;
			this.m_ShouldCloseStream = false;
			this.m_IsFirstFrame = true;
		}

		protected void SetSize(int w, int h)
		{
			this.m_Width = w;
			this.m_Height = h;
			this.m_IsSizeSet = true;
		}

		protected void GetImagePixels()
		{
			this.m_Pixels = new byte[3 * this.m_CurrentFrame.Width * this.m_CurrentFrame.Height];
			Color32[] data = this.m_CurrentFrame.Data;
			int num = 0;
			for (int i = this.m_CurrentFrame.Height - 1; i >= 0; i--)
			{
				for (int j = 0; j < this.m_CurrentFrame.Width; j++)
				{
					Color32 color = data[i * this.m_CurrentFrame.Width + j];
					this.m_Pixels[num] = color.r;
					num++;
					this.m_Pixels[num] = color.g;
					num++;
					this.m_Pixels[num] = color.b;
					num++;
				}
			}
		}

		protected void AnalyzePixels()
		{
			int num = this.m_Pixels.Length;
			int num2 = num / 3;
			this.m_IndexedPixels = new byte[num2];
			NeuQuant neuQuant = new NeuQuant(this.m_Pixels, num, this.m_SampleInterval);
			this.m_ColorTab = neuQuant.Process();
			int num3 = 0;
			for (int i = 0; i < num2; i++)
			{
				int num4 = neuQuant.Map((int)(this.m_Pixels[num3++] & byte.MaxValue), (int)(this.m_Pixels[num3++] & byte.MaxValue), (int)(this.m_Pixels[num3++] & byte.MaxValue));
				this.m_UsedEntry[num4] = true;
				this.m_IndexedPixels[i] = (byte)num4;
			}
			this.m_Pixels = null;
			this.m_ColorDepth = 8;
			this.m_PaletteSize = 7;
		}

		protected void WriteGraphicCtrlExt()
		{
			this.m_FileStream.WriteByte(33);
			this.m_FileStream.WriteByte(249);
			this.m_FileStream.WriteByte(4);
			this.m_FileStream.WriteByte(Convert.ToByte(0));
			this.WriteShort(this.m_FrameDelay);
			this.m_FileStream.WriteByte(Convert.ToByte(0));
			this.m_FileStream.WriteByte(0);
		}

		protected void WriteImageDesc()
		{
			this.m_FileStream.WriteByte(44);
			this.WriteShort(0);
			this.WriteShort(0);
			this.WriteShort(this.m_Width);
			this.WriteShort(this.m_Height);
			if (this.m_IsFirstFrame)
			{
				this.m_FileStream.WriteByte(0);
			}
			else
			{
				this.m_FileStream.WriteByte(Convert.ToByte(128 | this.m_PaletteSize));
			}
		}

		protected void WriteLSD()
		{
			this.WriteShort(this.m_Width);
			this.WriteShort(this.m_Height);
			this.m_FileStream.WriteByte(Convert.ToByte(240 | this.m_PaletteSize));
			this.m_FileStream.WriteByte(0);
			this.m_FileStream.WriteByte(0);
		}

		protected void WriteNetscapeExt()
		{
			this.m_FileStream.WriteByte(33);
			this.m_FileStream.WriteByte(byte.MaxValue);
			this.m_FileStream.WriteByte(11);
			this.WriteString("NETSCAPE2.0");
			this.m_FileStream.WriteByte(3);
			this.m_FileStream.WriteByte(1);
			this.WriteShort(this.m_Repeat);
			this.m_FileStream.WriteByte(0);
		}

		protected void WritePalette()
		{
			this.m_FileStream.Write(this.m_ColorTab, 0, this.m_ColorTab.Length);
			int num = 768 - this.m_ColorTab.Length;
			for (int i = 0; i < num; i++)
			{
				this.m_FileStream.WriteByte(0);
			}
		}

		protected void WritePixels()
		{
			LzwEncoder lzwEncoder = new LzwEncoder(this.m_Width, this.m_Height, this.m_IndexedPixels, this.m_ColorDepth);
			lzwEncoder.Encode(this.m_FileStream);
		}

		protected void WriteShort(int value)
		{
			this.m_FileStream.WriteByte(Convert.ToByte(value & 255));
			this.m_FileStream.WriteByte(Convert.ToByte(value >> 8 & 255));
		}

		protected void WriteString(string s)
		{
			char[] array = s.ToCharArray();
			for (int i = 0; i < array.Length; i++)
			{
				this.m_FileStream.WriteByte((byte)array[i]);
			}
		}

		protected int m_Width;

		protected int m_Height;

		protected int m_Repeat = -1;

		protected int m_FrameDelay;

		protected bool m_HasStarted;

		protected FileStream m_FileStream;

		protected GifFrame m_CurrentFrame;

		protected byte[] m_Pixels;

		protected byte[] m_IndexedPixels;

		protected int m_ColorDepth;

		protected byte[] m_ColorTab;

		protected bool[] m_UsedEntry = new bool[256];

		protected int m_PaletteSize = 7;

		protected int m_DisposalCode = -1;

		protected bool m_ShouldCloseStream;

		protected bool m_IsFirstFrame = true;

		protected bool m_IsSizeSet;

		protected int m_SampleInterval = 10;
	}
}

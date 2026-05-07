using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using PlatformSupport.Collections.ObjectModel;
using UnityEngine;

namespace OSNet
{
	public class NetManager
	{
		public static NetManager Instance
		{
			get
			{
				if (NetManager._instance == null)
				{
					NetManager._instance = new NetManager();
				}
				return NetManager._instance;
			}
		}

		public static void Destroy()
		{
			
		}

		public void Start(string uri)
		{
			
		}

		public void LoadEvents()
		{
			
		}

		public void Send(string eventConst, CSMessage message, bool isImportant = false)
		{
			UnityEngine.Debug.LogWarning("<color=#0092cc>" + DateTime.Now + "</color>");
			UnityEngine.Debug.LogWarning("<color=#0092cc>" + message.GetType().ToString() + "</color>");
			UnityEngine.Debug.LogWarning("<color=#0092cc>" + JsonUtility.ToJson(message) + "</color>");
			if (!this.IsOnline() && !(message is CSInitSession))
			{
				UnityEngine.Debug.LogWarning("<color=#0092cc>FalconSDK: Not online</color>");
				return;
			}
			
		}

		private void WriteToFile()
		{
			byte[] byteArray = this.ObjectToByteArray(this.listMessageImportant);
			this.ByteArrayToFile(Application.dataPath + "/ImportantMessage", byteArray);
		}

		private byte[] ObjectToByteArray(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				binaryFormatter.Serialize(memoryStream, obj);
				result = memoryStream.ToArray();
			}
			return result;
		}

		private object ByteArrayToObject(byte[] arrBytes)
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			memoryStream.Write(arrBytes, 0, arrBytes.Length);
			memoryStream.Seek(0L, SeekOrigin.Begin);
			return binaryFormatter.Deserialize(memoryStream);
		}

		private byte[] FileToByteArray(string fileName)
		{
			return File.ReadAllBytes(fileName);
		}

		private bool ByteArrayToFile(string fileName, byte[] byteArray)
		{
			bool result;
			try
			{
				using (FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
				{
					fileStream.Write(byteArray, 0, byteArray.Length);
					result = true;
				}
			}
			catch (Exception arg)
			{
				Console.WriteLine("Exception caught in process: {0}", arg);
				result = false;
			}
			return result;
		}

		private void ReadFile()
		{
			if (File.Exists(Application.dataPath + "/ImportantMessage"))
			{
				byte[] array = this.FileToByteArray(Application.dataPath + "/ImportantMessage");
				if (array.Length > 0)
				{
					try
					{
						object obj = this.ByteArrayToObject(array);
						this.listMessageImportant = (List<CSMessage>)obj;
						this.tempListMessageImportant = this.listMessageImportant;
					}
					catch (Exception)
					{
						UnityEngine.Debug.Log("Error read file");
						throw;
					}
				}
			}
		}

		public void PushQueue()
		{
			while (this.queueMessages.Count > 0)
			{
				CSMessage csmessage = this.queueMessages.Dequeue();
				if (csmessage != null)
				{
					csmessage.Send(false);
				}
			}
			this.ReadFile();
			this.listMessageImportant.Clear();
			this.WriteToFile();
			UnityEngine.Debug.Log("push quere list message important : " + this.listMessageImportant.Count);
			while (this.tempListMessageImportant.Count > 0)
			{
				this.tempListMessageImportant[0].Send(false);
				this.tempListMessageImportant.RemoveAt(0);
			}
		}

		public void Restart()
		{
			
		}

		

	

		
		

		public void StartSession()
		{
			this.isSessionStarted = true;
		}

		public void StopSession()
		{
			this.isSessionStarted = false;
		}

		public bool IsOnline()
		{
			return this.isSessionStarted;
		}

		private Queue<CSMessage> queueMessages = new Queue<CSMessage>();

		private List<CSMessage> listMessageImportant = new List<CSMessage>();

		private List<CSMessage> tempListMessageImportant = new List<CSMessage>();

		private static NetManager _instance;


		private string uri = string.Empty;

		private bool isSessionStarted;
	}
}

using System;
using System.Collections.Generic;

namespace I2.Loc
{
	public class TranslationJob_Main : TranslationJob
	{
		public TranslationJob_Main(Dictionary<string, TranslationQuery> requests, Action<Dictionary<string, TranslationQuery>, string> OnTranslationReady)
		{
			this._requests = requests;
			this._OnTranslationReady = OnTranslationReady;
			this.mPost = new TranslationJob_POST(requests, OnTranslationReady);
		}

		public override TranslationJob.eJobState GetState()
		{
			if (this.mWeb != null)
			{
				TranslationJob.eJobState state = this.mWeb.GetState();
				if (state == TranslationJob.eJobState.Running)
				{
					return TranslationJob.eJobState.Running;
				}
				if (state != TranslationJob.eJobState.Succeeded)
				{
					if (state == TranslationJob.eJobState.Failed)
					{
						this.mWeb.Dispose();
						this.mWeb = null;
						this.mPost = new TranslationJob_POST(this._requests, this._OnTranslationReady);
					}
				}
				else
				{
					this.mJobState = TranslationJob.eJobState.Succeeded;
				}
			}
			if (this.mPost != null)
			{
				TranslationJob.eJobState state2 = this.mPost.GetState();
				if (state2 == TranslationJob.eJobState.Running)
				{
					return TranslationJob.eJobState.Running;
				}
				if (state2 != TranslationJob.eJobState.Succeeded)
				{
					if (state2 == TranslationJob.eJobState.Failed)
					{
						this.mPost.Dispose();
						this.mPost = null;
						this.mGet = new TranslationJob_GET(this._requests, this._OnTranslationReady);
					}
				}
				else
				{
					this.mJobState = TranslationJob.eJobState.Succeeded;
				}
			}
			if (this.mGet != null)
			{
				TranslationJob.eJobState state3 = this.mGet.GetState();
				if (state3 == TranslationJob.eJobState.Running)
				{
					return TranslationJob.eJobState.Running;
				}
				if (state3 != TranslationJob.eJobState.Succeeded)
				{
					if (state3 == TranslationJob.eJobState.Failed)
					{
						this.mErrorMessage = this.mGet.mErrorMessage;
						if (this._OnTranslationReady != null)
						{
							this._OnTranslationReady(this._requests, this.mErrorMessage);
						}
						this.mGet.Dispose();
						this.mGet = null;
					}
				}
				else
				{
					this.mJobState = TranslationJob.eJobState.Succeeded;
				}
			}
			return this.mJobState;
		}

		public override void Dispose()
		{
			if (this.mPost != null)
			{
				this.mPost.Dispose();
			}
			if (this.mGet != null)
			{
				this.mGet.Dispose();
			}
			this.mPost = null;
			this.mGet = null;
		}

		private TranslationJob_WEB mWeb;

		private TranslationJob_POST mPost;

		private TranslationJob_GET mGet;

		private Dictionary<string, TranslationQuery> _requests;

		private Action<Dictionary<string, TranslationQuery>, string> _OnTranslationReady;

		public string mErrorMessage;
	}
}

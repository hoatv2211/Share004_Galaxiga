using System;

namespace EasyMobile
{
	public struct SavedGameInfoUpdate
	{
		private SavedGameInfoUpdate(SavedGameInfoUpdate.Builder builder)
		{
			this._descriptionUpdated = builder._descriptionUpdated;
			this._newDescription = builder._newDescription;
			this._coverImageUpdated = builder._coverImageUpdated;
			this._newPngCoverImage = builder._newPngCoverImage;
			this._playedTimeUpdated = builder._playedTimeUpdated;
			this._newPlayedTime = builder._newPlayedTime;
		}

		public bool IsDescriptionUpdated
		{
			get
			{
				return this._descriptionUpdated;
			}
		}

		public string UpdatedDescription
		{
			get
			{
				return this._newDescription;
			}
		}

		public bool IsCoverImageUpdated
		{
			get
			{
				return this._coverImageUpdated;
			}
		}

		public byte[] UpdatedPngCoverImage
		{
			get
			{
				return this._newPngCoverImage;
			}
		}

		public bool IsPlayedTimeUpdated
		{
			get
			{
				return this._playedTimeUpdated;
			}
		}

		public TimeSpan UpdatedPlayedTime
		{
			get
			{
				return this._newPlayedTime;
			}
		}

		private readonly bool _descriptionUpdated;

		private readonly string _newDescription;

		private readonly bool _coverImageUpdated;

		private readonly byte[] _newPngCoverImage;

		private readonly bool _playedTimeUpdated;

		private readonly TimeSpan _newPlayedTime;

		public struct Builder
		{
			public SavedGameInfoUpdate.Builder WithUpdatedDescription(string description)
			{
				this._descriptionUpdated = true;
				this._newDescription = description;
				return this;
			}

			public SavedGameInfoUpdate.Builder WithUpdatedPngCoverImage(byte[] newPngCoverImage)
			{
				this._coverImageUpdated = true;
				this._newPngCoverImage = newPngCoverImage;
				return this;
			}

			public SavedGameInfoUpdate.Builder WithUpdatedPlayedTime(TimeSpan newPlayedTime)
			{
				this._playedTimeUpdated = true;
				this._newPlayedTime = newPlayedTime;
				return this;
			}

			public SavedGameInfoUpdate Build()
			{
				return new SavedGameInfoUpdate(this);
			}

			internal bool _descriptionUpdated;

			internal string _newDescription;

			internal bool _coverImageUpdated;

			internal byte[] _newPngCoverImage;

			internal bool _playedTimeUpdated;

			internal TimeSpan _newPlayedTime;
		}
	}
}

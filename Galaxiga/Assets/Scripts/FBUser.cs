using System;
using System.Collections.Generic;

public class FBUser
{
	public string id;

	public string name;

	public FBUser.Picture picture = new FBUser.Picture();

	public FBUser.Friends friends = new FBUser.Friends();

	public class Picture
	{
		public FBUser.Picture.Data data = new FBUser.Picture.Data();

		public class Data
		{
			public int height;

			public bool is_silhouette;

			public string url;

			public int width;
		}
	}

	public class Friends
	{
		public List<FBUser.Friends.Data> data = new List<FBUser.Friends.Data>();

		public FBUser.Friends.Paging paging = new FBUser.Friends.Paging();

		public FBUser.Friends.Summary summary = new FBUser.Friends.Summary();

		public class Data
		{
			public string name;

			public string id;
		}

		public class Paging
		{
			public FBUser.Friends.Paging.Cursors cursors = new FBUser.Friends.Paging.Cursors();

			public class Cursors
			{
				public string before;

				public string after;
			}
		}

		public class Summary
		{
			public int total_count;
		}
	}
}

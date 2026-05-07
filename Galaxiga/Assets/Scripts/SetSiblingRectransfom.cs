using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SetSiblingRectransfom : MonoBehaviour
{
	private void Start()
	{
	}

	public void SetAsSibling()
	{
		List<SetSiblingRectransfom.Plane> list = (from i in this.planes
		where i.infoPlane.IsOwned
		orderby i.LevelPlane
		select i).ToList<SetSiblingRectransfom.Plane>();
		int index = 0;
		int count = list.Count;
		if (count > 1)
		{
			for (int j = 0; j < this.planes.Count; j++)
			{
				if (j < count)
				{
					list[j].SetFirstSibling();
					int idPlane = list[j].IdPlane;
					if (idPlane == GameContext.currentPlaneIDEquiped)
					{
						index = j;
					}
				}
			}
			list[index].SetFirstSibling();
		}
	}

	public List<SetSiblingRectransfom.Plane> planes;

	[Serializable]
	public class Plane
	{
		public int LevelPlane
		{
			get
			{
				return this.infoPlane.planeData.Level;
			}
		}

		public int IdPlane
		{
			get
			{
				return (int)this.infoPlane.planeID;
			}
		}

		public void SetFirstSibling()
		{
			this.rectTransformPlaneView.SetAsFirstSibling();
			this.rectTransformSelectPlane.SetAsFirstSibling();
		}

		public void SetLastSibling()
		{
			this.rectTransformPlaneView.SetAsLastSibling();
			this.rectTransformSelectPlane.SetAsLastSibling();
		}

		public ViewInforPlane infoPlane;

		public RectTransform rectTransformPlaneView;

		public RectTransform rectTransformSelectPlane;

		public int indexPlane;
	}
}

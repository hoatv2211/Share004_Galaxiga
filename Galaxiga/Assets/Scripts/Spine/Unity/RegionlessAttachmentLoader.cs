using System;
using UnityEngine;

namespace Spine.Unity
{
	public class RegionlessAttachmentLoader : AttachmentLoader
	{
		private static AtlasRegion EmptyRegion
		{
			get
			{
				if (RegionlessAttachmentLoader.emptyRegion == null)
				{
					RegionlessAttachmentLoader.emptyRegion = new AtlasRegion
					{
						name = "Empty AtlasRegion",
						page = new AtlasPage
						{
							name = "Empty AtlasPage",
							rendererObject = new Material(Shader.Find("Spine/Special/HiddenPass"))
							{
								name = "NoRender Material"
							}
						}
					};
				}
				return RegionlessAttachmentLoader.emptyRegion;
			}
		}

		public RegionAttachment NewRegionAttachment(Skin skin, string name, string path)
		{
			return new RegionAttachment(name)
			{
				RendererObject = RegionlessAttachmentLoader.EmptyRegion
			};
		}

		public MeshAttachment NewMeshAttachment(Skin skin, string name, string path)
		{
			return new MeshAttachment(name)
			{
				RendererObject = RegionlessAttachmentLoader.EmptyRegion
			};
		}

		public BoundingBoxAttachment NewBoundingBoxAttachment(Skin skin, string name)
		{
			return new BoundingBoxAttachment(name);
		}

		public PathAttachment NewPathAttachment(Skin skin, string name)
		{
			return new PathAttachment(name);
		}

		public PointAttachment NewPointAttachment(Skin skin, string name)
		{
			return new PointAttachment(name);
		}

		public ClippingAttachment NewClippingAttachment(Skin skin, string name)
		{
			return new ClippingAttachment(name);
		}

		private static AtlasRegion emptyRegion;
	}
}

using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Camera : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Camera);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Camera camera = (Camera)value;
			writer.WriteProperty<float>("fieldOfView", camera.fieldOfView);
			writer.WriteProperty<float>("nearClipPlane", camera.nearClipPlane);
			writer.WriteProperty<float>("farClipPlane", camera.farClipPlane);
			writer.WriteProperty<RenderingPath>("renderingPath", camera.renderingPath);
			writer.WriteProperty<bool>("allowHDR", camera.allowHDR);
			writer.WriteProperty<bool>("forceIntoRenderTexture", camera.forceIntoRenderTexture);
			writer.WriteProperty<bool>("allowMSAA", camera.allowMSAA);
			writer.WriteProperty<float>("orthographicSize", camera.orthographicSize);
			writer.WriteProperty<bool>("orthographic", camera.orthographic);
			writer.WriteProperty<OpaqueSortMode>("opaqueSortMode", camera.opaqueSortMode);
			writer.WriteProperty<TransparencySortMode>("transparencySortMode", camera.transparencySortMode);
			writer.WriteProperty<Vector3>("transparencySortAxis", camera.transparencySortAxis);
			writer.WriteProperty<float>("depth", camera.depth);
			writer.WriteProperty<float>("aspect", camera.aspect);
			writer.WriteProperty<int>("cullingMask", camera.cullingMask);
			writer.WriteProperty<Scene>("scene", camera.scene);
			writer.WriteProperty<int>("eventMask", camera.eventMask);
			writer.WriteProperty<Color>("backgroundColor", camera.backgroundColor);
			writer.WriteProperty<Rect>("rect", camera.rect);
			writer.WriteProperty<Rect>("pixelRect", camera.pixelRect);
			writer.WriteProperty<RenderTexture>("targetTexture", camera.targetTexture);
			writer.WriteProperty<Matrix4x4>("worldToCameraMatrix", camera.worldToCameraMatrix);
			writer.WriteProperty<Matrix4x4>("projectionMatrix", camera.projectionMatrix);
			writer.WriteProperty<Matrix4x4>("nonJitteredProjectionMatrix", camera.nonJitteredProjectionMatrix);
			writer.WriteProperty<bool>("useJitteredProjectionMatrixForTransparentRendering", camera.useJitteredProjectionMatrixForTransparentRendering);
			writer.WriteProperty<CameraClearFlags>("clearFlags", camera.clearFlags);
			writer.WriteProperty<float>("stereoSeparation", camera.stereoSeparation);
			writer.WriteProperty<float>("stereoConvergence", camera.stereoConvergence);
			writer.WriteProperty<CameraType>("cameraType", camera.cameraType);
			writer.WriteProperty<StereoTargetEyeMask>("stereoTargetEye", camera.stereoTargetEye);
			writer.WriteProperty<int>("targetDisplay", camera.targetDisplay);
			writer.WriteProperty<bool>("useOcclusionCulling", camera.useOcclusionCulling);
			writer.WriteProperty<Matrix4x4>("cullingMatrix", camera.cullingMatrix);
			writer.WriteProperty<float[]>("layerCullDistances", camera.layerCullDistances);
			writer.WriteProperty<bool>("layerCullSpherical", camera.layerCullSpherical);
			writer.WriteProperty<DepthTextureMode>("depthTextureMode", camera.depthTextureMode);
			writer.WriteProperty<bool>("clearStencilAfterLightingPass", camera.clearStencilAfterLightingPass);
			writer.WriteProperty<bool>("enabled", camera.enabled);
			writer.WriteProperty<string>("tag", camera.tag);
			writer.WriteProperty<string>("name", camera.name);
			writer.WriteProperty<HideFlags>("hideFlags", camera.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			Camera camera = SaveGameType.CreateComponent<Camera>();
			this.ReadInto(camera, reader);
			return camera;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			Camera camera = (Camera)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "fieldOfView":
					camera.fieldOfView = reader.ReadProperty<float>();
					break;
				case "nearClipPlane":
					camera.nearClipPlane = reader.ReadProperty<float>();
					break;
				case "farClipPlane":
					camera.farClipPlane = reader.ReadProperty<float>();
					break;
				case "renderingPath":
					camera.renderingPath = reader.ReadProperty<RenderingPath>();
					break;
				case "allowHDR":
					camera.allowHDR = reader.ReadProperty<bool>();
					break;
				case "forceIntoRenderTexture":
					camera.forceIntoRenderTexture = reader.ReadProperty<bool>();
					break;
				case "allowMSAA":
					camera.allowMSAA = reader.ReadProperty<bool>();
					break;
				case "orthographicSize":
					camera.orthographicSize = reader.ReadProperty<float>();
					break;
				case "orthographic":
					camera.orthographic = reader.ReadProperty<bool>();
					break;
				case "opaqueSortMode":
					camera.opaqueSortMode = reader.ReadProperty<OpaqueSortMode>();
					break;
				case "transparencySortMode":
					camera.transparencySortMode = reader.ReadProperty<TransparencySortMode>();
					break;
				case "transparencySortAxis":
					camera.transparencySortAxis = reader.ReadProperty<Vector3>();
					break;
				case "depth":
					camera.depth = reader.ReadProperty<float>();
					break;
				case "aspect":
					camera.aspect = reader.ReadProperty<float>();
					break;
				case "cullingMask":
					camera.cullingMask = reader.ReadProperty<int>();
					break;
				case "scene":
					camera.scene = reader.ReadProperty<Scene>();
					break;
				case "eventMask":
					camera.eventMask = reader.ReadProperty<int>();
					break;
				case "backgroundColor":
					camera.backgroundColor = reader.ReadProperty<Color>();
					break;
				case "rect":
					camera.rect = reader.ReadProperty<Rect>();
					break;
				case "pixelRect":
					camera.pixelRect = reader.ReadProperty<Rect>();
					break;
				case "targetTexture":
					if (camera.targetTexture == null)
					{
						camera.targetTexture = reader.ReadProperty<RenderTexture>();
					}
					else
					{
						reader.ReadIntoProperty<RenderTexture>(camera.targetTexture);
					}
					break;
				case "worldToCameraMatrix":
					camera.worldToCameraMatrix = reader.ReadProperty<Matrix4x4>();
					break;
				case "projectionMatrix":
					camera.projectionMatrix = reader.ReadProperty<Matrix4x4>();
					break;
				case "nonJitteredProjectionMatrix":
					camera.nonJitteredProjectionMatrix = reader.ReadProperty<Matrix4x4>();
					break;
				case "useJitteredProjectionMatrixForTransparentRendering":
					camera.useJitteredProjectionMatrixForTransparentRendering = reader.ReadProperty<bool>();
					break;
				case "clearFlags":
					camera.clearFlags = reader.ReadProperty<CameraClearFlags>();
					break;
				case "stereoSeparation":
					camera.stereoSeparation = reader.ReadProperty<float>();
					break;
				case "stereoConvergence":
					camera.stereoConvergence = reader.ReadProperty<float>();
					break;
				case "cameraType":
					camera.cameraType = reader.ReadProperty<CameraType>();
					break;
				case "stereoTargetEye":
					camera.stereoTargetEye = reader.ReadProperty<StereoTargetEyeMask>();
					break;
				case "targetDisplay":
					camera.targetDisplay = reader.ReadProperty<int>();
					break;
				case "useOcclusionCulling":
					camera.useOcclusionCulling = reader.ReadProperty<bool>();
					break;
				case "cullingMatrix":
					camera.cullingMatrix = reader.ReadProperty<Matrix4x4>();
					break;
				case "layerCullDistances":
					camera.layerCullDistances = reader.ReadProperty<float[]>();
					break;
				case "layerCullSpherical":
					camera.layerCullSpherical = reader.ReadProperty<bool>();
					break;
				case "depthTextureMode":
					camera.depthTextureMode = reader.ReadProperty<DepthTextureMode>();
					break;
				case "clearStencilAfterLightingPass":
					camera.clearStencilAfterLightingPass = reader.ReadProperty<bool>();
					break;
				case "enabled":
					camera.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					camera.tag = reader.ReadProperty<string>();
					break;
				case "name":
					camera.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					camera.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}

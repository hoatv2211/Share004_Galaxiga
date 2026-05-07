using System;
using UnityEngine;

[RequireComponent(typeof(CanvasRenderer))]
[ExecuteInEditMode]
public class MeshRendererToCanvasRenderer : MonoBehaviour
{
	private void LateUpdate()
	{
		MeshFilter component = base.GetComponent<MeshFilter>();
		MeshRenderer component2 = base.GetComponent<MeshRenderer>();
		CanvasRenderer component3 = base.GetComponent<CanvasRenderer>();
		Mesh sharedMesh = component.sharedMesh;
		component3.SetMesh(sharedMesh);
		Material[] sharedMaterials = component2.sharedMaterials;
		if (component3.materialCount != sharedMaterials.Length)
		{
			component3.materialCount = sharedMaterials.Length;
		}
		for (int i = 0; i < sharedMaterials.Length; i++)
		{
			component3.SetMaterial(sharedMaterials[i], i);
		}
	}

	private void OnDisable()
	{
		CanvasRenderer component = base.GetComponent<CanvasRenderer>();
		component.SetMesh(null);
	}
}

﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]


public class CirclePlane : MonoBehaviour {

	public GameObject city = null;


	[Range(1.0f, 1000.0f)] private float radius = 1000f;
	[Range(50, 500)] private int numVerts = 500; 

	Vector3[] verts = new Vector3[]{ };
	Vector2[] uvs = new Vector2[]{};
	int[] tris = new int[]{};

	MeshCollider meshCollider;
	MeshRenderer meshRenderer;
	MeshFilter meshFilter;
	Mesh mesh;

	private GenerateCity craft;

	void Start ()
	{
		craft = city.GetComponent<GenerateCity> ();

		Circle ();
	}

	void Update ()
	{

		UpdateColor ();
	}

//	private void OnDrawGizmos () {
//
//		if (vertices == null) {
//			return;
//		}
//
//		Gizmos.color = Color.yellow;
//		for (int i = 0; i < verts.Length; i++) {
//			Gizmos.DrawSphere(verts[i] + this.transform.position, 0.1f);
//		}
//
//	}


	void Circle () {

		this.name = "circularGround";

		meshFilter = GetComponent<MeshFilter>();
		if (meshFilter==null){
			Debug.LogError("MeshFilter not found!");
			return;
		}

		mesh = meshFilter.sharedMesh;
		if (mesh == null){
			meshFilter.mesh = new Mesh();
			mesh = meshFilter.sharedMesh;
		}

		mesh.Clear();

		 
		verts = new Vector3[numVerts];  
		uvs = new Vector2[numVerts];  
		tris = new int[(numVerts * 3)];

		verts[0] = Vector3.zero;  
		uvs[0] = new Vector2(0.5f, 0.5f);  
		float angle = 360.0f / (float)(numVerts - 1); 

	
		for (int i = 1; i < numVerts; ++i) 
		{  
			verts[i] = Quaternion.AngleAxis(angle * (float)(i - 1), Vector3.back) * Vector3.up;  
			float normedHorizontal = (verts[i].x + 1.0f) * 0.5f;  
			float normedVertical = (verts[i].x + 1.0f) * 0.5f;  
			uvs[i] = new Vector2(normedHorizontal, normedVertical);  
		} 

		for (int i = 0; i + 2 < numVerts; ++i) 
		{  
			int index = i * 3;  
			tris[index + 0] = 0;  
			tris[index + 1] = i + 1;  
			tris[index + 2] = i + 2;  
		}  

		// The last triangle has to wrap around to the first vert so we do this last and outside the lop  
		var lastTriangleIndex = tris.Length - 3;  
		tris[lastTriangleIndex + 0] = 0;  
		tris[lastTriangleIndex + 1] = numVerts - 1;  
		tris[lastTriangleIndex + 2] = 1;  

		meshRenderer = GetComponent<MeshRenderer> ();
		Material material = new Material (Shader.Find ("Standard"));
		material.color = Color.black;
		meshRenderer.material = material;

		mesh.vertices = verts;
		mesh.triangles = tris;
		mesh.uv = uvs;

		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		mesh.Optimize();

		meshCollider = gameObject.AddComponent (typeof(MeshCollider)) as MeshCollider;
		meshCollider.sharedMesh = mesh;


		this.transform.localScale = Vector3.one * radius;
		this.transform.eulerAngles = new Vector3 (90, 0.0f, 0.0f);
		this.transform.localPosition = new Vector3 (craft.mapWidth / 2, 0f, craft.mapWidth / 2);
	}

	void UpdateColor (){

		meshRenderer.material.color = Camera.main.gameObject.GetComponent<Skybox> ().bc;
	}

}

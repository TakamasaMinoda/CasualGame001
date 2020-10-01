﻿using System.Collections.Generic;
using UnityEngine;

namespace Slicer2D {
	[ExecuteInEditMode]
	public class ColliderLineRenderer2D : MonoBehaviour {
		public bool customColor = false;
		public Color color = Color.white;
		public float lineWidth = 1;

		VisualMesh visualMesh = new VisualMesh();
		private Polygon2D polygon = null;
		private float lineWidthSet = 1;

		private SmartMaterial material = null;
		private static SmartMaterial staticMaterial = null;
		public bool drawEdgeCollider = false;

		const float lineOffset = -0.01f;

		public SmartMaterial GetMaterial() {
			if (material == null || material.material == null) {
				material = MaterialManager.GetVertexLitCopy();
			}

			return(material);
		}

		public SmartMaterial GetStaticMaterial() {
			if (staticMaterial == null || staticMaterial.material == null)   {
				staticMaterial = MaterialManager.GetVertexLitCopy();
				staticMaterial.SetColor(Color.black);
				//staticMaterial.color =  ("_Emission", Color.black);
			}
			
			return(staticMaterial);
		}

		void Start () {
			Initialize();
		}

		public void Initialize() {
			polygon = null;

			GenerateMesh();
			Draw();
		}

		void OnDestroy() {
			if (Application.isPlaying) {
				if (visualMesh.GetMesh() != null) {
					Destroy(visualMesh.GetMesh());
				}
			}
		}
		
		public void LateUpdate() {
			if (lineWidth != lineWidthSet) {
				if (lineWidth < 0.01f) {
					lineWidth = 0.01f;
				}
				GenerateMesh();
			}

			Draw();
		}

		public Polygon2D GetPolygon() {
			if (polygon == null) {
				polygon = Polygon2DList.CreateFromGameObject (gameObject)[0];
			}
			return(polygon);
		}

		public void GenerateMesh() {
			lineWidthSet = lineWidth;

			visualMesh.CreatePolygon(transform, GetPolygon(), lineOffset, lineWidth, drawEdgeCollider == false);

			visualMesh.Export();
		}

		public void Draw() {
			SmartMaterial mat;
			
			if (customColor) {
				mat = GetMaterial();
				if (mat != null) {
					mat.SetColor(color);
					visualMesh.Draw(transform, mat.material);
				}
			} else {
				mat = GetStaticMaterial();
				if (mat != null) {
					visualMesh.Draw(transform, mat.material);
				}
			}
		}
	}
}
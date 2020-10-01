using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Extensions;

	namespace Slicer2D {
	public class VisualMesh {
		const float pi = Mathf.PI;
		const float pi2 = pi / 2;

		public List<Mesh> meshes = new List<Mesh>();

		public Vector3[] verticesArray = new Vector3[0];
		public Vector2[] uvArray = new Vector2[0];
		public int[] trianglesArray = new int[0];

		public List<Vector3> vertices = new List<Vector3>();
		public List<Vector2> uv = new List<Vector2>();
		public List<int> triangles = new List<int>();

		public int triCount = 0;
		public int uvCount = 0;
		public int vertCount = 0;
		public int tris = 0;
		
		public VisualMesh() {
			meshes = new List<Mesh>();
		}

		public Mesh GetMesh(int id = 0) {
			if (meshes.Count <= id) {
				Mesh m = new Mesh();
				meshes.Add(m);
			}
			return(meshes[id]);
		}

		public Mesh Export(int id = 0) {
			Mesh mesh = GetMesh(id);

			if (verticesArray.Length != vertCount) {
				verticesArray = new Vector3[vertCount];
				uvArray = new Vector2[uvCount];
				trianglesArray = new int[triCount];
			}
				
			for(int i = 0; i < vertCount; i++) {
				verticesArray[i] = vertices[i];
			}

			for(int i = 0; i < uvCount; i++) {
				uvArray[i] = uv[i];
			}

			for(int i = 0; i < triCount; i++) {
				trianglesArray[i] = triangles[i];
			}

			mesh.triangles = null;
			mesh.uv = null;
			mesh.vertices = null;
			

			mesh.vertices = verticesArray;
			mesh.uv = uvArray;
			mesh.triangles = trianglesArray;		

			Clear();
			return(mesh);
		}

		public void Clear() {	
			triCount = 0;
			uvCount = 0;
			vertCount = 0;
			tris = 0;
		}

		public void AddVertice(Vector3 v) {
			if (vertCount >= vertices.Count) {
				vertices.Add(v);
			} else {
				vertices[vertCount] = v;
			}
			vertCount++;
		}

		public void AddTriangle(int tri) {
			if (triCount >= triangles.Count) {
				triangles.Add(tri);
			} else {
				triangles[triCount] = tri;
			}
			triCount++;
		}

		public void AddUV(Vector2 uvVar) {
			if (uvCount >= uv.Count) {
				uv.Add(uvVar);
			} else {
				uv[uvCount] = uvVar;
			}
			uvCount++;
		}











		///// POINT //////
		public void GeneratePoint(Pair2 linearPair, Transform transform, float lineWidth, float zPosition) {
			CreateLine(linearPair, transform.localScale, lineWidth, zPosition);
		}


		///// POLYGON /////
		public void GeneratePolygonMesh(Vector2 pos, Polygon2D.PolygonType polygonType, float polygonSize, float minVertexDistance, Transform transform, float lineWidth, float zPosition) {
			Polygon2D slicePolygon = Polygon2D.Create (polygonType, polygonSize).ToOffset(pos);

			Vector2 vA, vB;
			foreach(Pair2 pair in Pair2.GetList(slicePolygon.pointsList, true)) {
				vA = pair.a;
				vB = pair.b;

				vA = vA.Push (pair.a.Atan2 (pair.b), -minVertexDistance / 5);
				vB = vB.Push (pair.a.Atan2 (pair.b), minVertexDistance / 5);

				CreateLine(new Pair2(vA, vB), transform.localScale, lineWidth, zPosition);
			}
		}

		public void GeneratePolygon2DMesh(Transform transform, Polygon2D polygon, float lineOffset, float lineWidth, bool connectedLine) {
			Polygon2D poly = polygon;

			foreach(Pair2 p in Pair2.GetList(poly.pointsList, connectedLine)) {
				CreateLine(p, transform.localScale, lineWidth, lineOffset);
			}

			foreach(Polygon2D hole in poly.holesList) {
				foreach(Pair2 p in Pair2.GetList(hole.pointsList, connectedLine)) {
					CreateLine(p, transform.localScale, lineWidth, lineOffset);
				}
			}
		}



		///// COMPLEX /////
		public void Complex_GenerateMesh(Vector2List complexSlicerPointsList, Transform transform, float lineWidth, float minVertexDistance, float zPosition, float squareSize, float lineEndWidth, float vertexSpace, Slicer2DLineEndingType endingType, int edges) {
			float size = squareSize;
			
			Vector2 vA, vB;
			float rot;

			List<Pair2> list = Pair2.GetList(complexSlicerPointsList, false);
			
			foreach(Pair2 pair in list) {
				vA = pair.a;
				vB = pair.b;

				rot = (float)Vector2D.Atan2 (pair.a, pair.b);

				vA = vA.Push (rot, -minVertexDistance * vertexSpace);
				vB = vB.Push (rot, minVertexDistance * vertexSpace);

				CreateLine(new Pair2(vA, vB), transform.localScale, lineWidth, zPosition);
			}

			Pair2 linearPair = Pair2.zero;;
			linearPair.a = complexSlicerPointsList.First();
			linearPair.b = complexSlicerPointsList.Last();

			GenerateSquare(linearPair.a, size, transform, lineEndWidth, zPosition, endingType, edges);

			GenerateSquare(linearPair.b, size, transform, lineEndWidth, zPosition, endingType, edges);
		}

		public void Complex_GenerateTrackerMesh(Dictionary<Slicer2D, SlicerTrackerObject> trackerList, Transform transform, float lineWidth, float zPosition) {
			foreach(KeyValuePair<Slicer2D, SlicerTrackerObject> trackerPair in trackerList) {
				SlicerTrackerObject tracker = trackerPair.Value;
				if (trackerPair.Key != null && tracker.tracking) {
					if (tracker.firstPosition == null || tracker.lastPosition == null) {
						continue;
					}
					List<Vector2D> points = Vector2DList.PointsToWorldSpace(trackerPair.Key.transform, tracker.GetLinearPoints());
					foreach(Pair2 pair in Pair2.GetList(points, false)) {
						CreateLine(pair, transform.localScale, lineWidth, zPosition);
					}
				}
			}
		}

		public void Complex_GenerateTrackerMesh(Vector2 pos, Dictionary<Slicer2D, SlicerTrackerObject> trackerList, Transform transform, float lineWidth, float zPosition, float squareSize, Slicer2DLineEndingType endingType, int edges) {
			float size = squareSize;

			GenerateSquare(pos, size, transform, lineWidth, zPosition, endingType, edges);

			CreateLine(new Pair2(pos, pos), transform.localScale, lineWidth, zPosition);

			foreach(KeyValuePair<Slicer2D, SlicerTrackerObject> trackerPair in trackerList) {
				SlicerTrackerObject tracker = trackerPair.Value;
				if (trackerPair.Key != null && tracker.tracking) {
					List<Vector2D> pointListWorld = Vector2DList.PointsToWorldSpace(trackerPair.Key.transform, tracker.pointsList);

					pointListWorld.Add(new Vector2D(pos));

					List<Pair2> pairList = Pair2.GetList(pointListWorld, false);

					foreach(Pair2 pair in pairList) {
						CreateLine(pair, transform.localScale, lineWidth, zPosition);
					}
				}
			}
		}

		public void Complex_GenerateCutMesh(List<Vector2D> complexSlicerPointsList, float cutSize, Transform transform, float lineWidth, float zPosition) {
			ComplexCut complexCutLine = ComplexCut.Create(complexSlicerPointsList, cutSize);
			foreach(Pair2 pair in Pair2.GetList(complexCutLine.GetPointsList(), true)) {
				CreateLine(pair, transform.localScale, lineWidth, zPosition);
			}
		}



		///// Create /////
		public void GenerateCreateMesh(Vector2 pos, Polygon2D.PolygonType polygonType, float polygonSize, Slicer2DCreateControllerObject.CreateType createType, List<Vector2D> complexSlicerPointsList, Pair2D linearPair, float minVertexDistance, Transform transform, float lineWidth, float zPosition, float squareSize, Slicer2DLineEndingType endingType, int edges) {
			float size = squareSize;

			if (createType == Slicer2DCreateControllerObject.CreateType.Slice) {
				if (complexSlicerPointsList.Count > 0) {
					linearPair.A = new Vector2D(complexSlicerPointsList.First());
					linearPair.B = new Vector2D(complexSlicerPointsList.Last());

					GenerateSquare(linearPair.A.ToVector2(), size, transform, lineWidth, zPosition, endingType, edges);

					GenerateSquare(linearPair.B.ToVector2(), size, transform, lineWidth, zPosition, endingType, edges);

					Vector2D vA, vB;
					foreach(Pair2 pair in Pair2.GetList(complexSlicerPointsList, true)) {
						vA = new Vector2D (pair.a);
						vB = new Vector2D (pair.b);

						vA.Push (Vector2D.Atan2 (pair.a, pair.b), -minVertexDistance / 5);
						vB.Push (Vector2D.Atan2 (pair.a, pair.b), minVertexDistance / 5);

						CreateLine(new Pair2(vA.ToVector2(), vB.ToVector2()), transform.localScale, lineWidth, zPosition);
					}
				}
			} else {
				Polygon2D poly = Polygon2D.Create(polygonType, polygonSize).ToOffset(pos);

				Vector2D vA, vB;
				foreach(Pair2 pair in Pair2.GetList(poly.pointsList, true)) {
					vA = new Vector2D (pair.a);
					vB = new Vector2D (pair.b);

					vA.Push (Vector2D.Atan2 (pair.a, pair.b), -minVertexDistance / 5);
					vB.Push (Vector2D.Atan2 (pair.a, pair.b), minVertexDistance / 5);

					CreateLine(new Pair2(vA.ToVector2(), vB.ToVector2()), transform.localScale, lineWidth, zPosition);
				}
			}
		}

		
		public void GenerateTrailMesh(Dictionary<Slicer2D, SlicerTrailObject> trailList, Transform transform, float lineWidth, float zPosition, float squareSize) {
			foreach(KeyValuePair<Slicer2D, SlicerTrailObject> s in trailList) {
				if (s.Key != null) {

					List<Vector2D> points = new List<Vector2D>();
					foreach(TrailPoint trailPoint in s.Value.pointsList) {

						points.Add(trailPoint.position);

					}

					foreach(Pair2 pair in Pair2.GetList(points, false)) {
				
						CreateLine(pair, new Vector3(1, 1, 1), lineWidth, zPosition);

					}
				}
			}
		}



		//// LINEAR /////
		public void Linear_GenerateMesh(Pair2 linearPair, Transform transform, float lineWidth, float zPosition, float size, float lineEndWidth, Slicer2DLineEndingType endingType, int edges) {
			CreateLine(linearPair, transform.localScale, lineWidth, zPosition);

			GenerateSquare(linearPair.a, size, transform, lineEndWidth, zPosition, endingType, edges);

			GenerateSquare(linearPair.b, size, transform, lineEndWidth, zPosition, endingType, edges);
		}

		public void Linear_GenerateCutMesh(Pair2 linearPair, float cutSize, Transform transform, float lineWidth, float zPosition) {
			LinearCut linearCutLine = LinearCut.Create(linearPair, cutSize);
			
			foreach(Pair2 pair in Pair2.GetList(linearCutLine.GetPointsList(), true)) {
				CreateLine(pair, transform.localScale, lineWidth, zPosition);
			}
		}

		public void Linear_GenerateTrackerMesh(Vector2 pos, Dictionary<Slicer2D, SlicerTrackerObject> trackerList, Transform transform, float lineWidth, float zPosition, float size, Slicer2DLineEndingType endingType, int edges) {
			GenerateSquare(pos, size, transform, lineWidth, zPosition, endingType, edges);

			CreateLine(new Pair2(pos, pos), transform.localScale, lineWidth, zPosition);

			foreach(KeyValuePair<Slicer2D, SlicerTrackerObject> trackerPair in trackerList) {
				SlicerTrackerObject tracker = trackerPair.Value;
				if (trackerPair.Key != null && tracker.tracking) {
					foreach(Pair2 pair in Pair2.GetList(Vector2DList.PointsToWorldSpace(trackerPair.Key.transform, tracker.GetLinearPoints()), false)) {
						CreateLine(pair, transform.localScale, lineWidth, zPosition);
					}
				}
			}
		}




		///// GENERAL /////
		public void GenerateSquare(Vector2 point, float size, Transform transform, float width, float z, Slicer2DLineEndingType endingType, int edges) {
			if (endingType == Slicer2DLineEndingType.Square) {
				
				CreateLine(new Pair2(new Vector2(point.x - size, point.y - size), new Vector2(point.x + size, point.y - size)), transform.localScale, width, z);
				CreateLine(new Pair2(new Vector2(point.x - size, point.y - size), new Vector2(point.x - size, point.y + size)), transform.localScale, width, z);
				CreateLine(new Pair2(new Vector2(point.x + size, point.y + size), new Vector2(point.x + size, point.y - size)), transform.localScale, width, z);
				CreateLine(new Pair2(new Vector2(point.x + size, point.y + size), new Vector2(point.x - size, point.y + size)), transform.localScale, width, z);
			
			} else {
				float step = 360f / edges;

				for(int i = 0; i < edges; i++) {
					float x0 = Mathf.Cos((i - 1) * step * Mathf.Deg2Rad) * size;
					float y0 =  Mathf.Sin((i - 1) * step * Mathf.Deg2Rad) * size;
					float x1 = Mathf.Cos(i * step * Mathf.Deg2Rad) * size;
					float y1 =  Mathf.Sin(i * step * Mathf.Deg2Rad) * size;

					CreateLine(new Pair2(new Vector2(point.x + x0, point.y + y0), new Vector2(point.x + x1, point.y + y1)), transform.localScale, width, z);
				}	
			}
		}


		
		public void CreatePolygon(Transform transform, Polygon2D polygon, float lineOffset, float lineWidth, bool connectedLine) {
			int count = polygon.pointsList.Count;
			int lastID = count - 1;
			int startID = 0;
			
			if (connectedLine == false) {
				lastID = 0;
				startID = 1;
			}
			
			Pair2 p = Pair2.zero;
			p.a = polygon.pointsList[lastID].ToVector2();
			
			for(int i = startID; i < count; i++) {
				p.b = polygon.pointsList[i].ToVector2();

				CreateLine(p, transform.localScale, lineWidth, lineOffset);

				p.a = p.b;
			}
			
			foreach(Polygon2D hole in polygon.holesList) {
				count = hole.pointsList.Count;
				lastID = count - 1;
				startID = 0;
			
				if (connectedLine == false) {
					lastID = 0;
					startID = 1;
				}

				p.a = hole.pointsList[lastID].ToVector2();
				
				for(int i = startID; i < count; i++) {
					p.b = hole.pointsList[i].ToVector2();

					CreateLine(p, transform.localScale, lineWidth, lineOffset);

					p.a = p.b;
				}
			} 
		}











		///// Box /////
		public void CreateBox(float size) {
			float uv0 = 0; 
			float uv1 = 1f;

			
			AddVertice( new Vector3(-size, -size, 0) );
			AddVertice( new Vector3(size, -size, 0) );
			AddVertice( new Vector3(size, size, 0) );
			AddVertice( new Vector3(-size, size, 0) );
		
			
			AddUV( new Vector2(uv0, uv0) );
			AddUV( new Vector2(uv1, uv0) );
			AddUV( new Vector2(uv1, uv1) );
			AddUV( new Vector2(uv1, uv0) );

			AddTriangle(tris + 0);
			AddTriangle(tris + 1);
			AddTriangle(tris + 2);

			AddTriangle(tris + 2);
			AddTriangle(tris + 3);
			AddTriangle(tris + 0);

			tris += 4;
		}
		




		public void CreateLine(Pair2 pair, Vector3 transformScale, float lineWidth, float z = 0f) {
			float xuv0 = 0; 
			float xuv1 = 1f - xuv0;
			float yuv0 = 0;
			float yuv1 = 1f - yuv0;

			float size = lineWidth / 6;
			float rot =  pair.a.Atan2(pair.b);

			Vector2 A1 = pair.a;
			Vector2 A2 = pair.a;
			Vector2 B1 = pair.b;
			Vector2 B2 = pair.b;

			Vector2 scale = new Vector2(1f / transformScale.x, 1f / transformScale.y);

			A1 = A1.Push (rot + pi2, size, scale);
			A2 = A2.Push (rot - pi2, size, scale);
			B1 = B1.Push (rot + pi2, size, scale);
			B2 = B2.Push (rot - pi2, size, scale);

			AddVertice(	new Vector3(B1.x, B1.y, z)		);
			AddVertice(	new Vector3(A1.x, A1.y, z)		);
			AddVertice(	new Vector3(A2.x, A2.y, z)		);
			AddVertice(	new Vector3(B2.x, B2.y, z)		);
		
			AddUV(		new Vector2(xuv1 / 3, yuv1)		); 
			AddUV( 		new Vector2(1 - xuv1 / 3, yuv1)	);
			AddUV( 		new Vector2(1 - xuv1 / 3, yuv0)	);
			AddUV(		new Vector2(yuv1 / 3, xuv0)		);

			Vector2 A3 = A1;
			Vector2 A4 = A1;

			A3 = A1;
			A4 = A2;

			A1 = A1.Push (rot, size, scale);
			A2 = A2.Push (rot, size, scale);

			AddVertice(	new Vector3(A3.x, A3.y, z)		);
			AddVertice(	new Vector3(A1.x, A1.y, z)		);
			AddVertice(	new Vector3(A2.x, A2.y, z)		);
			AddVertice(	new Vector3(A4.x, A4.y, z)		);

			AddUV( 	new Vector2(xuv1 / 3, yuv1)			); 
			AddUV(	new Vector2(xuv0, yuv1)				);
			AddUV( 	new Vector2(xuv0, yuv0)				);
			AddUV( 	new Vector2(yuv1 / 3, xuv0)			);


			A1 = B1;
			A2 = B2;

			B1 = B1.Push (rot - Mathf.PI, size, scale);
			B2 = B2.Push (rot - Mathf.PI, size, scale);
			
			AddVertice(	 new Vector3(B1.x, B1.y, z)	);
			AddVertice(	 new Vector3(A1.x, A1.y, z)	);
			AddVertice(	 new Vector3(A2.x, A2.y, z)	);
			AddVertice(	 new Vector3(B2.x, B2.y, z)	);

			AddUV(		new Vector2(xuv0, yuv1)			); 
			AddUV(		new Vector2(xuv1 / 3, yuv1)		);
			AddUV(		new Vector2(xuv1 / 3, yuv0)		);
			AddUV( 		new Vector2(yuv0, xuv0)			);

			AddTriangle(tris + 0);
			AddTriangle(tris + 1);
			AddTriangle(tris + 2);

			AddTriangle(tris + 2);
			AddTriangle(tris + 3);
			AddTriangle(tris + 0);


			AddTriangle(tris + 4);
			AddTriangle(tris + 5);
			AddTriangle(tris + 6);

			AddTriangle(tris + 6);
			AddTriangle(tris + 7);
			AddTriangle(tris + 4);


			
			AddTriangle(tris + 8);
			AddTriangle(tris + 9);
			AddTriangle(tris + 10);

			AddTriangle(tris + 10);
			AddTriangle(tris + 11);
			AddTriangle(tris + 8);

			tris += 12;
		}

		
		public void Draw(Transform transform, Material material, int id = 0) {
			Matrix4x4 matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);

			Graphics.DrawMesh(GetMesh(id), matrix, material, 0);
		}

		public void Draw(Material material, int id = 0) {
			Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0, 0, 0),  new Vector3(1, 1, 1));

			Graphics.DrawMesh(GetMesh(id), matrix, material, 0);
		}

	}


			/*

			
			*/
		
	/*
		static public Mesh GeneratePointMesh(Pair2D linearPair, Transform transform, float lineWidth, float zPosition) {
			Mesh2DMesh trianglesList = new Mesh2DMesh();

			trianglesList.Add(Max2DMesh.CreateLine(linearPair, transform.localScale, lineWidth, zPosition));

			return(Max2DMesh.Export(trianglesList));
		}
		static public Mesh2DSubmesh CreateLine(Pair2D pair, Vector3 transformScale, float lineWidth, float z = 0f) {
			Mesh2DSubmesh result = new Mesh2DSubmesh(18);

			float xuv0 = 0; 
			float xuv1 = 1f - xuv0;
			float yuv0 = 0;
			float yuv1 = 1f - yuv0;

			float size = lineWidth / 6;
			float rot = (float)Vector2D.Atan2 (pair.A, pair.B);

			A1.x = pair.A.x;
			A1.y = pair.A.y;

			A2.x = pair.A.x;
			A2.y = pair.A.y;

			B1.x = pair.B.x;
			B1.y = pair.B.y;

			B2.x = pair.B.x;
			B2.y = pair.B.y;

			Vector2 scale = new Vector2(1f / transformScale.x, 1f / transformScale.y);

			A1.Push (rot + pi2, size, scale);
			A2.Push (rot - pi2, size, scale);
			B1.Push (rot + pi2, size, scale);
			B2.Push (rot - pi2, size, scale);

			result.vertices[0] = new Vector3((float)B1.x, (float)B1.y, z);
			result.vertices[1] = new Vector3((float)A1.x, (float)A1.y, z);
			result.vertices[2] = new Vector3((float)A2.x, (float)A2.y, z);
			
			result.vertices[3] = new Vector3((float)A2.x, (float)A2.y, z);
			result.vertices[4] = new Vector3((float)B2.x, (float)B2.y, z);
			result.vertices[5] = new Vector3((float)B1.x, (float)B1.y, z);

			result.uv[0] = new Vector2(xuv1 / 3, yuv1); 
			result.uv[1] = new Vector2(1 - xuv1 / 3, yuv1);
			result.uv[2] = new Vector2(1 - xuv1 / 3, yuv0);
			
			result.uv[3] = new Vector2(1 - xuv1 / 3, yuv0);
			result.uv[4] = new Vector2(yuv1 / 3, xuv0);
			result.uv[5] = new Vector2(xuv1 / 3, yuv1);

			A3.x = A1.x;
			A3.y = A1.y;

			A4.x = A1.x;
			A4.y = A1.y;
		
			A3.Push (rot - pi2, size, scale);

			A3.x = A1.x;
			A3.y = A1.y;
			
			A4.x = A2.x;
			A4.y = A2.y;

			A1.Push (rot, size, scale);
			A2.Push (rot, size, scale);

			result.vertices[6] = new Vector3((float)A3.x, (float)A3.y, z);
			result.vertices[7] = new Vector3((float)A1.x, (float)A1.y, z);
			result.vertices[8] = new Vector3((float)A2.x, (float)A2.y, z);
			
			result.vertices[9] = new Vector3((float)A2.x, (float)A2.y, z);
			result.vertices[10] = new Vector3((float)A4.x, (float)A4.y, z);
			result.vertices[11] = new Vector3((float)A3.x, (float)A3.y, z);
			
			result.uv[6] = new Vector2(xuv1 / 3, yuv1); 
			result.uv[7] = new Vector2(xuv0, yuv1);
			result.uv[8] = new Vector2(xuv0, yuv0);

			result.uv[9] = new Vector2(xuv0, yuv0);
			result.uv[10] = new Vector2(yuv1 / 3, xuv0);
			result.uv[11] = new Vector2(xuv1 / 3, yuv1);

			A1.x = B1.x;
			A1.y = B1.y;

			A2.x = B2.x;
			A2.y = B2.y;

			B1.Push (rot - Mathf.PI, size, scale);
			B2.Push (rot - Mathf.PI, size, scale);
			
			result.vertices[12] = new Vector3((float)B1.x, (float)B1.y, z);
			result.vertices[13] = new Vector3((float)A1.x, (float)A1.y, z);
			result.vertices[14] = new Vector3((float)A2.x, (float)A2.y, z);

			result.vertices[15] = new Vector3((float)A2.x, (float)A2.y, z);
			result.vertices[16] = new Vector3((float)B2.x, (float)B2.y, z);
			result.vertices[17] = new Vector3((float)B1.x, (float)B1.y, z);

			result.uv[12] = new Vector2(xuv0, yuv1); 
			result.uv[13] = new Vector2(xuv1 / 3, yuv1);
			result.uv[14] = new Vector2(xuv1 / 3, yuv0);

			result.uv[15] = new Vector2(xuv1 / 3, yuv0);
			result.uv[16] = new Vector2(yuv0, xuv0);
			result.uv[17] = new Vector2(xuv0, yuv1);
			
			return(result);
		}
		*/










































	/*


	public class Slicer2DVisualsMesh {

		static public void GenerateSquare(Mesh2DMesh trianglesList, Vector2D point, float size, Transform transform, float width, float z, Slicer2DLineEndingType endingType, int edges) {
			if (endingType == Slicer2DLineEndingType.Square) {
				
				trianglesList.Add(Max2DMesh.CreateLine(new Pair2D(new Vector2D(point.x - size, point.y - size), new Vector2D(point.x + size, point.y - size)), transform.localScale, width, z));
				trianglesList.Add(Max2DMesh.CreateLine(new Pair2D(new Vector2D(point.x - size, point.y - size), new Vector2D(point.x - size, point.y + size)), transform.localScale, width, z));
				trianglesList.Add(Max2DMesh.CreateLine(new Pair2D(new Vector2D(point.x + size, point.y + size), new Vector2D(point.x + size, point.y - size)), transform.localScale, width, z));
				trianglesList.Add(Max2DMesh.CreateLine(new Pair2D(new Vector2D(point.x + size, point.y + size), new Vector2D(point.x - size, point.y + size)), transform.localScale, width, z));
			
			} else {
				float step = 360f / edges;

				for(int i = 0; i < edges; i++) {
					float x0 = Mathf.Cos((i - 1) * step * Mathf.Deg2Rad) * size;
					float y0 =  Mathf.Sin((i - 1) * step * Mathf.Deg2Rad) * size;
					float x1 = Mathf.Cos(i * step * Mathf.Deg2Rad) * size;
					float y1 =  Mathf.Sin(i * step * Mathf.Deg2Rad) * size;

					trianglesList.Add(Max2DMesh.CreateLine(new Pair2D(new Vector2D(point.x + x0, point.y + y0), new Vector2D(point.x + x1, point.y + y1)), transform.localScale, width, z));
				}	
			}
		}

		public class Complex {
			static public Mesh GenerateMesh(List<Vector2D> complexSlicerPointsList, Transform transform, float lineWidth, float minVertexDistance, float zPosition, float squareSize, float lineEndWidth, float vertexSpace, Slicer2DLineEndingType endingType, int edges) {
				Mesh2DMesh trianglesList = new Mesh2DMesh();

				float size = squareSize;
				
				Vector2D vA, vB;
				List<Pair2D> list = Pair2D.GetList(complexSlicerPointsList, false);
				foreach(Pair2D pair in list) {
					vA = new Vector2D (pair.A);
					vB = new Vector2D (pair.B);

					vA.Push (Vector2D.Atan2 (pair.A, pair.B), -minVertexDistance * vertexSpace);
					vB.Push (Vector2D.Atan2 (pair.A, pair.B), minVertexDistance * vertexSpace);

					trianglesList.Add(Max2DMesh.CreateLine(new Pair2D(vA, vB), transform.localScale, lineWidth, zPosition));
				}

				Pair2D linearPair = Pair2D.Zero();
				linearPair.A = new Vector2D(complexSlicerPointsList.First());
				linearPair.B = new Vector2D(complexSlicerPointsList.Last());

				GenerateSquare(trianglesList, linearPair.A, size, transform, lineEndWidth, zPosition, endingType, edges);

				GenerateSquare(trianglesList, linearPair.B, size, transform, lineEndWidth, zPosition, endingType, edges);

				return(Max2DMesh.Export(trianglesList));
			}

			static public Mesh GenerateCutMesh(List<Vector2D> complexSlicerPointsList, float cutSize, Transform transform, float lineWidth, float zPosition) {
				Mesh2DMesh trianglesList = new Mesh2DMesh();

				ComplexCut complexCutLine = ComplexCut.Create(complexSlicerPointsList, cutSize);
				foreach(Pair2D pair in Pair2D.GetList(complexCutLine.GetPointsList(), true)) {
					trianglesList.Add(Max2DMesh.CreateLine(pair, transform.localScale, lineWidth, zPosition));
				}

				return(Max2DMesh.Export(trianglesList));
			}

			static public Mesh GenerateTrailMesh(Vector2D pos, Dictionary<Slicer2D, SlicerTrailObject>  trailList, Transform transform, float lineWidth, float zPosition, float squareSize) {
				Mesh2DMesh trianglesList = new Mesh2DMesh();

				foreach(KeyValuePair<Slicer2D, SlicerTrailObject> s in trailList) {
					if (s.Key != null) {

						List<Vector2D> points = new List<Vector2D>();
						foreach(TrailPoint trailPoint in s.Value.pointsList) {
							points.Add(trailPoint.position);
						}
						foreach(Pair2D pair in Pair2D.GetList(points, false)) {
							trianglesList.Add(Max2DMesh.CreateLine(pair, new Vector3(1, 1, 1), lineWidth, zPosition));
						}
					}
				}

				return(Max2DMesh.Export(trianglesList));
			}
			
			static public Mesh GenerateTrackerMesh(Vector2D pos, Dictionary<Slicer2D, SlicerTrackerObject> trackerList, Transform transform, float lineWidth, float zPosition, float squareSize, Slicer2DLineEndingType endingType, int edges) {
				Mesh2DMesh trianglesList = new Mesh2DMesh();

				float size = squareSize;

				GenerateSquare(trianglesList, pos, size, transform, lineWidth, zPosition, endingType, edges);

				trianglesList.Add(Max2DMesh.CreateLine(new Pair2D(pos, pos), transform.localScale, lineWidth, zPosition));

				foreach(KeyValuePair<Slicer2D, SlicerTrackerObject> trackerPair in trackerList) {
					SlicerTrackerObject tracker = trackerPair.Value;
					if (trackerPair.Key != null && tracker.tracking) {
						List<Vector2D> pointListWorld = Vector2DList.PointsToWorldSpace(trackerPair.Key.transform, tracker.pointsList);

						pointListWorld.Add(pos);

						List<Pair2D> pairList = Pair2D.GetList(pointListWorld, false);

						foreach(Pair2D pair in pairList) {
							trianglesList.Add(Max2DMesh.CreateLine(pair, transform.localScale, lineWidth, zPosition));
						}
					}
				}

				return(Max2DMesh.Export(trianglesList));
			}

			// Duplicate
			static public Mesh GenerateTrackerMesh(Dictionary<Slicer2D, SlicerTrackerObject> trackerList, Transform transform, float lineWidth, float zPosition) {
				Mesh2DMesh trianglesList = new Mesh2DMesh();

				foreach(KeyValuePair<Slicer2D, SlicerTrackerObject> trackerPair in trackerList) {
					SlicerTrackerObject tracker = trackerPair.Value;
					if (trackerPair.Key != null && tracker.tracking) {
						List<Vector2D> pointListWorld = Vector2DList.PointsToWorldSpace(trackerPair.Key.transform, tracker.pointsList);
						pointListWorld.Add(new Vector2D(transform.TransformPoint(Vector2.zero)));

						List<Pair2D> pairList = Pair2D.GetList(pointListWorld, false);

						foreach(Pair2D pair in pairList) {
							trianglesList.Add(Max2DMesh.CreateLine(pair, transform.localScale, lineWidth, zPosition));
						}
					}
				}

				return(Max2DMesh.Export(trianglesList));
			}
		}

		public class Linear {

			static public Mesh GenerateMesh(Pair2D linearPair, Transform transform, float lineWidth, float zPosition, float size, float lineEndWidth, Slicer2DLineEndingType endingType, int edges) {
				Mesh2DMesh trianglesList = new Mesh2DMesh();

				trianglesList.Add(Max2DMesh.CreateLine(linearPair, transform.localScale, lineWidth, zPosition));

				GenerateSquare(trianglesList, linearPair.A, size, transform, lineEndWidth, zPosition, endingType, edges);

				GenerateSquare(trianglesList, linearPair.B, size, transform, lineEndWidth, zPosition, endingType, edges);

				return(Max2DMesh.Export(trianglesList));
			}

			static public Mesh GenerateCutMesh(Pair2D linearPair, float cutSize, Transform transform, float lineWidth, float zPosition) {
				Mesh2DMesh trianglesList = new Mesh2DMesh();

				LinearCut linearCutLine = LinearCut.Create(linearPair, cutSize);
				foreach(Pair2D pair in Pair2D.GetList(linearCutLine.GetPointsList(), true)) {
					trianglesList.Add(Max2DMesh.CreateLine(pair, transform.localScale, lineWidth, zPosition));
				}

				return(Max2DMesh.Export(trianglesList));
			}
				
			static public Mesh GenerateTrackerMesh(Vector2D pos, Dictionary<Slicer2D, SlicerTrackerObject> trackerList, Transform transform, float lineWidth, float zPosition, float size, Slicer2DLineEndingType endingType, int edges) {
				Mesh2DMesh trianglesList = new Mesh2DMesh();

				GenerateSquare(trianglesList, pos, size, transform, lineWidth, zPosition, endingType, edges);

				trianglesList.Add(Max2DMesh.CreateLine(new Pair2D(pos, pos), transform.localScale, lineWidth, zPosition));

				foreach(KeyValuePair<Slicer2D, SlicerTrackerObject> trackerPair in trackerList) {
					SlicerTrackerObject tracker = trackerPair.Value;
					if (trackerPair.Key != null && tracker.tracking) {
						foreach(Pair2D pair in Pair2D.GetList(Vector2DList.PointsToWorldSpace(trackerPair.Key.transform, tracker.GetLinearPoints()), false)) {
							trianglesList.Add(Max2DMesh.CreateLine(pair, transform.localScale, lineWidth, zPosition));
						}
					}
				}

				return(Max2DMesh.Export(trianglesList));
			}

			static public Mesh GenerateTrackerMesh(Dictionary<Slicer2D, SlicerTrackerObject> trackerList, Transform transform, float lineWidth, float zPosition) {
				Mesh2DMesh trianglesList = new Mesh2DMesh();

				foreach(KeyValuePair<Slicer2D, SlicerTrackerObject> trackerPair in trackerList) {
					SlicerTrackerObject tracker = trackerPair.Value;
					if (trackerPair.Key != null && tracker.tracking) {
						if (tracker.firstPosition == null || tracker.lastPosition == null) {
							continue;
						}
						List<Vector2D> points = Vector2DList.PointsToWorldSpace(trackerPair.Key.transform, tracker.GetLinearPoints());
						foreach(Pair2D pair in Pair2D.GetList(points, false)) {
							trianglesList.Add(Max2DMesh.CreateLine(pair, transform.localScale, lineWidth, zPosition));
						}
					}
				}

				return(Max2DMesh.Export(trianglesList));
			}
		}

		



		
		
		static public Mesh GeneratePolygonMesh(Vector2D pos, Polygon2D.PolygonType polygonType, float polygonSize, float minVertexDistance, Transform transform, float lineWidth, float zPosition) {
			Mesh2DMesh trianglesList = new Mesh2DMesh();

			Polygon2D slicePolygon = Polygon2D.Create (polygonType, polygonSize).ToOffset(pos);

			Vector2D vA, vB;
			foreach(Pair2D pair in Pair2D.GetList(slicePolygon.pointsList, true)) {
				vA = new Vector2D (pair.A);
				vB = new Vector2D (pair.B);

				vA.Push (Vector2D.Atan2 (pair.A, pair.B), -minVertexDistance / 5);
				vB.Push (Vector2D.Atan2 (pair.A, pair.B), minVertexDistance / 5);

				trianglesList.Add(Max2DMesh.CreateLine(new Pair2D(vA, vB), transform.localScale, lineWidth, zPosition));
			}

			return(Max2DMesh.Export(trianglesList));
		}

		static public Mesh GenerateCreateMesh(Vector2D pos, Polygon2D.PolygonType polygonType, float polygonSize, Slicer2DCreateControllerObject.CreateType createType, List<Vector2D> complexSlicerPointsList, Pair2D linearPair, float minVertexDistance, Transform transform, float lineWidth, float zPosition, float squareSize, Slicer2DLineEndingType endingType, int edges) {
			Mesh2DMesh trianglesList = new Mesh2DMesh();

			float size = squareSize;

			if (createType == Slicer2DCreateControllerObject.CreateType.Slice) {
				if (complexSlicerPointsList.Count > 0) {
					linearPair.A = new Vector2D(complexSlicerPointsList.First());
					linearPair.B = new Vector2D(complexSlicerPointsList.Last());

					GenerateSquare(trianglesList, linearPair.A, size, transform, lineWidth, zPosition, endingType, edges);

					GenerateSquare(trianglesList, linearPair.B, size, transform, lineWidth, zPosition, endingType, edges);

					Vector2D vA, vB;
					foreach(Pair2D pair in Pair2D.GetList(complexSlicerPointsList, true)) {
						vA = new Vector2D (pair.A);
						vB = new Vector2D (pair.B);

						vA.Push (Vector2D.Atan2 (pair.A, pair.B), -minVertexDistance / 5);
						vB.Push (Vector2D.Atan2 (pair.A, pair.B), minVertexDistance / 5);

						trianglesList.Add(Max2DMesh.CreateLine(new Pair2D(vA, vB), transform.localScale, lineWidth, zPosition));
					}
				}
			} else {
				Polygon2D poly = Polygon2D.Create(polygonType, polygonSize).ToOffset(pos);

				Vector2D vA, vB;
				foreach(Pair2D pair in Pair2D.GetList(poly.pointsList, true)) {
					vA = new Vector2D (pair.A);
					vB = new Vector2D (pair.B);

					vA.Push (Vector2D.Atan2 (pair.A, pair.B), -minVertexDistance / 5);
					vB.Push (Vector2D.Atan2 (pair.A, pair.B), minVertexDistance / 5);

					trianglesList.Add(Max2DMesh.CreateLine(new Pair2D(vA, vB), transform.localScale, lineWidth, zPosition));
				}
			}

			return(Max2DMesh.Export(trianglesList));
		}

		static public Mesh GeneratePolygon2DMesh(Transform transform, Polygon2D polygon, float lineOffset, float lineWidth, bool connectedLine) {
			Mesh2DMesh trianglesList = new Mesh2DMesh();

			Polygon2D poly = polygon;

			foreach(Pair2D p in Pair2D.GetList(poly.pointsList, connectedLine)) {
				trianglesList.Add(Max2DMesh.CreateLine(p, transform.localScale, lineWidth, lineOffset));
			}

			foreach(Polygon2D hole in poly.holesList) {
				foreach(Pair2D p in Pair2D.GetList(hole.pointsList, connectedLine)) {
					trianglesList.Add(Max2DMesh.CreateLine(p, transform.localScale, lineWidth, lineOffset));
				}
			}
			
			return(Max2DMesh.Export(trianglesList));
		}

		static public Mesh GeneratePointMesh(Pair2D linearPair, Transform transform, float lineWidth, float zPosition) {
			Mesh2DMesh trianglesList = new Mesh2DMesh();

			trianglesList.Add(Max2DMesh.CreateLine(linearPair, transform.localScale, lineWidth, zPosition));

			return(Max2DMesh.Export(trianglesList));
		}
	}
	*/
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slicer2D {
	public class Demo11ColliderSlice : MonoBehaviour {

		void Update () {
			if (Input.GetKeyDown(KeyCode.Space)) {
				Slice();
			}
		}

		public void Slice() {
			float timer = Time.realtimeSinceStartup;
				
			foreach(Transform t in transform) {
				Polygon2D poly = Polygon2DList.CreateFromGameObject(t.gameObject)[0].ToWorldSpace(t);

				Slicer2D.ComplexSliceAll(poly.pointsList, Slice2DLayer.Create());
			}

			Destroy(gameObject);

			Debug.Log(name + " in " + (Time.realtimeSinceStartup - timer) * 1000 + "ms");
		}
	}
}
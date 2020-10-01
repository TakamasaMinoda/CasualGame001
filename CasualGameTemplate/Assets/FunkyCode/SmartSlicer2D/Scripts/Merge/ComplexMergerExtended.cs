﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slicer2D {
	public class ComplexMergerExtended {

		static public Merge2D MergePolygon(Polygon2D polygon, Polygon2D mergePolygon) {
			Merge2D result = Merge2D.Create (mergePolygon.pointsList);

			Vector2D startPoint = null;

			foreach (Vector2D id in mergePolygon.pointsList) {
				if (polygon.PointInPoly (id) == true) {
					startPoint = id;
					break;
				}
			}

			if (startPoint == null) {
				if (Slicer2D.Debug.enabled) {
					Debug.LogWarning ("Starting Point Error In Polygon Merge");
				}
				return(result);
			}

			mergePolygon.pointsList = Vector2DList.GetListStartingPoint (mergePolygon.pointsList, startPoint);

			mergePolygon.AddPoint (startPoint);

			// Not Necessary
			if (polygon.SliceIntersectPoly (mergePolygon.pointsList) == false) {
				return(result);
			}

			result = ComplexMerger.Merge (polygon, new List<Vector2D> (mergePolygon.pointsList));

			if (result.polygons.Count < 1) {
				if (Slicer2D.Debug.enabled) {
					Debug.LogWarning ("Merger2D: Returns Empty Polygon Slice");
				}
			}
		
			return(result);
		}
	}
}
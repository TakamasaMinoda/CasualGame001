using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Extensions;

namespace Slicer2D {
	[System.Serializable]
	public class Slicer2DComplexTrackerControllerObject : Slicer2DControllerObject {
		// Algorhitmic
		List<Vector2D> pointsList = new List<Vector2D>();
		ComplexSlicerTracker complexTracker = new ComplexSlicerTracker();

		// Settings
		public Slicer2D.SliceType complexSliceType = Slicer2D.SliceType.SliceHole;
		public float minVertexDistance = 1f;

		public void Update(Vector2 pos) {
			if (input.GetInputClicked()) {
				pointsList.Clear ();
				complexTracker.trackerList.Clear ();
				pointsList.Add (new Vector2D(pos));
			}
							
			if (input.GetInputPressed() && pointsList.Count > 0) {
				Vector2 posMove = pointsList.Last ().ToVector2();

				int loopCount = 0;
				while ((Vector2.Distance (posMove, pos) > minVertexDistance)) {
					float direction = (float)Vector2D.Atan2 (pos, posMove);
					posMove = posMove.Push (direction, minVertexDistance);
					Slicer2D.complexSliceType = complexSliceType;
					pointsList.Add (posMove.ToVector2D());
					complexTracker.Update(posMove, 0);
				
					loopCount ++;
					if (loopCount > 150) {
						break;
					}
				}

				complexTracker.Update(posMove, minVertexDistance);
			}

			if (input.GetInputReleased()) {
				pointsList.Clear();

				complexTracker.trackerList.Clear();
			}
		}

		public void Draw(Transform transform) {
			if (pointsList.Count > 0) {
				visuals.Clear();
				visuals.GenerateComplexTrackerMesh(input.GetInputPosition(), complexTracker.trackerList);
				visuals.Draw();
			}
		}
	}
}
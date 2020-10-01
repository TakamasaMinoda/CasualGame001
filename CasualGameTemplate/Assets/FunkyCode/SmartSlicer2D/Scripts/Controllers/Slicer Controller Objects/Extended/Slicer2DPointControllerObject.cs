using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

namespace Slicer2D {
	[System.Serializable]
	public class Slicer2DPointControllerObject : Slicer2DControllerObject {
		public enum SliceRotation {Random, Vertical, Horizontal};

		// Settings
		public SliceRotation sliceRotation = SliceRotation.Random;

		public void Update(Vector2 pos) {
			if (input.GetInputClicked()) {
				PointSlice(pos);
			}
		}

		private void PointSlice(Vector2 pos) {
			float rotation = 0;

			switch (sliceRotation) {	
				case SliceRotation.Random:
					rotation = UnityEngine.Random.Range (0, Mathf.PI * 2);
					break;

				case SliceRotation.Vertical:
					rotation = Mathf.PI / 2f;
					break;

				case SliceRotation.Horizontal:
					rotation = Mathf.PI;
					break;
			}

			List<Slice2D> results = Slicer2D.PointSliceAll (pos.ToVector2D(), rotation, sliceLayer);
			foreach (Slice2D id in results) {
				eventHandler.Perform(id);
			}
		}


		public void Draw(Transform transform) {
			Vector2 pos = input.GetInputPosition();
			
			visuals.Clear();
			visuals.GeneratePointMesh(new Pair2(pos, pos));
			visuals.Draw();
		}
	}
}
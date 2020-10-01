using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Slicer2D {
	public class DemoObjectsCounter : MonoBehaviour {

		void OnRenderObject() {
			Text text = GetComponent<Text> ();
			text.text = "objects " + Slicer2D.GetListCount();
		}
	}
}
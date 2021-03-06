﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slicer2D {
	public class DemoSliceEventBlade : MonoBehaviour {

		void Start () {
			Slicer2D slicer = GetComponent<Slicer2D>();
			if (slicer != null) {
				slicer.AddAnchorResultEvent(sliceEvent);
			}
		}
		
		void sliceEvent (Slice2D slice) {
			foreach(GameObject g in slice.GetGameObjects()) {
				Destroy(g.GetComponent<DemoSpinBlade>());
				Destroy(g.GetComponent<DemoSliceEventBlade>());
				g.transform.parent = null;
			}
		}
	}
}
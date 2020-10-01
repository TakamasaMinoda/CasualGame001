using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Unity Physics Changes
public class PhysicsHelper2D {
	public static void AddForceAtPosition(Rigidbody2D body, Vector2 force,  Vector2 position) {
		body.AddForceAtPosition(force, position);

		//body.AddForceAtPosition(force, body.gameObject.transform.InverseTransformPoint(position));
	}		
}
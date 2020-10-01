using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ComplexCollision {
	public List<Point> collisionSlice = new List<Point>();
	public int collisionCount = 0; // Change?
	
	public bool error = false;

	private Pair2D outside = Pair2D.Zero();
	private Pair2D inside = Pair2D.Zero();
	
	private bool calculated = false;
	private List<Vector2D> points;

	public List<Pair2D> polygonCollisionPairs = new List<Pair2D>();

	public static double precision = 0.0001f;

	public ComplexCollision(Polygon2D polygon, List<Vector2D> slice) {
		bool enterCollision = false;

		Pair2D pair = Pair2D.Zero();

		// Generate correct intersected slice

		List<Vector2D> intersections;

		for(int i = 0; i < slice.Count - 1; i++) {
			pair.A = slice[i];
			pair.B = slice[i + 1];

			intersections = polygon.GetListLineIntersectPoly(pair);

			switch(intersections.Count) {
				case 1:
					collisionCount += 1;

					collisionSlice.Add (new Point(intersections[0], Point.Type.Intersection));

					enterCollision = !enterCollision;

					break;

				case 2:
					collisionCount += 2;

					if (Vector2D.Distance (intersections[0], pair.A) < Vector2D.Distance (intersections[1], pair.A)) {
						collisionSlice.Add (new Point(intersections[0], Point.Type.Intersection));
						collisionSlice.Add (new Point(intersections[1], Point.Type.Intersection));
					} else {
						collisionSlice.Add (new Point(intersections[1], Point.Type.Intersection));
						collisionSlice.Add (new Point(intersections[0], Point.Type.Intersection));
					}
					break;

				case 0:
					break;

				default:
					error = true;

					break;
			}

			if (enterCollision == true) {
				collisionSlice.Add (new Point(pair.B, Point.Type.Inside));
			}
		}

		List<Vector2D> insidePoints = GetPointsInside();

		// Complex Points Generating
		if (insidePoints.Count > 0) {
			///// Outside Points
			Vector2D first = insidePoints.First();
			Vector2D last = insidePoints.Last();

			Vector2D firstOutside = First();
			Vector2D lastOutside = Last();

			outside.A = firstOutside.Copy();
			outside.B = lastOutside.Copy();

			outside.A.Push(Vector2D.Atan2(firstOutside, first), precision);
			outside.B.Push(Vector2D.Atan2(lastOutside, last),  precision);

			///// Inside Points
			
			Vector2D firstInside = First();
			Vector2D lastInside = Last();

			inside.A = firstInside.Copy();
			inside.B = lastInside.Copy();

			inside.A.Push(Vector2D.Atan2(first, firstInside), precision);
			inside.B.Push(Vector2D.Atan2(last, lastInside),  precision);
			
		// Linear Points Generating
		} else { 
			Vector2D first = slice.Last();
			Vector2D last = slice.First();

			Vector2D firstOutside = slice.First();
			Vector2D lastOutside = slice.Last();

			outside.A = firstOutside.Copy();
			outside.B = lastOutside.Copy();

			outside.A.Push(Vector2D.Atan2(firstOutside, first), precision);
			outside.B.Push(Vector2D.Atan2(lastOutside, last),  precision);

			///// Inside Points
			Vector2D firstInside = slice.First();
			Vector2D lastInside = slice.Last();

			inside.A = firstInside.Copy();
			inside.B = lastInside.Copy();

			inside.A.Push(Vector2D.Atan2(first, firstInside), precision);
			inside.B.Push(Vector2D.Atan2(last, lastInside),  precision);
		}

		///// Pairs Collided
		List<Vector2D> slicePoints = GetSlicePoints();

		pair.A = polygon.pointsList.Last();

		foreach(Vector2D p in polygon.pointsList) {
			pair.B = p;

			if (Math2D.LineIntersectSlice (pair, slicePoints) == true) {
				polygonCollisionPairs.Add(pair);
			}

			pair.A = pair.B;
		}

		foreach (Polygon2D hole in polygon.holesList) {
			pair.A = hole.pointsList.Last();

			foreach(Vector2D p in hole.pointsList) {
				pair.B = p;

				if (Math2D.LineIntersectSlice (pair, slicePoints) == true) {
					polygonCollisionPairs.Add(pair);
				}

				pair.A = pair.B;
			}
		}
	}

	public Vector2D First() {
		return(collisionSlice.First().vector);
	}

	public Vector2D Last() {
		return(collisionSlice.Last().vector);
	}

	public void Reverse() {
		collisionSlice.Reverse();

		Vector2D temp = outside.A;
		outside.A = outside.B;
		outside.B = temp;

		temp = inside.A;
		inside.A = inside.B;
		inside.B = temp;

		calculated = false;
	}

	public List<Vector2D> GetPoints() {
		List<Vector2D> points = new List<Vector2D>();
		foreach(Point point in collisionSlice) {
			points.Add(point.vector);
		}
		return(points);
	}

	public List<Vector2D> GetPointsInside() {
		if (calculated == false) {
			points = new List<Vector2D>();
			foreach(Point point in collisionSlice) {
				if (point.collision != Point.Type.Inside) {
					continue;
				}
				points.Add(point.vector);
			}
			calculated = true;
		}
		return(points);
	}

	public List<Vector2D> GetPointsInsidePlus() {
		List<Vector2D> points = GetPointsInside();
		
		points.Insert(0, inside.A);
		points.Add(inside.B);

	
		Vector2D lastPoint = null;
		foreach(Vector2D point in new List<Vector2D>(points)) {
			if (lastPoint != null) {
				if (Vector2D.Distance(point, lastPoint) < precision) {
					points.Remove(lastPoint);
				}
			}
			lastPoint = point;
		}

		return(points);
	}

	public List<Vector2D> GetSlicePoints() {
		List<Vector2D> points = new List<Vector2D>(GetPointsInside());
		points.Insert(0, outside.A);
		points.Add(outside.B);
		return(points);
	}
	
	public class Point {
		public enum Type {Intersection, Inside, Outside};
		public Vector2D vector;
		public Type collision;
		
		public Point(Vector2D pos, Type col) {
			vector = pos;
			collision = col;
		}
	}
}
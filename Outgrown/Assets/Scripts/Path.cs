using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour {
	//	for ease of access
	private Vector3[] points;
	[SerializeField] private float[] gravArr;
	
	//	Constructor takes a parent object to a series of point objects as param
	//	Stores their positions in points, gravities in gravs
	void Start() {
		points = new Vector3[gravArr.Length];
		
		//	 Iterate through points in points, stores locations
		Transform[] positions = GetComponentsInChildren<Transform>();
		int iter = 0;
		foreach(Transform p in positions) {
			if(p.position != transform.position) {
				points[iter] = p.position;
				print(name + " added point " + iter + ": " + points[iter]);
				iter++;
			}
		}
		
		//	troubleshooting for wrong length of points or gravs
		//	for some reason, counts self as child. why.
		if(positions.Length - 1 != gravArr.Length)
			print("ERROR: wrong number of gravities in param for path " + name + "! (" + (positions.Length - 1) + " positions, " + gravArr.Length + " array entries)");
	}	
	
	//	getters
	public Vector3[] getPoints() {
		return points;
	}
	public float[] getGravs() {
		return gravArr;
	}
	public Vector3 getPoint(int num) {
		return points[num];
	}
	public float getGravity(int num) {
		return gravArr[num];
	}
}
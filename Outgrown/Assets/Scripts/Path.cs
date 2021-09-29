using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour {
	//	first two axes are XY plane, third is speed to move
	private List<Vector3> points;
	private List<int> speeds;
	
	//	Constructor takes a parent object to a series of point objects as param
	//	Stores their positions in points, speeds in speeds
	public Path(GameObject pointArr, int[] speedArr) {
		points = new List<Vector3>();
		speeds = new List<int>();
		
		//	 Iterate through points in points, stores locations
		Transform[] positions = pointArr.GetComponentsInChildren<Transform>();
		foreach(Transform p in positions)
			points.Add(p.position);
		
		//	troubleshooting for not enough speeds
		if(positions.Length != speeds.Capacity)
			print("ERROR: wrong number of speeds in Path param!");
		
		//	set speeds
		speeds.AddRange(speedArr);
	}
	
	//	getters
	public List<Vector3> getPoints() {
		return points;
	}
	public List<int> getSpeeds() {
		return speeds;
	}
	public Vector3 getPoint(int num) {
		return points[num];
	}
	public int getSpeed(int num) {
		return speeds[num];
	}
}
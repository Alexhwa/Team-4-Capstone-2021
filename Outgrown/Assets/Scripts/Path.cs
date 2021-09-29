using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour {
	//	first two axes are XY plane, third is speed to move
	private ArrayList points;
	private ArrayList speeds;
	
	//	Constructor takes a parent object to a series of point objects as param
	//	Stores their positions in points, speeds in speeds
	public Path(GameObject pointArr, int[] speedArr) {
		points = new ArrayList();
		speeds = new ArrayList();
		
		//	 Iterate through points in points, stores locations
		for(int i = 0; i < pointArr; i++)
			points.Add(pointArr.GetChild(i));
		
		//	troubleshooting for not enough speeds
		if(pointArr.Length != speeds.Length)
			print("ERROR: wrong number of speeds in Path param!");
		
		//	set speeds
		speeds.AddRange(speedArr);
	}
	
	//	getters
	public Vector2[] getPoints()
		return points;
	public int[] getSpeeds()
		return speeds;
	public Vector3 getPoint(int num)
		return points[num];
	public int getSpeed(int num)
		return speeds[num];
}
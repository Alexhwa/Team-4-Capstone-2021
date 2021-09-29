using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroBunny : MonoBehaviour
{
	//	paths and path accessories
	Path[] paths;
	int iter = 0;
	
	//	NOTE: update this if more/less movement paths used
	[SerializeField] private GameObject points1, points2, points3, points4;
	
	//	current path bunny is moving on
	private int currentPath = 0;
	
	//	if true, stop and wait for player to get close
	//	if false, move around path to next stop location
	private bool waitForPlayer = true;
	
	//	vars for storing current locations/speeds
	private Vector2 nextLocation;
	private float moveSpeed = 0;
	
    void Start(){
		//	init paths
		//	Currently, speeds must be edited manually HERE
		Path path1 = new Path(points1, new int[]{}),
			path2 = new Path(points2, new int[]{}),
			path3 = new Path(points3, new int[]{}),
			path4 = new Path(points4, new int[]{});
		paths = new Path[]{path1, path2, path3, path4};
	}
    
	void Update() {
		if(!waitForPlayer) {
			// move to nextLocation if not there yet
			if(transform.position != nextLocation) {
			}
			else {
				//	choose a new nextLocation
				iter++;
				
				//	if we're all out of checkpoints for this path, stop
				if(paths[currentPath].getCheckpoints().size() >= iter) {
					waitForPlayer = true;
				}
				//	else select next checkpoint to move towards
				else {
					nextLocation = paths[currentPath].getCheckpoint(iter);
					moveSpeed = paths[currentPath].getSpeed(iter);
				}
			}
		}
	}
	
	//	Ideally should only trigger if player enters area while out of checkpoints
	void OnTriggerEnter2D(Collider2D collider) {
		if(waitForPlayer) {
			currentPath++;
			nextLocation = paths[currentPath].getCheckpoint(0);
			moveSpeed = paths[currentPath].getSpeed(0);
			//	ideally there will always be a nextLocation, add troubleshooting case here if needed
			waitForPlayer = false;
			iter = 0;
		}
	}
	
	//	Insert method to remove bunny from level here
}

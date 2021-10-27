using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroBunny : MonoBehaviour
{
	//	paths and path accessories
	private List<GameObject> paths;
	int iter = 0;
	float initialV = 0f;
	[SerializeField] float elapsedTime = 0f;
	[SerializeField] float timeBetweenPaths = 1f;
	float currentGravity = 0f;
	
	//	NOTE: update this if more/less movement paths used
	[SerializeField] private GameObject path1, path2, path3, path4, path5;
	
	//	current path bunny is moving on
	private int currentPath = -1;
	
	//	if true, stop and wait for player to get close
	//	if false, move around path to next stop location
	private bool waitForPlayer = true;
	
	//	vars for storing current locations/speeds
	private Vector3 prevLocation;
	private Vector3 nextLocation;
	
    void Start(){
		//	init paths
		paths = new List<GameObject>();
		paths.Add(path1);
		paths.Add(path2);
		paths.Add(path3);
		paths.Add(path4);
		paths.Add(path5);
		nextLocation = transform.position;
	}
    
	//	Somebody move this to some method that only runs when !waitForPlayer
	void Update() {
		if(!waitForPlayer) {
			// move to nextLocation if not there yet
			if(elapsedTime < timeBetweenPaths)
			{
				float newX = prevLocation.x + (elapsedTime * (nextLocation.x - prevLocation.x));
				float newY = prevLocation.y + (initialV * elapsedTime) + (currentGravity * elapsedTime * elapsedTime/2);
				transform.position = new Vector3(newX, newY, 0);
				
				elapsedTime += Time.deltaTime;
			}
			
			//	if we ARE there, then reset vars and choose new nextLocation
			else {
				iter++;
				
				//	if we're all out of checkpoints for this path, stop
				// if(paths[currentPath].GetComponent<Path>().getPoints().Capacity >= iter) {
				if(paths[currentPath].GetComponent<Path>().getPoints().Length <= iter) {
					print("Bunny out of locations, waiting for player");
					waitForPlayer = true;
				}
				//	else select next point to move towards
				else {
					setVarsForNewPoint(iter);
				}	
			}
		}
	}
	
	//	Ideally should only trigger if player enters area while out of checkpoints
	void OnTriggerEnter2D(Collider2D collider) {
		print("Player triggered bunny");
		if(waitForPlayer) {
			//	move to next path
			currentPath++;
			
			//	if all out of paths, do nothing
			if(paths[currentPath] == null) {return;}
			
			setVarsForNewPoint(0);
			
			//	ideally there will always be a nextLocation, add troubleshooting case here if needed
			waitForPlayer = false;
			iter = 0;
		}
	}
	
	//	set vars for movement to first point in path
	void setVarsForNewPoint(int iter) {
		elapsedTime = 0f;
		prevLocation = nextLocation;
		nextLocation = paths[currentPath].GetComponent<Path>().getPoint(iter);
		currentGravity = paths[currentPath].GetComponent<Path>().getGravity(iter);
		initialV = nextLocation.y - prevLocation.y - (currentGravity/2);
		print("Bunny moving from point " + (iter - 1) + " " + prevLocation + " to point " + iter + " " + nextLocation);
	}
	
	//	Insert method to remove bunny from level here
}

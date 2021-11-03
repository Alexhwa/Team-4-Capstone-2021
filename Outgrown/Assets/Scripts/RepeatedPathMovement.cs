using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

/**	RepeatedPathMovement: A modified, shortened version of IntroBunny's behavior
	that loops around a path instead of going through it once.
	Can be used for linear or bouncing movement along a predefined path.
	Uses Path objects.
*/
public class RepeatedPathMovement : MonoBehaviour
{
	//	paths and path accessories
	[SerializeField] private GameObject path;
	[SerializeField] private bool stopped;
	int iter = 0;
	float initialV = 0f;
	[SerializeField] float elapsedTime = 0f;
	[SerializeField] float timeBetweenPoints = 1f;
	float currentGravity = 0f;
	[SerializeField] private Vector3 prevLocation, nextLocation;
	
	
    // Start is called before the first frame update
    void Start()
    {
        nextLocation = transform.position;
		prevLocation = nextLocation;
    }

    // Update is called once per frame
    void Update()
    {
		if(!stopped) {
			// move to nextLocation if not there yet
			if(elapsedTime < timeBetweenPoints)
			{
				float timeDiv = elapsedTime/timeBetweenPoints;
				float newX = prevLocation.x + (timeDiv * (nextLocation.x - prevLocation.x));
				float newY = prevLocation.y + (initialV * timeDiv) + (currentGravity * timeDiv * timeDiv/2);
				transform.position = new Vector3(newX, newY, 0);
				
				elapsedTime += Time.deltaTime;
			}
			
			//	if we ARE there, then reset vars and choose new nextLocation
			else {
				iter++;
				
				//	if we're all out of points, loop back around
				if(path.GetComponent<Path>().getPoints().Length <= iter) {
					print(name + " out of points, going back to 0");
					iter = 0;
				}
				//	select next point to move towards
				setVarsForNewPoint(iter);	
			}
		}
    }
	
		//	set vars for movement to first point in path
	void setVarsForNewPoint(int iter) {
		elapsedTime = 0f;
		prevLocation = nextLocation;
		nextLocation = path.GetComponent<Path>().getPoint(iter);
		currentGravity = path.GetComponent<Path>().getGravity(iter);
		initialV = nextLocation.y - prevLocation.y - (currentGravity/2);
		print(name + " moving from point " + (iter - 1) + " " + prevLocation + " to point " + iter + " " + nextLocation);
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		print("Player triggered " + name);
		if(stopped) {
			stopped = false;
			setVarsForNewPoint(0);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroBunny : MonoBehaviour
{
	//	each path has a series of checkpoints
	Path[] paths;
	//	if true, stop and wait for player to get close
	//	if false, move around path to next stop location
	private bool allowTrigger;
	private int movePhase;
	//	first two axes are XY, third is movement speed
	private Vector3 nextLocation;
	
    void Start(){
		movePhase = 0;
		moveSpeed = 0;
		// i'm gonna hardcode the paths here probably
		//	unless anyone knows of a better way to 
	}
    
	void Update() {
		/*
		if(!allowTrigger) {
			move towards next location
			if(reach it) {
				change nextLocation to next Vector3 in path[movePhase]
				if(no next location) {
					allowTrigger = true;
				}
			}
		}
		*/
	}
	
	//	Ideally should only trigger if player enters area
	void OnTriggerEnter2D(Collider2D collider) {
		if(allowTrigger) {
			movePhase++;
			nextLocation = paths[movePhase].getCheckpoint(0);
			//	ideally there will always be a nextLocation, add troubleshooting case here if needed
			allowTrigger = false;
		}
	}
	
	//	Insert method to remove bunny from level here
}

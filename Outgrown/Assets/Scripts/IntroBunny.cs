using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroBunny : MonoBehaviour
{
	//	each path has a series of checkpoints
	Path[] paths;
	private bool allowTrigger;
	private int movePhase;
	private int moveSpeed;
	private Vector3 nextLocation;
	
    void Start(){
		movePhase = 0;
		moveSpeed = 0;
	}
    
	void Update() {
		/*
		if(!allowTrigger) {
			move towards next location
			if(reach it || nextLocation == null) {
				change nextLocation to next position in path[movePhase]
				moveSpeed = nextLocation[2];
				if(no next position) {
					allowTrigger = true;
				}
			}
		}
		*/
	}
	
	//	
	void OnTriggerEnter2D(Collider2D collider) {
		if(allowTrigger) {
			movePhase++;
			nextLocation = paths[movePhase].getCheckpoint(0);
			allowTrigger = false;
		}
	}
	
	//	Insert method to remove bunny from level here
}

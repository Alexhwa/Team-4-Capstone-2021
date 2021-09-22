using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroBunny : MonoBehaviour
{
	// Path[] paths;
	int movePhase;
	
    void Start(){
		movePhase = 0;
		
	}
    
	void Update(){
		//	increase Timer by Time.deltaTime * moveSpeed
	}
	
	//	
	void OnTriggerEnter2D(Collider2D collider) {
		movePhase++;
	}
	
	//	Insert method to remove bunny from level here
}

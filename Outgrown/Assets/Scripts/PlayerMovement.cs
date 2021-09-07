using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	//	TODO: test these variables
	float accelFactor = 1;
	float gravity = 9.8f;
	Vector2 veloc = new Vector2();
	float jumpForce = 1;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //  analyze left/right input
        int horzInput = 0;
        horzInput += Convert.ToInt32(Input.GetKey("right"));
        horzInput -= Convert.ToInt32(Input.GetKey("left"));
        //	apply lerp: accel/decel
		veloc.x = Mathf.Lerp(veloc.x, horzInput, Time.deltaTime);
		
		//	jump
		// if(/* we're on the ground or on an edge */) {
		// 	if(Input.GetKey("jump")) {
		// 		veloc.y = jumpForce;
		// 	} else if(veloc.y != 0) {
		// 		veloc.y = 0;
		// 	}
		// }
		// //	fall
		// else {
		// 	veloc.y -= gravity * Time.deltaTime;
		// }
		
		//	hit wall or ceiling
		// if(/* collide with wall */) {
		// 	veloc.x = 0;
		// }
		// if(/* collide with ceiling */ && veloc.y != 0) {
		// 	veloc.y = 0;
		// }
		
        //  update position
		transform.position += new Vector3(veloc.x, veloc.y, 0);
		
    }
}

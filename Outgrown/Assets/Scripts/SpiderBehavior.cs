using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBehavior : MonoBehaviour {
	private enum State {Inactive, Begin, Chase};
	int currentState = State.Inactive;
	bool isClose = false;
	[SerializeField] private int speed;
	[SerializeField] private int breakSlow = 0.9;
	[SerializeField] private int closeBreakSlowMult = 0.8;
	
	void Update()
	{
		switch() {
			case State.Inactive: break;
			case State.Begin: begin(); break;
			case State.Chase: chase(); break;
			default: print("ERROR: Invalid SpiderBehavior State!");
		}
	}
	
	//	Commented because nothing really happens here.
	// void Inactive(){}
	void begin()
	{
		//	TODO: is it just a wait() here then?
	}
	
	//	Behavior of spider is exactly the same in chaseFar and chaseClose
	//	Variable changes and animations take place in onIsClose() now
	void chase()
	{
		
	}
	
	//	when receive signal onActivate
	void onActivate()
	{
		currentState = State.Begin();
	}
	
	void onEndBeginAnimation()
	{
		//	break wall, wait for a while
		//	play shreik animation and sound
	}
	
	//	when receive signal isClose
	//	TODO: could also just use a check to see if the player entered
	void onIsClose()
	{
		if(isClose)
		{
			//	play animation/sound of opening mouth
			breakSlow *= closeBreakSlowMult;
		}
		else
		{
			//	close mouth
			breakSlow /= closeBreakSlowMult;
		}
		isClose = !isClose;
	}
}
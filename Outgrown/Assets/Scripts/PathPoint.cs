using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPoint : MonoBehaviour {
	[SerializeField] float gravity;
	
	public Vector2 getPos() {
		return transform.position;
	}
	public float getGravity() {
		return gravity;
	}
}
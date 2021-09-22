using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour {
	//	first two axes are XY plane, third is speed to move
	private Vector3[] checkpoints;
	
	//	create paths with predetermined positions
	void createCheckpoints(Vector3[] these) {
		checkpoints = these;
	}
	
	public Vector3[] getCheckpoints() {
		return checkpoints;
	}
	
	//	may not really use but who knows
	public Vector3 getCheckpoint(int num) {
		return checkpoints[num];
	}
}
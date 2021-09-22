using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
//    void Start(){}

//    void Update() {}
	
	void OnCollisionEnter2D(Collision2D collision) {
		GameObject that = collision.gameObject;
		var PDscript = that.GetComponent<PlayerDeath>();
		if(PDscript != null) {
			PDscript.damagePlayer(1);
			print("LOL YOU FELL INTO A SPIKES");	
		}
	}
}

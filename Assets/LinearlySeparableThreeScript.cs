using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearlySeparableThreeScript : MonoBehaviour {

	public Transform[] spheres;

	// Use this for initialization
	void Start () {
		foreach(var sphere in spheres) {
			Debug.Log(sphere.position.x);
			Debug.Log(sphere.position.z);
		}	
	}
}

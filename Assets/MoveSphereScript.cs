using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSphereScript : MonoBehaviour {

	[SerializeField]
	private Transform[] sphereTransforms;

	void Start () {
		sphereTransforms [0].position += Vector3.down * 2f;
		sphereTransforms [1].position += Vector3.up * 2f;
		sphereTransforms [2].position += Vector3.forward * 2f;
	}
}

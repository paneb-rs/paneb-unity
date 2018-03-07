using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearlySeparableThreeScript : MonoBehaviour {

	public GameObject[] spheres;

	private void changeSphereMaterial(GameObject sphere, Color color) {
		var material = new Material (Shader.Find ("Specular"));
		material.SetColor ("_Color", color);

		sphere.GetComponent<Renderer>().material = material;
	}

	private void printWeights(System.IntPtr weights) {
		Debug.Log (
			PanebWrapper.classification_weights(weights, 0) + "; " +
			PanebWrapper.classification_weights(weights, 1) + "; " +
			PanebWrapper.classification_weights(weights, 2)
		);
	}

	private void trainModel(System.IntPtr weights) {
		for (int i = 0; i < 9001; ++i) {
			foreach(var sphere in spheres) {
				var transform = sphere.GetComponent<Transform> ();

				var x = transform.position.x;
				var z = transform.position.z;
				var expected = (int) transform.position.y;

				//Debug.Log ("Training [" + x + "," + z + "] = " + expected);
				PanebWrapper.classification_train (weights, x, z, expected);
			}
		}
	}

	private void classifyModel(System.IntPtr weights) {
		foreach (var sphere in spheres) {
			var transform = sphere.GetComponent<Transform> ();

			var x = transform.position.x;
			var z = transform.position.z;

			int actual = PanebWrapper.classification_compute (weights, x, z);
			Debug.Log ("Classified [" + x + "," + z + "] = " + actual);

			if (actual == 1) {
				changeSphereMaterial (sphere, Color.red);
			} else {
				changeSphereMaterial (sphere, Color.blue);
			}
		}
	}

	void Start () {
		var weights = PanebWrapper.classification_create ();

		printWeights (weights);
		trainModel (weights);
		printWeights (weights);
		classifyModel (weights);
	}
}

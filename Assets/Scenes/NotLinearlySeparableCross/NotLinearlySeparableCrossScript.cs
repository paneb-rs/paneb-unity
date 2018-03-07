using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotLinearlySeparableCrossScript : MonoBehaviour {

	public Transform[] spheres;
	public Transform[] whites;

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
		for (int i = 0; i < 20; ++i) {
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

	private void moveWhites(System.IntPtr weights) {
		foreach (var white in whites) {
			var x = white.position.x;
			var z = white.position.z;

			int direction = PanebWrapper.classification_compute (weights, x, z);
			white.position += direction == 1 ? Vector3.up : Vector3.down;
		}
	}

	void Start () {
		var weights = PanebWrapper.classification_create ();

		printWeights (weights);
		trainModel (weights);
		printWeights (weights);
		moveWhites (weights);
	}
}

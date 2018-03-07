using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearlySeparableThreeScript : MonoBehaviour {

	public GameObject[] spheres;

	void Start () {
		var weights = PanebWrapper.classification_create ();

		printWeights (weights);

		for (int i = 0; i < 9001; ++i) {
			foreach(var sphere in spheres) {
				var renderer = sphere.GetComponent<Renderer> ();
				var transform = sphere.GetComponent<Transform> ();

				var x = transform.position.x;
				var y = transform.position.y;
				var expected = (int) transform.position.y;

				PanebWrapper.classification_train (weights, x, y, expected);
			}
		}

		printWeights (weights);
	}

	void printWeights(System.IntPtr weights) {
		Debug.Log (
			PanebWrapper.classification_weights(weights, 0) + "; " +
			PanebWrapper.classification_weights(weights, 1) + "; " +
			PanebWrapper.classification_weights(weights, 2)
		);
	}
}

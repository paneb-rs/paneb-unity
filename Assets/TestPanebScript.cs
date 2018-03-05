using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPanebScript : MonoBehaviour {

	void Start () {
		System.IntPtr model = PanebWrapper.test_model_create ();
		double input = 7.0;
		double output = PanebWrapper.test_model_compute (model, input);
		Debug.Log(output);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotLinearlySeparableCrossScript : MonoBehaviour {

	public Transform[] spheres;
	public Transform[] whites;

    private void classificationPrintWeights(System.IntPtr weights)
    {
        Debug.Log(
            PanebWrapper.classification_weights(weights, 0) + "; " +
            PanebWrapper.classification_weights(weights, 1) + "; " +
            PanebWrapper.classification_weights(weights, 2)
        );
    }

    private void classificationTrainModel(System.IntPtr weights)
    {
        for (int i = 0; i < 9001; ++i)
        {
            foreach (var sphere in spheres)
            {
                var transform = sphere.GetComponent<Transform>();

                var x = transform.position.x;
                var z = transform.position.z;
                var expected = (int)transform.position.y;

                PanebWrapper.classification_train(weights, x, z, expected);
            }
        }
    }

    private void classificationMoveWhites(System.IntPtr weights)
    {
        foreach (var white in whites)
        {
            var x = white.position.x;
            var z = white.position.z;

            int direction = PanebWrapper.classification_compute(weights, x, z);
            white.position += direction == 1 ? Vector3.up : Vector3.down;
        }
    }

    private double[] regressionInputs()
    {
        List<double> inputs = new List<double>();

        foreach (var sphere in spheres)
        {
            var transform = sphere.GetComponent<Transform>();

            var x = transform.position.x;
            var z = transform.position.z;

            inputs.Add(1.0);
            inputs.Add(x);
            inputs.Add(z);
        }

        return inputs.ToArray();
    }

    private double[] regressionOutputs()
    {
        List<double> outputs = new List<double>();

        foreach (var sphere in spheres)
        {
            var transform = sphere.GetComponent<Transform>();

            var y = transform.position.y;
            outputs.Add(y);
        }

        return outputs.ToArray();
    }

    private void regressionMoveWhites(System.IntPtr weights)
    {
        foreach (var white in whites)
        {
            var x = white.position.x;
            var z = white.position.z;

            double result = PanebWrapper.regression_point(weights, 2, new double[] { x, z });
            Debug.Log("[" + x + ", " + z + "] = " + result);
            white.position += Vector3.up * (float)result;
        }
    }

    void Start()
    {
        /// Classification
		var weights = PanebWrapper.classification_create ();
		classificationTrainModel (weights);
		classificationMoveWhites (weights);

		/*/ // Regression
        var inputs = regressionInputs();
        var outputs = regressionOutputs();
        var weights = PanebWrapper.regression_compute(spheres.Length, 3, inputs, spheres.Length, 1, outputs);
        regressionMoveWhites(weights);
        //*/
    }
}

using System.Collections.Generic;
using UnityEngine;

public class Regression : MonoBehaviour {

    public GameObject[] spheres;
    public Transform[] whites;

    private double[] ComputeInputs()
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

    private double[] ComputeOutputs()
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

    private void MoveAxis(System.IntPtr weights)
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
        var inputs = ComputeInputs();
        var outputs = ComputeOutputs();
        var weights = PanebWrapper.regression_compute(spheres.Length, 3, inputs, spheres.Length, 1, outputs);
        MoveAxis(weights);
    }
}

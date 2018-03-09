using UnityEngine;

public class Classification : MonoBehaviour {

    public GameObject[] spheres;
    public Transform[] whites;

    private void PrintWeights(System.IntPtr weights)
    {
        Debug.Log(
            PanebWrapper.classification_weights(weights, 0) + "; " +
            PanebWrapper.classification_weights(weights, 1) + "; " +
            PanebWrapper.classification_weights(weights, 2)
        );
    }

    private void TrainModel(System.IntPtr weights)
    {
        for (int i = 0; i < 300; ++i)
        {
            foreach (var sphere in spheres)
            {
                var transform = sphere.GetComponent<Transform>();

                var x = transform.position.x;
                var z = transform.position.z;
                var expected = (int)transform.position.y;

                PanebWrapper.classification_train(weights, 2, new double[] {x, z}, expected);
            }
        }
    }

    private void MoveAxis(System.IntPtr weights)
    {
        foreach (var white in whites)
        {
            var x = white.position.x;
            var z = white.position.z;

            int direction = PanebWrapper.classification_compute(weights, 2, new double[] {x, z});
            white.position += direction == 1 ? Vector3.up : Vector3.down;
        }
    }

    void Start()
    {
		var weights = PanebWrapper.classification_create (3);
		TrainModel (weights);
		MoveAxis (weights);
    }
}

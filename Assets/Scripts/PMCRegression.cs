using UnityEngine;

public class PMCRegression : MonoBehaviour
{

    public int nbLayers;
    public int[] layers;

    public GameObject[] spheres;
    public Transform[] whites;

    private void TrainModel(System.IntPtr model)
    {
        for (int i = 0; i < 300; ++i)
        {
            foreach (var sphere in spheres)
            {
                var transform = sphere.GetComponent<Transform>();

                var x = transform.position.x;
                var z = transform.position.z;

                var expected = transform.position.y;

                PanebWrapper.pmc_train(nbLayers, layers, model, 2, new double[] { x, z }, 1, new double[] { expected }, 1);
            }
        }
    }

    private void MoveAxis(System.IntPtr model)
    {
        foreach (var white in whites)
        {
            var x = white.position.x;
            var z = white.position.z;

            System.IntPtr outputs = PanebWrapper.pmc_compute(nbLayers, layers, model, 2, new double[] { x, z }, 1);
            double value = PanebWrapper.pmc_value(outputs, 0);

            white.position += Vector3.up * (float)value;
        }
    }

    void Start()
    {
        var model = PanebWrapper.pmc_create(nbLayers, layers);
        TrainModel(model);
        MoveAxis(model);
    }
}
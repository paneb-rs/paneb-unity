using UnityEngine;

public class PMCRegression : MonoBehaviour
{

    private static int NB_LAYERS = 3;
    private static int[] LAYERS = new int[] { 2, 3, 1 };

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

                PanebWrapper.pmc_train(NB_LAYERS, LAYERS, model, 2, new double[] { x, z }, 1, new double[] { expected }, 1);
            }
        }
    }

    private void MoveAxis(System.IntPtr model)
    {
        foreach (var white in whites)
        {
            var x = white.position.x;
            var z = white.position.z;

            System.IntPtr outputs = PanebWrapper.pmc_compute(NB_LAYERS, LAYERS, model, 2, new double[] { x, z }, 1);
            double value = PanebWrapper.pmc_value(outputs, 0);

            white.position += Vector3.up * (float)value;
        }
    }

    void Start()
    {
        var model = PanebWrapper.pmc_create(NB_LAYERS, LAYERS);
        TrainModel(model);
        MoveAxis(model);
    }
}
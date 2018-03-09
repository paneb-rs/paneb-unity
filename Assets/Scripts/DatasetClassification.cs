using System;
using UnityEngine;

public class DatasetClassification : MonoBehaviour {

    public TextAsset csv;

    private double[,] StringArrayToDouble(string[,] array)
    {
        double[,] result = new double[array.GetUpperBound(0) + 1, array.GetUpperBound(1) + 1];

        for (int y = 0; y < array.GetUpperBound(1); y++)
        {
            for (int x = 0; x < array.GetUpperBound(0); x++)
            {
                result[x, y] = Convert.ToDouble(array[x, y]);
            }
        }

        return result;
    }

    private void DebugDoubleArray(double[,] array)
    {
        for (int y = 0; y < array.GetUpperBound(1); y++)
        {
            string line = "";
            for (int x = 0; x < array.GetUpperBound(0); x++)
            {
                line += array[x, y] + ",";
            }
            Debug.Log(line);
        }
    }

    private void TrainModel(System.IntPtr model, double[,] values)
    {
        var nbInputs = values.GetUpperBound(0) - 1;

        for (int y = 0; y < values.GetUpperBound(1); y++)
        {
            double[] inputs = new double[nbInputs];
            int expected = 0;

            for (int x = 0; x < values.GetUpperBound(0); x++) {
                var value = values[x, y];

                if(x == nbInputs)
                {
                    expected = Convert.ToInt32(value);
                }
                else
                {
                    inputs[x] = value;
                }
            }

            PanebWrapper.classification_train(model, inputs.Length, inputs, expected);
        }
    }

    void Start () {
        string[,] csvTraining = CSVReader.SplitCsvGrid(csv.text);
        double[,] trainingValues = StringArrayToDouble(csvTraining);

        var nbWeights = trainingValues.GetUpperBound(0);
        var model = PanebWrapper.classification_create(nbWeights);

        TrainModel(model, trainingValues);
    }
}

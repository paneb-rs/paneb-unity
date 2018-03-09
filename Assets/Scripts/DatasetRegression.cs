using System;
using UnityEngine;

public class DatasetRegression : MonoBehaviour
{

    public TextAsset trainingCsv;
    public TextAsset testCsv;

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

    private System.IntPtr CreateModel(double[,] values)
    {
        var nbInputs = values.GetUpperBound(0) - 1;

        var nbInputRows = values.GetUpperBound(1);
        var nbInputCols = values.GetUpperBound(0);
        double[] inputs = new double[nbInputRows * nbInputCols];
        var inputIterator = 0;

        var nbOutputRows = values.GetUpperBound(1);
        var nbOutputCols = 1;
        double[] outputs = new double[nbOutputRows * nbOutputCols];
        var outputIterator = 0;

        for (int y = 0; y < values.GetUpperBound(1); y++)
        {
            for (int x = 0; x < values.GetUpperBound(0); x++)
            {
                var value = values[x, y];

                if (x == nbInputs)
                {
                    outputs[outputIterator++] = value;
                }
                else
                {
                    inputs[inputIterator++] = value;
                }
            }
        }

        return PanebWrapper.regression_compute(nbInputRows, nbInputCols, inputs, nbOutputRows, nbOutputRows, outputs);
    }

    private void ComputeInputs(System.IntPtr model, double[,] values)
    {
        var nbInputs = values.GetUpperBound(0) - 1;

        for (int y = 0; y < values.GetUpperBound(1); y++)
        {
            double[] inputs = new double[nbInputs];
            double expected = 0.0;

            for (int x = 0; x < values.GetUpperBound(0); x++)
            {
                var value = values[x, y];

                if (x == nbInputs)
                {
                    expected = value;
                }
                else
                {
                    inputs[x] = value;
                }
            }

            double result = PanebWrapper.regression_point(model, inputs.Length, inputs);
            Debug.Log("Expected: " + expected + "; Result = " + result);
        }
    }

    void Start()
    {
        string[,] csvTraining = CSVReader.SplitCsvGrid(trainingCsv.text);
        double[,] trainingValues = StringArrayToDouble(csvTraining);

        string[,] csvTest = CSVReader.SplitCsvGrid(testCsv.text);
        double[,] testValues = StringArrayToDouble(csvTest);

        //var model = CreateModel(trainingValues);
        //ComputeInputs(model, testValues);
    }
}

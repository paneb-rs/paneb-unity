using System.Runtime.InteropServices;

public static class PanebWrapper {
	[DllImport("paneb")] public static extern System.IntPtr classification_create(int size);
	[DllImport("paneb")] public static extern double classification_weights(System.IntPtr weights, int index);
	[DllImport("paneb")] public static extern System.IntPtr classification_train(System.IntPtr weights, int size, double[] inputs, int expected);
	[DllImport("paneb")] public static extern int classification_compute(System.IntPtr weights, int size, double[] inputs);

	[DllImport("paneb")] public static extern System.IntPtr regression_compute(int inputRows, int inputColumns, double[] inputs, int outputRows, int outputColumns, double[] outputs);
    [DllImport("paneb")] public static extern double regression_point(System.IntPtr weights, int inputsSize, double[] inputs);

    [DllImport("paneb")] public static extern System.IntPtr pmc_create(int nbLayers, int[] layers);
    [DllImport("paneb")] public static extern System.IntPtr pmc_train(int nbLayers, int[] layers, System.IntPtr model, int inputsSize, double[] inputs, int outputsSize, double[] outputs, int isRegression);
    [DllImport("paneb")] public static extern System.IntPtr pmc_compute(int nbLayers, int[] layers, System.IntPtr model, int inputsSize, double[] inputs, int isRegression);
    [DllImport("paneb")] public static extern double pmc_value(System.IntPtr values, int index);
}
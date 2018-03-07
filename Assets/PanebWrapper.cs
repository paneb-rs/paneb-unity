using System.Runtime.InteropServices;

public static class PanebWrapper {
	[DllImport("paneb")] public static extern System.IntPtr test_model_create();
	[DllImport("paneb")] public static extern double test_model_compute(System.IntPtr model, double input);

	[DllImport("paneb")] public static extern System.IntPtr classification_create();
	[DllImport("paneb")] public static extern double classification_weights(System.IntPtr weights, int index);
	[DllImport("paneb")] public static extern System.IntPtr classification_train(System.IntPtr weights, double x, double y, int expected);
}
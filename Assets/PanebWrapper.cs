using System.Runtime.InteropServices;

public static class PanebWrapper {
	[DllImport("paneb")] public static extern System.IntPtr test_model_create();
	[DllImport("paneb")] public static extern double test_model_compute(System.IntPtr model, double input);
}
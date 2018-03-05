using System.Runtime.InteropServices;

public static class PanebWrapper {

	[DllImport("paneb")]
	public static extern double super_add(double value);
}
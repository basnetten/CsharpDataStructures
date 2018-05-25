namespace DataStructures.Vectors
{
	public interface IVector
	{
		double Length { get; }
		int    Count  { get; }

		double this[int index] { get; }

		IVector Add(IVector op2);
		IVector Sub(IVector op2);
		double  Mul(IVector op2);
		IVector Mul(double  sca);
		IVector Div(double  sca);
		IVector Neg();

		double  Manhattan();
		double  Euclidian();
		IVector Normalized();
		IVector Truncated(double length);

		IVector FromArray(double[] data);
	}
}
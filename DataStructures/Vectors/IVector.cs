using System.Collections.Generic;

namespace DataStructures.Vectors
{
	public interface IVector : IEnumerable<double>
	{
		/// <summary>
		/// The number of elements in the vector.
		/// </summary>
		int Count { get; }

		/// <summary>
		/// The magnitude/length of the vector.
		/// </summary>
		double Length { get; }

		double this[int index] { get; }

		/**
		 * Operators.
		 */
		IVector Add(IVector op2);
		IVector Sub(IVector op2);
		double  Mul(IVector op2);
		IVector Mul(double  sca);
		IVector Div(double  sca);
		IVector Neg();

		/**
		 * Helper functions.
		 */
		double  Manhattan();
		double  Euclidian();
		IVector Normalized();
		IVector Truncated(double length);

		/// <summary>
		/// Creates a new object with the data specified.
		/// </summary>
		/// <param name="data">The data to put into the vector.</param>
		/// <returns>A new vector object with data.</returns>
		IVector FromArray(params double[] data);

		IVector Build(List<double> data);
	}
}
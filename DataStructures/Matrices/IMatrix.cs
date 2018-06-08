using System.Collections.Generic;

namespace DataStructures.Matrices
{
	public interface IMatrix : IEnumerable<IEnumerable<double>>
	{
		/// <summary>
		/// The number of rows in the matrix.
		/// </summary>
		int RowCount { get; }
		
		/// <summary>
		/// The number of columns in the matrix.
		/// </summary>
		int ColCount { get; }
		
		double this[int row, int col] { get; }

		/*
		 * Operators.
		 */
		IMatrix Add(IMatrix op2);
		IMatrix Sub(IMatrix op2);
		IMatrix Mul(IMatrix op2);
		IMatrix Mul(double sca);

		/// <summary>
		/// Creates a new object with the data specified.
		/// </summary>
		/// <param name="data">The data to put into the matrix.</param>
		/// <returns>A new matrix object with data.</returns>
		IMatrix FromArray(double[,] data);
	}
}
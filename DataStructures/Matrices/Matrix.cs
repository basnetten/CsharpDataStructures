namespace DataStructures.Matrices
{
	public class Matrix : IMatrix
	{
		private double[,] Store { get; }

		public Matrix(int rowCount, int colCount)
		{
			Store = new double[rowCount, colCount];
		}

		public Matrix()
		{
		}

		private Matrix(double[,] data)
		{
			Store = data;
		}

		public int RowCount => Store.GetLength(0);
		public int ColCount => Store.GetLength(1);

		double IMatrix.this[int row, int col] => Store[row, col];

		IMatrix IMatrix.Add(IMatrix         op2)  => Add(op2);
		IMatrix IMatrix.Sub(IMatrix         op2)  => Sub(op2);
		IMatrix IMatrix.Mul(IMatrix         op2)  => Mul(op2);
		IMatrix IMatrix.Mul(double          sca)  => Mul(sca);
		IMatrix IMatrix.FromArray(double[,] data) => FromArray(data);

		public Matrix Add(IMatrix op2)
		{
			double[,] data = new double[RowCount, ColCount];

			for (int row = 0; row < RowCount; row++)
				for (int col = 0; col < ColCount; col++)
					data[row, col] = Store[row, col] + op2[row, col];

			return new Matrix(data);
		}

		public Matrix Sub(IMatrix op2)
		{
			double[,] data = new double[RowCount, ColCount];

			for (int row = 0; row < RowCount; row++)
				for (int col = 0; col < ColCount; col++)
					data[row, col] = Store[row, col] - op2[row, col];

			return new Matrix(data);
		}

		public Matrix Mul(IMatrix op2)
		{
			double[,] data = new double[RowCount, op2.ColCount];

			for (int row = 0; row < RowCount; row++)
				for (int col = 0; col < op2.ColCount; col++)
				{
					double sum = 0d;
					for (int i = 0; i < ColCount; i++)
						sum += Store[row, i] * op2[i, col];
					data[row, col] = sum;
				}

			return new Matrix(data);
		}

		public Matrix Mul(double sca)
		{
			double[,] data = new double[RowCount, ColCount];

			for (int row = 0; row < RowCount; row++)
				for (int col = 0; col < ColCount; col++)
					data[row, col] = Store[row, col] * sca;

			return new Matrix(data);
		}

		public static Matrix operator +(Matrix a, Matrix b) => a.Add(b);
		public static Matrix operator -(Matrix a, Matrix b) => a.Sub(b);
		public static Matrix operator *(Matrix a, Matrix b) => a.Mul(b);
		public static Matrix operator *(Matrix a, double b) => a.Mul(b);
		public static Matrix operator *(double a, Matrix b) => b.Mul(a);

		public Matrix FromArray(double[,] data) => new Matrix(data);
	}
}
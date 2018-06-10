using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using DataStructures.Vectors;

namespace DataStructures.Matrices
{
	public class Matrix : IMatrix
	{
		private int _curAddRow;

		public Matrix(int rowCount, int colCount)
		{
			Store = new double[rowCount, colCount];
		}

		public Matrix()
		{
			Store = new double[0, 0];
		}

		internal Matrix(double[,] data)
		{
			Store = data;
		}

		public int RowCount => Store.GetLength(0);
		public int ColCount => Store.GetLength(1);

		private double[,] Store { get; set; }

		double IMatrix.this[int row, int col] => Store[row, col];

		IMatrix IMatrix.Add(IMatrix         op2)  => Add(op2);
		IMatrix IMatrix.Sub(IMatrix         op2)  => Sub(op2);
		IMatrix IMatrix.Mul(IMatrix         op2)  => Mul(op2);
		IMatrix IMatrix.Mul(double          sca)  => Mul(sca);
		IMatrix IMatrix.FromArray(double[,] data) => FromArray(data);
		IMatrix IMatrix.Transposed()              => Transposed();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public IEnumerator<IEnumerable<double>> GetEnumerator() => new RowEnumerator(this);

		public void Add(params double[] v)
		{
			double[,] store = new double[RowCount + 1, _curAddRow == 0 ? v.Length : ColCount];
			CopyTo(Store, store);
			for (int i = 0; i < v.Length; i++)
				store[_curAddRow, i] = v[i];
			Store = store;
			_curAddRow++;
		}

		public Matrix FromArray(double[,] data) => new Matrix(data);

		public double this[int row, int col] => Store[row, col];

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

		public Matrix Transposed()
		{
			double[,] data = new double[ColCount, RowCount];

			for (int i = 0; i < RowCount; i++)
				for (int j = 0; j < ColCount; j++)
					data[j, i] = Store[i, j];

			return new Matrix(data);
		}

		public override string ToString()
		{
			string str = string.Empty;
			foreach (IEnumerable<double> row in this)
			{
				bool first = true;
				foreach (double d in row)
				{
					if (first) first =  false;
					else str         += ", ";

					str += d.ToString(CultureInfo.InvariantCulture);
				}

				str += Environment.NewLine;
			}

			return str;
		}

		public static Matrix operator +(Matrix a, Matrix b) => a.Add(b);
		public static Matrix operator -(Matrix a, Matrix b) => a.Sub(b);
		public static Matrix operator *(Matrix a, Matrix b) => a.Mul(b);
		public static Matrix operator *(Matrix a, double b) => a.Mul(b);
		public static Matrix operator *(double a, Matrix b) => b.Mul(a);
		public static Vector operator *(Matrix a, Vector b) => (Vector) a.Mul((Matrix) b);
		public static Vector operator *(Vector a, Matrix b) => (Vector) b.Mul((Matrix) a);

		/// <summary>
		/// Convert a Matrix to a Vector. The new vector is converted as if vertical, i.e., the first column.
		/// </summary>
		public static explicit operator Vector(Matrix matrix)
		{
			double[] data = new double[matrix.RowCount];

			for (int i = 0; i < matrix.RowCount; i++)
				data[i] = matrix[i, 0];

			return new Vector(data);
		}

		internal static void CopyTo(double[,] original, double[,] copy)
		{
			for (var i0 = 0; i0 < original.GetLength(0); i0++)
				for (var i1 = 0; i1 < original.GetLength(1); i1++)
					copy[i0, i1] = original[i0, i1];
		}

		public class Row : IEnumerable<double>
		{
			private Matrix _matrix;
			private int    _row;

			public Row(Matrix matrix, int row)
			{
				_matrix = matrix;
				_row    = row;
			}

			public IEnumerator<double> GetEnumerator()
			{
				return new ColEnumerator(this);
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}

			public int Count => _matrix.ColCount;

			public double this[int index] => _matrix[_row, index];
		}

		public class RowEnumerator : IEnumerator<IEnumerable<double>>
		{
			private Matrix _matrix;

			private int                 _nextRow;
			private IEnumerable<double> _current;

			public RowEnumerator(Matrix matrix)
			{
				_matrix  = matrix;
				_nextRow = 0;
				_current = default(Row);
			}

			public void Dispose()
			{
			}

			public bool MoveNext()
			{
				if (_nextRow == _matrix.RowCount) return false;

				_current = new Row(_matrix, _nextRow++);
				return true;
			}

			public void Reset()
			{
				_nextRow = 0;
				_current = default(Row);
			}

			public IEnumerable<double> Current => _current;

			object IEnumerator.Current => Current;
		}

		public class ColEnumerator : IEnumerator<double>
		{
			private Row _row;

			private int    _nextCol;
			private double _current;

			public ColEnumerator(Row row)
			{
				_row     = row;
				_nextCol = 0;
				_current = default(double);
			}

			public void Dispose()
			{
			}

			public bool MoveNext()
			{
				if (_nextCol == _row.Count) return false;

				_current = _row[_nextCol++];
				return true;
			}

			public void Reset()
			{
				_nextCol = 0;
				_current = default(double);
			}

			public double Current => _current;

			object IEnumerator.Current => Current;
		}
	}
}
using System;
using System.Collections;
using System.Collections.Generic;
using DataStructures.Matrices;
using DataStructures.Vectors;
using Xunit;
using Xunit.Abstractions;

namespace DataStructuresTests
{
	public class MatrixTests
	{
		private ITestOutputHelper _out;

		public MatrixTests(ITestOutputHelper @out)
		{
			_out = @out;
		}

		[Fact]
		public void Add()
		{
			IMatrix op1 = new Matrix().FromArray(new double[,]
			{
				{ 1, 2 },
				{ 4, 5 },
			});
			IMatrix op2 = new Matrix().FromArray(new double[,]
			{
				{ 7, 8 },
				{ 9, 10 },
			});
			IMatrix exp = new Matrix().FromArray(new double[,]
			{
				{ 8, 10 },
				{ 13, 15 },
			});

			IMatrix act = op1.Add(op2);

			for (int row = 0; row < exp.RowCount; row++)
				for (int col = 0; col < exp.ColCount; col++)
					Assert.Equal(exp[row, col], act[row, col]);
		}

		[Fact]
		public void Sub()
		{
			IMatrix op1 = new Matrix().FromArray(new double[,]
			{
				{ 1, 2 },
				{ 4, 5 },
			});
			IMatrix op2 = new Matrix().FromArray(new double[,]
			{
				{ 7, 8 },
				{ 9, 10 },
			});
			IMatrix exp = new Matrix().FromArray(new double[,]
			{
				{ -6, -6 },
				{ -5, -5 },
			});

			IMatrix act = op1.Sub(op2);

			for (int row = 0; row < exp.RowCount; row++)
				for (int col = 0; col < exp.ColCount; col++)
					Assert.Equal(exp[row, col], act[row, col]);
		}

		[Fact]
		public void Mul()
		{
			IMatrix op1 = new Matrix().FromArray(new double[,]
			{
				{ 1, 2, 3 },
				{ 4, 5, 6 },
			});
			IMatrix op2 = new Matrix().FromArray(new double[,]
			{
				{ 7, 8 },
				{ 9, 10 },
				{ 11, 12 },
			});
			IMatrix exp = new Matrix().FromArray(new double[,]
			{
				{ 58, 64 },
				{ 139, 154 },
			});

			IMatrix act = op1.Mul(op2);

			for (int row = 0; row < exp.RowCount; row++)
				for (int col = 0; col < exp.ColCount; col++)
					Assert.Equal(exp[row, col], act[row, col]);
		}

		[Fact]
		public void MulSca()
		{
			IMatrix op1 = new Matrix().FromArray(new double[,]
			{
				{ 1, 2 },
				{ 4, 5 },
			});
			double sca = 4;
			IMatrix exp = new Matrix().FromArray(new double[,]
			{
				{ 4, 8 },
				{ 16, 20 },
			});

			IMatrix act = op1.Mul(sca);

			for (int row = 0; row < exp.RowCount; row++)
				for (int col = 0; col < exp.ColCount; col++)
					Assert.Equal(exp[row, col], act[row, col]);
		}

		[Fact]
		public void EnumInit()
		{
			IMatrix m = new Matrix
			{
				{ 3, 4, 5 },
				{ 7, 8, 9 },
			};
			_out.WriteLine(m.ToString());
		}

		#region Transposed_Tests

		[Theory]
		[MemberData(nameof(Transpose_Should_Transpose_When_Square_Data))]
		[MemberData(nameof(Transpose_Should_Transpose_When_Rectangular_Data))]
		public void Transpose_Should_Transpose(IMatrix matrix, double[,] expected)
		{
			IMatrix actual = matrix.Transposed();

			for (int i = 0; i < actual.RowCount; i++)
				for (int j = 0; j < actual.ColCount; j++)
					Assert.Equal(expected[i, j], actual[i, j]);
		}

		public static IEnumerable<object> Transpose_Should_Transpose_When_Square_Data()
		{
			yield return new object[]
			{
				new Matrix
				{
					{ 1, 2, 3 },
					{ 4, 5, 6 },
					{ 7, 8, 9 },
				},
				new double[,]
				{
					{ 1, 4, 7 },
					{ 2, 5, 8 },
					{ 3, 6, 9 },
				}
			};

			yield return new object[]
			{
				new Matrix
				{
					{ 1, 2 },
					{ 4, 5 },
				},
				new double[,]
				{
					{ 1, 4 },
					{ 2, 5 },
				}
			};

			yield return new object[]
			{
				new Matrix
				{
					{ 1 },
				},
				new double[,]
				{
					{ 1 },
				}
			};
		}

		public static IEnumerable<object> Transpose_Should_Transpose_When_Rectangular_Data()
		{
			yield return new object[]
			{
				new Matrix
				{
					{ 1, 2, 3 },
					{ 4, 5, 6 },
				},
				new double[,]
				{
					{ 1, 4 },
					{ 2, 5 },
					{ 3, 6 },
				}
			};

			yield return new object[]
			{
				new Matrix
				{
					{ 1, 2 },
				},
				new double[,]
				{
					{ 1 },
					{ 2 },
				}
			};

			yield return new object[]
			{
				new Matrix
				{
					{ 1 },
				},
				new double[,]
				{
					{ 1 },
				}
			};
		}

		#endregion

		#region Cast_Tests

		[Theory]
		[MemberData(nameof(VectorCast_Should_Cast_Data))]
		public void VectorCast_Should_Cast(Matrix matrix, Vector expected)
		{
			Vector actual = (Vector) matrix;
			
			Assert.Equal((IEnumerable) expected, actual);
		}

		public static IEnumerable<object> VectorCast_Should_Cast_Data()
		{
			yield return new object[]
			{
				new Matrix { 1, 2, 3, 4 },
				new Vector.Builder { 1, 2, 3, 4 }.Get(),
			};
			yield return new object[]
			{
				new Matrix { 0 },
				new Vector.Builder { 0 }.Get(),
			};
			yield return new object[]
			{
				new Matrix
				{
					{ 1, 2, 3 },
					{ 4, 5, 6 },
					{ 7, 8, 9 },
				},
				new Vector.Builder { 1, 4, 7 }.Get(),
			};
		}

		#endregion
	}
}
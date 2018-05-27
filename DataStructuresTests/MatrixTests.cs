using DataStructures.Matrices;
using Xunit;

namespace DataStructuresTests
{
	public class MatrixTests
	{
		[Fact]
		public void Add()
		{
			IMatrix op1 = new Matrix().FromArray(new double[,]
			{
				{1, 2},
				{4, 5},
			});
			IMatrix op2 = new Matrix().FromArray(new double[,]
			{
				{7, 8},
				{9, 10},
			});
			IMatrix exp = new Matrix().FromArray(new double[,]
			{
				{8, 10},
				{13, 15},
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
				{1, 2},
				{4, 5},
			});
			IMatrix op2 = new Matrix().FromArray(new double[,]
			{
				{7, 8},
				{9, 10},
			});
			IMatrix exp = new Matrix().FromArray(new double[,]
			{
				{-6, -6},
				{-5, -5},
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
				{1, 2, 3},
				{4, 5, 6},
			});
			IMatrix op2 = new Matrix().FromArray(new double[,]
			{
				{7, 8},
				{9, 10},
				{11, 12},
			});
			IMatrix exp = new Matrix().FromArray(new double[,]
			{
				{58, 64},
				{139, 154},
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
				{1, 2},
				{4, 5},
			});
			double sca = 4;
			IMatrix exp = new Matrix().FromArray(new double[,]
			{
				{4, 8},
				{16, 20},
			});

			IMatrix act = op1.Mul(sca);

			for (int row = 0; row < exp.RowCount; row++)
				for (int col = 0; col < exp.ColCount; col++)
					Assert.Equal(exp[row, col], act[row, col]);
		}
	}
}
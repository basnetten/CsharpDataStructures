using System.Collections.Generic;
using DataStructures.Matrices;
using DataStructures.Vectors;
using Xunit;
using Xunit.Abstractions;

namespace DataStructuresTests
{
	public class VectorTests
	{
		private ITestOutputHelper _out;

		public VectorTests(ITestOutputHelper @out)
		{
			_out = @out;
		}

		#region IVector_Add_EqualSize

		// ReSharper disable once InconsistentNaming
		public static IEnumerable<object[]> IVector_Add_EqualSize_Data
		{
			get
			{
				yield return new object[] {new Vector(), new[] {10d, 20}, new[] {10d, 20}, new[] {20d, 40}};
				yield return new object[] {new Vector(), new[] {10d, 20}, new[] {-10d, -20}, new[] {0d, 0}};
				yield return new object[] {new Vector(), new[] {-10d, -20}, new[] {-10d, -20}, new[] {-20d, -40}};
				yield return new object[] {new Vector(), new[] {-10d, -20}, new[] {0d, 0}, new[] {-10d, -20}};
				yield return new object[] {new Vector(), new[] {0d, 0}, new[] {-10d, -20}, new[] {-10d, -20}};
			}
		}

		[Theory]
		[MemberData(nameof(IVector_Add_EqualSize_Data))]
		public void IVector_Add_EqualSize(IVector factory, double[] op1Data, double[] op2Data, double[] expected)
		{
			IVector v1 = factory.FromArray(op1Data);
			IVector v2 = factory.FromArray(op2Data);

			IVector actual = v1.Add(v2);

			_out.WriteLine("Length of data");
			Assert.Equal(expected.Length, actual.Count);
			for (int i = 0; i < expected.Length; i++)
			{
				_out.WriteLine($"ex: {expected[i]}, ac: {actual[i]}");
				Assert.Equal(expected[i], actual[i]);
			}
		}

		#endregion

		#region IVector_Sub_EqualSize

		// ReSharper disable once InconsistentNaming
		public static IEnumerable<object[]> IVector_Sub_EqualSize_Data
		{
			get
			{
				yield return new object[] {new Vector(), new[] {10d, 20}, new[] {10d, 20}, new[] {0d, 0}};
				yield return new object[] {new Vector(), new[] {10d, 20}, new[] {-10d, -20}, new[] {20d, 40}};
				yield return new object[] {new Vector(), new[] {-10d, -20}, new[] {-10d, -20}, new[] {0d, 0}};
				yield return new object[] {new Vector(), new[] {-10d, -20}, new[] {0d, 0}, new[] {-10d, -20}};
				yield return new object[] {new Vector(), new[] {0d, 0}, new[] {-10d, -20}, new[] {10d, 20}};
			}
		}

		[Theory]
		[MemberData(nameof(IVector_Sub_EqualSize_Data))]
		public void IVector_Sub_EqualSize(IVector factory, double[] op1Data, double[] op2Data, double[] expected)
		{
			IVector v1 = factory.FromArray(op1Data);
			IVector v2 = factory.FromArray(op2Data);

			IVector actual = v1.Sub(v2);

			_out.WriteLine("Length of data");
			Assert.Equal(expected.Length, actual.Count);
			for (int i = 0; i < expected.Length; i++)
			{
				_out.WriteLine($"ex: {expected[i]}, ac: {actual[i]}");
				Assert.Equal(expected[i], actual[i]);
			}
		}

		#endregion

		#region IVector_DotProduct_EqualSize

		// ReSharper disable once InconsistentNaming
		public static IEnumerable<object[]> IVector_DotProduct_EqualSize_Data
		{
			get
			{
				yield return new object[] {new Vector(), new[] {10d, 20}, new[] {10d, 20}, 500};
				yield return new object[] {new Vector(), new[] {10d, 20}, new[] {-10d, -20}, -500};
				yield return new object[] {new Vector(), new[] {-10d, -20}, new[] {-10d, -20}, 500};
				yield return new object[] {new Vector(), new[] {-10d, -20}, new[] {0d, 0}, 0};
				yield return new object[] {new Vector(), new[] {0d, 0}, new[] {-10d, -20}, 0};
				yield return new object[] {new Vector(), new[] {10d, 20, 2}, new[] {10d, 20, 5}, 510};
			}
		}

		[Theory]
		[MemberData(nameof(IVector_DotProduct_EqualSize_Data))]
		public void IVector_DotProduct_EqualSize(IVector factory, double[] op1Data, double[] op2Data, double expected)
		{
			IVector v1 = factory.FromArray(op1Data);
			IVector v2 = factory.FromArray(op2Data);

			double actual = v1.Mul(v2);

			Assert.Equal(expected, actual);
		}

		#endregion

		#region IVector_Div_Scalar

		// ReSharper disable once InconsistentNaming
		public static IEnumerable<object[]> IVector_Div_Scalar_Data
		{
			get
			{
				yield return new object[] {new Vector(), new[] {10d, 20}, 10, new[] {1d, 2}};
				yield return new object[] {new Vector(), new[] {10d, 20}, -10, new[] {-1d, -2}};
				yield return new object[] {new Vector(), new[] {-10d, -20}, -10, new[] {1d, 2}};
				yield return new object[] {new Vector(), new[] {-10d, -20}, 2, new[] {-5d, -10}};
				yield return new object[] {new Vector(), new[] {0d, 0}, 10, new[] {0d, 0}};
			}
		}

		[Theory]
		[MemberData(nameof(IVector_Div_Scalar_Data))]
		public void IVector_Div_Scalar(IVector factory, double[] op1Data, double op2Data, double[] expected)
		{
			IVector v1 = factory.FromArray(op1Data);

			IVector actual = v1.Div(op2Data);

			_out.WriteLine("Length of data");
			Assert.Equal(expected.Length, actual.Count);
			for (int i = 0; i < expected.Length; i++)
			{
				_out.WriteLine($"ex: {expected[i]}, ac: {actual[i]}");
				Assert.Equal(expected[i], actual[i]);
			}
		}

		#endregion

		#region IVector_Mul_Scalar

		// ReSharper disable once InconsistentNaming
		public static IEnumerable<object[]> IVector_Mul_Scalar_Data
		{
			get
			{
				yield return new object[] {new Vector(), new[] {10d, 20}, 10, new[] {100d, 200}};
				yield return new object[] {new Vector(), new[] {10d, 20}, -10, new[] {-100d, -200}};
				yield return new object[] {new Vector(), new[] {-10d, -20}, -10, new[] {100d, 200}};
				yield return new object[] {new Vector(), new[] {-10d, -20}, 2, new[] {-20d, -40}};
				yield return new object[] {new Vector(), new[] {0d, 0}, 10, new[] {0d, 0}};
			}
		}

		[Theory]
		[MemberData(nameof(IVector_Mul_Scalar_Data))]
		public void IVector_Mul_Scalar(IVector factory, double[] op1Data, double op2Data, double[] expected)
		{
			IVector v1 = factory.FromArray(op1Data);

			IVector actual = v1.Mul(op2Data);

			_out.WriteLine("Length of data");
			Assert.Equal(expected.Length, actual.Count);
			for (int i = 0; i < expected.Length; i++)
			{
				_out.WriteLine($"ex: {expected[i]}, ac: {actual[i]}");
				Assert.Equal(expected[i], actual[i]);
			}
		}

		#endregion

		#region IVector_Neg

		// ReSharper disable once InconsistentNaming
		public static IEnumerable<object[]> IVector_Neg_Data
		{
			get
			{
				yield return new object[] {new Vector(), new[] {10d, 20}, new[] {-10d, -20}};
				yield return new object[] {new Vector(), new[] {-10d, -20}, new[] {10d, 20}};
				yield return new object[] {new Vector(), new[] {-10d, 20}, new[] {10d, -20}};
				yield return new object[] {new Vector(), new[] {0d, 0}, new[] {0d, 0}};
			}
		}

		[Theory]
		[MemberData(nameof(IVector_Neg_Data))]
		public void IVector_Neg(IVector factory, double[] op1Data, double[] expected)
		{
			IVector v1 = factory.FromArray(op1Data);

			IVector actual = v1.Neg();

			_out.WriteLine("Length of data");
			Assert.Equal(expected.Length, actual.Count);
			for (int i = 0; i < expected.Length; i++)
			{
				_out.WriteLine($"ex: {expected[i]}, ac: {actual[i]}");
				Assert.Equal(expected[i], actual[i]);
			}
		}

		#endregion

		#region Cast_Tests

		[Theory]
		[MemberData(nameof(VectorCast_Should_Cast_Data))]
		public void VectorCast_Should_Cast(Vector vector, Matrix expected)
		{
			Matrix actual = vector;

			_out.WriteLine(actual.ToString());

			for (int i = 0; i < expected.RowCount; i++)
				for (int j = 0; j < expected.ColCount; j++)
					Assert.Equal(expected[i, j], actual[i, j]);
		}

		public static IEnumerable<object> VectorCast_Should_Cast_Data()
		{
			yield return new object[]
			{
				new Vector.Builder { 1, 2, 3, 4 }.Get(),
				new Matrix { 1, 2, 3, 4 },
			};
			yield return new object[]
			{
				new Vector.Builder { 0 }.Get(),
				new Matrix { 0 },
			};
			yield return new object[]
			{
				new Vector.Builder { 1, 4, 7 }.Get(),
				new Matrix { 1, 4, 7, },
			};
		}

		#endregion
	}
}
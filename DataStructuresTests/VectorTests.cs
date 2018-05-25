using System.Collections.Generic;
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

		#region IVector_Add_EqualSize

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
	}
}
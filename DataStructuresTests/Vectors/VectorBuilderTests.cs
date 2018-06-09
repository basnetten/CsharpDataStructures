using System.Collections.Generic;
using DataStructures.Vectors;
using Xunit;

namespace DataStructuresTests.Vectors
{
	public class VectorBuilderTests
	{
		private static List<object> Builders => new List<object>
		{
			new Vector.Builder(),
		};

		private static List<object> Data => new List<object>
		{
			new List<double> { 1, 2, 3, 4 },
		};

		[Theory]
		[MemberData(nameof(GetMemberData))]
		public void Add_Should_Add(IVectorBuilder x, List<double> data)
		{
			x.Set(data);
			IVector v = x.Get();
			Assert.Equal(data, v);
		}

		public static IEnumerable<object[]> GetMemberData() => Combiner(Builders, Data);

		public static IEnumerable<object[]> Combiner(List<object> outer, List<object> inner)
		{
			foreach (object i in outer)
				foreach (object j in inner)
					yield return new[] { i, j };
		}
	}
}
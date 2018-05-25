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

		[Fact]
		public void Test()
		{
			Vector v = new Vector(new double[] {10, 15});
			IVector iv = v;

			_out.WriteLine($"{(v + v) * 2}");
			_out.WriteLine($"{v + new Vector(1)}");
		}
	}
}
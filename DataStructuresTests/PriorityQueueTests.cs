using System;
using System.Linq;
using System.Threading;
using DataStructures;
using Xunit;
using Xunit.Abstractions;

namespace DataStructuresTests
{
	public class PriorityQueueTests
	{
		private ITestOutputHelper _out;

		public PriorityQueueTests(ITestOutputHelper @out)
		{
			_out = @out;
		}

		[Theory]
		[InlineData(new[] {92, 47, 21, 20, 12, 45, 63, 61, 17, 55, 37, 25, 64, 83, 73})]
		public void BuildHeap_Should_BuildHeap_WhenCorrectArray(int[] values)
		{
			PriorityQueue<int> queue = new PriorityQueue<int>();

			queue.BuildHeap(values);

			var list = values.ToList();
			list.Sort();
			for (int i = 0; i < values.Length; i++)
				Assert.Equal(list[i], queue.DeleteMin());
		}

		[Theory]
		[InlineData(-5)]
		[InlineData(0)]
		[InlineData(5)]
		public void Insert_Should_InsertValueCorrectly_When_Empty(int value)
		{
			PriorityQueue<int> queue = new PriorityQueue<int>();

			queue.Insert(value);

			Assert.Equal(value, queue.DeleteMin());
		}

		[Theory]
		[InlineData(new[] {1, 2, 3, 4})]
		[InlineData(new[] {1, 2, 3, 4, -1, 45})]
		[InlineData(new[] {-5})]
		[InlineData(new int[] { })]
		public void Count_Should_ReturnCountCorrectly_WhenAdding(int[] values)
		{
			PriorityQueue<int> queue = new PriorityQueue<int>();
			foreach (var value in values)
			{
				queue.Insert(value);
			}

			Assert.Equal(values.Length, queue.Count);
		}
	}
}
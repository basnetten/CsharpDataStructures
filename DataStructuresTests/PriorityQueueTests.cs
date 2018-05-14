using System.Linq;
using System.Runtime.InteropServices;
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

		[Theory]
		[InlineData(3, 2)]
		[InlineData(4, 3)]
		[InlineData(7, 3)]
		[InlineData(8, 4)]
		[InlineData(15, 4)]
		[InlineData(16, 5)]
		public void Depth_Should_ReturnCorrectDepth_When_IncreasingCapacity(int count, int expectedDepth)
		{
			PriorityQueue<int> queue = new PriorityQueue<int>();
			for (int i = 0; i < count; i++)
				queue.Insert(i);

			int actual = queue.CapacityDepth;

			Assert.Equal(expectedDepth, actual);
		}

		[Theory]
		[InlineData(3, 3)]
		[InlineData(4, 7)]
		[InlineData(7, 7)]
		[InlineData(8, 15)]
		[InlineData(15, 15)]
		[InlineData(16, 31)]
		public void Capacity_Should_ReturnCapacity_When_IncreasingCapacity(int count, int expectedCapacity)
		{
			PriorityQueue<int> queue = new PriorityQueue<int>();
			for (int i = 0; i < count; i++)
				queue.Insert(i);

			int actual = queue.Capacity;

			Assert.Equal(expectedCapacity, actual);
		}

		[Theory]
		[InlineData(-5)]
		[InlineData(0)]
		[InlineData(5)]
		public void Insert_Should_InsertValueCorrectly_When_Empty(int value)
		{
			PriorityQueue<int> queue = new PriorityQueue<int>();

			queue.Insert(value);

			Assert.Equal(value, queue.Dequeue());
		}

		[Theory]
		[InlineData(new[] {1, 2, 3, 4})]
		[InlineData(new[] {1, 2, 3, 4, -1, 45})]
		[InlineData(new[] {-5})]
		public void Peek_Should_ReturnMinvalue_When_Available(int[] values)
		{
			PriorityQueue<int> queue = new PriorityQueue<int>();
			for (int i = 0; i < values.Length; i++)
				queue.Insert(values[i]);
			int expectedValue = values.Min();
			
			int actual = queue.Peek();

			Assert.Equal(expectedValue, actual);
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
				Assert.Equal(list[i], queue.Dequeue());
		}
	}
}
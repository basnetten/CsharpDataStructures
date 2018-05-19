using System;
using DataStructures;
using Xunit;
using Xunit.Abstractions;

namespace DataStructuresTests
{
	public class DisjointSetTests
	{
		private ITestOutputHelper _out;

		public DisjointSetTests(ITestOutputHelper @out)
		{
			_out = @out;
		}

		[Theory]
		[InlineData(new[] {4, 5, 6}, 0)]
		[InlineData(new[] {4, 5, 6}, 1)]
		[InlineData(new[] {4, 5, 6}, 2)]
		public void Indexer_Should_ReturnValueOfArray_When_IndexValid(int[] array, int index)
		{
			DisjointSet disjointSet = new DisjointSet(array);
			int actual = disjointSet[index];
			int expected = array[index];
			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData(new int[0], 0)]
		[InlineData(new[] {4, 5, 6}, -1)]
		[InlineData(new[] {4, 5, 6}, 3)]
		public void Indexer_Should_ThrowIndexOutOfRange_When_IndexInvalid(int[] array, int index)
		{
			DisjointSet disjointSet = new DisjointSet(array);
			Assert.Throws(typeof(IndexOutOfRangeException), () =>
			{
				int i = disjointSet[index];
			});
		}

		[Theory]
		[InlineData(10, 0)]
		[InlineData(10, 5)]
		[InlineData(10, 9)]
		[InlineData(1000, 999)]
		[InlineData(1000, 0)]
		[InlineData(1000, 42)]
		public void Find_Should_FindSelf_When_SetEmpty(int size, int i)
		{
			DisjointSet disjointSet = new DisjointSet(size);
			int actual = disjointSet.Find(i);
			Assert.Equal(i, actual);
		}

		[Theory]
		[InlineData(new[] {-1, -1, -1, -1}, new[] {-1, -1, -1, -1}, 1)]
		[InlineData(new[] {-3, 0, 1, -1}, new[] {-3, 0, 0, -1}, 2)]
		[InlineData(new[] {3, 0, 1, -4}, new[] {3, 3, 1, -4}, 1)]
		[InlineData(new[] {3, 0, 1, -4}, new[] {3, 3, 3, -4}, 2)]
		public void Find_ShouldCompressPath_When_Possible(int[] start, int[] expected, int find)
		{
			DisjointSet disjointSet = new DisjointSet(start);
			disjointSet.Find(find);

			for (int i = 0; i < start.Length; i++)
			{
				Assert.Equal(expected[i], disjointSet[i]);
			}
		}

		[Theory]
		[InlineData(new[] {-3, 0, 1}, 1, 0)]
		[InlineData(new[] {-3, 0, 1}, 2, 0)]
		[InlineData(new[] {1, -2, -1}, 0, 1)]
		public void Find_ShouldReturnRoot_When_SelfNotRoot(int[] start, int findIndex, int expected)
		{
			DisjointSet disjointSet = new DisjointSet(start);
			int actual = disjointSet.Find(findIndex);
			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData(10, 0, 1)]
		[InlineData(10, 1, 0)]
		[InlineData(10, 0, 9)]
		[InlineData(10, 9, 0)]
		[InlineData(10, 5, 9)]
		[InlineData(1000, 0, 999)]
		[InlineData(1000, 999, 0)]
		public void Union_Should_UniteRoots_When_SetEmpty(int size, int i1, int i2)
		{
			DisjointSet disjointSet = new DisjointSet(size);
			disjointSet.Union(i1, i2);

			int actualDepthI1 = disjointSet[i1];
			int actualParentI2 = disjointSet[i2];

			Assert.Equal(-2, actualDepthI1);
			Assert.Equal(i1, actualParentI2);
		}

		[Theory]
		[InlineData(new[] {-3, -1}, 0, 1)]
		[InlineData(new[] {-3, -2}, 0, 1)]
		[InlineData(new[] {-3, -3}, 0, 1)]
		[InlineData(new[] {-3, -4}, 1, 0)]
		[InlineData(new[] {-3, -4, 0, 1, -7}, 4, 2)]
		[InlineData(new[] {-3, -4, 0, 1, -7}, 3, 2)]
		[InlineData(new[] {-3, -4, 0, 1, -7}, 3, 0)]
		public void Union_ShouldUniteBySize_When_RootsDifferentSize(int[] start, int bigRoot, int smallRoot)
		{
			DisjointSet disjointSet = new DisjointSet(start);
			int bigRootRoot = disjointSet.Find(bigRoot);

			disjointSet.Union(bigRoot, smallRoot);
			int actual = disjointSet.Find(smallRoot);

			Assert.Equal(bigRootRoot, actual);
		}

		[Theory]
		[InlineData(new[] {-1, -1}, 0, 1)]
		[InlineData(new[] {-1, -1}, 1, 0)]
		[InlineData(new[] {-2, -2, 0, 1}, 2, 3)]
		public void Union_Should_Unite_When_RootsSameSize(int[] start, int root1, int root2)
		{
			DisjointSet disjointSet = new DisjointSet(start);
			int root1Root = disjointSet.Find(root1);

			disjointSet.Union(root1Root, root2);
			int actual = disjointSet.Find(root2);

			Assert.Equal(root1Root, actual);
		}

		[Theory]
		[InlineData(new[] {-3, 0, 0}, 1, 2)]
		[InlineData(new[] {2, 0, -3}, 1, 0)]
		[InlineData(new[] {2, 0, -5, 0, 1}, 3, 4)]
		public void Union_Should_NotUnite_When_RootsSame(int[] start, int root1, int root2)
		{
			DisjointSet disjointSet = new DisjointSet(start);
			int root1Root = disjointSet.Find(root1);
			int root2Root = disjointSet.Find(root2);

			disjointSet.Union(root1Root, root2Root);
			int actual1 = disjointSet.Find(root1Root);
			int actual2 = disjointSet.Find(root2Root);

			Assert.Equal(root1Root, actual1);
			Assert.Equal(root2Root, actual2);
		}

		[Theory]
		[InlineData(10, 5, 1)]
		[InlineData(10, 5, 2)]
		[InlineData(10, 5, 3)]
		[InlineData(10, 5, 4)]
		[InlineData(10, 5, 5)]
		[InlineData(10, 5, 6)]
		[InlineData(10, 5, 7)]
		[InlineData(10, 5, 8)]
		[InlineData(10, 5, 9)]
		[InlineData(1000, 5, 9)]
		public void Union_Should_HandleRandomInput(int size, int unionCount, int seed)
		{
			DisjointSet disjointSet = new DisjointSet(size);
			Random r = new Random(seed);

			for (int i = 0; i < unionCount; i++)
			{
				int index1 = (int) (r.NextDouble() * size);
				int index2 = (int) (r.NextDouble() * size);

				_out.WriteLine($"Union({index1}, {index2})");
				_out.WriteLine($"On: {Environment.NewLine}{disjointSet}");

				disjointSet.Union(index1, index2);
			}

			Assert.True(true);
		}
	}
}
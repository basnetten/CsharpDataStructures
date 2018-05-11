using DataStructures;
using Xunit;

namespace DataStructuresTests
{
	public class DisjointSetTests
	{
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
	}
}
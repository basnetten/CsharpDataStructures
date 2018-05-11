using System;

namespace DataStructures
{
	/// <summary>
	/// DisjointSet is a data structure that can be used to unite sets of indices.
	/// </summary>
	public class DisjointSet
	{	
		/// <summary>
		/// The value that every index in the array is going to have initially. This is usually set to -1 for Union by
		/// size.
		/// </summary>
		private const int InitialValue = -1;

		/// <summary>
		/// Initialize a disjoint set with a certain size. It will be filled with the <see cref="InitialValue"/> at
		/// every index.
		/// </summary>
		/// <param name="size">The size that the array of the disjoint set will be.</param>
		public DisjointSet(int size)
		{
			Array = new int[size];

			for (int i = 0; i < size; i++)
				Array[i] = InitialValue;
		}

		/// <summary>
		/// Initialize a disjoint set with an array. The array will not be copied. The values will also not be checked,
		/// so use with care. This is mainly intended for testing.
		/// </summary>
		/// <param name="array">The array reference to use.</param>
		public DisjointSet(int[] array)
		{
			Array = array;
		}

		/// <summary>
		/// Indexer to access the underlying array of the disjoint set. This is mainly intended for testing, but should
		/// function fine in any situation.
		/// </summary>
		public int this[int index] => Array[index];

		/// <summary>
		/// The actual array with the associated values.
		/// </summary>
		private int[] Array { get; }

		/// <summary>
		/// Find the root index of the given index. If the given index is a root, it will simply be returned.
		/// </summary>
		/// <remarks>
		///	This Find function implements path compression. This should improve performance.
		/// </remarks>
		/// <param name="i">Index to find the root of.</param>
		/// <returns>Index of the root of <paramref name="i"/>.</returns>
		public int Find(int i)
		{
			// If < 0 this is a root, therefore return i.
			if (Array[i] < 0) return i;
			// Else find the root of the index that is the root of i. Set the result in the array for path compression.
			return Array[i] = Find(Array[i]);
		}

		/// <summary>
		/// Execute a union on two indices.
		/// </summary>
		/// <remarks>
		/// The indices do not have to be roots, <see cref="Find"/> will be called on both. If the root is the same for
		/// both indices, nothing happens. Uses Union by Size to determine which index will be the root.
		/// </remarks>
		public void Union(int i, int j)
		{
			int rootI = Find(i);
			int rootJ = Find(j);

			// J has the bigger tree, so we flip the roots.
			if (Array[rootI] > Array[rootJ])
			{
				int tmp = rootI;
				rootI = rootJ;
				rootJ = tmp;
			}

			// Increase the size of rootI by the size of rootJ. Then set the root of rootJ to rootI.
			Array[rootI] += Array[rootJ];
			Array[rootJ] = rootI;
		}

		/**
		 * A simple ToString for debugging.
		 */
		public override string ToString()
		{
			// First line, indices.
			string str = String.Empty;
			for (int i = 0; i < Array.Length; i++)
			{
				if (i != 0) str += ", ";
				str += $"{i:0000}";
			}

			str += Environment.NewLine;

			// Second line, values.
			for (int i = 0; i < Array.Length; i++)
			{
				if (i != 0) str += ", ";
				if (Array[i] < 0)
					str += $"{Array[i]:000}";
				else
					str += $"{Array[i]:0000}";
			}

			return str;
		}
	}
}
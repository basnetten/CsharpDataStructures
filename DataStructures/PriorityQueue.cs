using System;

namespace DataStructures
{
	public class PriorityQueue<T> where T : IComparable<T>
	{
		/// <summary>
		/// The default initial depth that the priority queue starts at.
		/// </summary>
		public const int InitialDepth = 2;

		/// <summary>
		/// Sets all values to the default.
		/// </summary>
		public PriorityQueue()
		{
			Store = new T[ArraySizeFromDepth(InitialDepth)];

			Count = 0;
			CapacityDepth = InitialDepth;
		}

		/// <summary>
		/// The current number of elements in the queue.
		/// </summary>
		public int Count { get; private set; }
		
		/// <summary>
		/// The current depth of the queue. The depth is directly related to <see cref="Capacity"/>.
		/// </summary>
		public int CapacityDepth { get; private set; }

		/// <summary>
		/// The current capacity of the queue. The capacity is directly related to <see cref="CapacityDepth"/>.
		/// </summary>
		public int Capacity => Store.Length - 1;

		/// <summary>
		/// The array with that holds the data.
		/// </summary>
		private T[] Store { get; set; }

		/// <summary>
		/// Insert a new value into the queue.
		/// </summary>
		/// <param name="data">The data to be inserted into the queue.</param>
		public void Insert(T data)
		{
			// Increase the Count of elements, and resize if necessary.
			Count += 1;
			if (Count > Capacity) Resize(++CapacityDepth);
			
			// Place the data in the 0th element. This is in preparation for the PercolateUp algorithm.
			Store[0] = data;
			
			// Move the data into the correct position.
			PercolateUp(Count);
		}

		/// <summary>
		/// Retrieve the first item in the queue.
		/// </summary>
		/// <returns>Minimal value in the queue.</returns>
		public T Peek()
		{
			return Store[1];
		}
		
		/// <summary>
		/// Retrieve and delete the first item in the queue.
		/// </summary>
		/// <returns>Minimal value in the queue.</returns>
		public T Dequeue()
		{
			// Retrieve minimal value.
			T result = Store[1];

			// Move the last item in the queue to the start.
			Store[1] = Store[Count];
			Count--;

			// Percolate the ex-last item in the queue down.
			PercolateDown(1);

			return result;
		}

		/// <summary>
		/// Build the heap from a random set of data.
		/// </summary>
		/// <remarks>
		/// The <see cref="Capacity"/> and <see cref="CapacityDepth"/> are changed to the minimal possible to still
		/// fit the data array.
		/// </remarks>
		/// <param name="data">Array with data to be inserted into the heap.</param>
		public void BuildHeap(T[] data)
		{
			// First the count is set.
			Count = data.Length;
			
			// Then the smallest depth that can fit the data is determined.
			CapacityDepth = DepthFromArraySize(data.Length + 1);
			
			// The depth is then used to resize the store, and the data copied to it.
			Resize(CapacityDepth);
			data.CopyTo(Store, 1);

			// Finally every index is percolated down to its correct position in the queue.
			for (int i = Capacity / 2; i > 0; i--)
				PercolateDown(i);
		}

		/// <summary>
		/// Percolates the index down (towards higher indices) to where its parent is bigger than it, and its
		/// children smaller.
		/// </summary>
		private void PercolateDown(int index)
		{
			// The data that needs to percolate down.
			T data = Store[index];

			// While still in range of the store, loop through the children.
			for (int childIndex = 0; index * 2 <= Count; index = childIndex)
			{
				childIndex = index * 2;
				
				// If the childindex is less than count, there is still a child in the right node too, so check
				// which of the children has the smaller value. If the right child is smaller, the child index
				// is increased to point to this child.
				if (childIndex < Count && Store[childIndex].CompareTo(Store[childIndex + 1]) > 0)
					childIndex++;

				// Check if the smallest child is smaller than the data, if so, move the child up and continue
				// the loop, otherwise break.
				if (data.CompareTo(Store[childIndex]) > 0)
					Store[index] = Store[childIndex];
				else
					break;
			}
			// Note that when the end of the store (Count) is reached, the index will be in the last position
			// and the data will be inserted there.

			// Finally assign the data to the correct position, indicated by the index state.
			Store[index] = data;
		}

		private void PercolateUp(int index)
		{
			for (; Store[0].CompareTo(Store[index / 2]) < 0; index /= 2)
				Store[index] = Store[index / 2];
			Store[index] = Store[0];
			Store[0] = default(T);
		}

		public override string ToString()
		{
			string str = String.Empty;

			for (var i = 0; i < Store.Length; i++)
			{
				if (i != 0) str += ", ";
				str += Store[i].ToString();
			}

			return str;
		}

		private void Resize(int depth)
		{
			T[] resizedArray = new T[ArraySizeFromDepth(depth)];
			Store.CopyTo(resizedArray, 0);
			Store = resizedArray;
		}

		/// <summary>
		/// Determines the size that the store needs to be to support depth.
		/// </summary>
		private static int ArraySizeFromDepth(int depth) => (int) Math.Pow(2, depth);

		/// <summary>
		/// Determines the smallest depth required to fit a dataset of size.
		/// </summary>
		private static int DepthFromArraySize(int size) => (int) Math.Log(size, 2);
	}
}
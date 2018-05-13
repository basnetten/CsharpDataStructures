using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Policy;

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
			Depth = InitialDepth;
		}

		/// <summary>
		/// The current depth of the queue.
		/// </summary>
		public int Depth { get; private set; }
		
		/// <summary>
		/// The current number of elements in the queue.
		/// </summary>
		public int Count { get; private set; }

		/// <summary>
		/// The current capacity of the queue.
		/// </summary>
		public int Capacity => Store.Length - 1;

		/// <summary>
		/// The array with that holds the data.
		/// </summary>
		private T[] Store { get; set; }

		/// <summary>
		/// Retrieve and delete the first item in the queue.
		/// </summary>
		/// <returns>Minimal value in the queue.</returns>
		public T DeleteMin()
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

		public void Insert(T data)
		{
			Count += 1;
			if (Count >= Capacity) Resize(Depth++);
			Store[0] = data;
			PercolateUp(Count);
		}

		public void BuildHeap(T[] data)
		{
			Count = data.Length;
			Depth = DepthFromArraySize(data.Length + 1);
			Resize(Depth);
			data.CopyTo(Store, 1);

			for (int i = Capacity / 2; i > 0; i--)
				PercolateDown(i);
		}

		private void PercolateDown(int index)
		{
			T data = Store[index];

			for (int childIndex = 0; index * 2 <= Count; index = childIndex)
			{
				childIndex = index * 2;
				if (childIndex < Count && Store[childIndex].CompareTo(Store[childIndex + 1]) > 0)
					childIndex++;

				if (data.CompareTo(Store[childIndex]) > 0)
					Store[index] = Store[childIndex];
				else
					break;
			}

			Store[index] = data;
		}

		private void PercolateUp(int index)
		{
			for (; Store[index].CompareTo(Store[index / 2]) < 0; index /= 2)
				Store[index] = Store[index / 2];
			Store[index] = Store[0];
			Store[0] = default(T);
		}

		private void PercolateDown(T data)
		{
			int index = Count;
			while (true)
			{
				if (index != 0 && data.CompareTo(Store[index / 2]) < 0)
				{
					Store[index] = Store[index / 2];
					index = index / 2;
					continue;
				}

				Store[index] = data;
				break;
			}
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

		private static int ArraySizeFromDepth(int depth) => (int) Math.Pow(2, depth);

		private static int DepthFromArraySize(int size) => (int) Math.Log(size, 2);
	}
}
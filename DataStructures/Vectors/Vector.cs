using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace DataStructures.Vectors
{
	public class Vector : IVector
	{
		/// <summary>
		/// String for the error message when the count of two vertices are not equal.
		/// </summary>
		private const string ExCountNotEqual = "Vector Counts are not equal.";

		private const string ExIndexOutOfRange = "Attempting to add too many elements to vector.";

		private bool _lengthCached;

		private double _cachedLength;

		private int _i;

		/// <summary>
		/// The array with the data for this vector.
		/// </summary>
		private double[] Store { get; }

		public Vector(int count)
		{
			Store = new double[count];
		}

		private Vector(double[] data)
		{
			Store = data;
		}

		/// <summary>
		/// Initializes an empty vector, with 0 elements. Intended for testing.
		/// </summary>
		public Vector()
		{
			Store = new double[0];
		}

		/// <inheritdoc />
		public double Length
		{
			get
			{
				if (_lengthCached) return _cachedLength;

				_lengthCached = true;
				return _cachedLength = Euclidian();
			}
		}

		public int Count => Store.Length;

		public double this[int index] => Store[index];

		/**
		 * IVector implementation.
		 */
		IVector IVector.Add(IVector op2)           => Add(op2);
		IVector IVector.Sub(IVector op2)           => Sub(op2);
		double IVector. Mul(IVector op2)           => DotProduct(op2);
		IVector IVector.Mul(double  sca)           => Mul(sca);
		IVector IVector.Div(double  sca)           => Div(sca);
		IVector IVector.Neg()                      => Neg();
		IVector IVector.Normalized()               => Normalized();
		IVector IVector.Truncated(double   length) => Truncated(length);
		IVector IVector.FromArray(double[] data)   => FromArray(data);

		/**
		 * IVector : IEnumerable implementation.
		 */
		IEnumerator IEnumerable.                GetEnumerator() => new Enumerator(this);
		IEnumerator<double> IEnumerable<double>.GetEnumerator() => new Enumerator(this);

		public void Add(double item)
		{
			AssertIndexInRange(Count, _i);
			Store[_i++] = item;
		}

		public Vector Add(IVector op2)
		{
			AssertEqualCounts(this, op2);

			double[] data = new double[Count];
			for (int i = 0; i < Count; i++)
				data[i] = Store[i] + op2[i];

			return new Vector(data);
		}

		public Vector Sub(IVector op2)
		{
			AssertEqualCounts(this, op2);

			double[] data = new double[Count];
			for (int i = 0; i < Count; i++)
				data[i] = Store[i] - op2[i];

			return new Vector(data);
		}

		public Vector Mul(double sca)
		{
			double[] data = new double[Count];
			for (int i = 0; i < Count; i++)
				data[i] = Store[i] * sca;

			return new Vector(data);
		}

		public Vector Div(double sca)
		{
			double[] data = new double[Count];
			for (int i = 0; i < Count; i++)
				data[i] = Store[i] / sca;

			return new Vector(data);
		}

		public Vector Neg()
		{
			double[] data = new double[Count];
			for (int i = 0; i < Count; i++)
				data[i] = -Store[i];

			return new Vector(data);
		}

		public double Manhattan()
		{
			double sum = 0d;
			foreach (var value in Store)
				sum += value;

			return sum;
		}

		public double Euclidian()
		{
			double sum = 0d;
			foreach (var value in Store)
			{
				sum += value * value;
			}

			return Math.Sqrt(sum);
		}

		public Vector Normalized()
		{
			return Length.CompareTo(0) == 0 ? this : Div(Length);
		}

		public Vector Truncated(double length)
		{
			return Length < length ? this : Normalized().Mul(length);
		}

		public double DotProduct(IVector op2)
		{
			AssertEqualCounts(this, op2);

			double sum = 0d;
			for (int i = 0; i < Count; i++)
			{
				sum += Store[i] * op2[i];
			}

			return sum;
		}

		public static Vector FromArray(double[] data)
		{
			return new Vector(data);
		}

		public override string ToString()
		{
			return $"Vector{Count}<{string.Join(", ", Store)}>";
		}

		/**
		 * Operators.
		 */
		public static Vector operator -(Vector a)           => a.Neg();
		public static Vector operator +(Vector a, Vector b) => a.Add(b);
		public static Vector operator -(Vector a, Vector b) => a.Sub(b);
		public static double operator *(Vector a, Vector b) => a.DotProduct(b);
		public static Vector operator *(Vector a, double b) => a.Mul(b);
		public static Vector operator *(double a, Vector b) => b.Mul(a);
		public static Vector operator /(Vector a, double b) => a.Div(b);
		public static Vector operator /(double a, Vector b) => b.Div(a);

		public struct Enumerator : IEnumerator<double>
		{
			private readonly Vector _vector;

			private int    _nextIndex;
			private double _current;

			internal Enumerator(Vector vector)
			{
				_vector    = vector;
				_nextIndex = 0;
				_current   = default(double);
			}

			public void Dispose()
			{
			}

			public bool MoveNext()
			{
				if (_nextIndex == _vector.Count) return false;

				_current = _vector[_nextIndex];
				_nextIndex++;
				return true;
			}

			public void Reset()
			{
				_nextIndex = 0;
				_current   = default(double);
			}

			public double Current => _current;

			object IEnumerator.Current => Current;
		}

		/// <summary>
		/// Tests the two vectors for equal counts.
		/// </summary>
		/// <remarks>
		/// Only gets called in debug mode, and is meant to provide a more meaningful error message when debugging but
		/// not slow down the program at all in release.
		/// </remarks>
		/// <exception cref="ArgumentNullException">When either vector is null.</exception>
		/// <exception cref="ArgumentException">When the counts of the vectors are not equal.</exception>
		[Conditional("DEBUG")]
		private static void AssertEqualCounts(IVector op1, IVector op2)
		{
			if (op1 == null) throw new ArgumentNullException(nameof(op1));
			if (op2 == null) throw new ArgumentNullException(nameof(op2));

			if (op1.Count != op2.Count) throw new ArgumentException(ExCountNotEqual, nameof(op2));
		}

		[Conditional("DEBUG")]
		private static void AssertIndexInRange(int count, int index)
		{
			if (index >= count) throw new IndexOutOfRangeException(ExIndexOutOfRange);
		}
	}
}
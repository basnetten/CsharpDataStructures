using System;

namespace DataStructures.Vectors
{
	public class Vector : IVector
	{
		private double[] Store { get; }

		public Vector(double[] data)
		{
			Store = data;
		}

		public Vector(int count)
		{
			Store = new double[count];
		}

		public double Length => Euclidian();
		public int Count => Store.Length;

		public double this[int index] => Store[index];

		IVector IVector.Add(IVector op2) => Add(op2);
		IVector IVector.Sub(IVector op2) => Sub(op2);
		double IVector.Mul(IVector op2) => DotProduct(op2);
		IVector IVector.Mul(double sca) => Mul(sca);
		IVector IVector.Div(double sca) => Div(sca);
		IVector IVector.Neg() => Neg();
		IVector IVector.Normalized() => Normalized();
		IVector IVector.Truncated(double length) => Truncated(length);

		public Vector Add(IVector op2)
		{
#if DEBUG
			if (Count != op2.Count) throw new ArgumentException("Count != op2.Count.");
#endif

			double[] data = new double[Count];
			for (int i = 0; i < Count; i++)
				data[i] = Store[i] + op2[i];

			return new Vector(data);
		}

		public Vector Sub(IVector op2)
		{
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
			// ReSharper disable once CompareOfFloatsByEqualityOperator
			return Length == 0 ? this : Div(Length);
		}

		public Vector Truncated(double length)
		{
			return Length < length ? this : Normalized().Mul(length);
		}

		public override string ToString()
		{
			return $"Vector{Count}<{string.Join(", ", Store)}>";
		}

		public double DotProduct(IVector vector)
		{
			double sum = 0d;
			for (int i = 0; i < Count; i++)
			{
				sum += Store[i] * vector[i];
			}

			return sum;
		}

		/**
		 * Operators.
		 */
		public static Vector operator -(Vector a) => a.Neg();
		public static Vector operator +(Vector a, Vector b) => a.Add(b);
		public static Vector operator -(Vector a, Vector b) => a.Sub(b);
		public static double operator *(Vector a, Vector b) => a.DotProduct(b);
		public static Vector operator *(Vector a, double b) => a.Mul(b);
		public static Vector operator *(double a, Vector b) => b.Mul(a);
		public static Vector operator /(Vector a, double b) => a.Div(b);
		public static Vector operator /(double a, Vector b) => b.Div(a);
	}
}
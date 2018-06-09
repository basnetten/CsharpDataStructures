using System.Collections;
using System.Collections.Generic;

namespace DataStructures.Vectors
{
	public interface IVectorBuilder : IEnumerable<double>
	{
		void    Add(double       item);
		void    Set(List<double> data);
		IVector Get();
	}

	public class VectorBuilder<T> : IVectorBuilder where T : IVector, new()
	{
		private List<double> _vectorData;

		public VectorBuilder() => _vectorData = new List<double>();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public IEnumerator<double> GetEnumerator() => _vectorData.GetEnumerator();

		public void    Add(double       item) => _vectorData.Add(item);
		public void    Set(List<double> data) => _vectorData = data;
		public IVector Get()                  => new T().Build(_vectorData);
	}
}
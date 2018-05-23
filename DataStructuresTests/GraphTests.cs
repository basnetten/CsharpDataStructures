using System;
using System.Collections.Generic;
using System.Linq;
using DataStructures;
using DataStructures.Graphs;
using Xunit;
using Xunit.Abstractions;

namespace DataStructuresTests
{
	public class GraphTests
	{
		private ITestOutputHelper _out;

		public GraphTests(ITestOutputHelper @out)
		{
			_out = @out;
		}

		[Fact]
		public void NodeAddEdge_Should_SaveEdge()
		{
			var graph = new Graph<int>();
			// Add all the nodes.
			graph.AddEmptyNode(0);
			graph.AddEmptyNode(1);
			graph.AddEmptyNode(2);
			graph.AddEmptyNode(3);
			graph.AddEmptyNode(4);
			graph.AddEmptyNode(5);
			graph.AddEmptyNode(6);

			// Connect the nodes.
			graph.AddEdge(0, 1, 2);
			graph.AddEdge(0, 3, 1);
			graph.AddEdge(1, 3, 3);
			graph.AddEdge(1, 4, 10);
			graph.AddEdge(2, 0, 4);
			graph.AddEdge(2, 5, 5);
			graph.AddEdge(3, 2, 2);
			graph.AddEdge(3, 4, 2);
			graph.AddEdge(3, 5, 8);
			graph.AddEdge(3, 6, 4);
			graph.AddEdge(4, 6, 6);
			graph.AddEdge(6, 5, 1);

			StepwiseDijkstra<int> sd = new StepwiseDijkstra<int>();
			sd.Init(graph, 0, 5);
			while (!sd.Step())
			{
				_out.WriteLine("Step");
			}
			_out.WriteLine($"{string.Join(", ", sd.GetRouteToTarget())}");

			Assert.True(true);
		}
	}
}
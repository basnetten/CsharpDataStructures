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

			// State variables.
			var queue = new PriorityQueue<Path>();
			var processed = new HashSet<int>();
			var bestPaths = new Dictionary<int, Path>();

			// Insert the first node. The search will start here.
			queue.Insert(new Path(new List<int> {0}, 0));

			// Run until the queue is empty.
			while (queue.Count > 0)
			{
				// The path being evaluated this loop.
				Path pathUnderEval = queue.Dequeue();

				// If the node that this path leads to has already been processed, it already has the best possible
				// path stored. Therefore the algoritm can skip processing this node.
				if (processed.Contains(pathUnderEval.LastOfRoute)) continue;
				processed.Add(pathUnderEval.LastOfRoute);

				// Retrieve the node from the Graph instance.
				Graph<int>.Node node = graph.Nodes[pathUnderEval.LastOfRoute];
				foreach (var edge in node.Edges)
				{
					// Calculate the length to reach the destination of this edge.
					double edgeLength = pathUnderEval.Length + edge.Cost;

					// If the destination of this edge is known, and the stored length is less (better) than the
					// length of the edge being evaluated now, this edge can be skipped. If it is not known, or this
					// path is better, the algorithm continues.
					if (bestPaths.ContainsKey(edge.Destination) && bestPaths[edge.Destination].Length < edgeLength) continue;

					// Copy the route of the path and add the destination of the edge to it..
					var edgeRoute = new List<int>(pathUnderEval.Route);
					edgeRoute.Add(edge.Destination);

					// Initialize the better path, and store and queue it.
					var betterPath = new Path(edgeRoute, edgeLength);
					bestPaths[edge.Destination] = betterPath;
					queue.Insert(betterPath);
				}
			}

			_out.WriteLine($"{graph}");
			_out.WriteLine($"{string.Join(Environment.NewLine, bestPaths)}");

			Assert.True(true);
		}

		class Path : IComparable<Path>
		{
			public Path(List<int> route, double length)
			{
				var tmp = new int[route.Count];
				route.CopyTo(tmp);
				Route = tmp.ToList();

				Length = length;
			}

			public List<int> Route { get; }
			public int LastOfRoute => Route.Last();
			public double Length { get; set; }

			public int CompareTo(Path other)
			{
				if (Length < other.Length) return -1;
				if (Length > other.Length) return 1;
				return 0;
			}

			public override string ToString()
			{
				return $"{Length} : {string.Join(", ", Route)}";
			}
		}
	}
}
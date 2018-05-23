using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.Graphs
{
	public class StepwiseDijkstra<TNodeId>
	{
		public Graph<TNodeId> Graph { get; set; }

		private PriorityQueue<Path> Queue { get; set; }
		private Dictionary<TNodeId, Path> BestPaths { get; set; }

		private TNodeId OriginNodeId { get; set; }
		private TNodeId TargetNodeId { get; set; }

		public void Init(Graph<TNodeId> graph, TNodeId originNodeId, TNodeId targetNodeId)
		{
			Graph = graph;
			OriginNodeId = originNodeId;
			TargetNodeId = targetNodeId;

			Queue = new PriorityQueue<Path>();
			BestPaths = new Dictionary<TNodeId, Path>();

			// Insert the first node. The search will start here.
			var pathToOrigin = new Path(new List<TNodeId> {OriginNodeId}, 0);
			Queue.Insert(pathToOrigin);
			BestPaths.Add(OriginNodeId, pathToOrigin);
		}

		public bool Step()
		{
			if (Queue.Count == 0) return true;

			// The path being evaluated this loop.
			Path pathUnderEval = Queue.Dequeue();

			// TODO If the node that this path leads to has already been processed, it already has the best possible
			// path stored. Therefore the algoritm can skip processing this node.
			TNodeId pathNodeId = pathUnderEval.LastOfRoute;
			if (BestPaths.ContainsKey(pathNodeId) &&
			    pathUnderEval.CompareTo(BestPaths[pathNodeId]) > 0) return false;

			// Retrieve the node from the Graph instance.
			Graph<TNodeId>.Node node = Graph.Nodes[pathNodeId];
			foreach (Graph<TNodeId>.Edge edge in node.Edges)
			{
				// Calculate the length to reach the destination of this edge.
				double edgeLength = pathUnderEval.Length + edge.Cost;

				// If the destination of this edge is known, and the stored length is less (better) than the
				// length of the edge being evaluated now, this edge can be skipped. If it is not known, or this
				// path is better, the algorithm continues.
				if (BestPaths.ContainsKey(edge.Destination) &&
				    BestPaths[edge.Destination].Length < edgeLength) continue;

				// Copy the route of the path and add the destination of the edge to it..
				var edgeRoute = new List<TNodeId>(pathUnderEval.Route);
				edgeRoute.Add(edge.Destination);

				// Initialize the better path, and store and queue it.
				var betterPath = new Path(edgeRoute, edgeLength);
				BestPaths[edge.Destination] = betterPath;
				Queue.Insert(betterPath);
			}

			Console.WriteLine($"{Graph}");
			Console.WriteLine($"{string.Join(Environment.NewLine, BestPaths)}");

			return false;
		}

		public List<TNodeId> GetRouteToTarget() => BestPaths[TargetNodeId].Route;

		class Path : IComparable<Path>
		{
			public Path(List<TNodeId> route, double length)
			{
				var tmp = new TNodeId[route.Count];
				route.CopyTo(tmp);
				Route = tmp.ToList();

				Length = length;
			}

			public List<TNodeId> Route { get; }
			public TNodeId LastOfRoute => Route.Last();
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
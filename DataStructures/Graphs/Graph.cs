using System;
using System.Collections.Generic;

namespace DataStructures.Graphs
{
	/// <summary>
	/// A simple graph class, usable for all kinds of purposes.
	/// </summary>
	/// <typeparam name="TNodeId">Identifier for the nodes.</typeparam>
	public class Graph<TNodeId>
	{
		/// <summary>
		/// Intitialize a new Graph with an empty list of nodes.
		/// </summary>
		public Graph()
		{
			Nodes = new Dictionary<TNodeId, Node>();
		}

		/// <summary>
		/// The nodes that are in this Graph. They are indexed by <see cref="TNodeId"/>.
		/// </summary>
		public Dictionary<TNodeId, Node> Nodes { get; }

		/// <summary>
		/// Add an existing node to this Graph.
		/// </summary>
		public void AddNode(TNodeId identifier, Node node) => Nodes.Add(identifier, node);

		/// <summary>
		/// Add a new node to the Graph with identifier as index. Calls the default Node constructor.
		/// </summary>
		public void AddEmptyNode(TNodeId identifier) => Nodes.Add(identifier, new Node());

		/// <summary>
		/// Add a new edge to the Graph. Edges are stored in the individual Nodes.
		/// </summary>
		public void AddEdge(TNodeId source, TNodeId destination, double cost = 0d)
			=> Nodes[source].AddEdge(new Edge(destination, cost));

		public override string ToString()
		{
			return string.Join(Environment.NewLine, Nodes);
		}

		/**
		 * Subclasses.
		 */
		/// <summary>
		/// A simple Edge class to accompany the <see cref="Graph{TNodeId}"/> class.
		/// </summary>
		public class Edge
		{
			/// <summary>
			/// Initialize a new Edge with destination and optionally a cost.
			/// </summary>
			public Edge(TNodeId destination, double cost = 0d)
			{
				Destination = destination;
				Cost = cost;
			}

			/// <summary>
			/// The destination index of this edge.
			/// </summary>
			public TNodeId Destination { get; }

			/// <summary>
			/// The cost to get to the destination over this edge.
			/// </summary>
			public double Cost { get; set; }

			public override string ToString()
			{
				// ReSharper disable once CompareOfFloatsByEqualityOperator
				return $"({Destination}{(Cost == 0 ? "" : $", {Cost}")})";
			}
		}

		/// <summary>
		/// A simple Node class to accompany the <see cref="Graph{TNodeId}"/> class.
		/// </summary>
		public class Node
		{
			/// <summary>
			/// Initialize a Node with an empty list of Edges.
			/// </summary>
			public Node()
			{
				Edges = new List<Edge>();
			}

			/// <summary>
			/// The edges that can be travelled from this node.
			/// </summary>
			public List<Edge> Edges { get; }

			/// <summary>
			/// Add an existing Edge to this node.
			/// </summary>
			public void AddEdge(Edge edge) => Edges.Add(edge);

			public override string ToString()
			{
				return $"Node: [{string.Join(", ", Edges)}]";
			}
		}
	}
}
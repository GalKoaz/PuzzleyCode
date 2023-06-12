using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dijkstra : MonoBehaviour
{
    [SerializeField] static int ZeroInit = 0;
    // FindPath method calculates the shortest paths from the startNode to all other nodes in the graph
    // NodeType: type of node in the graph
    // graph: the input graph that implements IWGraph<NodeType> interface
    // startNode: the starting node for pathfinding
    // Returns a dictionary of NodeType keys and NodeType values representing the shortest paths
// FindPath method calculates the shortest paths from the startNode to all other nodes in the graph
public static Dictionary<NodeType, NodeType> FindPath<NodeType>(IWGraph<NodeType> graph, NodeType startNode)
{
    // Initialize dictionaries for weights (shortest distances) and path (shortest paths)
    Dictionary<NodeType, int> weights = new Dictionary<NodeType, int>();
    Dictionary<NodeType, NodeType> path = new Dictionary<NodeType, NodeType>();

    // Create a priority queue to store nodes and their weights
    var queue = new PriorityQueue<NodeType, int>();

    // Set the weight of the startNode to zero and add it to the queue
    weights[startNode] = ZeroInit;
    queue.Enqueue(startNode, ZeroInit);

    // Continue processing nodes in the queue until it is empty
    while (queue.Count != ZeroInit)
    {
        NodeType currentNode;
        int currentNodeWeight;

        // Dequeue the node with the highest priority (lowest weight)
        queue.Dequeue(out currentNode, out currentNodeWeight);

        // Iterate through the neighbors of the current node
        foreach (var neighbor in graph.Neighbors(currentNode))
        {
            // Calculate the weight through the current node to the neighbor
            int weightThroughCurrentNode = currentNodeWeight + graph.getW(neighbor);

            // If the neighbor is not in the weights dictionary or if the weight through the current node
            // is less than the existing weight
            bool CorrNeighborWeight = !weights.ContainsKey(neighbor) || weightThroughCurrentNode < weights[neighbor];
            if (CorrNeighborWeight)
            {
                // Update the weight for the neighbor
                weights[neighbor] = weightThroughCurrentNode;

                // Update the path for the neighbor, setting the current node as the previous node in the path
                path[neighbor] = currentNode;

                // Enqueue the neighbor with the updated weight
                queue.Enqueue(neighbor, weightThroughCurrentNode);
            }
        }
    }

    // Return the dictionary containing the shortest paths
    return path;
}

// GetPath method returns the shortest path between the startNode and endNode in the graph
    // NodeType: type of node in the graph
    // graph: the input graph that implements IWGraph<NodeType> interface
    // startNode: the starting node for pathfinding
    // endNode: the target node for pathfinding
    // Returns a list of NodeType elements representing the shortest path from startNode to endNode
    public static List<NodeType> GetPath<NodeType>(IWGraph<NodeType> graph, NodeType startNode, NodeType endNode)
    {
        List<NodeType> ans_path = new List<NodeType>();
        Dictionary<NodeType, NodeType> path = FindPath<NodeType>(graph, startNode);
        if (path.ContainsKey(endNode))
        {
            NodeType td = endNode;
            ans_path.Insert(ZeroInit, td);

            while (!ans_path[0].Equals(startNode))
            {
                td = path[td];
                ans_path.Insert(ZeroInit, td);
            }
        }
        return ans_path;
    }
}

// PriorityQueue class implements a generic priority queue with TKey and TValue
// TKey: type of the key of the element in the priority queue
// TValue: type of the value of the element in the priority queue, must be comparable
public class PriorityQueue<TKey, TValue> where TValue : System.IComparable<TValue>
{
    // sortedDict is a sorted dictionary containing the TKey elements
    // mapped to their corresponding TValue priority
    private SortedDictionary<TValue, Queue<TKey>> sortedDict = new SortedDictionary<TValue, Queue<TKey>>();

    public int Count { get; private set; }

    public void Enqueue(TKey key, TValue value)
    {
        if (!sortedDict.ContainsKey(value))
            sortedDict[value] = new Queue<TKey>();

        sortedDict[value].Enqueue(key);
        Count++;
    }

    // Dequeue method to remove and return the element with the highest priority (smallest value) from the priority queue
    public bool Dequeue(out TKey key, out TValue value)
    {
        // Check if the priority queue is empty
        if (Count == 0)
        {
            // If empty, set the output key and value to their default values and return false
            key = default(TKey);
            value = default(TValue);
            return false;
        }

        // If not empty, get the first key-value pair in the sorted dictionary (i.e., the pair with the smallest value)
        var firstPair = sortedDict.First();
        value = firstPair.Key;  // Set the output value to the smallest value in the dictionary
        var queue = firstPair.Value;  // Get the queue of keys associated with the smallest value

        key = queue.Dequeue();  // Dequeue the key with the highest priority (smallest value) from the queue and
                                // set it as the output key

        // If the queue is empty after dequeuing the key, remove the value entry from the sorted dictionary
        if (queue.Count == 0)
            sortedDict.Remove(value);

        // Decrement the Count property to reflect the removed key-value pair
        Count--;

        // Return true to indicate that the dequeue operation was successful
        return true;
    }

}

using System.Collections;
/*using System.Collections.Generic;
using UnityEngine;

public class WaypointPathfinding2 : MonoBehaviour
{

    public Transform startWaypointTransform;
    public Transform targetWaypointTransfrom;
    [Space]
    public List<Transform> waypointsTransforms;
    private Node startWaypoint;
    private Node targetWaypoint;

    private List<Node> waypoints;
    private List<Node> path;

    void Start()
    {
        waypoints = new List<Node>();
        path = new List<Node>();
        foreach (var i in waypointsTransforms)
        {
            waypoints.Add(new Node(i.position));
        }

        Vector3 startPos = startWaypointTransform.position;
        Vector3 endPos = targetWaypointTransfrom.position;
        waypoints.Add(new Node(startPos))
        waypoints.Add(new Node(endPos));


        startWaypoint =
        targetWaypoint = waypoints[waypoints.Count - 1];

        // Find and display the path
        FindPath();
        DisplayPath();
    }

    void FindPath()
    {
        // Implement A* algorithm to find the path
        // This part is specific to your implementation of the A* algorithm

        // Open and closed sets for A*
        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();

        openSet.Add(new Node(startWaypoint.position));

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];

            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].FCost < currentNode.FCost || (openSet[i].FCost == currentNode.FCost && openSet[i].hCost < currentNode.hCost))
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetWaypoint)
            {
                // Path found, reconstruct and set path
                path = RetracePath(startWaypoint.transform, currentNode);
                return;
            }

            foreach (Node neighbor in GetNeighbors(currentNode))
            {
                if (closedSet.Contains(neighbor))
                    continue;

                int newCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
                if (newCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.gCost = newCostToNeighbor;
                    neighbor.hCost = GetDistance(neighbor, targetWaypoint);
                    neighbor.parent = currentNode;

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }
    }

    List<Node> RetracePath(Transform start, Node end)
    {
        List<Node> path = new List<Node>();
        Node currentNode = end;

        while (currentNode != null)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        return path;
    }

    List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        foreach (Node waypoint in waypoints)
        {
            if (Vector3.Distance(node.position, waypoint.position) < 1.5f) // Adjust this threshold based on your scenario
            {
                neighbors.Add(waypoint);
            }
        }

        return neighbors;
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        return Mathf.RoundToInt(Vector3.Distance(nodeA.position, nodeB.position));
    }

    void DisplayPath()
    {
        // Draw lines to visualize the path
        for (int i = 0; i < path.Count - 1; i++)
        {
            Debug.DrawLine(path[i].position, path[i + 1].position, Color.red, 5f);
        }
    }

    class Node
    {
        public Transform transform;
        public int gCost;
        public int hCost;
        public Node parent;

        public int FCost => gCost + hCost;

        public Vector3 position => transform.position;

        public Node(Vector3 pos)
        {
            transform = new GameObject("Node").transform;
            transform.position = pos;
        }
    
}
}*/

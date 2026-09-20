using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathfinding : MonoBehaviour
{
    public Transform seeker, target;
    public List<node> path;
    public Grid grid;
    void Awake()
    {
        grid = GetComponent<Grid>();
    }

    void Update()
    {
        
        FindPath(seeker.position, target.position);
        
        
    }
    void FindPath(Vector3 startPos, Vector3 targetPos)
    {

        
        node startNode = grid.NodefromWorldpoint(startPos);
        node targetNode = grid.NodefromWorldpoint(targetPos);

        Heap<node> openSet = new Heap<node>(grid.MaxSize);
        HashSet<node> closedSet = new HashSet<node>();
        openSet.Add(startNode);
        while (openSet.Count > 0)
        {
            node currentNode = openSet.RemoveFirst();
           
           
            closedSet.Add(currentNode);
            if (currentNode == targetNode)
            {
                
                RetracePath(startNode, targetNode);
                return;
            }
            foreach (node nei in grid.GetNeighbours(currentNode))
            {
                if (!nei.walkable || closedSet.Contains(nei))
                {
                    continue;
                }
                int newMovementCostToNeighbour = currentNode.gCost + getdistance(currentNode, nei);
                if (newMovementCostToNeighbour < nei.gCost || !openSet.Contains(nei))
                {
                    nei.gCost = newMovementCostToNeighbour;
                    nei.hCost = getdistance(nei, targetNode);
                    nei.Parent = currentNode;

                    if (!openSet.Contains(nei))
                    {
                        openSet.Add(nei);
                    }
               
                    
                }
            }
        }
    }

    void RetracePath(node startNode, node endNode)
    {
        path = new List<node>();
        node currentNode = endNode;
        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.Parent;
        }
        path.Reverse();
        grid.path = path;
    }



    int getdistance(node nodeA, node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);
        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);

        return 14 * dstX + 10 * (dstY - dstX);
    }









}

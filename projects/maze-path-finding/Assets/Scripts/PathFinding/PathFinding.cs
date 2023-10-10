using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Collections;

/// <summary>
/// Path Node
/// </summary>
struct PathNode
{
    public int x, y, index;
    public int gCost, hCost, fCost;
    public int parentIndex;

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }
}

/// <summary>
/// Path Finding
/// </summary>
public class PathFinding : MonoBehaviour
{
    const int MOVE_STRAIGHT_COST = 10;
    const int MOVE_DIAGONAL_COST = 14;

    [SerializeField] Vector2Int mazeSize;
    [SerializeField] int2 startPosition;
    [SerializeField] int2 endPosition;
    [SerializeField] Transform cellsParent;
    [SerializeField] List<Vector2Int> pathV;

    public Vector2Int MazeSize { 
        get 
        { 
            return mazeSize; 
        } 
        set { 
            mazeSize = value; 
        }
    }

    public List<Vector2Int> PathV
    {
        get
        {
            return pathV;
        }
    }

    #region Functions

    public void SetStartPosition(Vector2Int pos)
    {
        startPosition = new int2(pos.x, pos.y);
    }

    public void SetEndPosition(Vector2Int pos)
    {
        endPosition = new int2(pos.x, pos.y);
    }
   
    private int CalculateIndex(int x, int y, int width)
    {
        return x + y * width;
    }

    public List<Vector2Int> FindPath(MazeCell[,] mazeCells)
    {
        // Clean Previous Paths
        ResetPathV();

        NativeArray<PathNode> pathNodeArray = new NativeArray<PathNode>(mazeSize.x * mazeSize.y, Allocator.Temp);

        //Create all nodes
        for (int x = 0; x < mazeSize.x; x++)
        {
            for (int y = 0; y < mazeSize.y; y++)
            {
                // Create a new node
                PathNode pathNode = new PathNode();
                pathNode.x = x;
                pathNode.y = y;
                pathNode.index = CalculateIndex(x, y, mazeSize.x);

                pathNode.gCost = int.MaxValue;
                pathNode.hCost = CalculateHCost(new int2(x, y), endPosition);
                pathNode.CalculateFCost();

                //Set -1 as default parent index
                pathNode.parentIndex = -1;
                pathNodeArray[pathNode.index] = pathNode;
            }
        }

        //Initial start node
        PathNode startNode = pathNodeArray[CalculateIndex(startPosition.x, startPosition.y, mazeSize.x)];
        startNode.gCost = 0;
        startNode.CalculateFCost();
        pathNodeArray[startNode.index] = startNode;

        //Open and close lists for searching
        NativeList<int> openList = new NativeList<int>(Allocator.Temp);
        NativeList<int> closedList = new NativeList<int>(Allocator.Temp);

        openList.Add(startNode.index);

        //Index End Node
        int endNodeIndex = CalculateIndex(endPosition.x, endPosition.y, mazeSize.x);

        // Algorithm stops if currentNodeIndex == endNodeIndex or openList is Empty
        while (openList.Length > 0)
        {
            int currentNodeIndex = GetLowestCostFNodeIndex(openList, pathNodeArray);
            PathNode currentNode = pathNodeArray[currentNodeIndex];

            if (currentNodeIndex == endNodeIndex)
            {
                //Path found
                break;
            }

            // If path not found, remove current node from openlist array
            for (int i = 0; i < openList.Length; i++)
            {
                if (openList[i] == currentNodeIndex)
                {
                    openList.RemoveAtSwapBack(i);
                    break;
                }
            }

            // Add the node to the closed list
            closedList.Add(currentNodeIndex);

            List<MazePassage> neighborCells = mazeCells[currentNode.x, currentNode.y].AllPassage();

            // Node Neighbors search
            for (int i = 0; i < neighborCells.Count; i++)
            {
                MazeCell neighborCell = neighborCells[i].otherCell;
                int2 neighborPosition = new int2(neighborCell.position.x, neighborCell.position.y);

                int neighborNodeIndex = CalculateIndex(neighborPosition.x, neighborPosition.y, mazeSize.x);

                if (closedList.Contains(neighborNodeIndex))
                {
                    //Node already searched
                    continue;
                }

                PathNode neighborNode = pathNodeArray[neighborNodeIndex];

                int2 currentNodePosition = new int2(currentNode.x, currentNode.y);

                int tentativeGCost = currentNode.gCost + CalculateHCost(currentNodePosition, neighborPosition);
                if (tentativeGCost < neighborNode.gCost)
                {
                    neighborNode.parentIndex = currentNodeIndex;
                    neighborNode.gCost = tentativeGCost;
                    neighborNode.CalculateFCost();
                    pathNodeArray[neighborNodeIndex] = neighborNode;

                    if (!openList.Contains(neighborNode.index))
                    {
                        openList.Add(neighborNode.index);
                    }
                }
            }
        }

        PathNode endNode = pathNodeArray[endNodeIndex];
        if (endNode.parentIndex == -1)
        {
            // Path not found
        }
        else
        {
            // Path found
            NativeList<int2> path = CalculatePath(pathNodeArray, endNode);

            for (int i = 0; i < path.Length; i++)
            {
                Vector2Int pathPos = new Vector2Int(path[i].x, path[i].y);

                mazeCells[pathPos.x, pathPos.y].gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
                pathV.Add(pathPos);
            }

            path.Dispose();
        }

        // Dispose all temp array
        openList.Dispose();
        closedList.Dispose();
        pathNodeArray.Dispose();

        return pathV;
    }

    private NativeList<int2> CalculatePath(NativeArray<PathNode> pathNodeArray, PathNode endNode)
    {
        if (endNode.parentIndex == -1)
        {
            // Path not found
            return new NativeList<int2>(Allocator.Temp);
        }
        else
        {
            // Path found
            NativeList<int2> path = new NativeList<int2>(Allocator.Temp);
            path.Add(new int2(endNode.x, endNode.y));

            PathNode currentNode = endNode;
            while (currentNode.parentIndex != -1)
            {
                PathNode parrentNode = pathNodeArray[currentNode.parentIndex];
                path.Add(new int2(parrentNode.x, parrentNode.y));
                currentNode = parrentNode;
            }
            return path;
        }
    }

    private int CalculateHCost(int2 startPos, int2 endPos)
    {
        int xDistance = math.abs(startPos.x - endPos.x);
        int yDistance = math.abs(startPos.y - endPos.y);
        int remaining = math.abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * math.min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    private int GetLowestCostFNodeIndex(NativeList<int> openList, NativeArray<PathNode> pathNodeArray)
    {
        PathNode lowestCostPathNode = pathNodeArray[openList[0]];

        for (int i = 0; i < openList.Length; i++)
        {
            PathNode testPathNode = pathNodeArray[openList[i]];
            if (testPathNode.fCost < lowestCostPathNode.fCost)
            {
                lowestCostPathNode = testPathNode;
            }
        }
        return lowestCostPathNode.index;
    }

    public void ResetPathV()
    {
        while (pathV.Count > 0)
        {
            pathV.RemoveAt(0);
        }
    }
    #endregion
}

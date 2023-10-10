using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Class MazeGenerator
/// </summary>
public class MazeGenerator : MonoBehaviour
{
    [SerializeField] float offsetX, offsetY;
    [SerializeField] Vector2Int mazeSize;
    [SerializeField] MazeCell mazeCellPrefab;
    [SerializeField] MazePassage mazePassagePrefab;
    [SerializeField] MazeWall mazeWallPrefab;

    MazeCell[,] mazeCells;
    public MazeCell[,] MazeCells
    {
        get
        {
            return mazeCells;
        }
    }

    public Vector2Int MazeSize
    {
        get
        {
            return mazeSize;
        }
    }

    #region Functions
    public void Generate()
    {
        ResetMaze();
        mazeCells = new MazeCell[mazeSize.x, mazeSize.y];
        List<MazeCell> activeCells = new List<MazeCell>();
        DoFirstGenerationStep(activeCells);
        while (activeCells.Count > 0)
        {
            DoNextGenerationStep(activeCells);
        }
    }

    private void ResetMaze()
    {
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }

    private MazeCell CreateCell(Vector2Int pos)
    {
        MazeCell newMazeCell = Instantiate(mazeCellPrefab) as MazeCell;
        mazeCells[pos.x, pos.y] = newMazeCell;
        //newMazeCell.name = "Maze Cell [" + pos.x + "," + pos.y + "]";
        newMazeCell.name = string.Format("Maze Cell [{0},{1}]", pos.x, pos.y);
        newMazeCell.position = pos;
        newMazeCell.transform.parent = transform;
        newMazeCell.transform.localPosition = new Vector3(
            pos.x - mazeSize.x * offsetX + offsetX, 
            pos.y - mazeSize.y * offsetY + offsetY, 
            0f
            );

        return newMazeCell;
    }

    public Vector2Int RandomCoordinates
    {
        get
        {
            return new Vector2Int(Random.Range(0, mazeSize.x), Random.Range(0, mazeSize.y));
        }
    }

    private void DoFirstGenerationStep(List<MazeCell> activeCells)
    {
        activeCells.Add(CreateCell(RandomCoordinates));
    }

    public bool ContainsCoordinates(Vector2Int coordinate)
    {
        return coordinate.x >= 0 && 
            coordinate.x < mazeSize.x && 
            coordinate.y >= 0 && 
            coordinate.y < mazeSize.y;
    }

    public MazeCell GetMazeCell(Vector2Int coordinates)
    {
        return mazeCells[coordinates.x, coordinates.y];
    }

    private void CreateMazePassage(MazeCell cell, MazeCell otherCell, 
        MazeDirection direction)
    {
        MazePassage newMazePassage = Instantiate(mazePassagePrefab) as MazePassage;
        newMazePassage.Initialize(cell, otherCell, direction);
        newMazePassage = Instantiate(mazePassagePrefab) as MazePassage;
        newMazePassage.Initialize(otherCell, cell, MazeDirections.GetOpposite(direction));
    }

    private void CreateMazeWall(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazeWall newMazeWall = Instantiate(mazeWallPrefab) as MazeWall;
        newMazeWall.Initialize(cell, otherCell, direction);
        if (otherCell != null)
        {
            newMazeWall = Instantiate(mazeWallPrefab) as MazeWall;
            newMazeWall.Initialize(otherCell, cell, MazeDirections.GetOpposite(direction));
        }
    }

    private void DoNextGenerationStep(List<MazeCell> activeCells)
    {
        int currentIndex = activeCells.Count - 1;
        MazeCell currentCell = activeCells[currentIndex];

        if (currentCell.IsFullyInitialized)
        {
            activeCells.RemoveAt(currentIndex);
            return;
        }

        MazeDirection direction = currentCell.RandomUninitializedDirection;
        Vector2Int coordinates = currentCell.position + 
            MazeDirections.MazeDirectionToVector2Int(direction);

        if (ContainsCoordinates(coordinates))
        {
            MazeCell neighborMazeCell = GetMazeCell(coordinates);
            if (neighborMazeCell == null)
            {
                neighborMazeCell = CreateCell(coordinates);
                CreateMazePassage(currentCell, neighborMazeCell, direction);
                activeCells.Add(neighborMazeCell);
            }
            else
            {
                CreateMazeWall(currentCell, neighborMazeCell, direction);
            }
        }
        else
        {
            CreateMazeWall(currentCell, null, direction);
        }

    }
    #endregion
}

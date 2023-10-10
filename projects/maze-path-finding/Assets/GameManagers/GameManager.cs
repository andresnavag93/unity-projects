using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game Manager
/// </summary>
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    
    [SerializeField] Vector2Int endPos;
    [SerializeField] GameObject player, destinationObj, startObj;
    [SerializeField] Transform mazeGridParent;
    [SerializeField] MazeGenerator mazeGenerator;
    [SerializeField] PathFinding pathFinding;
    [SerializeField] CameraFollow cameraFollow;
    MazeCell[,] mazeCells;

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    #region Mono
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        mazeGenerator.Generate();
        pathFinding.MazeSize = mazeGenerator.MazeSize;
        InitialSetup();
    }
    #endregion

    #region Functions
    void InitialSetup()
    {
        GetAllNode();
        Vector2Int mazeSize = pathFinding.MazeSize;
        Vector2Int randomEndPos = new Vector2Int(mazeSize.x-1, mazeSize.y-1);
        pathFinding.SetEndPosition(randomEndPos);
        destinationObj.transform.position = mazeCells[randomEndPos.x, randomEndPos.y].gameObject.transform.position;
        destinationObj.SetActive(true);
        player.transform.position = mazeCells[0, 0].gameObject.transform.position;
        player.SetActive(true);
        startObj.transform.position = mazeCells[0, 0].gameObject.transform.position;
        startObj.SetActive(true);
    }

    public void GetAllNode()
    {
        Vector2Int mazeSize = pathFinding.MazeSize;
        MazeCell[] allMazeCells = mazeGridParent.gameObject.GetComponentsInChildren<MazeCell>();
        mazeCells = new MazeCell[mazeSize.x, mazeSize.y];

        for (int i = 0; i < allMazeCells.Length; i++)
        {
            MazeCell mazeCell = allMazeCells[i];
            mazeCells[mazeCell.position.x, mazeCell.position.y] = mazeCell;
        }
    }

    public void ActivatePathFinding()
    {
        MazeCell curCell = player.GetComponent<PlayerMovement>().CurrentCell;
        pathFinding.SetStartPosition(new Vector2Int(curCell.position.x, curCell.position.y));

        CleanExistingPathInMazeGenerator();
        List<Vector2Int> path = pathFinding.FindPath(mazeCells);
        int length = path.Count;
        Vector2[] pathInWorldPos = new Vector2[length];

        for (int i = 0; i < length; i++)
        {
            pathInWorldPos[length - i - 1] = mazeCells[path[i].x, path[i].y].transform.position;
        }
    }

    private void CleanExistingPathInMazeGenerator()
    {
        foreach (Vector2Int mazeCellPos in pathFinding.PathV)
        {
            mazeCells[mazeCellPos.x, mazeCellPos.y].gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;

        }
    }

    public void ResetGame()
    {
        pathFinding.ResetPathV();
        mazeGenerator.Generate();
        GetAllNode();
        player.transform.position = mazeCells[0, 0].gameObject.transform.position;
        player.SetActive(true);
        cameraFollow.ResetCameraPosition();
    }
    #endregion

}
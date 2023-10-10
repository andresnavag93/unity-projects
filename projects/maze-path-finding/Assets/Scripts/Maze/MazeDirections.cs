// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MazeDirection Enum
/// </summary>
public enum MazeDirection { North, East, South, West }

/// <summary>
/// Class Maze Directions
/// </summary>
public static class MazeDirections
{

    private static MazeDirection[] opposites = {
      MazeDirection.South,
      MazeDirection.West,
      MazeDirection.North,
      MazeDirection.East
    };

    private static Quaternion[] rotations = {
      Quaternion.identity,
      Quaternion.Euler(0f, 0f, -90f),
      Quaternion.Euler(0f, 0f, -180f),
      Quaternion.Euler(0f, 0f, -270f)
    };

    public static Vector2Int RandomMazeDirection
    {
        get
        {
            MazeDirection randomDir = (MazeDirection)Random.Range(0, 4);
            return MazeDirectionToVector2Int(randomDir);
        }
    }

    #region Functions
    public static MazeDirection GetOpposite(MazeDirection direction)
    {
        return opposites[(int)direction];
    }

    public static Vector2Int MazeDirectionToVector2Int(MazeDirection dir)
    {
        switch (dir)
        {
            case MazeDirection.North:
                return Vector2Int.up;
            case MazeDirection.East:
                return Vector2Int.right;
            case MazeDirection.South:
                return Vector2Int.down;
            case MazeDirection.West:
                return Vector2Int.left;
            default:
                return Vector2Int.one;
        }
    }

    public static Quaternion ToRotation(this MazeDirection direction)
    {
        return rotations[(int)direction];
    }

    #endregion
}


using UnityEngine;

public class Piece : MonoBehaviour
{
    public Board Board { get; private set; }
    public TetrominoData TetrominoData { get; private set; }
    public Vector3Int Position { get; private set; }
    public Vector3Int[] Cells { get; private set; }
    public int RotationIndex { get; private set; }

    public float stepDelay = 1f;
    public float lockDelay = 0.5f;
    public float levelDecreaseDelay = 1.25f;

    private float stepTime;
    private float lockTime;

    public void Initialize(Board board, Vector3Int position, TetrominoData tetrominoData)
    {
        Board = board;
        Position = position;
        TetrominoData = tetrominoData;
        RotationIndex = 0;
        stepTime = Time.time + stepDelay;
        lockTime = 0f;

        if (Cells == null)
        {
            Cells = new Vector3Int[TetrominoData.Cells.Length];
        }

        for (int i = 0; i < tetrominoData.Cells.Length; i++)
        {
            Cells[i] = (Vector3Int)tetrominoData.Cells[i];
        }
    }

    private void Update()
    {
        Board.Clear(this);

        lockTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Rotate(-1);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Rotate(1);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Move(Vector2Int.left);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Move(Vector2Int.right);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Move(Vector2Int.down);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            HardDrop();
        }

        if (Time.time >= stepTime)
        {
            Step();
        }
        Board.Set(this);
    }

    private void Step()
    {
        stepTime = Time.time + stepDelay;
        Move(Vector2Int.down);

        if (lockTime >= lockDelay)
        {
            Lock();
        }
    }

    private void Lock()
    {
        Board.Set(this);
        Board.ClearLines();
        Board.SpawnPiece();
    }

    private void HardDrop()
    {
        while (Move(Vector2Int.down))
        {
            continue;
        }
        Lock();
    }

    private bool Move(Vector2Int translaton)
    {
        Vector3Int newPosition = Position;
        newPosition.x += translaton.x;
        newPosition.y += translaton.y;

        bool valid = Board.IsValidPosition(this, newPosition);
        if (valid)
        {
            Position = newPosition;
            lockTime = 0f;
        }

        return valid;
    }

    private void Rotate(int direction)
    {
        int originalRotation = RotationIndex;
        RotationIndex = Wrap(RotationIndex + direction, 0, 4);

        ApplyRotationMatrix(direction);
        if (!TestWallKicks(RotationIndex, direction))
        {
            RotationIndex = originalRotation;
            ApplyRotationMatrix(-direction);
        }
    }

    private void ApplyRotationMatrix(int direction)
    {
        for (int i = 0; i < Cells.Length; i++)
        {
            Vector3 cell = Cells[i];
            int x, y;

            switch (TetrominoData.tetromino)
            {
                case Tetromino.I:
                case Tetromino.O:
                    cell.x -= 0.5f;
                    cell.y -= 0.5f;
                    x = Mathf.CeilToInt((cell.x * ProjectData.RotationMatrix[0] * direction) + (cell.y * ProjectData.RotationMatrix[1] * direction));
                    y = Mathf.CeilToInt((cell.x * ProjectData.RotationMatrix[2] * direction) + (cell.y * ProjectData.RotationMatrix[3] * direction));
                    break;
                default:
                    x = Mathf.RoundToInt((cell.x * ProjectData.RotationMatrix[0] * direction) + (cell.y * ProjectData.RotationMatrix[1] * direction));
                    y = Mathf.RoundToInt((cell.x * ProjectData.RotationMatrix[2] * direction) + (cell.y * ProjectData.RotationMatrix[3] * direction));
                    break;
            }

            Cells[i] = new Vector3Int(x, y, 0);

        }
    }

    private int Wrap(int input, int min, int max)
    {
        if (input < min)
        {
            return max - (min - input) % (max - min);
        }
        else
        {
            return min + (input - min) % (max - min);
        }
    }

    private bool TestWallKicks(int rotationIndex, int rotationDirection)
    {
        int wallKickIndex = GetWallKickIndex(rotationIndex, rotationDirection);
        for (int i = 0; i < TetrominoData.WallKicks.GetLength(1); i++)
        {
            Vector2Int translation = TetrominoData.WallKicks[wallKickIndex, i];
            if (Move(translation))
            {
                return true;
            }
        }
        return false;
    }

    private int GetWallKickIndex(int rotationIndex, int rotationDirection)
    {
        int wallKickIndex = rotationIndex * 2;

        if (rotationDirection < 0)
        {
            wallKickIndex--;
        }

        return Wrap(wallKickIndex, 0, TetrominoData.WallKicks.GetLength(0));
    }

    public void DecreaseStepDelay()
    {
        stepDelay /= levelDecreaseDelay;
    }


}

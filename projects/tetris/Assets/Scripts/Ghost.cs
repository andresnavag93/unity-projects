
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ghost : MonoBehaviour
{
    public Tile tile;
    public Board board;
    public Piece trackingPiece;

    public Tilemap GhostTilemap { get; private set; }
    public Vector3Int GhostPosition { get; private set; }
    public Vector3Int[] GhostCells { get; private set; }

    void Awake()
    {
        GhostTilemap = GetComponentInChildren<Tilemap>();
        GhostCells = new Vector3Int[4];

    }

    private void LateUpdate()
    {
        Clear();
        Copy();
        Drop();
        Set();
    }

    private void Clear()
    {
        for (int i = 0; i < GhostCells.Length; i++)
        {
            Vector3Int tilePosition = GhostCells[i] + GhostPosition;
            GhostTilemap.SetTile(tilePosition, null);
        }
    }

    private void Copy()
    {
        for (int i = 0; i < GhostCells.Length; i++)
        {
            GhostCells[i] = trackingPiece.Cells[i];
        }
    }

    private void Drop()
    {
        Vector3Int trackingPiecePosition = trackingPiece.Position;
        int currentPosition = trackingPiecePosition.y;
        int bottomPosition = -board.boardSize.y / 2 - 1;

        board.Clear(trackingPiece);
        for (int row = currentPosition; row >= bottomPosition; row--)
        {
            trackingPiecePosition.y = row;
            if (board.IsValidPosition(trackingPiece, trackingPiecePosition))
            {
                GhostPosition = trackingPiecePosition;
            }
            else
            {
                break;
            }
        }
        board.Set(trackingPiece);
    }

    private void Set()
    {
        for (int i = 0; i < GhostCells.Length; i++)
        {
            Vector3Int tilePosition = GhostCells[i] + GhostPosition;
            GhostTilemap.SetTile(tilePosition, tile);
        }
    }
}


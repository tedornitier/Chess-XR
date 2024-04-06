using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private GameObject chessBoardObject;
    [SerializeField]
    private GameObject playArea;
    [SerializeField]
    private GameObject possibleMove;
    [SerializeField]
    private GameObject b_king;
    [SerializeField]
    private GameObject b_queen;
    [SerializeField]
    private GameObject b_bishop;
    [SerializeField]
    private GameObject b_knight;
    [SerializeField]
    private GameObject b_rook;
    [SerializeField]
    private GameObject b_pawn;
    [SerializeField]
    private GameObject w_king;
    [SerializeField]
    private GameObject w_queen;
    [SerializeField]
    private GameObject w_bishop;
    [SerializeField]
    private GameObject w_knight;
    [SerializeField]
    private GameObject w_rook;
    [SerializeField]
    private GameObject w_pawn;

    private ChessAI ai = new ChessAI(ChessColor.White, 3);
    private ChessBoard chessBoard = new ChessBoard();
    (int, int) pickedUpPiecePosition = (-1, -1);
    List<GameObject> possibleMoveObjects = new List<GameObject>();
    private GameObject[,] chessPieces = new GameObject[8, 8];

    void Start()
    {
        chessBoard.SetupBoard();
        ArrangeChessBoard();
    }

    void Update()
    {
        
    }

    void ArrangeChessBoard()
    {
        Dictionary<(int, int), GameObject> chessPiecesCoordinates = new Dictionary<(int, int), GameObject>
        {
            { (4, 0), b_king}, { (3, 0), b_queen}, { (2, 0), b_bishop}, { (5, 0), b_bishop},
            { (1, 0), b_knight}, { (6, 0), b_knight}, { (0, 0), b_rook}, { (7, 0), b_rook},
            { (0, 1), b_pawn}, { (1, 1), b_pawn}, { (2, 1), b_pawn}, { (3, 1), b_pawn},
            { (4, 1), b_pawn}, { (5, 1), b_pawn}, { (6, 1), b_pawn}, { (7, 1), b_pawn},
            { (4, 7), w_king}, { (3, 7), w_queen}, { (2, 7), w_bishop}, { (5, 7), w_bishop},
            { (1, 7), w_knight}, { (6, 7), w_knight}, { (0, 7), w_rook}, { (7, 7), w_rook},
            { (0, 6), w_pawn}, { (1, 6), w_pawn}, { (2, 6), w_pawn}, { (3, 6), w_pawn},
            { (4, 6), w_pawn}, { (5, 6), w_pawn}, { (6, 6), w_pawn}, { (7, 6), w_pawn}
        };

        foreach (KeyValuePair<(int, int), GameObject> piece in chessPiecesCoordinates)
        {
            (double positionX, double positionZ) = ChessUtils.GetPieceCoordinateFromCell(piece.Key, getPlayAreaLength());
            GameObject chessPieceGameObject = Instantiate(piece.Value, chessBoardObject.transform);
            chessPieces[piece.Key.Item1, piece.Key.Item2] = chessPieceGameObject;
            chessPieceGameObject.transform.GetChild(0).localPosition = new Vector3((float)positionX, 0.0f, (float)positionZ);
        }
    }

    float getPlayAreaLength()
    {
        return playArea.transform.localScale.x * playArea.GetComponent<MeshFilter>().mesh.bounds.size.x;
    }

    public void onPiecePickUp(Vector3 position, bool debug)
    {
        Vector3 localPosition = chessBoardObject.transform.InverseTransformPoint(position);
        Vector3 piecePosition = localPosition;
        if (debug) { piecePosition = position; }
        pickedUpPiecePosition = ChessUtils.CalculateCellPosition(piecePosition, getPlayAreaLength());
        Debug.Log("Piece picked up at " + pickedUpPiecePosition + " at coordinates " + localPosition);

        ChessPiece piece = chessBoard.GetPieceAt(pickedUpPiecePosition.Item1, pickedUpPiecePosition.Item2);
        if (piece != null)
        {
            HashSet<(int, int)> possibleMoves = piece.GetPossibleMoves(chessBoard);
            foreach ((int, int) move in possibleMoves)
            {
                SpawnPossibleMove(move);
            }
        }
    }

    void SpawnPossibleMove((int, int) cellPosition)
    {
        (double positionX, double positionZ) = ChessUtils.GetPieceCoordinateFromCell(cellPosition, getPlayAreaLength());
        GameObject possibleMoveGameObject = Instantiate(possibleMove, chessBoardObject.transform);
        possibleMoveGameObject.transform.GetChild(0).localPosition = new Vector3((float)positionX, 0.5f, (float)positionZ);
        possibleMoveObjects.Add(possibleMoveGameObject);
    }

    public void onPieceDrop(Vector3 localPosition, bool debug)
    {
        if (pickedUpPiecePosition != (-1, -1))
        {
            (int, int) newPosition = ChessUtils.CalculateCellPosition(localPosition, getPlayAreaLength());
            if (newPosition == pickedUpPiecePosition)
            {
                Debug.Log("Piece was dropped at the same position");
                pickedUpPiecePosition = (-1, -1);
                foreach (GameObject possibleMoveObject in possibleMoveObjects)
                {
                    Destroy(possibleMoveObject);
                }
                return;
            }
            if (chessBoard.GetPieceAt(newPosition.Item1, newPosition.Item2) != null)
            {
                Debug.Log("Captured piece at " + newPosition);
                chessBoard.RemovePiece(newPosition);
                Destroy(chessPieces[newPosition.Item1, newPosition.Item2]);
            }
            chessBoard.MovePiece(pickedUpPiecePosition, newPosition);
            chessPieces[newPosition.Item1, newPosition.Item2] = chessPieces[pickedUpPiecePosition.Item1, pickedUpPiecePosition.Item2];
            chessPieces[pickedUpPiecePosition.Item1, pickedUpPiecePosition.Item2] = null;
            Debug.Log("Piece moved from " + pickedUpPiecePosition + " to " + newPosition);
            chessBoard.PrintBoard();
            pickedUpPiecePosition = (-1, -1);
            foreach (GameObject possibleMoveObject in possibleMoveObjects)
            {
                Destroy(possibleMoveObject);
            }

            (int fromX, int fromY, int toX, int toY) = ai.GetBestMove(chessBoard);
            if (fromX != -1 && fromY != -1 && toX != -1 && toY != -1)
            {
                Debug.Log("AI moved from " + fromX + ", " + fromY + " to " + toX + ", " + toY);
                chessBoard.MovePiece((fromX, fromY), (toX, toY));
                chessPieces[toX, toY] = chessPieces[fromX, fromY];
                chessPieces[fromX, fromY] = null;
            }
            else
            {
                Debug.Log("AI could not find a move");
            }
        }
        else
        {
            Debug.Log("No piece was picked up");
        }
    }
}

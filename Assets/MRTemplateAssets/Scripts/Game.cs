using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private GameObject chessBoard;
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


    void Start()
    {
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
            (double positionX, double positionZ) = GetPieceCoordinateFromCell(piece.Key);
            GameObject chessPiece = Instantiate(piece.Value, GameObject.Find("chessboard").transform);
            chessPiece.transform.localPosition = new Vector3((float)positionX, 0.5f, (float)positionZ);
        }
    }

    static (double, double) GetPieceCoordinateFromCell((int, int) cell)
    {
        double boardLength = 22.44;
        (int x, int y) = cell;
        double positionX = (boardLength * (x - 4) / 8 + boardLength * (x - 4 + 1) / 8) / 2;
        double positionZ = (boardLength * (y - 4) / 8 + boardLength * (y - 4 + 1) / 8) / 2;
        return (positionX, positionZ);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class ChessPieceTests : MonoBehaviour
{
    [Test]
    public void GetPossibleMovesTest()
    {
        ChessPiece piece = new ChessPiece(ChessType.Pawn, ChessColor.Black);
        ChessBoard board = new ChessBoard();
        board.SetPieceAt(0, 1, piece);

        HashSet<(int, int)> moves = piece.GetPossibleMoves(board);
        Assert.AreEqual(2, moves.Count);
    }
}
